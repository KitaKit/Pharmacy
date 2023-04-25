using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Sorting_models
{
    public static class MedicationsSort
    {
        private static bool _wasSorted = false; 
        static public void SetParameters(DataLists dataLists, StackPanel sortPanel)
        {
            var sortGrid = sortPanel.Children[0] as Grid;

            (sortGrid.Children[1] as ComboBox).ItemsSource = dataLists.MedicationFormsData.Select(x => x.Form);
            (sortGrid.Children[3] as ComboBox).ItemsSource = dataLists.CategoriesData.Select(x => x.Name);
            (sortGrid.Children[9] as ComboBox).ItemsSource = dataLists.WarehousesData.Select(x => x.Name);
            (sortGrid.Children[15] as ComboBox).ItemsSource = dataLists.ManufacturersData.Select(x => x.Name);
        }

        static public void Sort(ComboBox form, ComboBox category, ComboBox availability, Button buttonCount, TextBox count, ComboBox warehouse, ComboBox prescription, Button buttonPrice, TextBox price, ComboBox manufacturer, List<MedicationModel> data, DataGrid dataGrid, TabControl tabControl)
        {
            if (Validation.GetHasError(count) || Validation.GetHasError(price))
                return;

            if (_wasSorted)
                dataGrid.ItemsSource = data;

            _wasSorted = true;
            List<MedicationModel> sortedRows = data;

            if (form.SelectedIndex != -1)
                sortedRows = sortedRows.Where(x => x.Form == form.SelectedValue.ToString()).ToList();
            if (category.SelectedIndex != -1)
                sortedRows = sortedRows.Where(x => x.Category == category.SelectedValue.ToString()).ToList();
            if (availability.SelectedIndex != -1)
            {
                if (availability.SelectedIndex == 0)
                    sortedRows = sortedRows.Where(x => x.Availability == true).ToList();
                else
                    sortedRows = sortedRows.Where(x => x.Availability == false).ToList();
            }
            if (!string.IsNullOrEmpty(count.Text) && int.Parse(count.Text) != 0)
            {
                if (buttonCount.Content.ToString() == "Count from")
                    sortedRows = sortedRows.Where(x => x.Count > int.Parse(count.Text)).ToList();
                else
                    sortedRows = sortedRows.Where(x => x.Count < int.Parse(count.Text)).ToList();
            }
            if (warehouse.SelectedIndex != -1)
                sortedRows = sortedRows.Where(x => x.Warehouse == warehouse.SelectedValue.ToString()).ToList();
            if (prescription.SelectedIndex != -1)
            {
                if (prescription.SelectedIndex == 0)
                    sortedRows = sortedRows.Where(x => x.Prescription == true).ToList();
                else
                    sortedRows = sortedRows.Where(x => x.Prescription == false).ToList();
            }
            if (!string.IsNullOrEmpty(price.Text) && int.Parse(price.Text) != 0)
            {
                if (buttonCount.Content.ToString() == "Price from")
                    sortedRows = sortedRows.Where(x => x.Price > int.Parse(price.Text)).ToList();
                else
                    sortedRows = sortedRows.Where(x => x.Price < int.Parse(price.Text)).ToList();
            }
            if (manufacturer.SelectedIndex != -1)
                sortedRows = sortedRows.Where(x => x.Manufacturer == manufacturer.SelectedValue.ToString()).ToList();

            if (sortedRows.Count == 0)
            {
                MessageBox.Show("There is no data with such parameters!", "No data", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DataShow.ToSelectedDataGrid(SelectedTable.Medications, tabControl, sortedRows);
        }
    }
}
