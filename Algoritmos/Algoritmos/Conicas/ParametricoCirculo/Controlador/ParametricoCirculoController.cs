using System;
using System.Collections.Generic;
using GeometriaComputacional.Conicas.ParametricoCirculo.Modelo;

namespace GeometriaComputacional.Conicas.ParametricoCirculo.Controlador
{
    internal class ParametricoCirculoController
    {
        public ResultadoParametricoCirculo Calcular(int xc, int yc, int radio)
        {
            if (radio <= 0)
                throw new ArgumentException("El radio debe ser mayor que cero.", nameof(radio));

            var pasos = new List<PasoParametricoCirculo>(360);
            var vistos = new HashSet<(int, int)>();

            for (int t = 0; t < 360; t++)
            {
                double rad = t * Math.PI / 180.0;
                float xR = (float)(xc + radio * Math.Cos(rad));
                float yR = (float)(yc + radio * Math.Sin(rad));
                int px = (int)Math.Round(xR);
                int py = (int)Math.Round(yR);

                pasos.Add(new PasoParametricoCirculo(t, xR, yR, px, py));
                vistos.Add((px, py));
            }

            return new ResultadoParametricoCirculo(xc, yc, radio, vistos.Count, pasos);
        }
    }
}
