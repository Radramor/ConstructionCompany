using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using СonstructionСompany.Model;
using СonstructionСompany.Services;

namespace СonstructionСompany.View
{
    public partial class EstimateUserControl : UserControl
    {
        private readonly EstimateService _estimateService;
        private Estimate _selectedEstimate;
        private ApplicationDbContext _context;

        // Списки для хранения материалов и их количества
        private List<BuildingMaterial> _selectedBuildingMaterials = new List<BuildingMaterial>();
        private List<int> _selectedMaterialCounts = new List<int>();

        public EstimateUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _estimateService = new EstimateService(_context);
            LoadEstimates();
            LoadBuildingMaterials();
        }

        private void LoadEstimates()
        {
            try
            {
                var estimates = _context.Estimates
                    .Include(e => e.Object)
                    .Include(e => e.BuildingMaterials)
                    .ToList();

                DataGridEstimates.ItemsSource = estimates;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void LoadBuildingMaterials()
        {
            // Загружаем строительные материалы из базы данных для отображения в ComboBox
            var materials = _context.BuildingMaterials.ToList();
            BuildingMaterialComboBox.ItemsSource = materials;
        }

        private void DataGridEstimates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridEstimates.SelectedItem is Estimate selectedEstimate)
            {
                _selectedBuildingMaterials = selectedEstimate.BuildingMaterials;
                _selectedMaterialCounts = selectedEstimate.CountMaterials;
                _selectedEstimate = selectedEstimate;
                IdTextBox.Text = selectedEstimate.Id.ToString();
                ObjectIdTextBox.Text = selectedEstimate.ObjectId.ToString();
                ObjectNameTextBox.Text = selectedEstimate.Object?.Name;
                BuildingMaterialsTextBox.Text = string.Join(", ", selectedEstimate.BuildingMaterials.Select(b => b.Name));
                CountMaterialsTextBox.Text = string.Join(", ", selectedEstimate.CountMaterials.Select(c => c.ToString()));
                TotalPriceTextBox.Text = selectedEstimate.TotalPrice.ToString("F2");
            }
        }
        private void ButtonClearAllMaterials_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEstimate == null)
            {
                MessageBox.Show("Пожалуйста, выберите смету для очистки.");
                return;
            }

            try
            {
                _selectedEstimate.BuildingMaterials.Clear();
                _selectedEstimate.CountMaterials.Clear();
                _selectedEstimate.TotalPrice = 0;
                BuildingMaterialsTextBox.Clear();
                CountMaterialsTextBox.Clear();
                TotalPriceTextBox.Clear();
                _estimateService.Update(_selectedEstimate);
                LoadEstimates();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при очистке материалов: {ex.Message}");
            }
        }

        private void ButtonAddMaterial_Click(object sender, RoutedEventArgs e)
        {

            // Проверка на ввод данных
            if (BuildingMaterialComboBox.SelectedItem is BuildingMaterial selectedMaterial &&
                int.TryParse(CountMaterialTextBox.Text, out int count) &&
                !BuildingMaterialsTextBox.Text.Contains(selectedMaterial.Name))
            {
                // Добавляем материал и его количество в соответствующие списки
                _selectedBuildingMaterials.Add(selectedMaterial);
                _selectedMaterialCounts.Add(count);

                // Обновляем отображение
                BuildingMaterialsTextBox.Text = string.Join(", ", _selectedBuildingMaterials.Select(m => m.Name));
                CountMaterialsTextBox.Text = string.Join(", ", _selectedMaterialCounts.Select(c => c.ToString()));
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите неповторяющийся материал и введите количество.");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var newEstimate = new Estimate
            {
                Id = Guid.NewGuid(),
                ObjectId = Guid.Parse(ObjectIdTextBox.Text),
                BuildingMaterials = _selectedBuildingMaterials,
                CountMaterials = _selectedMaterialCounts,
                TotalPrice = _selectedBuildingMaterials
                    .Select((material, index) => material.Price * _selectedMaterialCounts[index])
                    .Sum() // Сумма произведений цены материала и его количества
            };

            _estimateService.Create(newEstimate);
            LoadEstimates();
            ClearFields();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEstimate != null)
            {
                _selectedEstimate.ObjectId = Guid.Parse(ObjectIdTextBox.Text);
                _selectedEstimate.BuildingMaterials = _selectedBuildingMaterials;
                _selectedEstimate.CountMaterials = _selectedMaterialCounts;
                _selectedEstimate.TotalPrice = _selectedBuildingMaterials
                    .Select((material, index) => material.Price * _selectedMaterialCounts[index])
                    .Sum(); // Сумма произведений цены материала и его количества

                _estimateService.Update(_selectedEstimate);
                LoadEstimates();
                ClearFields();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEstimate != null)
            {
                _estimateService.Delete(_selectedEstimate.Id);
                LoadEstimates();
                ClearFields();
            }
        }

        private void ClearFields()
        {
            // Очищаем все текстовые поля
            IdTextBox.Clear();
            ObjectIdTextBox.Clear();
            ObjectNameTextBox.Clear();
            BuildingMaterialsTextBox.Clear();
            CountMaterialsTextBox.Clear();
            TotalPriceTextBox.Clear();

            // Очищаем списки материалов и их количества
            _selectedBuildingMaterials = [];
            _selectedMaterialCounts = [];

            // Очищаем ComboBox выбора строительного материала
            BuildingMaterialComboBox.SelectedItem = null;

            // Сбрасываем выбранную смету
            DataGridEstimates.SelectedItem = null;
            DataGridEstimates.Items.Refresh();

        }


        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoadBuildingMaterials();
        }
    }
}
