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
    /// Логика взаимодействия для BuilderUserControl.xaml
    /// </summary>
    public partial class BuilderUserControl : UserControl
    {
        private readonly BuilderService _builderService;
        private Builder? _selectedBuilder;
        private ApplicationDbContext _context;

        public BuilderUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _builderService = new BuilderService(_context);
            LoadBuilders();
        }

        private void LoadBuilders()
        {
            try
            {
                var builders = _context.Builders
                    .Include(b => b.Brigade)
                    .ToList();
                BuilderDataGrid.ItemsSource = builders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newBuilder = new Builder
                {
                    Id = Guid.NewGuid(),
                    FirstName = FirstNameTextBox.Text,
                    SecondName = SecondNameTextBox.Text,
                    Patronymic = PatronymicTextBox.Text,
                    DateOfBirth = DateTime.SpecifyKind(DateOfBirthPicker.SelectedDate.Value, DateTimeKind.Utc).ToUniversalTime(),
                    ResidentialAddress = ResidentialAddressTextBox.Text,
                    LengthOfService = int.TryParse(LengthOfServiceTextBox.Text, out int length) ? length : 0,
                    Specialties = SpecialtiesTextBox.Text,
                    BrigadeId = Guid.Parse(BrigadeIdTextBox.Text) // Привязать к существующей бригаде
                };

                _builderService.Create(newBuilder);
                LoadBuilders();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBuilder == null)
            {
                MessageBox.Show("Выберите строителя для обновления.");
                return;
            }

            try
            {
                _selectedBuilder.FirstName = FirstNameTextBox.Text;
                _selectedBuilder.SecondName = SecondNameTextBox.Text;
                _selectedBuilder.Patronymic = PatronymicTextBox.Text;
                _selectedBuilder.DateOfBirth = DateTime.SpecifyKind(DateOfBirthPicker.SelectedDate.Value, DateTimeKind.Utc).ToUniversalTime();
                _selectedBuilder.ResidentialAddress = ResidentialAddressTextBox.Text;
                _selectedBuilder.LengthOfService = int.TryParse(LengthOfServiceTextBox.Text, out int length) ? length : _selectedBuilder.LengthOfService;
                _selectedBuilder.Specialties = SpecialtiesTextBox.Text;
                _selectedBuilder.BrigadeId = Guid.Parse(BrigadeIdTextBox.Text);

                _builderService.Update(_selectedBuilder);
                LoadBuilders();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBuilder == null)
            {
                MessageBox.Show("Выберите строителя для удаления.");
                return;
            }

            try
            {
                _builderService.Delete(_selectedBuilder.Id);
                LoadBuilders();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }

        private void BuilderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuilderDataGrid.SelectedItem is Builder selectedBuilder)
            {
                _selectedBuilder = selectedBuilder;

                IdTextBox.Text = _selectedBuilder.Id.ToString();
                FirstNameTextBox.Text = _selectedBuilder.FirstName;
                SecondNameTextBox.Text = _selectedBuilder.SecondName;
                PatronymicTextBox.Text = _selectedBuilder.Patronymic;
                DateOfBirthPicker.SelectedDate = _selectedBuilder.DateOfBirth;
                ResidentialAddressTextBox.Text = _selectedBuilder.ResidentialAddress;
                LengthOfServiceTextBox.Text = _selectedBuilder.LengthOfService.ToString();
                SpecialtiesTextBox.Text = _selectedBuilder.Specialties;
                BrigadeIdTextBox.Text = _selectedBuilder.BrigadeId.ToString();
                BrigadeNameTextBox.Text = _selectedBuilder.Brigade.Name.ToString();
            }
        }

        private void ClearFields()
        {
            BuilderDataGrid.SelectedItem = null;
            IdTextBox.Clear();
            FirstNameTextBox.Clear();
            SecondNameTextBox.Clear();
            PatronymicTextBox.Clear();
            DateOfBirthPicker.SelectedDate = null;
            ResidentialAddressTextBox.Clear();
            LengthOfServiceTextBox.Clear();
            SpecialtiesTextBox.Clear();
            BrigadeIdTextBox.Clear();
            _selectedBuilder = null;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoadBuilders();
        }
    }
}
