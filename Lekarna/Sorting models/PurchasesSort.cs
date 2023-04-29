using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Sorting_models
{
    public static class PurchasesSort
    {
        private static MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;
        private static ComboBox _providers = _mainWindow.comboBoxSortPurchasesProvider;
        private static WrapPanel _sortPanel = _mainWindow.sortPanelPurchases;
        private static Button _bCost = _mainWindow.buttonPurchasesSortCost;
        private static TextBox _cost = _mainWindow.textBoxPurchasesSortCost;
        private static DataGrid _dataGrid = _mainWindow.dataGridPurchases;
        private static bool _wasSorted = false;
        public static void SetParameters(DataLists dataLists)
        {
            _providers.ItemsSource = dataLists.ProvidersData.Select(x => x.Name);

            if (_sortPanel.Children.Count > 0)
                _sortPanel.Children.Clear();

            foreach (var row in dataLists.MedicationsData)
            {
                CheckBox box = new CheckBox();
                box.Content = row.Title;
                box.Margin = new Thickness(1, 1, 1, 1);
                _sortPanel.Children.Add(box);
            }
        }
        public static void Sort(List<PurchaseModel> purchases)
        {
            if (Validation.GetHasError(_cost))
                return;

            if (_wasSorted)
                _dataGrid.ItemsSource = purchases;

            List<PurchaseModel> checkedPurchases = purchases;
            var checkBoxes = _sortPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);

            if (!string.IsNullOrEmpty(_cost.Text) && int.Parse(_cost.Text) != 0)
            {
                if (_bCost.Content.ToString() == "Cost from")
                    checkedPurchases = checkedPurchases.Where(x => x.Cost > int.Parse(_cost.Text)).ToList();
                else
                    checkedPurchases = checkedPurchases.Where(x => x.Cost < int.Parse(_cost.Text)).ToList();
            }

            if (_providers.SelectedIndex != -1)
                checkedPurchases = checkedPurchases.Where(x => x.Provider == _providers.SelectedValue.ToString()).ToList();

            if (checkBoxes.Any())
            {
                List<string> selectedMedications = checkBoxes.Select(x => x.Content.ToString()).ToList();
                checkedPurchases = checkedPurchases.Where(x => selectedMedications.All(sm => x.Medications.Contains(sm))).ToList();
            }

            if (checkedPurchases.Count == 0)
            {
                MessageBox.Show("There is no data with such parameters!", "No data", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DataGridTables.ShowDataToTable(checkedPurchases);
        }
    }

}
