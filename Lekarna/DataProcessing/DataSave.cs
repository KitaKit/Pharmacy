using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy
{
    public static class DataSave
    {
        static public void SaveNewData<T>(T newModel, SelectedTable selectedTable)
        {
            DataLists.Add(newModel);

            if (!FileConnectionsList.IsEmpty())
            {
                var requiredFileConnection = FileConnectionsList.Connections.SingleOrDefault(x => x.SelectedTable == selectedTable);
                if (requiredFileConnection != null)
                {
                    (requiredFileConnection as FilesIOLogic).AppendData(newModel, selectedTable);
                    if (DatabaseConnectionService.IsConnected)
                    {
                        DatabaseIOLogic database = new DatabaseIOLogic();
                        database.WriteData(newModel, selectedTable);
                    }
                }
                else if (DatabaseConnectionService.IsConnected)
                {
                    DatabaseIOLogic database = new DatabaseIOLogic();
                    database.WriteData(newModel, selectedTable);
                }
                else
                    MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (DatabaseConnectionService.IsConnected)
            {
                DatabaseIOLogic database = new DatabaseIOLogic();
                database.WriteData(newModel, selectedTable);
            }
            else
                MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
