using System;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    public enum Dificultad { Fácil, Medio, Difícil, PvP };
	public class Tablero
	{
		Int32[,] tablero;
		Int32 Jugador = 1;
        Bot Jugador2;
		public event EventHandler<Ganador> Gano;
		public event EventHandler<Turno> CambioTurno;

		public Tablero (Dificultad Dif)
		{
            switch (Dif)
            {
                case Dificultad.Fácil:
                    Jugador2 = new BotFácil(this);
                    break;
                case Dificultad.PvP:
                    Jugador2 = null;
					break;
            }
			tablero = new int[15, 15];
		}

		void gano ()
		{
			int[][] data = {
                new int[] {1, 0, 0, 10, 0, 14}, // Avanzo en x
                new int[] {0, 1, 0, 14, 0, 10}, // Avanzo en y
                new int[] {1, 1, 0, 10, 0, 10}, // Avanzo en x, y
                new int[] {-1, 1, 4, 14, 0, 10} // Retrocedo en x, avanzo en y
            };
			foreach (int[] Config in data) {
				for (int x = Config[2]; x <= Config[3]; x++)
					for (int y = Config[4]; y <= Config[5]; y++) {
						int j = tablero [x, y];
						if (j == 0)
							continue;
						int c;
						for (c = 0; c < 5; c++)
							if (tablero [x + c * Config [0], y + c * Config [1]] != j)
								break;
						if (c == 5 && Gano != null)
							Gano (this, new Ganador (tablero [x, y]));
					}
			}
		}

		public void meterFicha (Int32 columna)
		{
			Int32 fila = ultimoLugar (columna);
			if(fila == -1)
				throw new InvalidOperationException ("Columna completa");
			tablero [columna, fila] = Jugador;
			gano ();
			CambioTurno (this, new Turno (Jugador, columna, fila));
			Jugador = Jugador == 1 ? 2 : 1;
            if (Jugador == 2 && Jugador2 != null)
            {
                meterFicha(Jugador2.Turno());
            }
		}

        public Boolean columnaLlena(Int32 columna)
        {
			return ultimoLugar(columna) == -1;
        }

		public Int32 ultimoLugar (Int32 columna)
		{
			Int32 fila;
			try {
				for (fila = 0; fila < 15 && tablero[columna, fila] == 0; fila++);
				return fila - 1;
			} catch (Exception) {
				return -1;
			}
		}

		public Int32 this [Int32 X, Int32 Y] {
			get { return tablero [X, Y]; }
		}
	}

	public class Ganador : EventArgs
	{
		public Int32 Jugador { get; private set; }

		public Ganador (Int32 J)
		{
			Jugador = J;
		}
	}

	public class Turno : EventArgs
	{
		public Int32 Jugador { get; private set; }

		public Int32 Columna { get; private set; }

		public Int32 Fila { get; private set; }

		public Turno (Int32 J, Int32 C, Int32 F)
		{
			Jugador = J;
			Columna = C;
			Fila = F;
		}
	}
    public abstract class Bot
    {
        protected Tablero T;
        public Bot(Tablero _T)
        {
            T = _T;
        }
        public abstract Int32 Turno();
    }
    public class BotFácil : Bot
    {
        Random R;
        public BotFácil(Tablero _T)
            : base(_T)
        {
            R = new Random();
        }
        public override int Turno()
        {
            Int32 Columna;
            do{
                Columna = R.Next(0, 14);
            } while (T.columnaLlena(Columna));
            return Columna;
        }
    }
}
