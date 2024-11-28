using ConstructionCompany.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Windows.Controls;
using СonstructionСompany.Model;
using СonstructionСompany.Services;

namespace СonstructionСompany.View
{
    public partial class BankUserControl : UserControl
    {
        private readonly BankService _bankService;
        private Bank _selectedBank;
        private ApplicationDbContext _context;

        public BankUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _bankService = new BankService(_context);
            LoadBanks();
        }

        private void LoadBanks()
        {
            try
            {
                var banks = _context.Banks.ToList();
                DataGridBank.ItemsSource = banks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void DataGridBank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridBank.SelectedItem is Bank selectedBank)
            {
                _selectedBank = selectedBank;
                IdTextBox.Text = selectedBank.Id.ToString();
                BankNameTextBox.Text = selectedBank.Name;
                BankAddressTextBox.Text = selectedBank.Address;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newBank = new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = BankNameTextBox.Text,
                    Address = BankAddressTextBox.Text
                };

                _bankService.Create(newBank);
                LoadBanks();
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
                var updatedBank = new Bank
                {
                    Id = Guid.Parse(IdTextBox.Text),
                    Name = BankNameTextBox.Text,
                    Address = BankAddressTextBox.Text
                };

                _bankService.Update(updatedBank);
                LoadBanks();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBank == null)
            {
                MessageBox.Show("Выберите банк для удаления.");
                return;
            }
            try
            {
                _bankService.Delete(_selectedBank.Id);
                LoadBanks();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            DataGridBank.SelectedItem = null;
            IdTextBox.Clear();
            BankNameTextBox.Clear();
            BankAddressTextBox.Clear();
        }
    }
}
