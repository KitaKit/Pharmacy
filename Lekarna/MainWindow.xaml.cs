using Pharmacy.Additional_windows;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
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
//*редактирование данных прямо в DataGrid с последующим сохранением при нажатии на кнопку
//*поиск и сортировка
//*удаление данных


namespace Pharmacy
{
    public enum SelectedTable
    {
        Medications,
        Warehouses,
        Manufacturers,
        Sales,
        Purchases,
        All
    }
    public partial class MainWindow : Window
    {
        private DataLists _mainDataLists = new DataLists();
        private DataLists _changedDataLists = new DataLists();
        private DataLists _deletedDataLists = null;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void PharmacyMainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void menuItemLoadFromDataBase_Click(object sender, RoutedEventArgs e)
        {
            DatabaseIOLogic database = new DatabaseIOLogic();
            database.ReadData(_mainDataLists);
            DataShow.ToSelectedDataGrid(SelectedTable.All, mainTabControl, _mainDataLists);
        }
        private void menuItemLoadFromCSVFile_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            FilesIOLogic file = new FilesIOLogic(FileConnectionType.Read, selectedTable);
            if (file.Path != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to load data from {file.Path} to {selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    file.ReadData(_mainDataLists);
                    FileConnectionsList.Add(file);
                }
            }
            else
                return;

