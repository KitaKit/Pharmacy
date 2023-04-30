using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy
{
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
           
            var count = int.Parse(countTextBox.Text);
            if (count > 0)
                prescriptionCheckBox.IsChecked = true;
            else if (count <= 0)
            {
                prescriptionCheckBox.IsChecked = false;
                countTextBox.Text = "0";
            }

            MedicationModel newMedication = new MedicationModel
                (
                _dataLists.MedicationsData.Max(x => x.Id) + 1, titleTextBox.Text, (bool)availabilityCheckBox.IsChecked, count, descriptionTextBox.Text, (bool)prescriptionCheckBox.IsChecked, (DateTime)expirationDateDatePicker.SelectedDate, decimal.Parse(priceTextBox.Text), warehouseComboBox.SelectedValue.ToString(), formComboBox.SelectedValue.ToString(), manufacturerComboBox.SelectedValue.ToString(), categoryComboBox.SelectedValue.ToString()
                );

            var refMedications = _dataLists.WarehousesData.Find(x => x.Name == newMedication.Warehouse).Medications;
            if (string.IsNullOrEmpty(refMedications))
                refMedications = newMedication.Title;
            else
                refMedications += (", " + newMedication.Title);

            refMedications = _dataLists.ManufacturersData.Find(x => x.Name == newMedication.Manufacturer).Medications;
            if (string.IsNullOrEmpty(refMedications))
                refMedications = newMedication.Title;
            else
                refMedications += (", " + newMedication.Title);

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
