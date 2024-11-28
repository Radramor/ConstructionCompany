using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using СonstructionСompany.Model;
using СonstructionСompany.Services;

namespace СonstructionСompany.View
{
    public partial class AccountingForWorkPerformedUserControl : UserControl
    {
        private readonly AccountingService _accountingService;
        private AccountingForWorkPerformed _selectedRecord;
        private ApplicationDbContext _context;

        public AccountingForWorkPerformedUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _accountingService = new AccountingService(_context);
            LoadAccountingForWorkPerformed();
        }

        private void LoadAccountingForWorkPerformed()
        {
            try
            {
                // Загружаем данные с подгрузкой связанных сущностей
                var accountingRecords = _context.AccountingForWorkPerformeds
                    .Include(a => a.Brigade) // Загружаем бригаду
                    .Include(a => a.Object) // Загружаем объект
                    .ToList();

                // Устанавливаем данные в DataGrid
                DataGridAccounting.ItemsSource = accountingRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void DataGridAccounting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridAccounting.SelectedItem is AccountingForWorkPerformed selectedRecord)
            {
                IdTextBox.Text = selectedRecord.Id.ToString();
                DatePickerDate.SelectedDate = selectedRecord.Date;
                BrigadeIdTextBox.Text = selectedRecord.BrigadeId.ToString();
                BrigadeNameTextBox.Text = selectedRecord.Brigade.Name.ToString();
                WorkDescriptionTextBox.Text = selectedRecord.WorkDescription;
                ObjectIdTextBox.Text = selectedRecord.ObjectId.ToString();
                ObjectNameTextBox.Text = selectedRecord.Object.Name.ToString();
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newRecord = new AccountingForWorkPerformed
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.SpecifyKind(DatePickerDate.SelectedDate.Value, DateTimeKind.Utc).ToUniversalTime(),
                    BrigadeId = Guid.Parse(BrigadeIdTextBox.Text),
                    WorkDescription = WorkDescriptionTextBox.Text,
                    ObjectId = Guid.Parse(ObjectIdTextBox.Text)
                };

                _accountingService.Create(newRecord);
                LoadAccountingForWorkPerformed();
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
                var updatedRecord = new AccountingForWorkPerformed
                {
                    Id = Guid.Parse(IdTextBox.Text),
                    Date = DateTime.SpecifyKind(DatePickerDate.SelectedDate.Value, DateTimeKind.Utc).ToUniversalTime(),
                    BrigadeId = Guid.Parse(BrigadeIdTextBox.Text),
                    WorkDescription = WorkDescriptionTextBox.Text,
                    ObjectId = Guid.Parse(ObjectIdTextBox.Text)
                };

                _accountingService.Update(updatedRecord);
                LoadAccountingForWorkPerformed();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRecord == null)
            {
                MessageBox.Show("Выберите описание работ для удаления.");
                return;
            }
            try
            {
                _accountingService.Delete(_selectedRecord.Id);
                LoadAccountingForWorkPerformed();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            DataGridAccounting.SelectedItem = null;
            IdTextBox.Clear();
            DatePickerDate.SelectedDate = null;
            BrigadeIdTextBox.Clear();
            BrigadeNameTextBox.Clear();
            WorkDescriptionTextBox.Clear();
            ObjectIdTextBox.Clear();
            ObjectNameTextBox.Clear();
        }
    }
}
