using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Sorting_models
{
    public static class ManufacturersSort
    {
        private static MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;
        private static WrapPanel _sortPanel = _mainWindow.wrapPanelSortManufacturers;
        private static ComboBox _country = _mainWindow.comboBoxSortManufacturersCountry;
        private static DataGrid _dataGrid = _mainWindow.dataGridManufacturers;
        private static bool _wasSorted = false;
        public static void SetParameters(DataLists dataLists)
        {
            _country.ItemsSource = dataLists.ManufacturersData.Select(x => x.Country).Distinct();

            foreach (var row in dataLists.MedicationsData)
            {
                CheckBox box = new CheckBox();
                box.Content = row.Title;
                box.Margin = new Thickness(1, 1, 1, 1);
                _sortPanel.Children.Add(box);
            }
        }
        public static void Sort(List<ManufacturerModel> manufacturers)
        {
            if (_wasSorted)
                _dataGrid.ItemsSource = manufacturers;

            _wasSorted = true;
            List<ManufacturerModel> checkedManufacturers = manufacturers;
            var checkBoxes = _sortPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);

            if (_country.SelectedIndex != -1)
                checkedManufacturers = manufacturers.Where(x => x.Country == _country.SelectedValue.ToString()).ToList();

            if (checkBoxes.Any())
            {
                List<string> selectedMedications = checkBoxes.Select(x => x.Content.ToString()).ToList();
                checkedManufacturers = checkedManufacturers.Where(x => selectedMedications.All(sm => x.Medications.Contains(sm))).ToList();
            }

            if (checkedManufacturers.Count == 0)
            {
                MessageBox.Show("There is no data with such parameters!", "No data", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DataGridTables.ShowDataToTable(checkedManufacturers);
        }
    }
}
