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

namespace СonstructionСompany.View
{
    /// <summary>
    /// Логика взаимодействия для AdministratorPanelUserControl.xaml
    /// </summary>
    public partial class AdministratorPanelUserControl : UserControl
    {
        private readonly ApplicationDbContext _context;

        // Список доступных ролей
        public List<string> Roles { get; } = new List<string> { "Админ", "Менеджер", "Строитель" };

        public AdministratorPanelUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            LoadUsers();
            // Здесь назначаем Roles в DataContext, чтобы привязка в XAML сработала
            this.DataContext = this;
        }

        // Загрузка списка пользователей
        private void LoadUsers()
        {
            var users = _context.Users.ToList();
            UsersDataGrid.ItemsSource = users;
        }

        // Обработчик кнопки "Добавить"
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                MessageBox.Show("Введите логин пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User
            {
                Login = LoginTextBox.Text,
                Role = RoleComboBox.SelectedItem?.ToString() ?? "Строитель"  // По умолчанию роль "Строитель"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            LoadUsers(); // Перезагружаем список пользователей
        }

        // Обработчик кнопки "Изменить"
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = (User)UsersDataGrid.SelectedItem;
            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для изменения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Обновляем роль выбранного пользователя
            selectedUser.Role = RoleComboBox.SelectedItem?.ToString() ?? selectedUser.Role;

            _context.SaveChanges();
            LoadUsers();
        }

        // Обработчик кнопки "Удалить"
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = (User)UsersDataGrid.SelectedItem;
            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirmation = MessageBox.Show($"Вы уверены, что хотите удалить пользователя {selectedUser.Login}?",
                                                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmation == MessageBoxResult.Yes)
            {
                _context.Users.Remove(selectedUser);
                _context.SaveChanges();
                LoadUsers();
            }
        }

        // Обработчик изменения выбора в DataGrid
        private void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedUser = (User)UsersDataGrid.SelectedItem;
            if (selectedUser != null)
            {
                LoginTextBox.Text = selectedUser.Login;
                RoleComboBox.SelectedItem = selectedUser.Role; // Отображаем роль в ComboBox
            }
        }
    }
}
