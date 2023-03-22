using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//Здесь будет описано подключение к CSV файлу
//Путь к файлу берётся из диалогового окна открытия файла Windows

//Tady bude popsáno připojení k souboru CSV
//Cesta k souboru bude převzata z dialogového okna otevření souboru systému Windows.

namespace Pharmacy
{
    public class FilesConnectionService
    {
        private readonly string _filePath;
        public string FilePath => _filePath;

       public FilesConnectionService(MenuItem item)
        {
            try
            {
                switch (item.Header)
                {
                    case "Load from CSV-file":
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = "C:\\Documents";
                        openFileDialog.Filter = "CSV files(*.csv)|*.csv";
                        if (openFileDialog.ShowDialog() == true)
                            _filePath = openFileDialog.FileName;
                        break;
                    case "Save to new CSV-file":
                        SaveToFileWindow saveToFileWindow = new SaveToFileWindow();
                        saveToFileWindow.Owner = Application.Current.MainWindow;
                        saveToFileWindow.ShowDialog();
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.InitialDirectory = "C:\\Documents";
                        saveFileDialog.Filter = "CSV files(*.csv)|*.csv";
                        if (saveFileDialog.ShowDialog() == true)
                            _filePath = saveFileDialog.FileName;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
