using System.Linq;
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
                MessageBox.Show("At least 1 element must be selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            foreach (var item in checkBoxes)
            {
                var medicationCount = (wrapPanel.Children[wrapPanel.Children.IndexOf(item) + 1] as TextBox).Text;
                if (string.IsNullOrEmpty(medicationCount) || !int.TryParse(medicationCount, out _))
                {
                    MessageBox.Show("Invalid value in the quantity line!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            return true;
        }
    }
}
