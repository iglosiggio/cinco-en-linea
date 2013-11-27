using System;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    public enum Dificultad { Fácil, Medio, Difícil, PvP };
	public class Tablero : ICloneable
	{
		Int32[,] tablero;
		public Int32 Jugador { get; private set; }
        Bot Jugador2;
		public event EventHandler<Ganador> Gano;
		public event EventHandler<Turno> CambioTurno;

		public Tablero (Dificultad Dif)
		{
			Jugador = 1;
            switch (Dif)
            {
                case Dificultad.Fácil:
                    Jugador2 = new BotFácil(new Tablero(Logica.Dificultad.PvP));
                    break;
                case Dificultad.Medio:
                    Jugador2 = new BotAlfaBeta(new Tablero(Logica.Dificultad.PvP), 4);
                    break;
				case Dificultad.Difícil:
					Jugador2 = new BotAlfaBeta(new Tablero(Logica.Dificultad.PvP), 7);
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
			Jugador = (Jugador % 2) + 1;
            if (Jugador == 2 && Jugador2 != null)
            {
                meterFicha(Jugador2.Turno(columna));
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
        public abstract Int32 Turno(Int32 Previo);
    }
    public class BotFácil : Bot
    {
        Random R;
        public BotFácil(Tablero _T)
            : base(_T)
        {
            R = new Random();
        }
        public override int Turno(Int32 Previo)
        {
            Int32 Columna;
            do{
                Columna = R.Next(0, 14);
            } while (T.columnaLlena(Columna));
            return Columna;
        }
    }
    public class BotAlfaBeta : Bot
    {
		Int32 Complejidad;
		Explorador E;
        public BotAlfaBeta(Tablero _T, Int32 C) : base(_T)
		{
			Complejidad = C;
			E = new Explorador(T);
		}
        public override Int32 Turno(Int32 Previo)
        {
			Pair<Int32, Int32> Best = Sum (E.Get(Previo), Complejidad, 2, -99999999, 99999999);
			E = E.Get(Previo).Get(Best.A);
			Console.WriteLine();
            return Best.A;
        }
        Pair<Int32, Int32> Sum (Explorador E, Int32 Profundidad, Int32 Jugador, Int32 alfa, Int32 beta)
		{
			if (E.P.B != 0 || Profundidad == 0)
				return E.P;
			Int32 RColumna = 0;
            for (Int32 Columna = 0; Columna < 15; Columna++)
            {
                Pair<Int32, Int32> P;
                P = Sum(E.Get(Columna), Profundidad - 1, (Jugador % 2) + 1, -beta, -alfa);
                if (alfa < P.B)
                {
                    alfa = P.B;
                    RColumna = P.A;
                }
                if (beta <= alfa)
                    break;
            }
            return new Pair<Int32,Int32> (RColumna, alfa);
        }
    }
    public struct Pair<T1, T2>
    {
        public T1 A;
        public T2 B;
        public Pair(T1 a, T2 b)
        {
            A = a;
            B = b;
        }
        public Pair<T1, T2> SetA(T1 a)
        {
            A = a;
            return this;
        }
        public Pair<T1, T2> SetB(T2 b)
        {
            B = b;
            return this;
        }
		public override string ToString ()
		{
			return string.Format ("({0}; {1})", A, B);
		}
    }
	class Explorador
	{
		public Tablero T;
		Explorador[] Futuros;
		public Pair<Int32, Int32> P { get; private set; }
		public Explorador (Tablero _T)
		{
			T = _T;
			Futuros = new Explorador[15];
		}
		public Explorador (Tablero _T, Pair<Int32, Int32> _P)
		{
			T = _T;
			Futuros = new Explorador[15];
			P = _P;
		}
		public Explorador Get (Int32 N)
		{
			if (P.B != 0)
				return this;
			if (Futuros [N] != null) {
				return Futuros [N];
			}
			if (N >= 15 || N < 0)
				throw new ArgumentOutOfRangeException ();
			Pair<Int32, Int32> NewP;
			Tablero NewT;
			try {
				NewT = ((Tablero)T.Clone ()).meterFicha (N);
				if (NewT.NFichas (5) == T.Jugador)
					NewP = new Pair<Int32, Int32> (N, 10);
				else
					NewP = new Pair<Int32, Int32> (N, 0);
			} catch (Exception) {
				NewT = (Tablero)T.Clone();
				NewP = new Pair<Int32, Int32> (N, -99999999);
			}
			return Futuros [N] = new Explorador (NewT, NewP);
		}
	}
}
