using System;
using System.Collections.Generic;
using GeometriaComputacional.Relleno.RellenoScanline.Modelo;

namespace GeometriaComputacional.Relleno.RellenoScanline.Controlador
{
    internal class RellenoScanlineController
    {
        public const int COLS = 23;
        public const int ROWS = 15;
        public const byte VACIA = 0;
        public const byte BORDE = 1;

        public byte[,] GenerarMatriz(TipoFiguraScanline figura)
        {
            var m = new byte[ROWS, COLS];
            if (figura == TipoFiguraScanline.Pentagono)
                DibujarPoligono(m, ObtenerVerticesPentagono());
            else
                DibujarPoligono(m, ObtenerVerticesHexagono());
            return m;
        }

        // Scanline con cola — rellena tramos horizontales completos
        public ResultadoRelleno Calcular(byte[,] matriz, int semX, int semY)
        {
            var pasos = new List<PasoRelleno>();
            var agregado = new bool[ROWS, COLS];
            var cola = new Queue<(int X, int Y)>();

            if (matriz[semY, semX] != VACIA)
                return new ResultadoRelleno(semX, semY, pasos);

            cola.Enqueue((semX, semY));
            agregado[semY, semX] = true;

            int orden = 1;
            while (cola.Count > 0)
            {
                var (sx, sy) = cola.Dequeue();
                if (matriz[sy, sx] != VACIA) continue;

                // Buscar limite izquierdo
                int xIzq = sx;
                while (xIzq > 0 && matriz[sy, xIzq - 1] == VACIA) xIzq--;

                // Buscar limite derecho
                int xDer = sx;
                while (xDer < COLS - 1 && matriz[sy, xDer + 1] == VACIA) xDer++;

                // Pintar tramo y buscar semillas arriba/abajo
                for (int x = xIzq; x <= xDer; x++)
                {
                    if (matriz[sy, x] != VACIA) continue;
                    matriz[sy, x] = 2;
                    pasos.Add(new PasoRelleno(x, sy, orden++));

                    // Arriba
                    if (sy > 0 && matriz[sy - 1, x] == VACIA && !agregado[sy - 1, x])
                    {
                        agregado[sy - 1, x] = true;
                        cola.Enqueue((x, sy - 1));
                    }
                    // Abajo
                    if (sy < ROWS - 1 && matriz[sy + 1, x] == VACIA && !agregado[sy + 1, x])
                    {
                        agregado[sy + 1, x] = true;
                        cola.Enqueue((x, sy + 1));
                    }
                }
            }

            return new ResultadoRelleno(semX, semY, pasos);
        }

        private static (int X, int Y)[] ObtenerVerticesPentagono()
        {
            int cx = 11, cy = 7;
            double r = 5.5;
            var v = new (int, int)[5];
            for (int i = 0; i < 5; i++)
            {
                double ang = Math.PI / 2 + i * 2 * Math.PI / 5;
                v[i] = ((int)Math.Round(cx + r * Math.Cos(ang)),
                        (int)Math.Round(cy - r * Math.Sin(ang)));
            }
            return v;
        }

        private static (int X, int Y)[] ObtenerVerticesHexagono()
        {
            int cx = 11, cy = 7;
            double r = 5.5;
            var v = new (int, int)[6];
            for (int i = 0; i < 6; i++)
            {
                double ang = i * Math.PI / 3;
                v[i] = ((int)Math.Round(cx + r * Math.Cos(ang)),
                        (int)Math.Round(cy - r * Math.Sin(ang)));
            }
            return v;
        }

        private static void DibujarPoligono(byte[,] m, (int X, int Y)[] vertices)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                var (x1, y1) = vertices[i];
                var (x2, y2) = vertices[(i + 1) % vertices.Length];
                DibujarLinea(m, x1, y1, x2, y2);
            }
        }

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

    internal enum TipoFiguraScanline { Pentagono, Hexagono }
}
