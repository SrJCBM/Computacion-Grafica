using System;
using System.Collections.Generic;
using GeometriaComputacional.Lineas.Bresenham.Modelo;

namespace GeometriaComputacional.Lineas.Bresenham.Controlador
{
    internal class BresenhamController
    {
        public ResultadoBresenham Calcular(PuntoLinea inicio, PuntoLinea fin)
        {
            ArgumentNullException.ThrowIfNull(inicio);
            ArgumentNullException.ThrowIfNull(fin);

            int dx = Math.Abs(fin.X - inicio.X);
            int dy = Math.Abs(fin.Y - inicio.Y);
            int sx = inicio.X < fin.X ? 1 : -1;
            int sy = inicio.Y < fin.Y ? 1 : -1;
            int error = dx - dy;
            int x = inicio.X;
            int y = inicio.Y;
            int pasos = Math.Max(dx, dy);
            var puntos = new List<PasoBresenham>();

            for (int i = 0; ; i++)
            {
                puntos.Add(new PasoBresenham(i, x, y, error, "Pintar pixel actual"));

                if (x == fin.X && y == fin.Y)
                {
                    break;
                }

                int error2 = 2 * error;
                string decision = string.Empty;

                if (error2 > -dy)
                {
                    error -= dy;
                    x += sx;
                    decision = "Avanza en X";
                }

                if (error2 < dx)
                {
                    error += dx;
                    y += sy;
                    decision = string.IsNullOrEmpty(decision) ? "Avanza en Y" : "Avanza en X e Y";
                }
            }

            int deltaX = fin.X - inicio.X;
            int deltaY = fin.Y - inicio.Y;
            float pendiente = deltaX == 0 ? float.PositiveInfinity : (float)deltaY / deltaX;

            return new ResultadoBresenham(inicio, fin, deltaX, deltaY, pendiente, pasos, puntos);
        }
    }
}
