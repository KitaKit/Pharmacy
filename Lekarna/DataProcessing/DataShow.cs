using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy
{
    public static class DataShow
    {
        public static void ToSelectedDataGrid(SelectedTable selectedTable, TabControl tabControl, dynamic data)
        {
            if (data == null || (data is IList && (data as IList).Count == 0))
                return;

            var medicationsGrid = (((tabControl.Items[0] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
            var warehousesGrid = (((tabControl.Items[1] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
            var manufacturersGrid = (((tabControl.Items[2] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
            var salesData = (((tabControl.Items[3] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
            var purchasesGrid = (((tabControl.Items[4] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;

            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    if (data is DataLists)
                    {
                        if (medicationsGrid.HasItems && medicationsGrid.ItemsSource == data.MedicationsData)
                            medicationsGrid.Items.Refresh();
                        else
                            if ((data as DataLists).MedicationsData.Any())
                                medicationsGrid.ItemsSource = data.MedicationsData;
                    }
                    else
                        medicationsGrid.ItemsSource = data;
                    break;
                case SelectedTable.Warehouses:
                    if (data is DataLists)
                    {
                        if (warehousesGrid.HasItems && warehousesGrid.ItemsSource == data.WarehousesData)
                            warehousesGrid.Items.Refresh();
                        else
                            if ((data as DataLists).WarehousesData.Any())
                                warehousesGrid.ItemsSource = data.WarehousesData;
                    }
                    else
                        warehousesGrid.ItemsSource = data;
                    break;
                case SelectedTable.Manufacturers:
                    if (data is DataLists)
                    {
                        if (manufacturersGrid.HasItems && manufacturersGrid.ItemsSource == data.ManufacturersData)
                            manufacturersGrid.Items.Refresh();
                        else
                        {
                            if ((data as DataLists).ManufacturersData.Any())
                                manufacturersGrid.ItemsSource = data.ManufacturersData;
                        }
                    }
                    else if (data != null)
                        manufacturersGrid.ItemsSource = data;
                    break;
                case SelectedTable.Sales:
                    if (data is DataLists)
                    {
                        if (salesData.HasItems && salesData.ItemsSource == data.SalesData)
                            salesData.Items.Refresh();
                        else 
                        {
                            if ((data as DataLists).SalesData.Any())
                                salesData.ItemsSource = data.SalesData;
                        }
                    }
                    else if (data != null)
                        salesData.ItemsSource = data;
                    break;
                case SelectedTable.Purchases:
                    if (data is DataLists)
                    {
                        if (purchasesGrid.HasItems && purchasesGrid.ItemsSource == data.PurchasesData)
                            purchasesGrid.Items.Refresh();
                        else
                        {
                            if ((data as DataLists).PurchasesData.Any())
                                purchasesGrid.ItemsSource = data.PurchasesData;
                        }
                    }
                    else if (data != null)
                        purchasesGrid.ItemsSource = data;
                    break;
                case SelectedTable.All:
                    ToSelectedDataGrid(SelectedTable.Medications, tabControl, data);
                    ToSelectedDataGrid(SelectedTable.Warehouses, tabControl, data);
                    ToSelectedDataGrid(SelectedTable.Manufacturers, tabControl, data);
                    ToSelectedDataGrid(SelectedTable.Sales, tabControl, data);
                    ToSelectedDataGrid(SelectedTable.Purchases, tabControl, data);
                    break;
            }
        }
    }
}
