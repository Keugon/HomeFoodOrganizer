﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Essensausgleich
{
    /// <summary>
    /// Form to Display the Single Items of the List component of Bewohner
    /// </summary>
    public partial class beitragsauflistungForm : Form
    {
        public beitragsauflistungForm(MainForm mainForm)
        {
            InitializeComponent();
        }
        public void FillDataGrid(List<Betrag> betragsListe)
        {
            dGridVbeitraege.Columns.Add("KategorieD", "Kategorie");
            dGridVbeitraege.Columns.Add("BetragD", "Betrag");
            foreach (var Betrag in betragsListe)
            {
                dGridVbeitraege.Rows.Add(Betrag.kategorie.ToString(), Betrag.wert.ToString());
            }
            this.Size = dGridVbeitraege.Size;
        }

    }
}
