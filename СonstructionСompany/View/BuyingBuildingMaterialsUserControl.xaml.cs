using ConstructionCompany.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using СonstructionСompany.Model;
using СonstructionСompany.Services;

namespace СonstructionСompany.View
{
    /// <summary>
    /// Логика взаимодействия для BuyingBuildingMaterialsUserControl.xaml
    /// </summary>
    public partial class BuyingBuildingMaterialsUserControl : UserControl
    {
        private ApplicationDbContext _context;
        private BuyingBuildingMaterials _selectedBuying;
        private List<BuildingMaterial> _selectedMaterials = new List<BuildingMaterial>();
        private List<int> _selectedMaterialCounts = new List<int>();

        public BuyingBuildingMaterialsUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            LoadData();
        }

        private void LoadData()
        {
            DataGridBuyingMaterials.ItemsSource = _context.BuyingBuildingMaterials
                .Include(b => b.Supplier)
                .Include(b => b.Warehouse)
                .Include(b => b.BuildingMaterials)
                .ToList();

            // Проверка: наличие свойства Name у склада
            WarehouseComboBox.ItemsSource = _context.Warehouses.ToList();
            BuildingMaterialComboBox.ItemsSource = _context.BuildingMaterials.ToList();
        }

        private void DataGridBuyingMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridBuyingMaterials.SelectedItem is BuyingBuildingMaterials selected)
            {
                _selectedBuying = selected;
                _selectedMaterials = selected.BuildingMaterials.ToList();
                _selectedMaterialCounts = selected.CountMaterials.ToList();

                IdTextBox.Text = selected.Id.ToString();
                SupplierIdTextBox.Text = selected.SupplierId.ToString();
                SupplierNameTextBox.Text = selected.Supplier?.NameOfOrganization;
                DateDatePicker.SelectedDate = selected.Date;

                // Установка выбранного элемента в WarehouseComboBox
                WarehouseComboBox.SelectedItem = _context.Warehouses
                    .FirstOrDefault(w => w.Id == selected.WarehouseId);

                BuildingMaterialsTextBox.Text = selected.BuildingMaterialsString;
                CountMaterialsTextBox.Text = selected.CountMaterialsString;
                TotalPriceTextBox.Text = selected.TotalPrice.ToString("F2");
            }
        }

        private void ButtonAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (BuildingMaterialComboBox.SelectedItem is BuildingMaterial material &&
                int.TryParse(CountMaterialTextBox.Text, out int count) &&
                !_selectedMaterials.Contains(material))
            {
                _selectedMaterials.Add(material);
                _selectedMaterialCounts.Add(count);

                BuildingMaterialsTextBox.Text = string.Join(", ", _selectedMaterials.Select(m => m.Name));
                CountMaterialsTextBox.Text = string.Join(", ", _selectedMaterialCounts);
            }
            else
            {
                MessageBox.Show("Введите корректное количество и выберите материал.");
            }
        }

        private void ButtonClearMaterials_Click(object sender, RoutedEventArgs e)
        {
            _selectedMaterials.Clear();
            _selectedMaterialCounts.Clear();

            BuildingMaterialsTextBox.Clear();
            CountMaterialsTextBox.Clear();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var newBuying = new BuyingBuildingMaterials
            {
                Id = Guid.NewGuid(),
                SupplierId = Guid.Parse(SupplierIdTextBox.Text),
                Date = DateTime.SpecifyKind(DateDatePicker.SelectedDate.Value, DateTimeKind.Utc).ToUniversalTime(),
                Warehouse = (Warehouse)WarehouseComboBox.SelectedItem,
                BuildingMaterials = _selectedMaterials,
                CountMaterials = _selectedMaterialCounts,
                TotalPrice = _selectedMaterials.Select((m, i) => m.Price * _selectedMaterialCounts[i]).Sum()
            };

            _context.BuyingBuildingMaterials.Add(newBuying);
            _context.SaveChanges();
            LoadData();
            ClearForm();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBuying == null)
            {
                MessageBox.Show("Выберите запись для обновления.");
                return;
            }

            try
            {
                _selectedBuying.SupplierId = Guid.Parse(SupplierIdTextBox.Text);
                _selectedBuying.Date = DateTime.SpecifyKind(DateDatePicker.SelectedDate.Value, DateTimeKind.Utc).ToUniversalTime();
                _selectedBuying.Warehouse = (Warehouse)WarehouseComboBox.SelectedItem;
                _selectedBuying.BuildingMaterials = _selectedMaterials;
                _selectedBuying.CountMaterials = _selectedMaterialCounts;
                _selectedBuying.TotalPrice = _selectedMaterials.Select((m, i) => m.Price * _selectedMaterialCounts[i]).Sum();

                _context.BuyingBuildingMaterials.Update(_selectedBuying);
                _context.SaveChanges();

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBuying == null)
            {
                MessageBox.Show("Выберите запись для удаления.");
                return;
            }

            try
            {
                _context.BuyingBuildingMaterials.Remove(_selectedBuying);
                _context.SaveChanges();

                LoadData();

                // Очистка формы
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении данных: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            IdTextBox.Clear();
            SupplierIdTextBox.Clear();
            SupplierNameTextBox.Clear();
            DateDatePicker.SelectedDate = null;
            WarehouseComboBox.SelectedItem = null;
            BuildingMaterialsTextBox.Clear();
            CountMaterialsTextBox.Clear();
            TotalPriceTextBox.Clear();

            _selectedBuying = null;
            _selectedMaterials = [];
            _selectedMaterialCounts = [];
        }
    }
}
