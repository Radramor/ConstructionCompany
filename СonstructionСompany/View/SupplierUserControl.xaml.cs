using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using СonstructionСompany.Model;
using СonstructionСompany.Services;

namespace СonstructionСompany.View
{
    public partial class SupplierUserControl : UserControl
    {
        private readonly SupplierService _supplierService;
        private Supplier _selectedSupplier;
        private ApplicationDbContext _context;

        public SupplierUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _supplierService = new SupplierService(_context);
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = _context.Suppliers.Include(s => s.Bank).ToList();
                DataGridSuppliers.ItemsSource = suppliers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void DataGridSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridSuppliers.SelectedItem is Supplier selectedSupplier)
            {
                _selectedSupplier = selectedSupplier;
                IdTextBox.Text = selectedSupplier.Id.ToString();
                NameOfOrganizationTextBox.Text = selectedSupplier.NameOfOrganization;
                FirstNameOfSupervisorTextBox.Text = selectedSupplier.FirstNameOfSupervisor;
                SecondNameOfSupervisorTextBox.Text = selectedSupplier.SecondNameOfSupervisor;
                PatronymicOfSupervisorTextBox.Text = selectedSupplier.PatronymicOfSupervisor;
                PhoneTextBox.Text = selectedSupplier.Phone;
                INNTextBox.Text = selectedSupplier.INN;
                KPPTextBox.Text = selectedSupplier.KPP;
                OGRNTextBox.Text = selectedSupplier.OGRN;
                SettlementAccountTextBox.Text = selectedSupplier.SettlementAccount;
                CorrespondentAccountTextBox.Text = selectedSupplier.CorrespondentAccount;
                BIKTextBox.Text = selectedSupplier.BIK;
                BankIdTextBox.Text = selectedSupplier.BankId.ToString();
                BankNameTextBox.Text = selectedSupplier.Bank?.Name;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newSupplier = new Supplier
                {
                    Id = Guid.NewGuid(),
                    NameOfOrganization = NameOfOrganizationTextBox.Text,
                    FirstNameOfSupervisor = FirstNameOfSupervisorTextBox.Text,
                    SecondNameOfSupervisor = SecondNameOfSupervisorTextBox.Text,
                    PatronymicOfSupervisor = PatronymicOfSupervisorTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    INN = INNTextBox.Text,
                    KPP = KPPTextBox.Text,
                    OGRN = OGRNTextBox.Text,
                    SettlementAccount = SettlementAccountTextBox.Text,
                    CorrespondentAccount = CorrespondentAccountTextBox.Text,
                    BIK = BIKTextBox.Text,
                    BankId = Guid.Parse(BankIdTextBox.Text),
                    Bank = _context.Banks.Find(Guid.Parse(BankIdTextBox.Text))
                };

                _supplierService.Create(newSupplier);
                LoadSuppliers();
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
                var updatedSupplier = new Supplier
                {
                    Id = Guid.Parse(IdTextBox.Text),
                    NameOfOrganization = NameOfOrganizationTextBox.Text,
                    FirstNameOfSupervisor = FirstNameOfSupervisorTextBox.Text,
                    SecondNameOfSupervisor = SecondNameOfSupervisorTextBox.Text,
                    PatronymicOfSupervisor = PatronymicOfSupervisorTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    INN = INNTextBox.Text,
                    KPP = KPPTextBox.Text,
                    OGRN = OGRNTextBox.Text,
                    SettlementAccount = SettlementAccountTextBox.Text,
                    CorrespondentAccount = CorrespondentAccountTextBox.Text,
                    BIK = BIKTextBox.Text,
                    BankId = Guid.Parse(BankIdTextBox.Text),
                    Bank = _context.Banks.Find(Guid.Parse(BankIdTextBox.Text))
                };

                _supplierService.Update(updatedSupplier);
                LoadSuppliers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSupplier == null)
            {
                MessageBox.Show("Выберите поставщика для удаления.");
                return;
            }
            try
            {
                _supplierService.Delete(_selectedSupplier.Id);
                LoadSuppliers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            DataGridSuppliers.SelectedItem = null;
            IdTextBox.Clear();
            NameOfOrganizationTextBox.Clear();
            FirstNameOfSupervisorTextBox.Clear();
            SecondNameOfSupervisorTextBox.Clear();
            PatronymicOfSupervisorTextBox.Clear();
            PhoneTextBox.Clear();
            INNTextBox.Clear();
            KPPTextBox.Clear();
            OGRNTextBox.Clear();
            SettlementAccountTextBox.Clear();
            CorrespondentAccountTextBox.Clear();
            BIKTextBox.Clear();
            BankIdTextBox.Clear();
            BankNameTextBox.Clear();
        }
    }
}
