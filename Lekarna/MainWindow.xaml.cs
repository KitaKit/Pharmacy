using Pharmacy.Additional_windows;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public DataLists mainDataLists = new DataLists();
        public DataLists changedDataLists = null;
        public DataLists deletedDataLists = null;
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
            database.ReadData(mainDataLists);
            DataShow.ToSelectedDataGrid(SelectedTable.All, mainTabControl, mainDataLists);
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
                    file.ReadData(mainDataLists);
                    FileConnectionsList.Add(file);
                }
            }
            else
                return;

            DataShow.ToSelectedDataGrid(selectedTable, mainTabControl, mainDataLists);
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
                    file.WriteDataToNew(mainDataLists);
                    FileConnectionsList.Add(file);
                }
            }
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeData.SaveAll(deletedDataLists, changedDataLists, mainDataLists);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    AddMedicationWindow addMedicationWindow = new AddMedicationWindow(mainDataLists);
                    addMedicationWindow.Owner = this;
                    addMedicationWindow.ShowDialog();
                    break;
                case SelectedTable.Warehouses:
                    AddWarehouseWindow addWarehouseWindow = new AddWarehouseWindow(mainDataLists);
                    addWarehouseWindow.Owner = this;
                    addWarehouseWindow.ShowDialog();
                    break;
                case SelectedTable.Manufacturers:
                    AddManufacturerWindow addManufacturerWindow = new AddManufacturerWindow(mainDataLists);
                    addManufacturerWindow.Owner = this;
                    addManufacturerWindow.ShowDialog();
                    break;
                case SelectedTable.Sales:
                    AddSaleWindow addSaleWindow = new AddSaleWindow(mainDataLists);
                    addSaleWindow.Owner = this;
                    addSaleWindow.ShowDialog();
                    break;
                case SelectedTable.Purchases:
                    AddPurchaseWindow addPurchaseWindow = new AddPurchaseWindow(mainDataLists);
                    addPurchaseWindow.Owner = this;
                    addPurchaseWindow.ShowDialog();
                    break;
            }
            DataShow.ToSelectedDataGrid(selectedTable, mainTabControl, mainDataLists);
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            var selectedRow = ((((mainTabControl.SelectedItem as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBoxResult.None;
            deletedDataLists = new DataLists();

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
            deletedDataLists.Add(selectedRow);
            mainDataLists.Delete(selectedRow);

            DataShow.ToSelectedDataGrid(selectedTable, mainTabControl, mainDataLists);
        }
        private void DataGridScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - (e.Delta / 5));
            e.Handled = true;
        }
    }
}
//при нажатиии на кнопку сохранения при этом будет браться вся изменённая (в основном окне) инфа и сохраняться так же в бд и файл (надо понять как проверять на то, что было изменение или нет, сейчас я знаю, что когда мы меняем инфу в DataGrid в строках, то оно сразу изменяется в списке, который привязан к этому DataGrid'у)
//при нажатии на кнопку удаления будет удаляться выбранная строка и будет попытка удаления из бд и файла
//прописать и обдумать сортировку и поиск
//?что будет если попробовать добавить уже существующие данные?
//добавить чтобы при считывании из файла из таблиц с покупками и продажами столбец с медикаментами выгружался в список проданых и купленых медикоментов в цикле через создание новых объектов купленного и проданного медикамента
//написать нажатие на кнопку сохранения, т.е. будет сверка данных из выбранной для сохранения таблицы, если есть отличия, то будет передаваться запрос на изменение. бот предлагает это сделать через метод Except(), который монжо использовать после переопределния методов GetHashCode() и Equals() для каждой модели для сравнения хорошо 
//при изменении данных, будет перехватываться событие изменения CellEditEnding, там мы будем брать изменённый объект и сохранять его в список изменений, потом этот список выгружать в бд