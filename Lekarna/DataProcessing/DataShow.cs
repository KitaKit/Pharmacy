using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy
{
    public static class DataShow
    {
        public static void ToSelectedDataGrid(SelectedTable selectedTable, TabControl tabControl, DataLists dataLists)
        {
            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    if (((((tabControl.Items[0] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).HasItems)
                        ((((tabControl.Items[0] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    else
                        if(dataLists.MedicationsData.Any())
                            ((((tabControl.Items[0] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = dataLists.MedicationsData;
                    break;
                case SelectedTable.Warehouses:
                    if (((((tabControl.Items[1] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).HasItems)
                        ((((tabControl.Items[1] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    else
                        if(dataLists.WarehousesData.Any())
                            ((((tabControl.Items[1] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = dataLists.WarehousesData;
                    break;
                case SelectedTable.Manufacturers:
                    if(((((tabControl.Items[2] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).HasItems)
                        ((((tabControl.Items[2] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    else
                        if (dataLists.ManufacturersData.Any())
                            ((((tabControl.Items[2] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = dataLists.ManufacturersData;
                    break;
                case SelectedTable.Sales:
                    if (((((tabControl.Items[3] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).HasItems)
                        ((((tabControl.Items[3] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    else
                        if (dataLists.SalesData.Any())
                            ((((tabControl.Items[3] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = dataLists.SalesData;
                    break;
                case SelectedTable.Purchases:
                    if (((((tabControl.Items[4] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).HasItems)
                        ((((tabControl.Items[4] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    else
                        if(dataLists.PurchasesData.Any())
                            ((((tabControl.Items[4] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = dataLists.PurchasesData;
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
