using System;
using System.Collections.Generic;
using GeometriaComputacional.Conicas.PolarCirculo.Modelo;

namespace GeometriaComputacional.Conicas.PolarCirculo.Controlador
{
    internal class PolarCirculoController
    {
        public ResultadoPolarCirculo Calcular(int xc, int yc, int radio)
        {
            if (radio <= 0)
                throw new ArgumentException("El radio debe ser mayor que cero.", nameof(radio));

            var pasos = new List<PasoPolarCirculo>(360);
            var vistos = new HashSet<(int, int)>();

            for (int theta = 0; theta < 360; theta++)
            {
                double rad = theta * Math.PI / 180.0;
                int px = (int)Math.Round(xc + radio * Math.Cos(rad));
                int py = (int)Math.Round(yc + radio * Math.Sin(rad));

                pasos.Add(new PasoPolarCirculo(theta, radio, px, py));
                vistos.Add((px, py));
            }

            return new ResultadoPolarCirculo(xc, yc, radio, vistos.Count, pasos);
        }
    }
}
