using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Additional_windows
{
    public partial class AddManufacturerWindow : Window
    {
        private DataLists _dataLists= null;
        public AddManufacturerWindow(DataLists dataLists)
        {
            InitializeComponent();
            _dataLists = dataLists;
            DataContext = new ManufacturerModel();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(licenseTextBox) || Validation.GetHasError(countryTextBox) || Validation.GetHasError(nameTextBox) || string.IsNullOrEmpty(licenseTextBox.Text) || string.IsNullOrEmpty(countryTextBox.Text) || string.IsNullOrEmpty(nameTextBox.Text))
                return;

            ManufacturerModel newManufacturer = new ManufacturerModel
                (
                _dataLists.ManufacturersData.Max(x => x.Id) + 1, nameTextBox.Text, countryTextBox.Text, licenseTextBox.Text
                );

            ChangeData.SaveNew(newManufacturer, SelectedTable.Manufacturers, _dataLists);
            Close();
        }
    }
}
