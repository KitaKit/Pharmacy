using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;

namespace Pharmacy.Sorting_models
{
    public static class SalesSort
    {
        private static MainWindow _mainWindow = Application.Current.MainWindow as MainWindow;
        private static WrapPanel _sortPanel = _mainWindow.wrapPanelSortSales;
        private static Button _bPrice = _mainWindow.buttonSalesSortPrice;
        private static TextBox _price = _mainWindow.textBoxSalesSortPrice;
        private static DataGrid _dataGrid = _mainWindow.dataGridSales;
        private static bool _wasSorting = false;
        public static void SetParameters(List<MedicationModel> medications)
        {
            foreach (var row in medications)
            {
                CheckBox box = new CheckBox();
                box.Content = row.Title;
                box.Margin = new Thickness(1, 1, 1, 1);
                _sortPanel.Children.Add(box);
            }
        }

        public static void Sort(List<SaleModel> sales)
        {
            if (Validation.GetHasError(_price))
                return;

            if (_wasSorting)
                _dataGrid.ItemsSource = sales;

            var checkBoxes = _sortPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
            List<SaleModel> checkedSales = sales;

            if (!string.IsNullOrEmpty(_price.Text) && int.Parse(_price.Text) != 0)
            {
                if (_bPrice.Content.ToString() == "Price from")
                    checkedSales = checkedSales.Where(x => x.Price > int.Parse(_price.Text)).ToList();
                else
                    checkedSales = checkedSales.Where(x => x.Price < int.Parse(_price.Text)).ToList();
            }

            if (checkBoxes.Any())
            {
                List<string> selectedMedications = checkBoxes.Select(x => x.Content.ToString()).ToList();
                checkedSales = checkedSales.Where(x => selectedMedications.All(sm => x.Medications.Contains(sm))).ToList();
            }

            if (checkedSales.Count == 0)
            {
                MessageBox.Show("There is no data with such parameters!", "No data", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DataGridTables.ShowDataToTable(checkedSales);
        }
    }
}
