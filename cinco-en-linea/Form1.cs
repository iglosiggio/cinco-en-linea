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
	public partial class Pasión : Form
	{
		public Pasión ()
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

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
				"                                                              \n" +
                " ¿Como se juega?                                              \n" +
                "                                                              \n" +
				" - Para ganar deben lograr colocar 5 fichas consecutivas del  \n" +
				"   mismo color en forma horizontal, vertical o diagonal       \n" +
                " - Haga click en la columna donde quiere poner la ficha, esta \n" +
                "   caerá hasta el ultimo espacio libre de la fila             \n" +
                "     (para saber en que columna se esta posicionado hay un    \n" +
                "                        señalizador gris que marca la columna)\n" +
                " - El juego incluye un indicador de turno                     \n" +
                "     (una luz en la parte inferior del tablero que indica el  \n" +
                "                                     color del jugador actual)\n" +
                " - Existen dos modos de juego:                                \n" +
                "    * Contra la computadora                                   \n" +
                "    * Contra otra persona (tiene tres dificultades)           \n" +
                "   Estos modos se pueden acceder desde el menú superior,      \n" +
                "   accederlos reinicia la partida actual.                     \n"
            );
        }

        private void Pasión_Resize (object sender, EventArgs e)
		{
			try {
				if (ClientSize.Width < 370)
					Program.Saba.ClientSize = new Size (370, 370);
				else
					Program.Saba.ClientSize = new Size (ClientSize.Width, ClientSize.Width);
			} catch {
			}
        }
	}
}
