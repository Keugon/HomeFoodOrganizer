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

namespace Essensausgleich
{
    /// <summary>
    /// Interaktionslogik für contributionWindow.xaml
    /// </summary>
    public partial class contributionWindow : Window
    {
        /// <summary>
        /// init contributionWindow
        /// </summary>
        public contributionWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Fills the DataGrid with information coresponding to the selected User
        /// </summary>
        /// <param name="betragsListe"></param>
        public void FillDataGrid(List<Betrag> betragsListe)
        {
            dGridVbeitraege.ItemsSource = betragsListe;
            
            
           
            
        }
    }
}
