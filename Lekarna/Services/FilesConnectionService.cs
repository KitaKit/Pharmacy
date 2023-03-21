using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

       public FilesConnectionService()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "C:\\Documents";
                openFileDialog.Filter = "CSV files(*.csv)|*.csv";
                _filePath = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
