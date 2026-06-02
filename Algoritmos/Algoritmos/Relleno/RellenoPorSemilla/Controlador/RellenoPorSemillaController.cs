using System;
using System.Collections.Generic;
using GeometriaComputacional.Relleno.RellenoPorSemilla.Modelo;

namespace GeometriaComputacional.Relleno.RellenoPorSemilla.Controlador
{
    internal class RellenoPorSemillaController
    {
        public const int COLS = 23;
        public const int ROWS = 15;
        public const byte VACIA = 0;
        public const byte BORDE = 1;

        // Genera la matriz con el borde de la figura elegida
        public byte[,] GenerarMatriz(TipoFiguraSemilla figura)
        {
            var m = new byte[ROWS, COLS];

            if (figura == TipoFiguraSemilla.Cuadrado)
                DibujarCuadrado(m);
            else
                DibujarCirculo(m);

            return m;
        }

        // BFS (cola) — orden: Arriba, Derecha, Abajo, Izquierda
        public ResultadoRelleno Calcular(byte[,] matriz, int semX, int semY)
        {
            var pasos = new List<PasoRelleno>();
            var enCola = new bool[ROWS, COLS];
            var cola = new Queue<(int X, int Y)>();

            if (matriz[semY, semX] != VACIA || !EsDentroDelaFigura(matriz, semX, semY))
                return new ResultadoRelleno(semX, semY, pasos);

            cola.Enqueue((semX, semY));
            enCola[semY, semX] = true;

            int orden = 1;
            while (cola.Count > 0)
            {
                var (x, y) = cola.Dequeue();

                if (matriz[y, x] != VACIA) continue;

                matriz[y, x] = 2; // rellena
                pasos.Add(new PasoRelleno(x, y, orden++));

                // Arriba, Derecha, Abajo, Izquierda
                foreach (var (nx, ny) in new[] { (x, y - 1), (x + 1, y), (x, y + 1), (x - 1, y) })
                {
                    if (nx >= 0 && nx < COLS && ny >= 0 && ny < ROWS
                        && matriz[ny, nx] == VACIA && !enCola[ny, nx])
                    {
                        enCola[ny, nx] = true;
                        cola.Enqueue((nx, ny));
                    }
                }
            }

            return new ResultadoRelleno(semX, semY, pasos);
        }

        // BFS de pre-validacion: retorna false si la semilla alcanza el borde de la matriz
        // (significa que esta fuera de la figura, no encerrada por un borde)
        private static bool EsDentroDelaFigura(byte[,] matriz, int semX, int semY)
        {
            var visitado = new bool[ROWS, COLS];
            var cola = new Queue<(int X, int Y)>();
            cola.Enqueue((semX, semY));
            visitado[semY, semX] = true;

            while (cola.Count > 0)
            {
                var (x, y) = cola.Dequeue();
                if (x == 0 || x == COLS - 1 || y == 0 || y == ROWS - 1)
                    return false;
                foreach (var (nx, ny) in new[] { (x, y - 1), (x + 1, y), (x, y + 1), (x - 1, y) })
                {
                    if (nx >= 0 && nx < COLS && ny >= 0 && ny < ROWS
                        && matriz[ny, nx] == VACIA && !visitado[ny, nx])
                    {
                        visitado[ny, nx] = true;
                        cola.Enqueue((nx, ny));
                    }
                }
            }
            return true;
        }

        private static void DibujarCuadrado(byte[,] m)
        {
            // Rectangulo del col 4 al 18, fila 2 al 12
            for (int c = 4; c <= 18; c++) { m[2, c] = BORDE; m[12, c] = BORDE; }
            for (int r = 2; r <= 12; r++) { m[r, 4] = BORDE; m[r, 18] = BORDE; }
        }

        private static void DibujarCirculo(byte[,] m)
        {
            int xc = 11, yc = 7, r = 5;
            int x = 0, y = r, p = 1 - r;
            while (x <= y)
            {
                PintarOctantes(m, xc, yc, x, y);
                if (p < 0) p += 2 * x + 3;
                else { y--; p += 2 * (x - y) + 5; }
                x++;
            }
        }

        private static void PintarOctantes(byte[,] m, int xc, int yc, int x, int y)
        {
            foreach (var (col, row) in new[]
            {
                (xc+x, yc+y),(xc-x, yc+y),(xc+x, yc-y),(xc-x, yc-y),
                (xc+y, yc+x),(xc-y, yc+x),(xc+y, yc-x),(xc-y, yc-x)
            })
            {
                if (col >= 0 && col < COLS && row >= 0 && row < ROWS)
                    m[row, col] = BORDE;
            }
        }
    }

    internal enum TipoFiguraSemilla { Cuadrado, Circulo }
}
