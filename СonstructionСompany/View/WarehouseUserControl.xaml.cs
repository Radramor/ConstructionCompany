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
    /// Логика взаимодействия для WarehouseUserControl.xaml
    /// </summary>
    public partial class WarehouseUserControl : UserControl
    {
        private ApplicationDbContext _context;
        private Warehouse _selectedWarehouse;
        private List<MaterialCounts> _materialCounts = new List<MaterialCounts>();

        public WarehouseUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            LoadData();
        }

        private void LoadData()
        {
            // Получаем все склады из базы данных и заполняем WarehouseComboBox
            var warehouses = _context.Warehouses.ToList();
            WarehouseComboBox.ItemsSource = warehouses;
            WarehouseComboBox.DisplayMemberPath = "Address";  // Адрес склада
            WarehouseComboBox.SelectedValuePath = "Id";  // Id склада

            // При изменении выбранного склада в ComboBox
            WarehouseComboBox.SelectionChanged += (sender, e) =>
            {
                var selectedWarehouseId = (Guid?)WarehouseComboBox.SelectedValue;

                if (selectedWarehouseId.HasValue)
                {
                    // Загружаем операции для выбранного склада
                    var warehouseMaterials = GetWarehouseMaterials(selectedWarehouseId.Value);
                    DataGridWarehouse.ItemsSource = warehouseMaterials;
                }
                else
                {
                    // Если склад не выбран, таблица пуста
                    DataGridWarehouse.ItemsSource = null;
                }
            };
        }

        // Метод для получения материалов для выбранного склада
        private List<MaterialCounts> GetWarehouseMaterials(Guid warehouseId)
        {
            // Сначала получаем материалы из BuyingBuildingMaterialsUserControl для выбранного склада
            var buyingMaterials = _context.BuyingBuildingMaterials
                .Where(b => b.WarehouseId == warehouseId)
                .SelectMany(b => b.BuildingMaterials.Select((material, index) => new
                {
                    MaterialId = material.Id,
                    MaterialName = material.Name, // Сохраняем имя материала
                    MaterialCount = b.CountMaterials.ElementAtOrDefault(index)
                }))
                .GroupBy(m => m.MaterialId)
                .Select(g => new MaterialCounts
                {
                    MaterialName = g.FirstOrDefault() != null ? g.FirstOrDefault().MaterialName : "Неизвестный материал", // Используем имя материала
                    MaterialCount = g.Sum(m => m.MaterialCount)
                })
                .ToList();

            // Далее получаем данные из BuildingMaterialDistributionUserControl для выбранного склада
            var distributionMaterials = _context.BuildingMaterialDistributions
                .Where(d => d.WarehouseId == warehouseId)
                .SelectMany(d => d.BuildingMaterials.Select((material, index) => new
                {
                    MaterialId = material.Id,
                    MaterialName = material.Name, // Сохраняем имя материала
                    MaterialCount = d.CountMaterials.ElementAtOrDefault(index)
                }))
                .GroupBy(m => m.MaterialId)
                .Select(g => new MaterialCounts
                {
                    MaterialName = g.FirstOrDefault() != null ? g.FirstOrDefault().MaterialName : "Неизвестный материал", // Используем имя материала
                    MaterialCount = g.Sum(m => m.MaterialCount)
                })
                .ToList();

            // Объединяем оба списка (материалы из покупок и распределения)
            var allMaterials = buyingMaterials.Concat(distributionMaterials)
                .GroupBy(m => m.MaterialName)
                .Select(g => new MaterialCounts
                {
                    MaterialName = g.Key,
                    MaterialCount = g.Sum(m => m.MaterialCount)
                })
                .ToList();

            return allMaterials;
        }




        private void WarehouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WarehouseComboBox.SelectedItem is Warehouse warehouse)
            {
                _selectedWarehouse = warehouse;
                LoadData();
            }
        }

        private void ButtonAddWarehouse_Click(object sender, RoutedEventArgs e)
        {
            var newWarehouse = new Warehouse
            {
                Id = Guid.NewGuid(),
                Address = AddressTextBox.Text
            };

            _context.Warehouses.Add(newWarehouse);
            _context.SaveChanges();
            LoadData();
        }

        private void ButtonUpdateWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedWarehouse != null)
            {
                _selectedWarehouse.Address = AddressTextBox.Text;
                _context.SaveChanges();
                LoadData();
            }
        }

        private void ButtonDeleteWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedWarehouse != null)
            {
                _context.Warehouses.Remove(_selectedWarehouse);
                _context.SaveChanges();
                LoadData();
            }
        }
    }

    public class MaterialCounts
    {
        public string MaterialName { get; set; }
        public int MaterialCount { get; set; }
    }
}
