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

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "¿Como se juega?\n" +
                "===============\n" +
                "El 5 en linea es un juego por turnos de dos jugadores. Para ganar debe lograr colocar 5 fichas consecutivas del mismo color en forma horizontal, vertical o diagonal.\n" +
                "Cuando el jugador hace clik en la columna donde quiere poner la ficha esta va cayendo hasta el ultimo espacio libre de la fila; para saber en que columna se esta posicionado hay un señalizador gris que marca la columna.\n" +
                "Para saber de quien es el turno hay una luz indicadora en la parte interior del tablero donde se indica el color del jugador.\n" +
                "Hay 2 modos de juego: PvP y PvCOM \n" +
                "El PvP es el modo para jugar contra otro jugador.\n" +
                "En el PvCOM existen 3 niveles de dificultad: Facil, Medio y Dificil"
            );
        }
	}
}
