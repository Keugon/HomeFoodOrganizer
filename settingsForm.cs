using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich
{
    /// <summary>
    /// settingsForm
    /// </summary>
    public partial class settingsForm : Form
    {
        private XMLPersistence _XMLPersistance;
        /// <summary>
        /// initofSettingsform
        /// </summary>
        public settingsForm()
        {
            InitializeComponent();
            _XMLPersistance = FilesSystemManager.GetXMLPersistance();
        }

        private void settingsForm_Load(object sender, EventArgs e)
        {
            RbtnFileSave.Select();
            txtBoxFileNameXML.Text = _XMLPersistance.XMLFileName;

        }

        private void BtnApplyFilesystemChange_Click(object sender, EventArgs e)
        {
           _XMLPersistance.ChangePath(txtBoxFileNameXML.Text);
            Log.WriteLine(txtBoxFileNameXML.Text);
            this.Close();
        }
    }
}
