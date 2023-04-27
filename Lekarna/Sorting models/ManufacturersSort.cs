using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Sorting_models
{
    public static class ManufacturersSort
    {
        public static void SetParameters(ComboBox country, WrapPanel sortPanel, DataLists dataLists)
        {
            country.ItemsSource = dataLists.ManufacturersData.Select(x => x.Country).Distinct();

            foreach (var row in dataLists.MedicationsData)
            {
                CheckBox box = new CheckBox();
                box.Content = row.Title;
                box.Margin = new Thickness(1, 1, 1, 1);
                sortPanel.Children.Add(box);
            }
        }
        public static void Sort(WrapPanel sortPanel, ComboBox country, List<ManufacturerModel> manufacturers, TabControl tabControl)
        {
            List<ManufacturerModel> checkedManufacturers = manufacturers;
            var checkBoxes = sortPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
            if (country.SelectedIndex != -1)
                checkedManufacturers = manufacturers.Where(x => x.Country == country.SelectedValue.ToString()).ToList();

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
            DataShow.ToSelectedDataGrid(SelectedTable.Manufacturers, tabControl, checkedManufacturers);
        }
    }
}
