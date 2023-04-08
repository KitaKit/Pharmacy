using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pharmacy.Additional_windows
{
    /// <summary>
    /// Interaction logic for AddManufacturerWindow.xaml
    /// </summary>
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
