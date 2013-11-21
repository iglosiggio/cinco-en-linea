using System;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    public enum Dificultad { Fácil, Medio, Difícil, PvP };
	public class Tablero : ICloneable
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
                case Dificultad.Difícil:
                    Jugador2 = new BotDifícil(this);
                    break;
                case Dificultad.PvP:
                    Jugador2 = null;
					break;
            }
			tablero = new int[15, 15];
		}
        Tablero(Int32[,] _tablero, Int32 _Jugador, Bot _Jugador2)
        {
            tablero = _tablero;
            Jugador = _Jugador;
            Jugador2 = _Jugador2;
        }

        public Int32 NFichas(Int32 N)
        {
            int[][] data = {
                new int[] {1, 0, 0, 10, 0, 14}, // Avanzo en x
                new int[] {0, 1, 0, 14, 0, 10}, // Avanzo en y
                new int[] {1, 1, 0, 10, 0, 10}, // Avanzo en x, y
                new int[] {-1, 1, 4, 14, 0, 10} // Retrocedo en x, avanzo en y
            };
            foreach (int[] Config in data)
            {
                for (int x = Config[2]; x <= Config[3]; x++)
                    for (int y = Config[4]; y <= Config[5]; y++)
                    {
                        int j = tablero[x, y];
                        if (j == 0)
                            continue;
                        int c;
                        for (c = 0; c < 5; c++)
                            if (tablero[x + c * Config[0], y + c * Config[1]] != j)
                                break;
                        if (c == N)
                            return tablero[x, y];
                    }
            }
            return 0;
        }

		void gano ()
		{
            Int32 elGanador = NFichas(5);
            if(Gano != null && elGanador != 0)
                Gano(this, new Ganador(elGanador));
		}

		public Tablero meterFicha (Int32 columna)
		{
			Int32 fila = ultimoLugar (columna);
			if(fila == -1)
				throw new InvalidOperationException ("Columna completa");
			tablero [columna, fila] = Jugador;
			gano ();
            if(CambioTurno != null)
			    CambioTurno (this, new Turno (Jugador, columna, fila));
			Jugador = Jugador == 1 ? 2 : 1;
            if (Jugador == 2 && Jugador2 != null)
            {
                meterFicha(Jugador2.Turno());
            }
            return this;
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

        public object Clone()
        {
            return new Tablero((Int32[,])tablero.Clone(), Jugador, null);
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
    public class BotDifícil : Bot
    {
        public BotDifícil(Tablero _T) : base(_T) { }
        public override Int32 Turno()
        {
            Int32 Columna;
            Tablero CurrentTablero = (Tablero)T.Clone();
            for (Columna = 0; CurrentTablero.columnaLlena(Columna); Columna++) ;
            Int32[] MejorTurno = {0, Sum(CurrentTablero.meterFicha(Columna), 0)};
            for (Columna = 1; Columna < 15; Columna++)
            {
                CurrentTablero = (Tablero)T.Clone();
                try
                {
                    Int32[] CurrentTurno = { Columna, Sum(CurrentTablero.meterFicha(Columna), 1) };
                    if (CurrentTurno[1] > MejorTurno[1])
                        MejorTurno = CurrentTurno;
                    else if (CurrentTurno[1] == MejorTurno[1] && new Random().Next() % 3 == 1)
                        MejorTurno = CurrentTurno;
                }
                catch (Exception) { }
            }
            return MejorTurno[0];
        }
        Int32 Sum(Tablero ElTablero, Int32 Profundidad)
        {
            Int32 STotal = 0;
            if(Profundidad == 3)
                return 0;
            for (Int32 Columna = 0; Columna < 15; Columna++)
            {
                Int32 N5 = ElTablero.NFichas(5);
                Tablero NuevoTurno = (Tablero)ElTablero.Clone();
                try
                {
                    STotal += Sum(NuevoTurno.meterFicha(Columna), Profundidad + 1);
                    STotal += N5 != 0 ? N5 == 2 ? 60 / Profundidad : -900 / Profundidad : 0;
                }
                catch (Exception) { }
            }
            return STotal;
        }
    }
}
