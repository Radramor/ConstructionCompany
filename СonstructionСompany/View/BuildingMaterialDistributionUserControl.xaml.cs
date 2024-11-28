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
using Object = СonstructionСompany.Model.Object;

namespace СonstructionСompany.View
{
    public partial class BuildingMaterialDistributionUserControl : UserControl
    {
        private ApplicationDbContext _context;
        private BuildingMaterialDistribution _selectedDistribution;
        private List<BuildingMaterial> _selectedMaterials = new List<BuildingMaterial>();
        private List<int> _selectedMaterialCounts = new List<int>();

        public BuildingMaterialDistributionUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            LoadData();
        }

        private void LoadData()
        {
            DataGridBuildingMaterialDistribution.ItemsSource = _context.BuildingMaterialDistributions
                .Include(d => d.Object)
                .Include(d => d.Warehouse)
                .Include(d => d.BuildingMaterials)
                .ToList();

            WarehouseComboBox.ItemsSource = _context.Warehouses.ToList();
            BuildingMaterialComboBox.ItemsSource = _context.BuildingMaterials.ToList();
        }

        private void DataGridBuildingMaterialDistribution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridBuildingMaterialDistribution.SelectedItem is BuildingMaterialDistribution selected)
            {
                _selectedDistribution = selected;
                _selectedMaterials = selected.BuildingMaterials.ToList();
                _selectedMaterialCounts = selected.CountMaterials.ToList();

                IdTextBox.Text = selected.Id.ToString();
                ObjectIdTextBox.Text = selected.ObjectId.ToString();
                ObjectNameTextBox.Text = selected.Object?.Name;
                DateDatePicker.SelectedDate = selected.Date;

                WarehouseComboBox.SelectedItem = _context.Warehouses
                    .FirstOrDefault(w => w.Id == selected.WarehouseId);

                BuildingMaterialsTextBox.Text = selected.BuildingMaterialsString;
                CountMaterialsTextBox.Text = selected.CountMaterialsString;
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
            var newDistribution = new BuildingMaterialDistribution
            {
                Id = Guid.NewGuid(),
                ObjectId = Guid.Parse(ObjectIdTextBox.Text),
                Date = DateTime.SpecifyKind(DateDatePicker.SelectedDate.Value, DateTimeKind.Utc).ToUniversalTime(),
                WarehouseId = (WarehouseComboBox.SelectedItem as Warehouse)?.Id ?? Guid.Empty,
                BuildingMaterials = _selectedMaterials,
                CountMaterials = _selectedMaterialCounts
            };

            _context.BuildingMaterialDistributions.Add(newDistribution);
            _context.SaveChanges();
            LoadData();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDistribution == null) return;

            _selectedDistribution.ObjectId = Guid.Parse(ObjectIdTextBox.Text);
            _selectedDistribution.Date = DateTime.SpecifyKind(DateDatePicker.SelectedDate.Value, DateTimeKind.Utc).ToUniversalTime();
            _selectedDistribution.WarehouseId = (WarehouseComboBox.SelectedItem as Warehouse)?.Id ?? Guid.Empty;
            _selectedDistribution.BuildingMaterials = _selectedMaterials;
            _selectedDistribution.CountMaterials = _selectedMaterialCounts;

            _context.SaveChanges();
            LoadData();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDistribution != null)
            {
                _context.BuildingMaterialDistributions.Remove(_selectedDistribution);
                _context.SaveChanges();
                LoadData();
            }
        }
    }
}
