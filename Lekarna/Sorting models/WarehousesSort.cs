using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Sorting_models
{
    public static class WarehousesSort
    {
        private static MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;
        private static WrapPanel _sortPanel = _mainWindow.wrapPanelSortWarehouses;
        private static DataGrid _dataGrid = _mainWindow.dataGridWarehouses;
        private static bool _wasSorted = false;

        public static void SetParameters(List<MedicationModel> medications)
        {
            if (_sortPanel.Children.Count > 0)
                _sortPanel.Children.Clear();

            foreach (var row in medications)
            {
                CheckBox box = new CheckBox();
                box.Content = row.Title;
                box.Margin = new Thickness(1, 1, 1, 1);
                _sortPanel.Children.Add(box);
            }
        }

        public static void Sort(List<WarehouseModel> warehouses)
        {
            if (_wasSorted)
                _dataGrid.ItemsSource = warehouses;

            List<WarehouseModel> checkedWarehouses = new List<WarehouseModel>();
            var checkBoxes = _sortPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);

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

            DataGridTables.ShowDataToTable(checkedWarehouses);
        }
    }
}
