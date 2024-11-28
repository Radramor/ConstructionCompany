using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using СonstructionСompany.Model;
using СonstructionСompany.Services;

namespace СonstructionСompany.View
{
    public partial class BuildingMaterialUserControl : UserControl
    {
        private readonly BuildingMaterialService _buildingMaterialService;
        private BuildingMaterial _selectedBuildingMaterial;
        private ApplicationDbContext _context;

        public BuildingMaterialUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _buildingMaterialService = new BuildingMaterialService(_context);
            LoadBuildingMaterials();
        }

        private void LoadBuildingMaterials()
        {
            try
            {
                var buildingMaterials = _context.BuildingMaterials.Include(b => b.Supplier).ToList();
                DataGridBuildingMaterials.ItemsSource = buildingMaterials;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void DataGridBuildingMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridBuildingMaterials.SelectedItem is BuildingMaterial selectedBuildingMaterial)
            {
                _selectedBuildingMaterial = selectedBuildingMaterial;
                IdTextBox.Text = selectedBuildingMaterial.Id.ToString();
                NameTextBox.Text = selectedBuildingMaterial.Name;
                UnitTextBox.Text = selectedBuildingMaterial.Unit;
                PriceTextBox.Text = selectedBuildingMaterial.Price.ToString("F2");
                SupplierIdTextBox.Text = selectedBuildingMaterial.SupplierId.ToString();
                SupplierNameTextBox.Text = selectedBuildingMaterial.Supplier?.NameOfOrganization;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newBuildingMaterial = new BuildingMaterial
                {
                    Id = Guid.NewGuid(),
                    Name = NameTextBox.Text,
                    Unit = UnitTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    SupplierId = Guid.Parse(SupplierIdTextBox.Text),
                    Supplier = _context.Suppliers.Find(Guid.Parse(SupplierIdTextBox.Text))
                };

                _buildingMaterialService.Create(newBuildingMaterial);
                LoadBuildingMaterials();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}");
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var updatedBuildingMaterial = new BuildingMaterial
                {
                    Id = Guid.Parse(IdTextBox.Text),
                    Name = NameTextBox.Text,
                    Unit = UnitTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    SupplierId = Guid.Parse(SupplierIdTextBox.Text),
                    Supplier = _context.Suppliers.Find(Guid.Parse(SupplierIdTextBox.Text))
                };

                _buildingMaterialService.Update(updatedBuildingMaterial);
                LoadBuildingMaterials();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBuildingMaterial == null)
            {
                MessageBox.Show("Выберите строительный материал для удаления.");
                return;
            }
            try
            {
                _buildingMaterialService.Delete(_selectedBuildingMaterial.Id);
                LoadBuildingMaterials();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            DataGridBuildingMaterials.SelectedItem = null;
            IdTextBox.Clear();
            NameTextBox.Clear();
            UnitTextBox.Clear();
            PriceTextBox.Clear();
            SupplierIdTextBox.Clear();
            SupplierNameTextBox.Clear();
        }
    }
}
