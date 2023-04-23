using System;
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
        public static void ToSelectedDataGrid(SelectedTable selectedTable, TabControl tabControl, DataLists dataLists = null, dynamic dataList = null)
        {
            var medicationsGrid = (((tabControl.Items[0] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
            var warehousesGrid = (((tabControl.Items[1] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
            var manufacturersGrid = (((tabControl.Items[2] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
            var salesData = (((tabControl.Items[3] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;
            var purchasesGrid = (((tabControl.Items[4] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid;

            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    if (dataLists != null)
                    {
                        if (medicationsGrid.HasItems && medicationsGrid.ItemsSource == dataLists.MedicationsData)
                            medicationsGrid.Items.Refresh();
                        else
                        {
                            if (dataLists.MedicationsData.Any())
                                medicationsGrid.ItemsSource = dataLists.MedicationsData;
                        }
                    }
                    else if (dataList != null)
                        medicationsGrid.ItemsSource = dataList;
                    break;
                case SelectedTable.Warehouses:
                    if (dataLists != null)
                    {
                        if (warehousesGrid.HasItems && warehousesGrid.ItemsSource == dataLists.WarehousesData)
                            warehousesGrid.Items.Refresh();
                        else
                        {
                            if (dataLists.WarehousesData.Any())
                                warehousesGrid.ItemsSource = dataLists.WarehousesData;
                        }
                    }
                    else if (dataList != null)
                        warehousesGrid.ItemsSource = dataList;
                    break;
                case SelectedTable.Manufacturers:
                    if (dataLists != null)
                    {
                        if (manufacturersGrid.HasItems && manufacturersGrid.ItemsSource == dataLists.ManufacturersData)
                            manufacturersGrid.Items.Refresh();
                        else
                        {
                            if (dataLists.ManufacturersData.Any())
                                manufacturersGrid.ItemsSource = dataLists.ManufacturersData;
                        }
                    }
                    else if (dataList != null)
                        manufacturersGrid.ItemsSource = dataList;
                    break;
                case SelectedTable.Sales:
                    if (dataLists != null)
                    {
                        if (salesData.HasItems && salesData.ItemsSource == dataLists.SalesData)
                            salesData.Items.Refresh();
                        else 
                        {
                            if (dataLists.SalesData.Any())
                                salesData.ItemsSource = dataLists.SalesData;
                        }
                    }
                    else if (dataList != null)
                        salesData.ItemsSource = dataList;
                    break;
                case SelectedTable.Purchases:
                    if (dataLists != null)
                    {
                        if (purchasesGrid.HasItems && purchasesGrid.ItemsSource == dataLists.PurchasesData)
                            purchasesGrid.Items.Refresh();
                        else
                        {
                            if (dataLists.PurchasesData.Any())
                                purchasesGrid.ItemsSource = dataLists.PurchasesData;
                        }
                    }
                    else if (dataList != null)
                        purchasesGrid.ItemsSource = dataList;
                    break;
                case SelectedTable.All:
                    ToSelectedDataGrid(SelectedTable.Medications, tabControl, dataLists);
                    ToSelectedDataGrid(SelectedTable.Warehouses, tabControl, dataLists);
                    ToSelectedDataGrid(SelectedTable.Manufacturers, tabControl, dataLists);
                    ToSelectedDataGrid(SelectedTable.Sales, tabControl, dataLists);
                    ToSelectedDataGrid(SelectedTable.Purchases, tabControl, dataLists);
                    break;
            }
        }
    }
}
