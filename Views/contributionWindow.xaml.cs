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
using Essensausgleich.Controller;
using Essensausgleich.Infra;
using Essensausgleich.ViewModel;

namespace Essensausgleich
{
    /// <summary>
    /// Interaktionslogik für contributionWindow.xaml
    /// </summary>
    public partial class contributionWindow : Window, IAppObjekt
    {
        /// <summary>
        /// Get or Sets the Kontext
        /// </summary>
        public Infrastruktur Kontext { get; set; } = null!;
        /// <summary>
        /// init contributionWindow
        /// </summary>
        public contributionWindow()
        {           
            InitializeComponent();
            //DataContext = this.DataContext;
        }

        
    }
}
