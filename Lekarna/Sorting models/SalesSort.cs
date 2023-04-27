using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Pharmacy.Sorting_models
{
    public static class SalesSort
    {
        public static void SetParameters(List<MedicationModel> medications, WrapPanel sortPanel)
        {
            foreach (var row in medications)
            {
                CheckBox box = new CheckBox();
                box.Content = row.Title;
                box.Margin = new Thickness(1, 1, 1, 1);
                sortPanel.Children.Add(box);
            }
        }

        public static void Sort(Button buttonPrice, TextBox price, WrapPanel sortPanel, List<SaleModel> sales, TabControl tabControl)
        {
            if (Validation.GetHasError(price))
                return;
            var checkBoxes = sortPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
            List<SaleModel> checkedSales = sales;
            if (!string.IsNullOrEmpty(price.Text) && int.Parse(price.Text) != 0)
            {
                if (buttonPrice.Content.ToString() == "Price from")
                    checkedSales = checkedSales.Where(x => x.Price > int.Parse(price.Text)).ToList();
                else
                    checkedSales = checkedSales.Where(x => x.Price < int.Parse(price.Text)).ToList();
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
            DataShow.ToSelectedDataGrid(SelectedTable.Sales, tabControl, checkedSales);
        }
    }
}
