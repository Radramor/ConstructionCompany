using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using СonstructionСompany.Model;

namespace СonstructionСompany
{
    public partial class LoginWindow : Window
    {
        private readonly ApplicationDbContext _context;

        public LoginWindow()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            CreateAdminUserIfNotExists();
        }

        private void CreateAdminUserIfNotExists()
        {
            var adminUser = _context.Users.FirstOrDefault(u => u.Login == "admin");

            if (adminUser == null)
            {
                // Если админ-пользователь не найден, создаём его
                var hashedPassword = HashPassword("admin");  // Хэшируем пароль для "admin"

                adminUser = new User
                {
                    Login = "admin",
                    Password = hashedPassword,
                    Role = "Админ"  // Роль администратора
                };

                _context.Users.Add(adminUser);
                _context.SaveChanges();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
                return;
            }

            var user = _context.Users.FirstOrDefault(u => u.Login == username);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                MessageBox.Show("Неверный логин или пароль.");
                return;
            }

            // Переход в основное окно
            MainWindow mainWindow = new MainWindow(user.Role);
            mainWindow.Show();
            Close();
        }

        private void NavigateToRegister_Click(object sender, RoutedEventArgs e)
        {
            var registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            Close();
        }

        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            var hashedPassword = HashPassword(inputPassword);
            return hashedPassword == storedHashedPassword;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }

}
