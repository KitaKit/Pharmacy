using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy
{
    public static class DataSaveService
    {
        static public void SaveNewData<T>(T newModel, SelectedTable selectedTable)
        {
            if (!FileConnectionsList.IsEmpty())
            {
                var requiredFileConnection = FileConnectionsList.Connections.SingleOrDefault(x => x.SelectedTable == selectedTable);
                if (requiredFileConnection != null)
                {
                    (requiredFileConnection as FilesIOLogic).AppendData(newModel, selectedTable);
                    if (DatabaseConnectionService.IsConnected)
                    {
                        DatabaseIOLogic database = new DatabaseIOLogic();
                        database.WriteData();
                    }
                }
                else if (DatabaseConnectionService.IsConnected)
                {
                    DatabaseIOLogic database = new DatabaseIOLogic();
                    database.WriteData();
                }
                else
                    MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (DatabaseConnectionService.IsConnected)
            {
                DatabaseIOLogic database = new DatabaseIOLogic();
                database.WriteData();
            }
            else
                MessageBox.Show("There is no connection to the database or the corresponding table file is not open", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
