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
        public static void ToSelectedDataGrid(SelectedTable selectedTable, TabControl tabControl)
        {
            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    ((((tabControl.Items[0] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = DataLists.MedicationsData;
                    break;
                case SelectedTable.Warehouses:
                    ((((tabControl.Items[1] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = DataLists.WarehousesData;
                    break;
                case SelectedTable.Manufacturers:
                    ((((tabControl.Items[2] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = DataLists.ManufacturersData;
                    break;
                case SelectedTable.Sales:
                    ((((tabControl.Items[3] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = DataLists.SalesData;
                    break;
                case SelectedTable.Purchases:
                    ((((tabControl.Items[4] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).ItemsSource = DataLists.PurchasesData;
                    break;
                case SelectedTable.All:
                    ToSelectedDataGrid(SelectedTable.Medications, tabControl);
                    ToSelectedDataGrid(SelectedTable.Warehouses, tabControl);
                    ToSelectedDataGrid(SelectedTable.Manufacturers, tabControl);
                    ToSelectedDataGrid(SelectedTable.Sales, tabControl);
                    ToSelectedDataGrid(SelectedTable.Purchases, tabControl);
                    break;
            }
            
        }
        public static void Refresh(SelectedTable selectedTable, TabControl tabControl)
        {
            switch (selectedTable)
            {
                case SelectedTable.Medications:
                    ((((tabControl.Items[0] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    break;
                case SelectedTable.Warehouses:
                    ((((tabControl.Items[1] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    break;
                case SelectedTable.Manufacturers:
                    ((((tabControl.Items[2] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    break;
                case SelectedTable.Sales:
                    ((((tabControl.Items[3] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    break;
                case SelectedTable.Purchases:
                    ((((tabControl.Items[4] as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).Items.Refresh();
                    break;
                case SelectedTable.All:
                    Refresh(SelectedTable.Medications, tabControl);
                    Refresh(SelectedTable.Warehouses, tabControl);
                    Refresh(SelectedTable.Manufacturers, tabControl);
                    Refresh(SelectedTable.Sales, tabControl);
                    Refresh(SelectedTable.Purchases, tabControl);
                    break;
            }
        }
        public static bool HasData(TabControl tabControl)
        {
            foreach(var item in tabControl.Items)
            {
                if (((((item as TabItem).Content as Grid).Children[0] as ScrollViewer).Content as DataGrid).HasItems)
                    return true;
            }
            return false;
        }
    }
}
