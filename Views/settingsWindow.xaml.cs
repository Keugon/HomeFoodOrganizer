using Essensausgleich.Controller;
using Essensausgleich.Infra;
using System;
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
using Log = System.Diagnostics.Debug;

namespace Essensausgleich
{
    /// <summary>
    /// Interaktionslogik für settingsWindow.xaml
    /// </summary>
    public partial class settingsWindow : Window
    {
        /// <summary>
        /// settingsWindow to handle user interactions regarding the Variants of Filemanagement
        /// </summary>
        public settingsWindow()
        {
            InitializeComponent();            
        }      
        private void BtnApplyFilesystemChange_Click(object sender, RoutedEventArgs e)
        {                                  
            this.Close();
        }
    }
}
