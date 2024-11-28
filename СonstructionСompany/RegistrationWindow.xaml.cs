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
using System.Windows.Shapes;
using System.Security.Cryptography;
using СonstructionСompany.Model;

namespace СonstructionСompany
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly ApplicationDbContext _context;

        public RegistrationWindow()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _context = new ApplicationDbContext(optionsBuilder.Options);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
                return;
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Login == username);
            if (existingUser != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует.");
                return;
            }

            // Хэшируем пароль перед сохранением
            var hashedPassword = HashPassword(password);

            var newUser = new User
            {
                Login = username,
                Password = hashedPassword,
                Role = "Строитель"  // Роль по умолчанию
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            MessageBox.Show("Регистрация прошла успешно. Теперь войдите в систему.");
            NavigateToLogin();
        }

        private void NavigateToLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigateToLogin();
        }

        private void NavigateToLogin()
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
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
