using ConstructionCompany;
using ConstructionCompany.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using СonstructionСompany;
using СonstructionСompany.Model;

namespace СonstructionСompany
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            var services = new ServiceCollection();

            // Чтение строки подключения из app.config
            string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString)); // Подключение к PostgreSQL через Npgsql

            // Регистрация MainWindow
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginWindow>();
            _serviceProvider = services.BuildServiceProvider();
        }

        /*protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<BankWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }*/
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
    }

}
