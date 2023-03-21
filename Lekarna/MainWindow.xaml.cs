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

//Здесь будет описана логика основных взаимодействий с окном программы и его содержимым

//Tady bude popsána logika hlavních interakcí s oknem programu a jeho obsahem.

namespace Pharmacy
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void menuItemLoadFromDataBase_Click(object sender, RoutedEventArgs e) //при нажатии на пункт меню "Load from DataBase" (Menu слева сверху) вызывается данный метод и происходит подключение и считывание данных из базы данных с помощью класса DatabaseLogic и метода LoadData()
                                                                                     // Kliknutím na položku menu "Load from DataBase" (Menu vlevo nahoře) se vyvolá tato metoda a připojí se a načte data z databáze pomocí třídy DatabaseLogic a metody LoadData()
        {
            DatabaseLogic dataBase = new DatabaseLogic();
            dataBase.LoadData();
            
            //ниже мы присваиваем для каждого DataGrid источник данных, которым являются наши списки с данными из базы данных, чтобы они выводились на экран в приложении
            //níže přiřadíme každé DataGrid zdroj dat, což jsou naše databázové seznamy, které se mají v aplikaci zobrazit
            dataGridMedications.ItemsSource = dataBase.MedicationsData;
            dataGridWarehouses.ItemsSource = dataBase.WarehousesData;
            dataGridManufacturers.ItemsSource = dataBase.ManufacturersData;
            dataGridSales.ItemsSource = dataBase.SalesData;
            dataGridPurchases.ItemsSource = dataBase.PurchasesData;
        }

        private void PharmacyMainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void DataGridScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - (e.Delta/5));
            e.Handled = true;
        }
    }
}
