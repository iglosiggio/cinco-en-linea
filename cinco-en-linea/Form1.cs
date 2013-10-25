using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cinco_en_linea
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tablero_Graphics1_ColumnaClick(object sender, TableroClickEventArgs e)
        {
            MessageBox.Show(String.Format("Click\n  Fila: {0}\n  Columna: {1}", e.Fila, e.Columna));
        }
    }
}
