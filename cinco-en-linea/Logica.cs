﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace Logica
{
	public enum Dificultad
	{
		Fácil,
		Medio,
		Difícil,
		PvP
	};

	public class Tablero : ICloneable
	{
		Byte[,] tablero;

		public Byte Jugador { get; private set; }

		Bot Jugador2;

		public event EventHandler<Ganador> Gano;
		public event EventHandler<Turno> CambioTurno;

		public Tablero (Dificultad Dif)
		{
			Jugador = 1;
			switch (Dif) {
			case Dificultad.Fácil:
				Jugador2 = new BotFácil ();
				break;
			case Dificultad.Medio:
				Jugador2 = new BotAlfaBeta (5);
				break;
			case Dificultad.Difícil:
				Jugador2 = new BotAlfaBeta (7);
				break;
			case Dificultad.PvP:
				Jugador2 = null;
				break;
			}
			tablero = new Byte[15, 15];
		}

		Tablero (Byte[,] _tablero, Byte _Jugador, Bot _Jugador2)
		{
			tablero = _tablero;
			Jugador = _Jugador;
			Jugador2 = _Jugador2;
		}

		public Byte NFichas (Int32 N)
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
						if (c == N)
							return tablero [x, y];
					}
			}
			return 0;
		}

		void gano ()
		{
			Int32 elGanador = NFichas (5);
			if (Gano != null && elGanador != 0)
				Gano (this, new Ganador (elGanador));
		}

		public Tablero meterFicha (Int32 columna)
		{
			Int32 fila = ultimoLugar (columna);
			if (fila == -1)
				throw new InvalidOperationException ("Columna completa");
			tablero [columna, fila] = Jugador;
			gano ();
			if (CambioTurno != null)
				CambioTurno (this, new Turno (Jugador, columna, fila));
			Jugador = (Byte)((Jugador % 2) + 1);
			if (Jugador == 2 && Jugador2 != null) {
                cinco_en_linea.Program.Saba.jugador.Text = "Calculando...";
				meterFicha (Jugador2.Turno (columna, this));
			}
			return this;
		}

		public Boolean columnaLlena (Int32 columna)
		{
			return ultimoLugar (columna) == -1;
		}

		public Int32 ultimoLugar (Int32 columna)
		{
			Int32 fila;
			try {
				for (fila = 0; fila < 15 && tablero[columna, fila] == 0; fila++)
					;
				return fila - 1;
			} catch (Exception) {
				return -1;
			}
		}

		public Byte this [Int32 X, Int32 Y] {
			get { return tablero [X, Y]; }
		}

		public object Clone ()
		{
			return new Tablero ((Byte[,])tablero.Clone (), Jugador, null);
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
		public abstract Int32 Turno (Int32 Previo, Tablero T);
	}

	public class BotFácil : Bot
	{
		Random R;

		public BotFácil ()
		{
			R = new Random ();
		}

		public override int Turno (Int32 Previo, Tablero T)
		{
			Int32 Columna;
			do {
				Columna = R.Next (0, 14);
			} while (T.columnaLlena(Columna));
			return Columna;
		}
	}

	public class BotAlfaBeta : Bot
	{
		Int32 Complejidad;
		Explorador E;

		public BotAlfaBeta (Int32 C)
		{
			Complejidad = C;
			E = new Explorador(new Tablero(Logica.Dificultad.PvP));
		}

		public override Int32 Turno (Int32 Previo, Tablero T)
		{
			Pair<Int32, Int32> Best = Sum (E.Get (Previo), Complejidad, 2, -99999999, 99999999);
			E = E.Get (Previo).Get (Best.A);
            GC.Collect();
			return Best.A;
		}

		Pair<Int32, Int32> Sum (Explorador E, Int32 Profundidad, Int32 Jugador, Int32 alfa, Int32 beta)
		{
			if (E.P.B != 0 || Profundidad == 0)
				return new Pair<Int32, Int32>(E.P.A, E.P.B);
			Int32 RColumna = 0;
			for (Int32 Columna = 0; Columna < 15; Columna++) {
				Pair<Int32, Int32> P;
				P = Sum (E.Get (Columna), Profundidad - 1, (Jugador % 2) + 1, -beta, -alfa);
				if (alfa < P.B) {
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

		public Pair (T1 a, T2 b)
		{
			A = a;
			B = b;
		}

		public Pair<T1, T2> SetA (T1 a)
		{
			A = a;
			return this;
		}

		public Pair<T1, T2> SetB (T2 b)
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
#if DEBUG
			Count.Increment();
#endif
		}

		public Explorador (Tablero _T, Pair<Int32, Int32> _P)
		{
			T = _T;
			Futuros = new Explorador[15];
			P = _P;
#if DEBUG
			Count.Increment();
#endif
		}

#if DEBUG
		public static PerformanceCounter Count;
		~Explorador ()
		{
			Count.Decrement();

		}
#endif

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
				NewT = (Tablero)T.Clone ();
				NewP = new Pair<Int32, Int32> (N, -99999999);
			}
			return Futuros [N] = new Explorador (NewT, NewP);
		}
	}

	public class Muchacho<R, V>
	{
		public delegate R Work (V panam); // ILARI LARI E UO-UO-UO
		R _rval;                          // ILARI LARI LARI E UO-UO-UO
		public R Return {				  // Ah no pará, esa es Xuxa...
			get {
				if (IsAlive)
					throw new InvalidOperationException ("Lo' muchacho aún no terminaron el laburo");
				return _rval;
			}
			private set {
				_rval = value;
			}
		}
		public bool IsAlive {
			get {
				try { return Worker.IsAlive; }
				catch { return false; }
			}
		}
		Work ElThread;
		Thread Worker;
		V Parámetro;
		Exception e;
		public Exception Throw {
			get {
				if (IsAlive)
					throw new InvalidOperationException ("Lo' muchacho aún no terminaron el laburo");
				return e;
			}
			set {
				e = value;
			}
		}
		Boolean throwConfig;
		public Muchacho (Work W) : this(W, false) { }
		public Muchacho (Work W, Boolean realWorldThrow)
		{
			ElThread = W;
			throwConfig = realWorldThrow;
		}
		public void Start (V Param)
		{
			if(IsAlive)
				throw new InvalidOperationException("Los muchacho' ya estamo' en eso");
			Worker = new Thread(new ThreadStart(DoWork));
			Parámetro = Param;
			Worker.Start();
		}
		public void Join ()
		{
			Worker.Join();
		}
		void DoWork ()
		{
			Throw = null;
			try {
				Return = ElThread (Parámetro);
			} catch (Exception ouch) {
				if(throwConfig)
					throw ouch;
				else Throw = ouch;
			}
		}
		public static implicit operator Muchacho <R, V>(Work W)
		{
			return new Muchacho<R, V>(W);
		}
	}
}
