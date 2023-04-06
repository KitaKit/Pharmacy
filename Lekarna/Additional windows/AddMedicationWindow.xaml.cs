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
            
            MedicationModel newMedication = new MedicationModel
                (
                DataLists.MedicationsData.Max(x => x.Id) + 1, titleTextBox.Text, (bool)availabilityCheckBox.IsChecked, int.Parse(countTextBox.Text), descriptionTextBox.Text, (bool)prescriptionCheckBox.IsChecked, (DateTime)expirationDateDatePicker.SelectedDate, decimal.Parse(priceTextBox.Text), warehouseComboBox.SelectedValue.ToString(), formComboBox.SelectedValue.ToString(), manufacturerComboBox.SelectedValue.ToString(), categoryComboBox.SelectedValue.ToString()
                );

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
