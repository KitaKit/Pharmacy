using Pharmacy.Additional_windows;
using Pharmacy.Sorting_models;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Pharmacy
{
    public enum SelectedTable
    {
        Medications,
        Warehouses,
        Manufacturers,
        Sales,
        Purchases,
        All, 
        None
    }
    public partial class MainWindow : Window
    {
        private DataLists _mainDataLists = new DataLists();
        private DataLists _changedDataLists = new DataLists();
        private DataLists _deletedDataLists = new DataLists();
        private dynamic originalGridRowValue = null;
        private bool isComboBoxDropDownOpened = false;
        private SelectedTable _selectedTable = SelectedTable.None;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void menuItemLoadFromDataBase_Click(object sender, RoutedEventArgs e)
        {
            DatabaseIOLogic database = new DatabaseIOLogic();
            database.ReadData(_mainDataLists);

            DataGridTables.ShowDataToTable(_mainDataLists, SelectedTable.All);

            MedicationsSort.SetParameters(_mainDataLists);
            WarehousesSort.SetParameters(_mainDataLists.MedicationsData);
            ManufacturersSort.SetParameters(_mainDataLists);
            SalesSort.SetParameters(_mainDataLists.MedicationsData);
            PurchasesSort.SetParameters(_mainDataLists);
        }
        private void menuItemLoadFromCSVFile_Click(object sender, RoutedEventArgs e)
        {
            _selectedTable = DataGridTables.GetSelectedTable();
            FilesIOLogic file = new FilesIOLogic(FileConnectionType.Read, _selectedTable);

            if (file.Path == null)
                return;

            MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to load data from {file.Path} to {_selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (!file.ReadData(_mainDataLists))
                    return;
                FileConnectionsList.Add(file);
            }

            DataGridTables.ShowDataToTable(_mainDataLists);

            MedicationsSort.SetParameters(_mainDataLists);
            WarehousesSort.SetParameters(_mainDataLists.MedicationsData);
            ManufacturersSort.SetParameters(_mainDataLists);
            SalesSort.SetParameters(_mainDataLists.MedicationsData);
            PurchasesSort.SetParameters(_mainDataLists);
        }
        private void menuItemSaveToNewCSVFile_Click(object sender, RoutedEventArgs e)
        {
            _selectedTable = DataGridTables.GetSelectedTable();

            FilesIOLogic file = new FilesIOLogic(FileConnectionType.Write, _selectedTable);

            if (file.Path != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to save data from {_selectedTable} to {file.Path}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    file.WriteDataToNew(_mainDataLists);
                    FileConnectionsList.Add(file);
                }
            }
        }
        private void dataGrid_GeneralBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var row = e.Row.Item;
            var column = e.Column;
            originalGridRowValue = (column.GetCellContent(row) as TextBlock).Text;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_deletedDataLists.HasItems() && !_changedDataLists.HasItems())
                return;

            if (ChangeData.SaveAll(_deletedDataLists, _changedDataLists, _mainDataLists))
            {
                DataGridTables.ShowDataToTable(_mainDataLists, SelectedTable.All);

                _changedDataLists.ClearAll();
                _deletedDataLists.ClearAll();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedTable = DataGridTables.GetSelectedTable();

            switch (_selectedTable)
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

            DataGridTables.ShowDataToTable(_mainDataLists, SelectedTable.All);

            MedicationsSort.SetParameters(_mainDataLists);
            WarehousesSort.SetParameters(_mainDataLists.MedicationsData);
            ManufacturersSort.SetParameters(_mainDataLists);
            SalesSort.SetParameters(_mainDataLists.MedicationsData);
            PurchasesSort.SetParameters(_mainDataLists);
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = ((((mainTabControl.SelectedItem as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).SelectedItem;
            if (selectedRow == null)
                return;

            MessageBoxResult messageBoxResult = MessageBoxResult.None;
            _selectedTable = DataGridTables.GetSelectedTable();

            switch (_selectedTable)
            {
                case SelectedTable.Medications:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as MedicationModel).Title} from {_selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    selectedRow = selectedRow as MedicationModel;
                    break;
                case SelectedTable.Warehouses:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as WarehouseModel).Name} from {_selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (!string.IsNullOrEmpty((selectedRow as WarehouseModel).Medications))
                    {
                        MessageBox.Show("There are medicines in this warehouse, you cannot delete a filled warehouse!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    selectedRow = selectedRow as WarehouseModel;
                    break;
                case SelectedTable.Manufacturers:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as ManufacturerModel).Name} from {_selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (!string.IsNullOrEmpty((selectedRow as ManufacturerModel).Medications))
                    {
                        MessageBox.Show("We buy medicines from this manufacturer, you can't delete the manufacturer we work with!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    selectedRow = selectedRow as ManufacturerModel;
                    break;
                case SelectedTable.Sales:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as SaleModel).Date} from {_selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    selectedRow = selectedRow as SaleModel;
                    break;
                case SelectedTable.Purchases:
                    messageBoxResult = MessageBox.Show($"Do you want to delete information about {(selectedRow as PurchaseModel).DeliveryDate} from {_selectedTable}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
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

            DataGridTables.ShowDataToTable(_mainDataLists, SelectedTable.All);

            MedicationsSort.SetParameters(_mainDataLists);
            WarehousesSort.SetParameters(_mainDataLists.MedicationsData);
            ManufacturersSort.SetParameters(_mainDataLists);
            SalesSort.SetParameters(_mainDataLists.MedicationsData);
            PurchasesSort.SetParameters(_mainDataLists);
        }

        private void DataGridScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - (e.Delta / 5));
            e.Handled = true;
        }

        private void menuItemSort_Click(object sender, RoutedEventArgs e)
        {
            _selectedTable = DataGridTables.GetSelectedTable();

            switch (_selectedTable)
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
                        MenuItemAnimations.Visible(stackPanelSortWarehouses, HeightProperty, 180);
                    break;
                case SelectedTable.Manufacturers:
                    if (stackPanelSortManufacturers.Visibility == Visibility.Visible)
                        MenuItemAnimations.Invisible(stackPanelSortManufacturers, HeightProperty);
                    else
                        MenuItemAnimations.Visible(stackPanelSortManufacturers, HeightProperty, 200);
                    break;
                case SelectedTable.Sales:
                    if (stackPanelSortSales.Visibility == Visibility.Visible)
                        MenuItemAnimations.Invisible(stackPanelSortSales, HeightProperty);
                    else
                        MenuItemAnimations.Visible(stackPanelSortSales, HeightProperty, 190);
                    break;
                case SelectedTable.Purchases:
                    if (stackPanelSortPurchases.Visibility == Visibility.Visible)
                        MenuItemAnimations.Invisible(stackPanelSortPurchases, HeightProperty);
                    else
                        MenuItemAnimations.Visible(stackPanelSortPurchases, HeightProperty, 220);
                    break;
            }
        }

//----------------------------------------------------------------------------------------------------------------------------------------------//
        private void buttonClearSearchText_Click(object sender, RoutedEventArgs e)
        {
            textBoxSearch.Text = string.Empty;
        }
        private void menuItemSearch_Click(object sender, RoutedEventArgs e)
        {
            if (stackPanelSearch.Visibility == Visibility.Visible)
                MenuItemAnimations.Invisible(stackPanelSearch, HeightProperty);
            else
                MenuItemAnimations.Visible(stackPanelSearch, HeightProperty, 50);

            _selectedTable = DataGridTables.GetSelectedTable();
        }
        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchedWord = textBoxSearch.Text.ToLower();
            var searchedRows = SearchModel.GetSearchedRows(searchedWord, _mainDataLists);

            if (searchedRows != null)
                DataGridTables.ShowDataToTable(searchedRows);
        }
//---------------------------------------------------------------------------------------------------------------------------------------------------//
        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            isComboBoxDropDownOpened = true;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            isComboBoxDropDownOpened = false;
        }
        private void PharmacyMainWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var sourceElement = e.OriginalSource as FrameworkElement;

            if (isComboBoxDropDownOpened)
                return;

            if (stackPanelMedicationsSort.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelMedicationsSort))
                MenuItemAnimations.Invisible(stackPanelMedicationsSort, HeightProperty);
            if (stackPanelSearch.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSearch))
                MenuItemAnimations.Invisible(stackPanelSearch, HeightProperty);
            if (stackPanelSortWarehouses.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSortWarehouses))
                MenuItemAnimations.Invisible(stackPanelSortWarehouses, HeightProperty);
            if (stackPanelSortManufacturers.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSortManufacturers))
                MenuItemAnimations.Invisible(stackPanelSortManufacturers, HeightProperty);
            if (stackPanelSortSales.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSortSales))
                MenuItemAnimations.Invisible(stackPanelSortSales, HeightProperty);
            if (stackPanelSortPurchases.Visibility == Visibility.Visible && !IsDescendantOfStackPanel(sourceElement, stackPanelSortPurchases))
                MenuItemAnimations.Invisible(stackPanelSortPurchases, HeightProperty);
        }
        private bool IsDescendantOfStackPanel(DependencyObject element, StackPanel stackPanel)
        {
            if (element == null)
                return false;

            if (element == stackPanel)
                return true;

            return IsDescendantOfStackPanel(VisualTreeHelper.GetParent(element), stackPanel);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------//
        private void buttonSortApply_Click(object sender, RoutedEventArgs e)
        {
            _selectedTable = DataGridTables.GetSelectedTable();

            switch(_selectedTable)
            {
                case SelectedTable.Medications:
                    MedicationsSort.Sort(_mainDataLists.MedicationsData);
                    break;
                case SelectedTable.Warehouses:
                    WarehousesSort.Sort(_mainDataLists.WarehousesData);
                    break;
                case SelectedTable.Manufacturers:
                    ManufacturersSort.Sort(_mainDataLists.ManufacturersData);
                    break;
                case SelectedTable.Sales:
                    SalesSort.Sort(_mainDataLists.SalesData);
                    break;
                case SelectedTable.Purchases:
                    PurchasesSort.Sort(_mainDataLists.PurchasesData);
                    break;
            }
        }

//--------------------------------------------------------------------------------------------------------------------------------//
        private void buttonSortManufacturersClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxSortManufacturersCountry.SelectedIndex = -1;
            foreach (var box in sortPanelManufacturers.Children.OfType<CheckBox>())
            {
                box.IsChecked = false;
            }
            DataGridTables.ShowDataToTable(_mainDataLists.ManufacturersData, SelectedTable.Manufacturers);
        }

        private void buttonSortWarehousesClear_Click(object sender, RoutedEventArgs e)
        {
            foreach (var box in sortPanelWarehouses.Children.OfType<CheckBox>())
            {
                box.IsChecked = false;
            }
            DataGridTables.ShowDataToTable(_mainDataLists.WarehousesData, SelectedTable.Warehouses);
        }

        private void buttonSortPurchasesClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxPurchasesSortCost.Text = Convert.ToString(0);
            comboBoxSortPurchasesProvider.SelectedIndex = -1;
            foreach (var box in sortPanelPurchases.Children.OfType<CheckBox>())
            {
                box.IsChecked = false;
            }
            DataGridTables.ShowDataToTable(_mainDataLists.PurchasesData, SelectedTable.Purchases);
        }

        private void buttonSortSalesClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxSalesSortPrice.Text = Convert.ToString(0);
            foreach (var box in sortPanelSales.Children.OfType<CheckBox>())
            {
                box.IsChecked = false;
            }
            DataGridTables.ShowDataToTable(_mainDataLists.SalesData, SelectedTable.Sales);
        }
        private void buttonSortMedicationsClear_Click(object sender, RoutedEventArgs e)
        {
            comboBoxSortForm.SelectedIndex = -1;
            comboBoxSortCategory.SelectedIndex = -1;
            comboBoxSortAvailability.SelectedIndex = -1;
            textBoxSortCount.Text = Convert.ToString(0);
            comboBoxSortWarehouse.SelectedIndex = -1;
            comboBoxSortPrescription.SelectedIndex = -1;
            textBoxSortPrice.Text = Convert.ToString(0);
            comboBoxSortManufacturer.SelectedIndex = -1;

            DataGridTables.ShowDataToTable(_mainDataLists.MedicationsData, SelectedTable.Medications);
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
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
        private void buttonPurchasesSortCost_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            button.Content = button.Content.ToString() == "Cost from" ? "Cost up to" : "Cost from";
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------//
        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (Validation.GetHasError(e.EditingElement))
                return;

            var newVal = (e.EditingElement as TextBox).Text;

            if (originalGridRowValue == newVal)
                return;

            _selectedTable = DataGridTables.GetSelectedTable();

            switch (_selectedTable)
            {
                case SelectedTable.Medications:

                    if (e.Column.DisplayIndex == 1)
                    {
                        DataLists.ReplaceInStringInModel(originalGridRowValue as string, newVal, x => x.Medications.Contains(originalGridRowValue), _mainDataLists.WarehousesData);
                        DataLists.ReplaceInStringInModel(originalGridRowValue as string, newVal, x => x.Medications.Contains(originalGridRowValue), _mainDataLists.ManufacturersData);
                        DataLists.ReplaceInStringInModel(originalGridRowValue as string, newVal, x => x.Medications.Contains(originalGridRowValue), _mainDataLists.SalesData);
                        DataLists.ReplaceInStringInModel(originalGridRowValue as string, newVal, x => x.Medications.Contains(originalGridRowValue), _mainDataLists.PurchasesData);
                    }
                    if (e.Column.DisplayIndex == 2)
                        if (_mainDataLists.CategoriesData.FirstOrDefault(x => x.Name == (e.EditingElement as TextBox).Text) == null)
                        {
                            MessageBox.Show("Wrong category!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            newVal = originalGridRowValue;
                            return;
                        }
                    if (e.Column.DisplayIndex == 3)
                        if (_mainDataLists.MedicationFormsData.FirstOrDefault(x => x.Form == (e.EditingElement as TextBox).Text) == null)
                        {
                            MessageBox.Show("Wrong form!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            newVal = originalGridRowValue;
                            return;
                        }
                    if (e.Column.DisplayIndex == 6)
                        if (_mainDataLists.WarehousesData.FirstOrDefault(x => x.Name == (e.EditingElement as TextBox).Text) == null)
                        {
                            MessageBox.Show("Wrong warehouse!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            newVal = originalGridRowValue;
                            return;
                        }
                    if (e.Column.DisplayIndex == 10)
                        if (_mainDataLists.ManufacturersData.FirstOrDefault(x => x.Name == (e.EditingElement as TextBox).Text) == null)
                        {
                            MessageBox.Show("Wrong manufacturer!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            newVal = originalGridRowValue;
                            return;
                        }
                    _changedDataLists.Add(e.Row.Item as MedicationModel);
                    break;

                case SelectedTable.Warehouses:

                    if (e.Column.DisplayIndex == 1)
                        DataLists.ReplaceInStringInModel(originalGridRowValue as string, newVal, x => x.Warehouse.Contains(originalGridRowValue), _mainDataLists.MedicationsData);
                    _changedDataLists.Add(e.Row.Item as WarehouseModel);
                    break;

                case SelectedTable.Manufacturers:

                    if (e.Column.DisplayIndex == 1)
                        DataLists.ReplaceInStringInModel(originalGridRowValue as string, newVal, x => x.Manufacturer.Contains(originalGridRowValue), _mainDataLists.MedicationsData);
                    _changedDataLists.Add(e.Row.Item as ManufacturerModel);
                    break;

                case SelectedTable.Sales:
                    _changedDataLists.Add(e.Row.Item as SaleModel);
                    break;

                case SelectedTable.Purchases:
                    _changedDataLists.Add(e.Row.Item as PurchaseModel);
                    break;
            }
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to exit?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (_changedDataLists.HasItems() || _deletedDataLists.HasItems())
                {
                    messageBoxResult = MessageBox.Show("Do you want to save all changes?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                        ChangeData.SaveAll(_deletedDataLists, _changedDataLists, _mainDataLists);
                }
                Application.Current.Shutdown();
            }
        }
    }
}