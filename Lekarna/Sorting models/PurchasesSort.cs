using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Sorting_models
{
    public static class PurchasesSort
    {
        public static void SetParameters(DataLists dataLists, WrapPanel sortPanel, ComboBox providers)
        {
            providers.ItemsSource = dataLists.ProvidersData.Select(x => x.Name);

            foreach (var row in dataLists.MedicationsData)
            {
                CheckBox box = new CheckBox();
                box.Content = row.Title;
                box.Margin = new Thickness(1, 1, 1, 1);
                sortPanel.Children.Add(box);
            }
        }
        public static void Sort(List<PurchaseModel> purchases, WrapPanel sortPanel, ComboBox providers, Button buttonCost, TextBox cost, TabControl tabControl)
        {
            

            if (Validation.GetHasError(cost))
                return;
            List<PurchaseModel> checkedPurchases = purchases;
            var checkBoxes = sortPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);

            if (!string.IsNullOrEmpty(cost.Text) && int.Parse(cost.Text) != 0)
            {
                if (buttonCost.Content.ToString() == "Cost from")
                    checkedPurchases = checkedPurchases.Where(x => x.Cost > int.Parse(cost.Text)).ToList();
                else
                    checkedPurchases = checkedPurchases.Where(x => x.Cost < int.Parse(cost.Text)).ToList();
            }

            if (providers.SelectedIndex != -1)
                checkedPurchases = checkedPurchases.Where(x => x.Provider == providers.SelectedValue.ToString()).ToList();

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
            DataShow.ToSelectedDataGrid(SelectedTable.Purchases, tabControl, checkedPurchases);
        }
    }

}