            DataShow.ToSelectedDataGrid(selectedTable, mainTabControl, _mainDataLists);
        }
        private void menuItemSaveToNewCSVFile_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            FilesIOLogic file = new FilesIOLogic(FileConnectionType.WriteToNew, selectedTable);
            if (file.Path != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to save data from {selectedTable} to {file.Path}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    file.WriteDataToNew(_mainDataLists);
                    FileConnectionsList.Add(file);
                }
            }
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeData.SaveAll(_deletedDataLists, _changedDataLists, _mainDataLists);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    AddMedicationWindow addMedicationWindow = new AddMedicationWindow(_mainDataLists);
                    addMedicationWindow.Owner = this;
                    addMedicationWindow.ShowDialog();
                    break;
                case SelectedTable.Warehouses:
                    AddWarehouseWindow addWarehouseWindow = new AddWarehouseWindow(_mainDataLists);
                    addWarehouseWindow.Owner = this;
                    addWarehouseWindow.ShowDialog();
                    break;
                case SelectedTable.Manufacturers:
                    AddManufacturerWindow addManufacturerWindow = new AddManufacturerWindow(_mainDataLists);
                    addManufacturerWindow.Owner = this;
                    addManufacturerWindow.ShowDialog();
                    break;
                case SelectedTable.Sales:
                    AddSaleWindow addSaleWindow = new AddSaleWindow(_mainDataLists);
                    addSaleWindow.Owner = this;
                    addSaleWindow.ShowDialog();
                    break;
                case SelectedTable.Purchases:
                    AddPurchaseWindow addPurchaseWindow = new AddPurchaseWindow(_mainDataLists);
                    addPurchaseWindow.Owner = this;
                    addPurchaseWindow.ShowDialog();
                    break;
            }
            DataShow.ToSelectedDataGrid(SelectedTable.All, mainTabControl, _mainDataLists);
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            var selectedRow = ((((mainTabControl.SelectedItem as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBoxResult.None;
            _deletedDataLists = new DataLists();

            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as MedicationModel).Title} from {selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    selectedRow = selectedRow as MedicationModel;
                    break;
                case SelectedTable.Warehouses:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as WarehouseModel).Name} from {selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    selectedRow = selectedRow as WarehouseModel;
                    break;
                case SelectedTable.Manufacturers:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as ManufacturerModel).Name} from {selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    selectedRow = selectedRow as ManufacturerModel;
                    break;
                case SelectedTable.Sales:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as SaleModel).Date} from {selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    selectedRow = selectedRow as SaleModel;
                    break;
                case SelectedTable.Purchases:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as PurchaseModel).DeliveryDate} from {selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    selectedRow = selectedRow as PurchaseModel;
                    break;
            }
            _deletedDataLists.Add(selectedRow);
            _mainDataLists.Delete(selectedRow);

            DataShow.ToSelectedDataGrid(SelectedTable.All, mainTabControl, _mainDataLists);
        }
        private void DataGridScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - (e.Delta / 5));
            e.Handled = true;
        }

        private void dataGridMedications_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.DisplayIndex == 2)
                if (_mainDataLists.CategoriesData.FirstOrDefault(x => x.Name == (e.EditingElement as TextBox).Text) == null)
                {
                    MessageBox.Show("Wrong category!");
                    return;
                }
            if (e.Column.DisplayIndex == 3)
                if (_mainDataLists.MedicationFormsData.FirstOrDefault(x => x.Form == (e.EditingElement as TextBox).Text) == null)
                {
                    MessageBox.Show("Wrong form!");
                    return;
                }
            if (e.Column.DisplayIndex == 6)
                if (_mainDataLists.WarehousesData.FirstOrDefault(x => x.Name == (e.EditingElement as TextBox).Text) == null)
                {
                    MessageBox.Show("Wrong warehouse!");
                    return;
                }
            if (e.Column.DisplayIndex == 10)
                if (_mainDataLists.ManufacturersData.FirstOrDefault(x => x.Name == (e.EditingElement as TextBox).Text) == null)
                {
                    MessageBox.Show("Wrong manufacturer!");
                    return;
                }

            _changedDataLists.Add(e.Row.Item as MedicationModel);
        }

        private void dataGridWarehouses_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _changedDataLists.Add(e.Row.Item as WarehouseModel);
        }

        private void dataGridManufacturers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _changedDataLists.Add(e.Row.Item as ManufacturerModel);
        }

        private void dataGridSales_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _changedDataLists.Add(e.Row.Item as SaleModel);
        }

        private void dataGridPurchases_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _changedDataLists.Add(e.Row.Item as PurchaseModel);
        }

        private void menuItemSearch_Click(object sender, RoutedEventArgs e)
        {
            if (stackPanelSearch.Visibility == Visibility.Visible)
            {
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = stackPanelSearch.ActualHeight,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                stackPanelSearch.BeginAnimation(HeightProperty, animation);
                stackPanelSearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                stackPanelSearch.Visibility = Visibility.Visible;
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = 0,
                    To = stackPanelSearch.ActualHeight + 20,
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                stackPanelSearch.BeginAnimation(HeightProperty, animation);
            }
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchedWord = textBoxSearch.Text;
            dynamic searchedRows = null;
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;

            if (string.IsNullOrEmpty(searchedWord))
                DataShow.ToSelectedDataGrid(selectedTable, mainTabControl, _mainDataLists);
            else if (string.IsNullOrWhiteSpace(searchedWord))
                return;
            else
            {
                switch (selectedTable)
                {
                    case SelectedTable.Medications:
                        searchedRows = _mainDataLists.MedicationsData.Where(x => x.Id.ToString().Contains(searchedWord) || x.Title.Contains(searchedWord) || x.Description.Contains(searchedWord)).Distinct().ToList();
                        break;
                    case SelectedTable.Warehouses:
                        searchedRows = _mainDataLists.WarehousesData.Where(x => x.Id.ToString().Contains(searchedWord) || x.Name.Contains(searchedWord) || x.Medications.Contains(searchedWord)).Distinct().ToList();
                        break;
                    case SelectedTable.Manufacturers:
                        searchedRows = _mainDataLists.ManufacturersData.Where(x => x.Id.ToString().Contains(searchedWord) || x.Name.Contains(searchedWord) || x.Country.Contains(searchedWord) || x.License.Contains(searchedWord) || x.Medications.Contains(searchedWord)).Distinct().ToList();
                        break;
                    case SelectedTable.Sales:
                        searchedRows = _mainDataLists.SalesData.Where(x => x.Id.ToString().Contains(searchedWord) || x.Medications.Contains(searchedWord)).Distinct().ToList();
                        break;
                    case SelectedTable.Purchases:
                        searchedRows = _mainDataLists.PurchasesData.Where(x => x.Id.ToString().Contains(searchedWord) || x.Medications.Contains(searchedWord)).Distinct().ToList();
                        break;
                }
                DataShow.ToSelectedDataGrid(selectedTable, mainTabControl, null, searchedRows);
            }
        }
    }
}
//при нажатиии на кнопку сохранения при этом будет браться вся изменённая (в основном окне) инфа и сохраняться так же в бд и файл (надо понять как проверять на то, что было изменение или нет, сейчас я знаю, что когда мы меняем инфу в DataGrid в строках, то оно сразу изменяется в списке, который привязан к этому DataGrid'у) - вроде готова часть, но ничего не проверено, может не работать
//при нажатии на кнопку удаления будет удаляться выбранная строка и будет попытка удаления из бд и файла - вроде готово, но не проверено 
//прописать и обдумать сортировку и поиск - поиск готов
//?что будет если попробовать добавить уже существующие данные?
//добавить чтобы при считывании из файла из таблиц с покупками и продажами столбец с медикаментами выгружался в список проданых и купленых медикоментов в цикле через создание новых объектов купленного и проданного медикамента
//при изменении данных, будет перехватываться событие изменения CellEditEnding, там мы будем брать изменённый объект и сохранять его в список изменений, потом этот список выгружать в бд - вроде готова часть, но не проверено