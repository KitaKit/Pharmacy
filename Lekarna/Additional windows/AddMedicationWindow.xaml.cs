using System;
using System.Collections;
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

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddMedicationWindow : Window
    {
        private DataLists _dataLists = null;
        public AddMedicationWindow(DataLists dataLists)
        {
            InitializeComponent();
            _dataLists = dataLists;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(titleTextBox) || string.IsNullOrEmpty(titleTextBox.Text) || categoryComboBox.SelectedItem == null || formComboBox.SelectedItem == null || Validation.GetHasError(countTextBox) || string.IsNullOrEmpty(countTextBox.Text) || warehouseComboBox.SelectedItem == null || Validation.GetHasError(expirationDateDatePicker) || expirationDateDatePicker.SelectedDate == null || Validation.GetHasError(priceTextBox) || string.IsNullOrEmpty(priceTextBox.Text) || manufacturerComboBox.SelectedItem == null || string.IsNullOrEmpty(descriptionTextBox.Text))
                return;

            MedicationModel newMedication = new MedicationModel
                (
                _dataLists.MedicationsData.Max(x => x.Id) + 1, titleTextBox.Text, (bool)availabilityCheckBox.IsChecked, int.Parse(countTextBox.Text), descriptionTextBox.Text, (bool)prescriptionCheckBox.IsChecked, (DateTime)expirationDateDatePicker.SelectedDate, decimal.Parse(priceTextBox.Text), warehouseComboBox.SelectedValue.ToString(), formComboBox.SelectedValue.ToString(), manufacturerComboBox.SelectedValue.ToString(), categoryComboBox.SelectedValue.ToString()
                );

            _dataLists.WarehousesData.Find(x => x.Name == newMedication.Warehouse).Medications += (", " + newMedication.Title);
            _dataLists.ManufacturersData.Find(x => x.Name == newMedication.Manufacturer).Medications += (", " + newMedication.Title);
            ChangeData.SaveNew(newMedication, SelectedTable.Medications, _dataLists);
            Close();
        }

        private void addMedicationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MedicationModel(null, false, 0, null, false, DateTime.Now, 0, null, null, null, null);

            categoryComboBox.ItemsSource = _dataLists.CategoriesData.Select(x => x.Name);
            formComboBox.ItemsSource = _dataLists.MedicationFormsData.Select(x=>x.Form);
            warehouseComboBox.ItemsSource = _dataLists.WarehousesData.Select(x=>x.Name);
            manufacturerComboBox.ItemsSource = _dataLists.ManufacturersData.Select(x => x.Name);
        }
    }
}
