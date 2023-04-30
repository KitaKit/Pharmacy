using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy
{
    public static class DataGridTables
    {
        private static TabControl _mainTabControl = (Application.Current.MainWindow as MainWindow).mainTabControl;
        private static DataGrid _medicationsGrid = (((_mainTabControl.Items[0] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
        private static DataGrid _warehousesGrid = (((_mainTabControl.Items[1] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
        private static DataGrid _manufacturersGrid = (((_mainTabControl.Items[2] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
        private static DataGrid _salesGrid = (((_mainTabControl.Items[3] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
        private static DataGrid _purchasesGrid = (((_mainTabControl.Items[4] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;

        public static SelectedTable GetSelectedTable()
        {
            return (SelectedTable)_mainTabControl.SelectedIndex;
        }

        public static void ShowDataToTable(dynamic data, SelectedTable table = SelectedTable.None)
        {
            if (data == null || (data is IList && (data as IList).Count == 0))
            {
                MessageBox.Show("No data to Show!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SelectedTable selectedTable;

            if (table == SelectedTable.None)
                selectedTable = GetSelectedTable();
            else
                selectedTable = table;

            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    if (data is DataLists)
                    {
                        if (_medicationsGrid.HasItems && _medicationsGrid.ItemsSource == data.MedicationsData)
                        {
                            _medicationsGrid.Items.SortDescriptions.Clear();
                            _medicationsGrid.Items.Refresh();
                        }
                        else
                            if ((data as DataLists).MedicationsData.Any())
                                _medicationsGrid.ItemsSource = data.MedicationsData;
                    }
                    else
                        _medicationsGrid.ItemsSource = data;
                    break;
                case SelectedTable.Warehouses:
                    if (data is DataLists)
                    {
                        if (_warehousesGrid.HasItems && _warehousesGrid.ItemsSource == data.WarehousesData)
                        {
                            _warehousesGrid.Items.SortDescriptions.Clear();
                            _warehousesGrid.Items.Refresh();
                        }
                        else
                            if ((data as DataLists).WarehousesData.Any())
                                _warehousesGrid.ItemsSource = data.WarehousesData;
                    }
                    else
                        _warehousesGrid.ItemsSource = data;
                    break;
                case SelectedTable.Manufacturers:
                    if (data is DataLists)
                    {
                        if (_manufacturersGrid.HasItems && _manufacturersGrid.ItemsSource == data.ManufacturersData)
                        { 
                            _manufacturersGrid.Items.SortDescriptions.Clear();
                            _manufacturersGrid.Items.Refresh();
                        }
                        else
                        {
                            if ((data as DataLists).ManufacturersData.Any())
                                _manufacturersGrid.ItemsSource = data.ManufacturersData;
                        }
                    }
                    else if (data != null)
                        _manufacturersGrid.ItemsSource = data;
                    break;
                case SelectedTable.Sales:
                    if (data is DataLists)
                    {
                        if (_salesGrid.HasItems && _salesGrid.ItemsSource == data.SalesData)
                        {
                            _salesGrid.Items.SortDescriptions.Clear();
                            _salesGrid.Items.Refresh();
                        }
                        else
                        {
                            if ((data as DataLists).SalesData.Any())
                                _salesGrid.ItemsSource = data.SalesData;
                        }
                    }
                    else if (data != null)
                        _salesGrid.ItemsSource = data;
                    break;
                case SelectedTable.Purchases:
                    if (data is DataLists)
                    {
                        if (_purchasesGrid.HasItems && _purchasesGrid.ItemsSource == data.PurchasesData)
                        {
                            _purchasesGrid.Items.SortDescriptions.Clear();
                            _purchasesGrid.Items.Refresh();
                        }
                        else
                        {
                            if ((data as DataLists).PurchasesData.Any())
                                _purchasesGrid.ItemsSource = data.PurchasesData;
                        }
                    }
                    else if (data != null)
                        _purchasesGrid.ItemsSource = data;
                    break;
                case SelectedTable.All:
                    ShowDataToTable(data, SelectedTable.Medications);
                    ShowDataToTable(data, SelectedTable.Warehouses);
                    ShowDataToTable(data, SelectedTable.Manufacturers);
                    ShowDataToTable(data, SelectedTable.Sales);
                    ShowDataToTable(data, SelectedTable.Purchases);
                    break;
            }
        }
    }
}
