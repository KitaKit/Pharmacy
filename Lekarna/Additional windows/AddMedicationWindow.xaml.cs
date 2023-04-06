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
        public AddMedicationWindow()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //в начале будет проверка на валидность данных (в чате с ботом есть как примерно)

            //MedicationModel newMedication = new MedicationModel
            //    (
            //    titleTextBox.Text, availabilityCheckBox.IsChecked.GetValueOrDefault(), int.Parse(countTextBox.Text), descriptionTextBox.Text, prescriptionCheckBox.IsChecked.GetValueOrDefault(), expirationDateDatePicker.SelectedDate.Value, decimal.Parse(priceTextBox.Text)
            //    ) ;
            
            MedicationModel newMedication = new MedicationModel();
            newMedication.Title = titleTextBox.Text;
            newMedication.Category = categoryComboBox.SelectedValue.ToString();
            newMedication.Form = formComboBox.SelectedValue.ToString();
            newMedication.Availability = (bool)availabilityCheckBox.IsChecked;
            newMedication.Count = int.Parse(countTextBox.Text);
            newMedication.Warehouse = warehouseComboBox.SelectedValue.ToString();
            newMedication.Prescription = (bool)prescriptionCheckBox.IsChecked;
            newMedication.ExpirationDate = (DateTime)expirationDateDatePicker.SelectedDate;
            newMedication.Price = decimal.Parse(priceTextBox.Text);
            newMedication.Manufacturer = manufacturerComboBox.SelectedValue.ToString();
            newMedication.Description = descriptionTextBox.Text;
            DataSave.SaveNewData(newMedication, SelectedTable.Medications);
        }

        private void addMedicationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            categoryComboBox.ItemsSource = DataLists.CategoriesData.Select(x => x.Name);
            formComboBox.ItemsSource = DataLists.MedicationFormsData.Select(x=>x.Form);
            warehouseComboBox.ItemsSource = DataLists.WarehousesData.Select(x=>x.Name);
            manufacturerComboBox.ItemsSource = DataLists.ManufacturersData.Select(x => x.Name);
        }
    }
}
