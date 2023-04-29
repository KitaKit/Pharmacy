﻿using Microsoft.Win32;
using System;
using System.Windows;

namespace Pharmacy
{
    public enum FileConnectionType
    {
        Read, 
        WriteToNew,
        Append
    }
    public class FilesConnectionService
    {
        private readonly string _filePath;
        public string FilePath => _filePath;

       public FilesConnectionService(FileConnectionType connectionType)
        {
            try
            {
                switch (connectionType)
                {
                    case FileConnectionType.Read:
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = "C:\\Documents";
                        openFileDialog.Filter = "CSV files(*.csv)|*.csv";
                        if (openFileDialog.ShowDialog() == true)
                            _filePath = openFileDialog.FileName;
                        break;
                    case FileConnectionType.WriteToNew:
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.InitialDirectory = "C:\\Documents";
                        saveFileDialog.Filter = "CSV files(*.csv)|*.csv";
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            _filePath = saveFileDialog.FileName;
                        }
                        break;
                    case FileConnectionType.Append:

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
