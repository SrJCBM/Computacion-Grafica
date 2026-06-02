using System;
using System.Collections.Generic;
using GeometriaComputacional.Conicas.PuntoMedioCirculo.Modelo;

namespace GeometriaComputacional.Conicas.PuntoMedioCirculo.Controlador
{
    internal class PuntoMedioCirculoController
    {
        public ResultadoPuntoMedioCirculo Calcular(int xc, int yc, int radio)
        {
            if (radio <= 0)
                throw new ArgumentException("El radio debe ser mayor que cero.", nameof(radio));

            var pasos = new List<PasoPuntoMedioCirculo>();
            int x = 0, y = radio, p = 1 - radio, iter = 0;

            while (x <= y)
            {
                var puntos = ObtenerSimetrias(xc, yc, x, y);
                pasos.Add(new PasoPuntoMedioCirculo(iter, x, y, p, FormatearSimetrias(puntos), puntos));

                // Formulas del doc: p < 0 → Este; p >= 0 → Sureste (y decrementa antes de calcular p)
                if (p < 0)
                {
                    p += 2 * x + 3;
                }
                else
                {
                    y--;
                    p += 2 * (x - y) + 5;
                }
                x++;
                iter++;
            }

            return new ResultadoPuntoMedioCirculo(xc, yc, radio, pasos);
        }

        private static List<(int X, int Y)> ObtenerSimetrias(int xc, int yc, int x, int y)
        {
            var seen = new HashSet<(int, int)>();
            var result = new List<(int, int)>();
            foreach (var pt in new (int, int)[]
            {
                (xc + x, yc + y), (xc - x, yc + y), (xc + x, yc - y), (xc - x, yc - y),
                (xc + y, yc + x), (xc - y, yc + x), (xc + y, yc - x), (xc - y, yc - x)
            })
            {
                if (seen.Add(pt))
                    result.Add(pt);
            }
            return result;
        }

        private static string FormatearSimetrias(List<(int X, int Y)> puntos)
            => string.Join("  ", puntos.ConvertAll(p => $"({p.X},{p.Y})"));
    }
}
