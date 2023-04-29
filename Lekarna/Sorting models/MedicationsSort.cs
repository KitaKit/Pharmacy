using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Sorting_models
{
    public static class MedicationsSort
    {
        private static MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;
        private static ComboBox _forms = _mainWindow.comboBoxSortForm;
        private static ComboBox _categories = _mainWindow.comboBoxSortCategory;
        private static ComboBox _warehouses = _mainWindow.comboBoxSortWarehouse;
        private static ComboBox _manufacturers = _mainWindow.comboBoxSortManufacturer;
        private static ComboBox _availability = _mainWindow.comboBoxSortAvailability;
        private static Button _bCount = _mainWindow.buttonSortCount;
        private static TextBox _count = _mainWindow.textBoxSortCount;
        private static ComboBox _prescription = _mainWindow.comboBoxSortPrescription;
        private static Button _bPrice = _mainWindow.buttonSortPrice;
        private static TextBox _price = _mainWindow.textBoxSortPrice;
        private static DataGrid _dataGrid = _mainWindow.dataGridMedications;
        private static bool _wasSorted = false; 
        static public void SetParameters(DataLists dataLists)
        {
            _forms.ItemsSource = dataLists.MedicationFormsData.Select(x => x.Form);
            _categories.ItemsSource = dataLists.CategoriesData.Select(x => x.Name);
            _warehouses.ItemsSource = dataLists.WarehousesData.Select(x => x.Name);
            _manufacturers.ItemsSource = dataLists.ManufacturersData.Select(x => x.Name);
        }

        static public void Sort(List<MedicationModel> data)
        {
            if (Validation.GetHasError(_count) || Validation.GetHasError(_price))
                return;

            if (_wasSorted)
                _dataGrid.ItemsSource = data;

            _wasSorted = true;
            List<MedicationModel> sortedRows = data;

            if (_forms.SelectedIndex != -1)
                sortedRows = sortedRows.Where(x => x.Form == _forms.SelectedValue.ToString()).ToList();
            if (_categories.SelectedIndex != -1)
                sortedRows = sortedRows.Where(x => x.Category == _categories.SelectedValue.ToString()).ToList();
            if (_availability.SelectedIndex != -1)
            {
                if (_availability.SelectedIndex == 0)
                    sortedRows = sortedRows.Where(x => x.Availability == true).ToList();
                else
                    sortedRows = sortedRows.Where(x => x.Availability == false).ToList();
            }
            if (!string.IsNullOrEmpty(_count.Text) && int.Parse(_count.Text) != 0)
            {
                if (_bCount.Content.ToString() == "Count from")
                    sortedRows = sortedRows.Where(x => x.Count > int.Parse(_count.Text)).ToList();
                else
                    sortedRows = sortedRows.Where(x => x.Count < int.Parse(_count.Text)).ToList();
            }
            if (_warehouses.SelectedIndex != -1)
                sortedRows = sortedRows.Where(x => x.Warehouse == _warehouses.SelectedValue.ToString()).ToList();
            if (_prescription.SelectedIndex != -1)
            {
                if (_prescription.SelectedIndex == 0)
                    sortedRows = sortedRows.Where(x => x.Prescription == true).ToList();
                else
                    sortedRows = sortedRows.Where(x => x.Prescription == false).ToList();
            }
            if (!string.IsNullOrEmpty(_count.Text) && int.Parse(_count.Text) != 0)
            {
                if (_bPrice.Content.ToString() == "Price from")
                    sortedRows = sortedRows.Where(x => x.Price > int.Parse(_price.Text)).ToList();
                else
                    sortedRows = sortedRows.Where(x => x.Price < int.Parse(_price.Text)).ToList();
            }
            if (_manufacturers.SelectedIndex != -1)
                sortedRows = sortedRows.Where(x => x.Manufacturer == _manufacturers.SelectedValue.ToString()).ToList();

            if (sortedRows.Count == 0)
            {
                MessageBox.Show("There is no data with such parameters!", "No data", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DataGridTables.ShowDataToTable(sortedRows);
        }
    }
}
