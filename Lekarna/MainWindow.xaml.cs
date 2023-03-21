using Microsoft.Win32;
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
using System.IO;

//Здесь будет описана логика основных взаимодействий с окном программы и его содержимым

//Tady bude popsána logika hlavních interakcí s oknem programu a jeho obsahem.

namespace Pharmacy
{
    public partial class MainWindow : Window
    {
        private DataLists mainDataLists = new DataLists();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void PharmacyMainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void menuItemLoadFromDataBase_Click(object sender, RoutedEventArgs e) //при нажатии на пункт меню "Load from DataBase" (Menu слева сверху) вызывается данный метод и происходит подключение и считывание данных из базы данных с помощью класса DatabaseLogic и метода LoadData()
                                                                                     // Kliknutím na položku menu "Load from DataBase" (Menu vlevo nahoře) se vyvolá tato metoda a připojí se a načte data z databáze pomocí třídy DatabaseLogic a metody LoadData()
        {
            DatabaseIOLogic dataBase = new DatabaseIOLogic();
            dataBase.ReadData(mainDataLists);

            //тут мы вызываем метод для отображения данных на экран приложения
            //zde voláme metodu pro zobrazení dat na obrazovce aplikace
            ShowData();
        }

        private void menuItemLoadFromCSVFile_Click(object sender, RoutedEventArgs e)
        {
            FilesIOLogic file = new FilesIOLogic();
            file.ReadData(mainDataLists);

            ShowData();
        }

        private void DataGridScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - (e.Delta / 5));
            e.Handled = true;
        }

        private void ShowData()
        {
            mainDataLists.ShowDataFromDataListsToDataGrid(dataGridMedications, mainDataLists.MedicationsData);
            mainDataLists.ShowDataFromDataListsToDataGrid(dataGridWarehouses, mainDataLists.WarehousesData);
            mainDataLists.ShowDataFromDataListsToDataGrid(dataGridManufacturers, mainDataLists.ManufacturersData);
            mainDataLists.ShowDataFromDataListsToDataGrid(dataGridSales, mainDataLists.SalesData);
            mainDataLists.ShowDataFromDataListsToDataGrid(dataGridPurchases, mainDataLists.PurchasesData);
        }
    }
}
/*Список того, что работает прямо сейчас:
 - Подключение к базе данных (класс DatabaseConnectionService)
 - Считывание данных из база данных (класс DatabaseLogic)
 - Сохранение данных в соответствующие списки (класс DatabaseLogic с помощью метода AddToDataList() из класса DataLists)
 - Отображение данных в окне приложения (класс DataLists метод ShowDataToDataGrid())
 
 Seznam toho, co právě teď funguje:
 - Připojení k databázi (třída DatabaseConnectionService)
 - Načtení dat z databáze (třída DatabaseLogic)
 - Uložení dat do příslušných seznamů (třída DatabaseLogic pomocí metody AddToDataList() ze třídy DataLists)
 - Zobrazení dat v okně aplikace (třída DataLists metoda ShowDataToDataGrid())
 */