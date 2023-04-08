using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Pharmacy
{
    public static class ChangeData
    {
        static public void SaveNew<T>(T newModel, SelectedTable selectedTable, DataLists dataLists)
        {
            dataLists.Add(newModel);

            if (!FileConnectionsList.IsEmpty())
            {
                var requiredFileConnection = FileConnectionsList.Connections.SingleOrDefault(x => x.SelectedTable == selectedTable);
                if (requiredFileConnection != null)
                {
                    requiredFileConnection.AppendData(newModel);
                    if (DatabaseConnectionService.IsConnected)
                    {
                        DatabaseIOLogic database = new DatabaseIOLogic();
                        database.WriteData(newModel, selectedTable, dataLists);
                    }
                }
                else if (DatabaseConnectionService.IsConnected)
                {
                    DatabaseIOLogic database = new DatabaseIOLogic();
                    database.WriteData(newModel, selectedTable, dataLists);
                }
                else
                    MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (DatabaseConnectionService.IsConnected)
            {
                DatabaseIOLogic database = new DatabaseIOLogic();
                database.WriteData(newModel, selectedTable, dataLists);
            }
            else
                MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void SaveAll(DataLists deletedData, DataLists changedData)
        {
            DatabaseIOLogic database = new DatabaseIOLogic();
            if (deletedData.ManufacturersData.Any())
            {
                foreach(var row in deletedData.ManufacturersData)
                {
                    database.DeleteData(row);
                }
            }    
            if (deletedData.MedicationsData.Any())
            {
                foreach (var row in deletedData.MedicationsData)
                {
                    database.DeleteData(row);
                }
            }
            if (deletedData.WarehousesData.Any())
            {
                foreach (var row in deletedData.WarehousesData)
                {
                    database.DeleteData(row);
                }
            }
            if (deletedData.SalesData.Any())
            {
                foreach (var row in deletedData.SalesData)
                {
                    database.DeleteData(row);
                }
            }
            if (deletedData.PurchasesData.Any())
            {
                foreach (var row in deletedData.PurchasesData)
                {
                    database.DeleteData(row);
                }
            }

            if (changedData.ManufacturersData.Any())
            {
                foreach (var row in changedData.ManufacturersData)
                {
                    database.EditData(row);
                }
            }
            if (changedData.MedicationsData.Any())
            {
                foreach (var row in changedData.MedicationsData)
                {
                    database.EditData(row);
                }
            }
            if (changedData.WarehousesData.Any())
            {
                foreach (var row in changedData.WarehousesData)
                {
                    database.EditData(row);
                }
            }
            if (changedData.SalesData.Any())
            {
                foreach (var row in changedData.SalesData)
                {
                    database.EditData(row);
                }
            }
            if (changedData.PurchasesData.Any())
            {
                foreach (var row in changedData.PurchasesData)
                {
                    database.EditData(row);
                }
            }
        }
    }
}
