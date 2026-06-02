using System;
using System.Collections.Generic;
using GeometriaComputacional.Relleno.RellenoPorPila.Modelo;

namespace GeometriaComputacional.Relleno.RellenoPorPila.Controlador
{
    internal class RellenoPorPilaController
    {
        public const int COLS = 23;
        public const int ROWS = 15;
        public const byte VACIA = 0;
        public const byte BORDE = 1;

        public byte[,] GenerarMatriz(TipoFiguraPila figura)
        {
            var m = new byte[ROWS, COLS];
            if (figura == TipoFiguraPila.Triangulo)
                DibujarTriangulo(m);
            else
                DibujarRombo(m);
            return m;
        }

        // DFS (pila) — insercion: Arriba, Derecha, Abajo, Izquierda
        // Como LIFO: procesado en orden Izquierda, Abajo, Derecha, Arriba
        public ResultadoRelleno Calcular(byte[,] matriz, int semX, int semY)
        {
            var pasos = new List<PasoRelleno>();
            var visitado = new bool[ROWS, COLS];
            var pila = new Stack<(int X, int Y)>();

            if (matriz[semY, semX] != VACIA || !EsDentroDelaFigura(matriz, semX, semY))
                return new ResultadoRelleno(semX, semY, pasos);

            pila.Push((semX, semY));

            int orden = 1;
            while (pila.Count > 0)
            {
                var (x, y) = pila.Pop();

                if (x < 0 || x >= COLS || y < 0 || y >= ROWS) continue;
                if (matriz[y, x] != VACIA || visitado[y, x]) continue;

                visitado[y, x] = true;
                matriz[y, x] = 2;
                pasos.Add(new PasoRelleno(x, y, orden++));

                // Insertar en orden: Arriba, Derecha, Abajo, Izquierda
                // LIFO → sale Izquierda primero
                pila.Push((x, y - 1)); // Arriba
                pila.Push((x + 1, y)); // Derecha
                pila.Push((x, y + 1)); // Abajo
                pila.Push((x - 1, y)); // Izquierda
            }

            return new ResultadoRelleno(semX, semY, pasos);
        }

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

        private static void DibujarTriangulo(byte[,] m)
        {
            // Vertices: cima (11,2), izq (4,12), der (18,12)
            DibujarLinea(m, 11, 2, 4, 12);
            DibujarLinea(m, 11, 2, 18, 12);
            DibujarLinea(m, 4, 12, 18, 12);
        }

        private static void DibujarRombo(byte[,] m)
        {
            // Vertices: top(11,2) right(18,7) bottom(11,12) left(4,7)
            DibujarLinea(m, 11, 2, 18, 7);
            DibujarLinea(m, 18, 7, 11, 12);
            DibujarLinea(m, 11, 12, 4, 7);
            DibujarLinea(m, 4, 7, 11, 2);
        }

        // DDA para rasterizar bordes de polígono
        private static void DibujarLinea(byte[,] m, int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1), dy = Math.Abs(y2 - y1);
            int pasos = Math.Max(dx, dy);
            if (pasos == 0) { m[y1, x1] = BORDE; return; }
            float ix = (float)(x2 - x1) / pasos;
            float iy = (float)(y2 - y1) / pasos;
            float x = x1, y = y1;
            for (int i = 0; i <= pasos; i++)
            {
                int cx = (int)Math.Round(x), cy = (int)Math.Round(y);
                if (cx >= 0 && cx < COLS && cy >= 0 && cy < ROWS)
                    m[cy, cx] = BORDE;
                x += ix; y += iy;
            }
        }
    }

    internal enum TipoFiguraPila { Triangulo, Rombo }
}
