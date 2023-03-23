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
using System.CodeDom;
using System.Collections;
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
        private DataLists _mainDataLists = new DataLists();
        private DataLists _changedDataLists = new DataLists();
        private bool _databaseConnectionSuccess = false;
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
            DatabaseIOLogic database = new DatabaseIOLogic();
            database.ReadData(_mainDataLists);
            _databaseConnectionSuccess = true;

            //тут мы вызываем метод для отображения данных на экран приложения
            //zde voláme metodu pro zobrazení dat na obrazovce aplikace
            ShowData(SelectedTable.All);
        }
        private void menuItemLoadFromCSVFile_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            FilesIOLogic file = new FilesIOLogic(FileConnectionType.Read, selectedTable);
            if (file.Path != null)
            {
                file.ReadData(_mainDataLists);
                FileConnectionsList.Add(file);
            }
            else
                return;

            ShowData(selectedTable);
        }
        private void menuItemSaveToNewCSVFile_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
                //var selectedDataList = ((((((((mainTabControl.SelectedItem) as TabItem).Content) as Grid).Children)[0] as ScrollViewer).Content) as DataGrid).ItemsSource;
                FilesIOLogic file = new FilesIOLogic(FileConnectionType.WriteToNew, selectedTable);
                if (file.Path != null)
                {
                    file.WriteDataToNew(_mainDataLists);
                    FileConnectionsList.Add(file);
                }
                else
                    return;
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("No data was load to selected table!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var checkerDataLists in typeof(DataLists).GetProperties())
            {
                var changedDataList = checkerDataLists.GetValue(_changedDataLists) as IList;

                if (changedDataList != null && changedDataList.Count != 0)
                {
                    if (!FileConnectionsList.IsEmpty())
                    {
                        SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
                        var requiredFileConnection = FileConnectionsList.Connections.SingleOrDefault(x => x.SelectedTable == selectedTable);
                        if (requiredFileConnection != null)
                        {
                            (requiredFileConnection as FilesIOLogic).AppendData(_changedDataLists, selectedTable);

                            if (_databaseConnectionSuccess)
                            {
                                DatabaseIOLogic database = new DatabaseIOLogic();
                                database.WriteData();
                            }
                        }
                        else if (_databaseConnectionSuccess)
                        {
                            DatabaseIOLogic database = new DatabaseIOLogic();
                            database.WriteData();
                        }
                        else
                            MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (_databaseConnectionSuccess)
                    {
                        DatabaseIOLogic database = new DatabaseIOLogic();
                        database.WriteData();
                    }
                    else 
                        MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
                    _mainDataLists.ShowDataToDataGrid(dataGridMedications, _mainDataLists.MedicationsData);
                    break;
                case SelectedTable.Warehouses:
                    _mainDataLists.ShowDataToDataGrid(dataGridWarehouses, _mainDataLists.WarehousesData);
                    break;
                case SelectedTable.Manufacturers:
                    _mainDataLists.ShowDataToDataGrid(dataGridManufacturers, _mainDataLists.ManufacturersData);
                    break;
                case SelectedTable.Sales:
                    _mainDataLists.ShowDataToDataGrid(dataGridSales, _mainDataLists.SalesData);
                    break;
                case SelectedTable.Purchases:
                        _mainDataLists.ShowDataToDataGrid(dataGridPurchases, _mainDataLists.PurchasesData);
                    break;
                case SelectedTable.All:
                    _mainDataLists.ShowDataToDataGrid(dataGridMedications, _mainDataLists.MedicationsData);
                    _mainDataLists.ShowDataToDataGrid(dataGridWarehouses, _mainDataLists.WarehousesData);
                    _mainDataLists.ShowDataToDataGrid(dataGridManufacturers, _mainDataLists.ManufacturersData);
                    _mainDataLists.ShowDataToDataGrid(dataGridSales, _mainDataLists.SalesData);
                    _mainDataLists.ShowDataToDataGrid(dataGridPurchases, _mainDataLists.PurchasesData);
                    break;
            }
        }

        private void menuItemConnectToDatabase_Click(object sender, RoutedEventArgs e)
        {
            _databaseConnectionSuccess = true;
        }
    }
}