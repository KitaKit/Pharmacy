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
                titleTextBox.Text, availabilityCheckBox.IsChecked.GetValueOrDefault(), int.Parse(countTextBox.Text), descriptionTextBox.Text, prescriptionCheckBox.IsChecked.GetValueOrDefault(), expirationDateDatePicker.SelectedDate.Value, decimal.Parse(priceTextBox.Text)
                ) ;

            DataSaveService.SaveNewData(newMedication, SelectedTable.Medications);
        }
    }
}
