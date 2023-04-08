using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy
{
    public static class WrapPanelCustomValidation
    {
        public static bool IsValid(WrapPanel wrapPanel)
        {
            var checkBoxes = wrapPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
            if (!checkBoxes.Any())
            {
                MessageBox.Show("");
                return false;
            }

            foreach (var item in checkBoxes)
            {
                var medicationCount = (wrapPanel.Children[wrapPanel.Children.IndexOf(item) + 1] as TextBox).Text;
                if (string.IsNullOrEmpty(medicationCount) || !int.TryParse(medicationCount, out _))
                {
                    MessageBox.Show("");
                    return false;
                }
            }
            return true;
        }
    }
}
