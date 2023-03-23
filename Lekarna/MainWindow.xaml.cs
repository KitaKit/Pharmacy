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
/*
--------------------Список того, что работает прямо сейчас:--------------------

*** Общее
 - Отображение данных в окне приложения (класс DataLists метод ShowDataToDataGrid())
 - Метод при нажатии на кнопку Load from DataBase в верхнем левом меню преложения
 - Метод при нажатии на кнопку Load from CSV-file в верхнем левом меню преложения
 - Метод при нажатии на кнопку Save to new CSV-file в верхнем левом меню преложения
 
*** Database
 - Подключение к базе данных (класс DatabaseConnectionService)
 - Считывание данных из база данных (класс DatabaseLogic)
 - Сохранение данных в соответствующие списки (класс DatabaseLogic с помощью метода AddToDataList() из класса DataLists)
 
*** File
 - Подключение к CSV-файлам (класс FileConnectionService)
 - Считывание данных из отдельных файлов для каждой отдельной таблицы (класс FileIOLogic)
 - Сохранение данных в соответствующий список из соответствующего файла (класс FileIOLogic)
 - Запись данных из соответствующего списка в соответствующий !!!НОВЫЙ!!! файл (класс FileIOLogic)

--------------------Seznam toho, co právě teď funguje:--------------------

*** General
 - Zobrazení dat v okně aplikace (metoda třídy DataLists ShowDataToDataGrid())
 - Metoda při stisknutí tlačítka "Load from DataBase" v levém horním menu aplikace
 - Metoda při stisknutí tlačítka "Load from CSV-file" v levém horním menu aplikace
 - Metoda při stisknutí tlačítka "Save to new CSV-file" v levém horním menu aplikace

*** Database
 - Připojení k databázi (třída DatabaseConnectionService)
 - Načtení dat z databáze (třída DatabaseLogic)
 - Uložení dat do příslušných seznamů (třída DatabaseLogic pomocí metody AddToDataList() ze třídy DataLists)

*** File
 - Připojení k souborům CSV (třída FileConnectionService)
 - Čtení dat z jednotlivých souborů pro každou jednotlivou tabulku (třída FileIOLogic)
 - Ukládání dat do příslušného seznamu z příslušného souboru (třída FileIOLogic)
 - Zápis dat z příslušného seznamu do příslušného !!!NOVÉHO!!! souboru (třída FileIOLogic)
 */


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
            ShowData(SelectedTable.All);
        }

        private void menuItemLoadFromCSVFile_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)(Application.Current.MainWindow.FindName("mainTabControl") as TabControl).SelectedIndex;
            FilesIOLogic file = new FilesIOLogic(FileConnectionType.Read, selectedTable);
            if (file.Path != null)
                file.ReadData(mainDataLists);
            else
                return;

            ShowData(selectedTable);
        }

        private void menuItemSaveToNewCSVFile_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)(Application.Current.MainWindow.FindName("mainTabControl") as TabControl).SelectedIndex;
            FilesIOLogic file = new FilesIOLogic(FileConnectionType.WriteToNew, selectedTable);
            if (file.Path != null)
                file.WriteDataToNew(mainDataLists);
            else
                return;
        }

        private void DataGridScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - (e.Delta / 5));
            e.Handled = true;
        }

        private void ShowData(SelectedTable selectedTable)
        {
            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    mainDataLists.ShowDataToDataGrid(dataGridMedications, mainDataLists.MedicationsData);
                    break;
                case SelectedTable.Warehouses:
                    mainDataLists.ShowDataToDataGrid(dataGridWarehouses, mainDataLists.WarehousesData);
                    break;
                case SelectedTable.Manufacturers:
                    mainDataLists.ShowDataToDataGrid(dataGridManufacturers, mainDataLists.ManufacturersData);
                    break;
                case SelectedTable.Sales:
                    mainDataLists.ShowDataToDataGrid(dataGridSales, mainDataLists.SalesData);
                    break;
                case SelectedTable.Purchases:
                        mainDataLists.ShowDataToDataGrid(dataGridPurchases, mainDataLists.PurchasesData);
                    break;
                case SelectedTable.All:
                    mainDataLists.ShowDataToDataGrid(dataGridMedications, mainDataLists.MedicationsData);
                    mainDataLists.ShowDataToDataGrid(dataGridWarehouses, mainDataLists.WarehousesData);
                    mainDataLists.ShowDataToDataGrid(dataGridManufacturers, mainDataLists.ManufacturersData);
                    mainDataLists.ShowDataToDataGrid(dataGridSales, mainDataLists.SalesData);
                    mainDataLists.ShowDataToDataGrid(dataGridPurchases, mainDataLists.PurchasesData);
                    break;
            }
        }
    }
}