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
    /// Interaction logic for SaveToFileWindow.xaml
    /// </summary>
    public partial class SaveToFileWindow : Window
    {
        public SaveToFileWindow()
        {
            InitializeComponent();
        }

        private void SaveToNewFileWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tablesToSaveComboBox.ItemsSource = (Application.Current.MainWindow.FindName("mainTabControl") as TabControl).Items.Cast<TabItem>().Select(x => x.Header.ToString());
        }
    }
}
