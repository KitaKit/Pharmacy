using Pharmacy.Additional_windows;
using Pharmacy.Sorting_models;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

/*
 * меняю warehouse name меняется в medications
 * manufacturers medications.manufacturer
 * 
 */
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
        private DataLists _deletedDataLists = new DataLists();
        private dynamic originalGridRowValue = null;
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
            MedicationsSort.SetParameters(_mainDataLists, stackPanelMedicationsSort);
            WarehousesSort.SetParameters(_mainDataLists.MedicationsData, wrapPanelSortWarehouses);
            ManufacturersSort.SetParameters(comboBoxSortManufacturersCountry, wrapPanelSortManufacturers, _mainDataLists);
            SalesSort.SetParameters(_mainDataLists.MedicationsData, wrapPanelSortSales);
            PurchasesSort.SetParameters(_mainDataLists, wrapPanelSortPurchases, comboBoxSortPurchasesProvider);
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
            MedicationsSort.SetParameters(_mainDataLists, stackPanelMedicationsSort);
            WarehousesSort.SetParameters(_mainDataLists.MedicationsData, wrapPanelSortWarehouses);
            ManufacturersSort.SetParameters(comboBoxSortManufacturersCountry, wrapPanelSortManufacturers, _mainDataLists);
            SalesSort.SetParameters(_mainDataLists.MedicationsData, wrapPanelSortSales);
            PurchasesSort.SetParameters(_mainDataLists, wrapPanelSortPurchases, comboBoxSortPurchasesProvider);
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
            DataShow.ToSelectedDataGrid(SelectedTable.All, mainTabControl, _mainDataLists);
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
            MedicationsSort.SetParameters(_mainDataLists, stackPanelMedicationsSort);
            WarehousesSort.SetParameters(_mainDataLists.MedicationsData, wrapPanelSortWarehouses);
            ManufacturersSort.SetParameters(comboBoxSortManufacturersCountry, wrapPanelSortManufacturers, _mainDataLists);
            SalesSort.SetParameters(_mainDataLists.MedicationsData, wrapPanelSortSales);
            PurchasesSort.SetParameters(_mainDataLists, wrapPanelSortPurchases, comboBoxSortPurchasesProvider);
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;
            var selectedRow = ((((mainTabControl.SelectedItem as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).SelectedItem;
            MessageBoxResult messageBoxResult = MessageBoxResult.None;
            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as MedicationModel).Title} from {selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    selectedRow = selectedRow as MedicationModel;
                    break;
                case SelectedTable.Warehouses:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as WarehouseModel).Name} from {selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (!string.IsNullOrEmpty((selectedRow as WarehouseModel).Medications))
                    {
                        MessageBox.Show("There are medicines in this warehouse, you cannot delete a filled warehouse!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    selectedRow = selectedRow as WarehouseModel;
                    break;
                case SelectedTable.Manufacturers:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as ManufacturerModel).Name} from {selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (!string.IsNullOrEmpty((selectedRow as ManufacturerModel).Medications))
                    {
                        MessageBox.Show("We buy medicines from this manufacturer, you can't delete the manufacturer we work with!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
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
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _deletedDataLists.Add(selectedRow);
                _mainDataLists.Delete(selectedRow);
            }
            else
                return;

            DataShow.ToSelectedDataGrid(SelectedTable.All, mainTabControl, _mainDataLists);
            MedicationsSort.SetParameters(_mainDataLists, stackPanelMedicationsSort);
            WarehousesSort.SetParameters(_mainDataLists.MedicationsData, wrapPanelSortWarehouses);
            ManufacturersSort.SetParameters(comboBoxSortManufacturersCountry, wrapPanelSortManufacturers, _mainDataLists);
            SalesSort.SetParameters(_mainDataLists.MedicationsData, wrapPanelSortSales);
            PurchasesSort.SetParameters(_mainDataLists, wrapPanelSortPurchases, comboBoxSortPurchasesProvider);
        }

        private void dataGridMedications_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (Validation.GetHasError(e.EditingElement))
            {
                return;
            }
            var editedCell = e.EditingElement as TextBox;
            var newVal = editedCell.Text;
            if (originalGridRowValue != newVal)
            {
                if(e.Column.DisplayIndex == 1)
                {
                    dynamic toReplace = _mainDataLists.WarehousesData.Where(x => x.Medications.Contains(originalGridRowValue)).ToList();
                    foreach(var model in toReplace)
                    {
                        model.Medications.Replace(originalGridRowValue, newVal);
                    }
                    toReplace = _mainDataLists.ManufacturersData.Where(x => x.Medications.Contains(originalGridRowValue)).ToList();
                    foreach (var model in toReplace)
                    {
                        model.Medications.Replace(originalGridRowValue, newVal);
                    }
                    toReplace = _mainDataLists.SalesData.Where(x => x.Medications.Contains(originalGridRowValue)).ToList();
                    foreach (var model in toReplace)
                    {
                        model.Medications.Replace(originalGridRowValue, newVal);
                    }
                    toReplace = _mainDataLists.PurchasesData.Where(x => x.Medications.Contains(originalGridRowValue)).ToList();
                    foreach (var model in toReplace)
                    {
                        model.Medications.Replace(originalGridRowValue, newVal);
                    }
                }
                if (e.Column.DisplayIndex == 2)
                    if (_mainDataLists.CategoriesData.FirstOrDefault(x => x.Name == (e.EditingElement as TextBox).Text) == null)
                    {
                        MessageBox.Show("Wrong category!");
                        newVal = originalGridRowValue;
                        return;
                    }
                if (e.Column.DisplayIndex == 3)
                    if (_mainDataLists.MedicationFormsData.FirstOrDefault(x => x.Form == (e.EditingElement as TextBox).Text) == null)
                    {
                        MessageBox.Show("Wrong form!");
                        newVal = originalGridRowValue;
                        return;
                    }
                if (e.Column.DisplayIndex == 6)
                    if (_mainDataLists.WarehousesData.FirstOrDefault(x => x.Name == (e.EditingElement as TextBox).Text) == null)
                    {
                        MessageBox.Show("Wrong warehouse!");
                        newVal = originalGridRowValue;
                        return;
                    }
                if (e.Column.DisplayIndex == 10)
                    if (_mainDataLists.ManufacturersData.FirstOrDefault(x => x.Name == (e.EditingElement as TextBox).Text) == null)
                    {
                        MessageBox.Show("Wrong manufacturer!");
                        newVal = originalGridRowValue;
                        return;
                    }

                _changedDataLists.Add(e.Row.Item as MedicationModel);
            }
        }

        private void dataGridWarehouses_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (Validation.GetHasError(e.EditingElement))
            {
                return;
            }
            var editedCell = e.EditingElement as TextBox;
            var newVal = editedCell.Text;
            if (e.Column.DisplayIndex == 1)
            {
                dynamic toReplace = _mainDataLists.MedicationsData.Where(x=>x.Warehouse.Contains(originalGridRowValue)).ToList();
                foreach (var model in toReplace)
                {
                    model.Warehouse.Replace(originalGridRowValue, newVal);
                }
            }
            if (originalGridRowValue != newVal)
                _changedDataLists.Add(e.Row.Item as WarehouseModel);
        }

        private void dataGridManufacturers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (Validation.GetHasError(e.EditingElement))
            {
                return;
            }
            var editedCell = e.EditingElement as TextBox;
            var newVal = editedCell.Text;
            if (e.Column.DisplayIndex == 1)
            {
                dynamic toReplace = _mainDataLists.MedicationsData.Where(x => x.Manufacturer.Contains(originalGridRowValue)).ToList();
                foreach (var model in toReplace)
                {
                    model.Manufacturer.Replace(originalGridRowValue, newVal);
                }
            }
            if (originalGridRowValue != newVal)
            {
                _changedDataLists.Add(e.Row.Item as ManufacturerModel);
            }
        }

        private void dataGridSales_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (Validation.GetHasError(e.EditingElement))
            {
                return;
            }
            var editedCell = e.EditingElement as TextBox;
            var newVal = editedCell.Text;
            if (originalGridRowValue != newVal)
                _changedDataLists.Add(e.Row.Item as SaleModel);
        }

        private void dataGridPurchases_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (Validation.GetHasError(e.EditingElement))
            {
                return;
            }
            var editedCell = e.EditingElement as TextBox;
            var newVal = editedCell.Text;
            if (originalGridRowValue != newVal)
                _changedDataLists.Add(e.Row.Item as PurchaseModel);
        }

        private void menuItemSearch_Click(object sender, RoutedEventArgs e)
        {
            if (stackPanelSearch.Visibility == Visibility.Visible)
                MenuItemAnimations.Invisible(stackPanelSearch, HeightProperty);
            else
                MenuItemAnimations.Visible(stackPanelSearch, HeightProperty, 50);
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
                DataShow.ToSelectedDataGrid(selectedTable, mainTabControl, searchedRows);
            }
        }

        private void menuItemSort_Click(object sender, RoutedEventArgs e)
        {
            SelectedTable selectedTable = (SelectedTable)mainTabControl.SelectedIndex;

            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    if (stackPanelMedicationsSort.Visibility == Visibility.Visible)
                        MenuItemAnimations.Invisible(stackPanelMedicationsSort, HeightProperty);
                    else
                        MenuItemAnimations.Visible(stackPanelMedicationsSort, HeightProperty, 225);
                    break;
                case SelectedTable.Warehouses:
                    if (stackPanelSortWarehouses.Visibility == Visibility.Visible)
                        MenuItemAnimations.Invisible(stackPanelSortWarehouses, HeightProperty);
                    else
                        MenuItemAnimations.Visible(stackPanelSortWarehouses, HeightProperty, 225);
                    break;
                case SelectedTable.Manufacturers:
                    if (stackPanelSortManufacturers.Visibility == Visibility.Visible)
                        MenuItemAnimations.Invisible(stackPanelSortManufacturers, HeightProperty);
                    else
                        MenuItemAnimations.Visible(stackPanelSortManufacturers, HeightProperty, 225);
                    break;
                case SelectedTable.Sales:
                    if (stackPanelSortSales.Visibility == Visibility.Visible)
                        MenuItemAnimations.Invisible(stackPanelSortSales, HeightProperty);
                    else
                        MenuItemAnimations.Visible(stackPanelSortSales, HeightProperty, 225);
                    break;
                case SelectedTable.Purchases:
                    if (stackPanelSortPurchases.Visibility == Visibility.Visible)
                        MenuItemAnimations.Invisible(stackPanelSortPurchases, HeightProperty);
                    else
                        MenuItemAnimations.Visible(stackPanelSortPurchases, HeightProperty, 225);
                    break;
            }
        }

        private void buttonClearSearchText_Click(object sender, RoutedEventArgs e)
        {
            textBoxSearch.Text = string.Empty;
        }
        private void buttonSortCount_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            button.Content = button.Content.ToString() == "Count from" ? "Count up to" : "Count from";
        }

        private void buttonSortPrice_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            button.Content = button.Content.ToString() == "Price from" ? "Price up to" : "Price from";
        }
        private void DataGridScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - (e.Delta / 5));
            e.Handled = true;
        }

        private void buttonSortApply_Click(object sender, RoutedEventArgs e)
        {
            MedicationsSort.Sort(comboBoxSortForm, comboBoxSortCategory, comboBoxSortAvailability, buttonSortCount, textBoxSortCount, comboBoxSortWarehouse, comboBoxSortPrescription, buttonSortPrice, textBoxSortPrice, comboBoxSortManufacturer, _mainDataLists.MedicationsData,dataGridMedications, mainTabControl);
        }

        private bool isComboBoxDropDownOpened = false;

        private void PharmacyMainWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var sourceElement = e.OriginalSource as FrameworkElement;

            if (!isComboBoxDropDownOpened)
            {
                if (stackPanelMedicationsSort.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelMedicationsSort))
                    MenuItemAnimations.Invisible(stackPanelMedicationsSort, HeightProperty);
                if (stackPanelSearch.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSearch))
                    MenuItemAnimations.Invisible(stackPanelSearch, HeightProperty);
                if(stackPanelSortWarehouses.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSortWarehouses))
                    MenuItemAnimations.Invisible(stackPanelSortWarehouses, HeightProperty);
                if(stackPanelSortManufacturers.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSortManufacturers))
                    MenuItemAnimations.Invisible(stackPanelSortManufacturers, HeightProperty);
                if(stackPanelSortSales.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSortSales))
                    MenuItemAnimations.Invisible(stackPanelSortSales, HeightProperty);
                if(stackPanelSortPurchases.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSortPurchases))
                    MenuItemAnimations.Invisible(stackPanelSortPurchases, HeightProperty);
            }
        }
        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            isComboBoxDropDownOpened = true;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            isComboBoxDropDownOpened = false;
        }

        private bool IsDescendantOfStackPanel(DependencyObject element, StackPanel stackPanel)
        {
            if (element == null)
                return false;

            if (element == stackPanel)
                return true;

            return IsDescendantOfStackPanel(VisualTreeHelper.GetParent(element), stackPanel);
        }

        private void buttonSortClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxSortForm.SelectedIndex = -1;
            comboBoxSortCategory.SelectedIndex = -1;
            comboBoxSortAvailability.SelectedIndex = -1;
            textBoxSortCount.Text = Convert.ToString(0);
            comboBoxSortWarehouse.SelectedIndex = -1;
            comboBoxSortPrescription.SelectedIndex = -1;
            textBoxSortPrice.Text = Convert.ToString(0);
            comboBoxSortManufacturer.SelectedIndex = -1;
            DataShow.ToSelectedDataGrid(SelectedTable.Medications, mainTabControl, _mainDataLists.MedicationsData);
        }

        private void buttonSortWarehousesApply_Click(object sender, RoutedEventArgs e)
        {
            WarehousesSort.Sort(wrapPanelSortWarehouses, _mainDataLists.WarehousesData, mainTabControl);
        }

        private void buttonSortWarehousesClear_Click(object sender, RoutedEventArgs e)
        {
            foreach(var box in wrapPanelSortWarehouses.Children.OfType<CheckBox>())
            {
                box.IsChecked = false;
            }
            DataShow.ToSelectedDataGrid(SelectedTable.Warehouses, mainTabControl, _mainDataLists.WarehousesData);
        }

        private void buttonSortManufacturersApply_Click(object sender, RoutedEventArgs e)
        {
            ManufacturersSort.Sort(wrapPanelSortManufacturers, comboBoxSortManufacturersCountry, _mainDataLists.ManufacturersData, mainTabControl);
        }

        private void buttonSortManufacturersClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxSortManufacturersCountry.SelectedIndex = -1;
            foreach(var box in wrapPanelSortManufacturers.Children.OfType<CheckBox>())
            {
                box.IsChecked = false;
            }
            DataShow.ToSelectedDataGrid(SelectedTable.Manufacturers, mainTabControl, _mainDataLists.ManufacturersData);
        }

        private void buttonSortPurchasesApply_Click(object sender, RoutedEventArgs e)
        {
            PurchasesSort.Sort(_mainDataLists.PurchasesData, wrapPanelSortPurchases, comboBoxSortPurchasesProvider, buttonPurchasesSortCost, textBoxPurchasesSortCost, mainTabControl);
        }

        private void buttonSortPurchasesClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxPurchasesSortCost.Text = Convert.ToString(0);
            comboBoxSortPurchasesProvider.SelectedIndex = -1;
            foreach (var box in wrapPanelSortPurchases.Children.OfType<CheckBox>())
            {
                box.IsChecked = false;
            }
            DataShow.ToSelectedDataGrid(SelectedTable.Purchases, mainTabControl, _mainDataLists.PurchasesData);
        }

        private void buttonPurchasesSortCost_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            button.Content = button.Content.ToString() == "Cost from" ? "Cost up to" : "Cost from";
        }

        private void buttonSortSalesApply_Click(object sender, RoutedEventArgs e)
        {
            SalesSort.Sort(buttonSalesSortPrice, textBoxSalesSortPrice, wrapPanelSortSales, _mainDataLists.SalesData, mainTabControl);
        }

        private void buttonSortSalesClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxSalesSortPrice.Text = Convert.ToString(0);
            foreach (var box in wrapPanelSortSales.Children.OfType<CheckBox>())
            {
                box.IsChecked = false;
            }
            DataShow.ToSelectedDataGrid(SelectedTable.Sales, mainTabControl, _mainDataLists.SalesData);
        }

        private void buttonSalesSortPrice_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            button.Content = button.Content.ToString() == "Price from" ? "Price up to" : "Price from";
        }

        private void dataGrid_GeneralBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var row = e.Row.Item;
            var column = e.Column;
            var value = (column.GetCellContent(row) as TextBlock).Text;
            originalGridRowValue = value;
        }
    }
}
//при нажатиии на кнопку сохранения при этом будет браться вся изменённая (в основном окне) инфа и сохраняться так же в бд и файл (надо понять как проверять на то, что было изменение или нет, сейчас я знаю, что когда мы меняем инфу в DataGrid в строках, то оно сразу изменяется в списке, который привязан к этому DataGrid'у) - вроде готова часть, но ничего не проверено, может не работать
//при нажатии на кнопку удаления будет удаляться выбранная строка и будет попытка удаления из бд и файла - вроде готово, но не проверено 
//прописать и обдумать сортировку и поиск - поиск готов
//?что будет если попробовать добавить уже существующие данные?
//добавить чтобы при считывании из файла из таблиц с покупками и продажами столбец с медикаментами выгружался в список проданых и купленых медикоментов в цикле через создание новых объектов купленного и проданного медикамента
//при изменении данных, будет перехватываться событие изменения CellEditEnding, там мы будем брать изменённый объект и сохранять его в список изменений, потом этот список выгружать в бд - вроде готова часть, но не проверено