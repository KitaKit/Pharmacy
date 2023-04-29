using System.Collections.Generic;
using System.Linq;
using System.Windows;

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

        public static bool SaveAll(DataLists deletedData, DataLists changedData, DataLists mainData)
        {
            DatabaseIOLogic database = new DatabaseIOLogic();
            SelectedTable selectedTable = SelectedTable.All;

            List<List<object>> deletedDataLists = new List<List<object>>
            {
                deletedData.MedicationsData.Cast<object>().ToList(),
                deletedData.ManufacturersData.Cast<object>().ToList(),
                deletedData.WarehousesData.Cast<object>().ToList(),
                deletedData.SalesData.Cast<object>().ToList(),
                deletedData.PurchasesData.Cast<object>().ToList()
            };

            List<List<object>> changedDataLists = new List<List<object>>
            {
                changedData.MedicationsData.Cast<object>().ToList(),
                changedData.ManufacturersData.Cast<object>().ToList(),
                changedData.WarehousesData.Cast<object>().ToList(),
                changedData.SalesData.Cast<object>().ToList(),
                changedData.PurchasesData.Cast<object>().ToList()
            }; 
            
            foreach (var dataList in deletedDataLists)
            {
                if (dataList.Any())
                {
                    foreach (var row in dataList)
                    {
                        if (!database.DeleteData(row))
                            return false;

                        foreach(var item in changedDataLists)
                            item.RemoveAll(x => x.GetType() == row.GetType() && (x as dynamic).Id == (row as dynamic).Id);
                    }

                    if (!FileConnectionsList.IsEmpty())
                    {
                        if (dataList.FirstOrDefault() is MedicationModel)
                            selectedTable = SelectedTable.Medications;
                        else if (dataList.FirstOrDefault() is WarehouseModel)
                            selectedTable = SelectedTable.Warehouses;
                        else if (dataList.FirstOrDefault() is ManufacturerModel)
                            selectedTable = SelectedTable.Manufacturers;
                        else if (dataList.FirstOrDefault() is SaleModel)
                            selectedTable = SelectedTable.Sales;
                        else if (dataList.FirstOrDefault() is PurchaseModel)
                            selectedTable = SelectedTable.Purchases;
                        else
                            continue;

                        var requiredFileConnection = FileConnectionsList.Connections.SingleOrDefault(x => x.SelectedTable == selectedTable);
                        if (requiredFileConnection != null)
                        {
                            requiredFileConnection.WriteDataToNew(mainData);
                        }
                    }
                }
            }

            foreach (var dataList in changedDataLists)
            {
                if (dataList.Any())
                {
                    foreach (var row in dataList)
                    {
                        if (!database.EditData(row, mainData))
                            return false;
                    }

                    if (!FileConnectionsList.IsEmpty())
                    {
                        if (dataList.FirstOrDefault() is MedicationModel)
                            selectedTable = SelectedTable.Medications;
                        else if (dataList.FirstOrDefault() is WarehouseModel)
                            selectedTable = SelectedTable.Warehouses;
                        else if (dataList.FirstOrDefault() is ManufacturerModel)
                            selectedTable = SelectedTable.Manufacturers;
                        else if (dataList.FirstOrDefault() is SaleModel)
                            selectedTable = SelectedTable.Sales;
                        else if (dataList.FirstOrDefault() is PurchaseModel)
                            selectedTable = SelectedTable.Purchases;
                        else
                            continue;

                        var requiredFileConnection = FileConnectionsList.Connections.SingleOrDefault(x => x.SelectedTable == selectedTable);
                        if (requiredFileConnection != null)
                        {
                            requiredFileConnection.WriteDataToNew(mainData);
                        }
                    }
                }
            }
            return true;
        }
    }
}
