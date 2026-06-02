using System;
using System.Collections.Generic;
using GeometriaComputacional.Lineas.PuntoMedioLinea.Modelo;

namespace GeometriaComputacional.Lineas.PuntoMedioLinea.Controlador
{
    internal class PuntoMedioLineaController
    {
        public ResultadoPuntoMedioLinea Calcular(PuntoLinea inicio, PuntoLinea fin)
        {
            ArgumentNullException.ThrowIfNull(inicio);
            ArgumentNullException.ThrowIfNull(fin);

            int dx = Math.Abs(fin.X - inicio.X);
            int dy = Math.Abs(fin.Y - inicio.Y);
            int sx = inicio.X < fin.X ? 1 : -1;
            int sy = inicio.Y < fin.Y ? 1 : -1;
            bool dominanteX = dx >= dy;
            int decision = dominanteX ? 2 * dy - dx : 2 * dx - dy;
            int x = inicio.X;
            int y = inicio.Y;
            int pasos = Math.Max(dx, dy);
            var puntos = new List<PasoPuntoMedioLinea>();

            for (int i = 0; i <= pasos; i++)
            {
                puntos.Add(new PasoPuntoMedioLinea(i, x, y, decision, decision < 0 ? "Pixel recto" : "Pixel diagonal"));

                if (x == fin.X && y == fin.Y)
                {
                    break;
                }

                if (dominanteX)
                {
                    x += sx;
                    if (decision < 0)
                    {
                        decision += 2 * dy;
                    }
                    else
                    {
                        y += sy;
                        decision += 2 * (dy - dx);
                    }
                }
                else
                {
                    y += sy;
                    if (decision < 0)
                    {
                        decision += 2 * dx;
                    }
                    else
                    {
                        x += sx;
                        decision += 2 * (dx - dy);
                    }
                }
            }

            int deltaX = fin.X - inicio.X;
            int deltaY = fin.Y - inicio.Y;
            float pendiente = deltaX == 0 ? float.PositiveInfinity : (float)deltaY / deltaX;

            return new ResultadoPuntoMedioLinea(inicio, fin, deltaX, deltaY, pendiente, pasos, puntos);
        }
    }
}
