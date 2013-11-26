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
		public Form1 ()
		{
			InitializeComponent ();
		}

        private void conUnAmigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablero_Graphics1.CambiarDificultad(Logica.Dificultad.PvP);
        }

        private void fácilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablero_Graphics1.CambiarDificultad(Logica.Dificultad.Fácil);
        }

        private void medioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablero_Graphics1.CambiarDificultad(Logica.Dificultad.Medio);
        }

		private void difícilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablero_Graphics1.CambiarDificultad(Logica.Dificultad.Difícil);
        }
	}
}
