using System;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    class Tablero
    {
        Int32[,] tablero;
        Int32 Jugador = 1;
        public event EventHandler<Ganador> Gano;
        public event EventHandler<Turno> CambioTurno;
        Tablero() {
            tablero = new int[15, 15];
        }
        void gano()
        {
            int[][] data = {
                new int[] {1, 0, 0, 10, 0, 15}, // Avanzo en x
                new int[] {0, 1, 0, 15, 0, 10}, // Avanzo en y
                new int[] {1, 1, 0, 10, 0, 10}, // Avanzo en x, y
                new int[] {-1, 1, 4, 15, 0, 10} // Retrocedo en x, avanzo en y
            };
            foreach(int[] Config in data)
            {
                for (int x = Config[2]; x < Config[3]; x++)
                    for (int y = Config[4]; y < Config[5]; y++)
                    {
                        int j = tablero[x, y];
                        if (j == 0)
                            continue;
                        int c;
                        for (c = 0; c < 5; c++)
                            if (tablero[x + c * Config[0], y + c * Config[1]] != j)
                                break;
                        if (c == 5 && Gano != null)
                            Gano(this, new Ganador(tablero[x, y]));
                    }
            }
        }
        public void meterFicha(Int32 columna)
        {
            Int32 fila = ultimoLugar(columna);
            tablero[columna, fila] = Jugador;
            gano();
            if (Jugador == 1) Jugador = 2;
            else Jugador = 1;
            CambioTurno(this, new Turno(Jugador, columna, fila));
        }
        public Int32 ultimoLugar(Int32 columna)
        {
            Int32 fila;
            for (fila = 0; tablero[columna, fila] != 0 && fila < 15; fila++);
            if (fila == 0) throw new Exception("Columna completa");
            return fila;
        }
    }
    class Ganador : EventArgs
    {
        public Int32 Jugador { get; private set; }
        public Ganador(Int32 J)
        {
            Jugador = J;
        }
    }
    class Turno : EventArgs
    {
        public Int32 Jugador { get; private set; }
        public Int32 Columna { get; private set; }
        public Int32 Fila { get; private set; }
        public Turno(Int32 J, Int32 C, Int32 F)
        {
            Jugador = J;
            Columna = C;
            Fila = F;
        }
    }
}
