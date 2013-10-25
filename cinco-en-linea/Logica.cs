using System;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    class Tablero
    {
        int[,] tablero;
        Tablero() {
            tablero = new int[15, 15];
        }
        public int gano()
        {
            int ganador = 0;
            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (tablero[y, x] == tablero[y, x + 1] && tablero[y, x] == tablero[y, x + 2] && tablero[y, x] == tablero[y, x + 3] && tablero[y, x] == tablero[y, x + 4] && tablero[y, x] != 0)
                        ganador = tablero[y, x];
                }
            }
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    if (tablero[y, x] == tablero[y + 1, x] && tablero[y, x] == tablero[y + 2, x] && tablero[y, x] == tablero[y + 3, x] && tablero[y, x] == tablero[y + 4, x] && tablero[y, x] != 0)
                        ganador = tablero[y, x];
                }
            }
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (tablero[y, x] == tablero[y + 1, x + 1] && tablero[y, x] == tablero[y + 2, x + 2] && tablero[y, x] == tablero[y + 3, x + 3] && tablero[y, x] == tablero[y + 4, x + 4] && tablero[y, x] != 0)
                        ganador = tablero[y, x];
                    
                }
            }
            for (int y = 0; y < 10; y++)
            {
                for (int x = 5; x < 15; x++)
                {
                    if (tablero[y, x] == tablero[y - 1, x - 1] && tablero[y, x] == tablero[y - 2, x - 2] && tablero[y, x] == tablero[y - 3, x - 3] && tablero[y, x] == tablero[y - 4, x - 4] && tablero[y, x] != 0)
                        ganador = tablero[y, x];
                    
                }
            }
            return ganador;
        }
        public void meterFicha(Int32 columna int )
        {
        }
    }
}
