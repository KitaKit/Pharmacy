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
using Pharmacy.Additional_windows;
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


//------Сейчас в работе------//
//*окна добавления новых данных (готовы не все поля, потому что нужно продумать логику выборки данных для отображения из других таблиц (моделей)) (написать добавление в бд)
//*редактирование данных прямо в DataGrid с последующим сохранением при нажатии на кнопку
//*поиск и сортировка
//*удаление данных

//Здесь будет описана логика основных взаимодействий с окном программы и его содержимым

//Tady bude popsána logika hlavních interakcí s oknem programu a jeho obsahem.

namespace Pharmacy
{
    public partial class MainWindow : Window
    {
        //private DataLists _addedDataLists = new DataLists();
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
            database.ReadData();

            //тут мы вызываем метод для отображения данных на экран приложения
            //zde voláme metodu pro zobrazení dat na obrazovce aplikace
            DataShow.ToSelectedDataGrid(SelectedTable.All, mainTabControl);
        }
        private void menuItemLoadFromCSVFile_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            FilesIOLogic file = new FilesIOLogic(FileConnectionType.Read, selectedTable);
            if (file.Path != null)
            {
                file.ReadData();
                FileConnectionsList.Add(file);
            }
            else
                return;

            DataShow.ToSelectedDataGrid(selectedTable, mainTabControl);
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
                    file.WriteDataToNew();
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
            SaveAllChangedData();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    AddMedicationWindow addMedicationWindow = new AddMedicationWindow();
                    addMedicationWindow.ShowDialog();
                    break;
                case SelectedTable.Warehouses:
                    AddWarehouseWindow addWarehouseWindow = new AddWarehouseWindow();
                    addWarehouseWindow.ShowDialog();
                    break;
                case SelectedTable.Manufacturers:
                    AddManufacturerWindow addManufacturerWindow = new AddManufacturerWindow();
                    addManufacturerWindow.ShowDialog();
                    break;
                case SelectedTable.Sales:
                    AddSaleWindow addSaleWindow = new AddSaleWindow();
                    addSaleWindow.ShowDialog();
                    break;
                case SelectedTable.Purchases:
                    AddPurchaseWindow addPurchaseWindow = new AddPurchaseWindow();
                    addPurchaseWindow.ShowDialog();
                    break;
            }
        }
        private void DataGridScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - (e.Delta / 5));
            e.Handled = true;
        }

        private void SaveAllChangedData()
        {
            //_addedDataLists.AddToDataList(_mainDataLists.MedicationsData[0], _addedDataLists.MedicationsData);
            //foreach (var checkerDataLists in typeof(DataLists).GetProperties())
            //{
            //    var changedDataList = checkerDataLists.GetValue(_addedDataLists) as IList;

            //    if (changedDataList != null && changedDataList.Count != 0)
            //    {
            //        if (!FileConnectionsList.IsEmpty())
            //        {
                        
            //            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            //            var requiredFileConnection = FileConnectionsList.Connections.SingleOrDefault(x => x.SelectedTable == selectedTable);
            //            if (requiredFileConnection != null)
            //            {
            //                (requiredFileConnection as FilesIOLogic).AppendData(_addedDataLists, selectedTable);

            //                if (_databaseConnectionSuccess)
            //                {
            //                    DatabaseIOLogic database = new DatabaseIOLogic();
            //                    database.WriteData();
            //                }
            //            }
            //            else if (_databaseConnectionSuccess)
            //            {
            //                DatabaseIOLogic database = new DatabaseIOLogic();
            //                database.WriteData();
            //            }
            //            else
            //                MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //        }
            //        else if (_databaseConnectionSuccess)
            //        {
            //            DatabaseIOLogic database = new DatabaseIOLogic();
            //            database.WriteData();
            //        }
            //        else
            //            MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
        }
    }
}
//добавление в окне добавлений будет работать так, что при нажатии на кнопку сохранения, новый экземпляр будет добавляться в список, потом обновление отображения списка на экране, потом попытка сохранения в бд и файл по коду который написан выше с пометкой. нужно добавить свойство IsChecked для всех моделей данных, чтобы можно было привязать их к чекбоксам в юзерконтролах и реализовать для всех юзерконтролов интерфейс INotifyPropertyChanged, чтобы свойство изменялось при нажатии на чекбокс, после чего брать все выбранные и добавлять их id в бд, а имя в файл 
//при нажатиии на кнопку сохранения при этом будет браться вся изменённая (в основном окне) инфа и сохраняться так же в бд и файл (надо понять как проверять на то, что было изменение или нет, сейчас я знаю, что когда мы меняем инфу в DataGrid в строках, то оно сразу изменяется в списке, который привязан к этому DataGrid'у)
//при нажатии на кнопку удаления будет удаляться выбранная строка и будет попытка удаления из бд и файла
//прописать и обдумать сортировку и поиск
//?что будет если попробовать добавить уже существующие данные?