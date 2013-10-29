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
                        if (c == 5)
                            return tablero[x, y];
                    }
            }
            return 0;
        }
        public void meterFicha(Int32 columna, Int32 Jugador)
        {
            if (columna
        }
    }
}
