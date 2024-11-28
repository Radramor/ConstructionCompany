using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ConstructionCompany;
using ConstructionCompany.Services;
using Microsoft.EntityFrameworkCore;
using СonstructionСompany.Model;
using СonstructionСompany.Services;

namespace СonstructionСompany.View
{
    /// <summary>
    /// Логика взаимодействия для BrigadeUserControl.xaml
    /// </summary>
    public partial class BrigadeUserControl : UserControl
    {
        private readonly BrigadeService _brigadeService;

        public BrigadeUserControl()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=ConstructionCompany;Username=postgres;Password=admin");
            _brigadeService = new BrigadeService(new ApplicationDbContext(optionsBuilder.Options));
            LoadBrigades();
        }

        private void LoadBrigades()
        {
            BrigadesDataGrid.ItemsSource = _brigadeService.GetAll();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newBrigade = new Brigade
            {
                Id = Guid.NewGuid(),
                Name = BrigadeNameTextBox.Text
            };

            _brigadeService.Create(newBrigade);
            LoadBrigades();
            ClearFields();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Guid.TryParse(BrigadeIdTextBox.Text, out var brigadeId))
            {
                var brigadeToUpdate = _brigadeService.GetById(brigadeId);

                if (brigadeToUpdate != null)
                {
                    brigadeToUpdate.Name = BrigadeNameTextBox.Text;
                    _brigadeService.Update(brigadeToUpdate);
                    LoadBrigades();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Бригада не найдена!");
                }
            }
            else
            {
                MessageBox.Show("Введите корректный ID бригады.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Guid.TryParse(BrigadeIdTextBox.Text, out var brigadeId))
            {
                _brigadeService.Delete(brigadeId);
                LoadBrigades();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Введите корректный ID бригады.");
            }
        }

        private void BrigadesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BrigadesDataGrid.SelectedItem is Brigade selectedBrigade)
            {
                BrigadeIdTextBox.Text = selectedBrigade.Id.ToString();
                BrigadeNameTextBox.Text = selectedBrigade.Name;
            }
        }

        private void ClearFields()
        {
            BrigadesDataGrid.SelectedItem = null;
            BrigadeIdTextBox.Clear();
            BrigadeNameTextBox.Clear();
        }
    }
}
