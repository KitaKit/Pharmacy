using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Sorting_models
{
    public static class WarehousesSort
    {
        public static void SetParameters(List<MedicationModel> medications, WrapPanel sortPanel)
        {
            foreach(var row in medications)
            {
                CheckBox box = new CheckBox();
                box.Content = row.Title;
                box.Margin = new Thickness(1, 1, 1, 1);
                sortPanel.Children.Add(box);
            }
        }

        public static void Sort(WrapPanel sortPanel, List<WarehouseModel> warehouses, TabControl tabControl)
        {
            List<WarehouseModel> checkedWarehouses = new List<WarehouseModel>();
            var checkBoxes = sortPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
            foreach (var medication in checkBoxes)
            {
                var medicationTitle = medication.Content.ToString();
                checkedWarehouses.AddRange(warehouses.Where(x => x.Medications.Contains(medicationTitle)).Except(checkedWarehouses));
            }
            if(checkedWarehouses.Count == 0)
            {
                MessageBox.Show("There is no data with such parameters!", "No data", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DataShow.ToSelectedDataGrid(SelectedTable.Warehouses, tabControl, checkedWarehouses);
        }
    }
}
