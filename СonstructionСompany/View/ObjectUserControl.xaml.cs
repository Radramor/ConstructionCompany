using ConstructionCompany.Services;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using Object = СonstructionСompany.Model.Object;

namespace СonstructionСompany.View
{
    /// <summary>
    /// Логика взаимодействия для ObjectUserControl.xaml
    /// </summary>
    public partial class ObjectUserControl : UserControl
    {
        private readonly ObjectService _objectService;
        private Object? _selectedObject;

        public ObjectUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _objectService = new ObjectService(new ApplicationDbContext(optionsBuilder.Options));
            LoadData();
        }

        private void LoadData()
        {
            var objects = _objectService.GetAll();
            DataGridObjects.ItemsSource = objects.ToList();
        }

        private void DataGridObjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridObjects.SelectedItem is Object selectedObject)
            {
                _selectedObject = selectedObject;

                IdTextBox.Text = _selectedObject.Id.ToString();
                NameTextBox.Text = _selectedObject.Name;
                AddressTextBox.Text = _selectedObject.Address;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newObject = new Object
                {
                    Id = Guid.NewGuid(),
                    Name = NameTextBox.Text,
                    Address = AddressTextBox.Text
                };

                _objectService.Create(newObject);
                LoadData();
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
                var updatedObject = new Object
                {
                    Id = Guid.Parse(IdTextBox.Text),
                    Name = NameTextBox.Text,
                    Address = AddressTextBox.Text
                };

                _objectService.Update(updatedObject);
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedObject == null)
            {
                MessageBox.Show("Выберите строителя для удаления.");
                return;
            }
            try
            {
                _objectService.Delete(_selectedObject.Id);
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            DataGridObjects.SelectedItem = null;
            IdTextBox.Clear();
            NameTextBox.Clear();
            AddressTextBox.Clear();
        }
    }
}
