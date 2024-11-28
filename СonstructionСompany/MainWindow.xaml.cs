using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using СonstructionСompany.View;

namespace СonstructionСompany
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string role)
        {
            InitializeComponent();
            LoadTabs(role);
        }
        private void LoadTabs(string role)
        {
            TabsControl.Items.Clear();

            switch (role)
            {
                case "Админ":
                    TabsControl.Items.Add(new TabItem { Header = "Бригады", Content = new BrigadeUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Строители", Content = new BuilderUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Описание работ", Content = new AccountingForWorkPerformedUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Строительные материалы", Content = new BuildingMaterialUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Склады", Content = new WarehouseUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Закупка материалов", Content = new BuyingBuildingMaterialsUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Отправка материалов", Content = new BuildingMaterialDistributionUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Объекты", Content = new ObjectUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Сметы", Content = new EstimateUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Поставщики", Content = new SupplierUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Банки", Content = new BankUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Admin Panel", Content = new AdministratorPanelUserControl() });
                    break;

                case "Менеджер":
                    TabsControl.Items.Add(new TabItem { Header = "Бригады", Content = new BrigadeUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Строители", Content = new BuilderUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Описание работ", Content = new AccountingForWorkPerformedUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Строительные материалы", Content = new BuildingMaterialUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Склады", Content = new WarehouseUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Закупка материалов", Content = new BuyingBuildingMaterialsUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Отправка материалов", Content = new BuildingMaterialDistributionUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Объекты", Content = new ObjectUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Сметы", Content = new EstimateUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Поставщики", Content = new SupplierUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Банки", Content = new BankUserControl() });
                    break;

                case "Строитель":
                    TabsControl.Items.Add(new TabItem { Header = "Бригады", Content = new BrigadeUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Строители", Content = new BuilderUserControl() });
                    TabsControl.Items.Add(new TabItem { Header = "Описание работ", Content = new AccountingForWorkPerformedUserControl() });
                    break;

                default:
                    MessageBox.Show("Неверная роль.");
                    Close();
                    break;
            }
        }
    }
}