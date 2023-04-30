using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Additional_windows
{
    public partial class AddSaleWindow : Window
    {
        private DataLists _dataLists = null;
        private DataLists _changedData = null;
        private DataLists _deletedData = null;
        public AddSaleWindow(DataLists dataLists, DataLists changedData, DataLists deletedData)
        {
            InitializeComponent();
            _dataLists = dataLists;
            _changedData = changedData;
            _deletedData = deletedData;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!WrapPanelCustomValidation.IsValid(medicationsWrapPanel, _dataLists.MedicationsData) || dateDatePicker.SelectedDate == null || Validation.GetHasError(dateDatePicker))
                return;

            DatabaseIOLogic db = new DatabaseIOLogic();
            int nextId = db.GetLastId(SelectedTable.Sales) + 1;
            string medication = null;
            List<string> checkedMedications = new List<string>();
            decimal cost = 0;
            MedicationModel medicationModel = null;
            int medicationCount = 0;
            var checkBoxes = medicationsWrapPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);

            foreach (var item in checkBoxes)
            {
                if (item is CheckBox && item.IsChecked == true)
                {
                    medication = item.Content.ToString();
                    checkedMedications.Add(medication);
                    medicationModel = _dataLists.MedicationsData.Find(x => x.Title == medication);
                    medicationCount = int.Parse((medicationsWrapPanel.Children[medicationsWrapPanel.Children.IndexOf(item) + 1] as TextBox).Text);
                    medicationModel.Count -= medicationCount;
                    if (medicationModel.Count == 0)
                        medicationModel.Availability = false;
                    _dataLists.Add(new SoldMedicationModel(nextId, medicationModel.Id, medicationCount));
                    _changedData.Add(medicationModel);
                    cost += (medicationModel.Price*medicationCount);
                }
            }
            string medications = string.Join(", ", checkedMedications);

            SaleModel newSale = new SaleModel
                (
                nextId, cost, (DateTime)dateDatePicker.SelectedDate, medications
                );

            ChangeData.SaveAll(_deletedData, _changedData, _dataLists);
            ChangeData.SaveNew(newSale, SelectedTable.Sales, _dataLists);
            Close();
        }

        private void addSaleWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new SaleModel(0, DateTime.Now, null);
            foreach (var row in _dataLists.MedicationsData)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = row.Title;
                checkBox.Margin = new Thickness(1, 1, 1, 1);
                medicationsWrapPanel.Children.Add(checkBox);

                TextBox textBox = new TextBox();
                textBox.Width = 50;
                textBox.Margin = new Thickness(1, 1, 5, 1);
                medicationsWrapPanel.Children.Add(textBox);
            }
        }
    }
}
