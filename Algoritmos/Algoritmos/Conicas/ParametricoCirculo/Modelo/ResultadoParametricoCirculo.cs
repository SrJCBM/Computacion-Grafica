using System.Collections.Generic;

namespace GeometriaComputacional.Conicas.ParametricoCirculo.Modelo
{
    internal class ResultadoParametricoCirculo
    {
        public int Xc { get; }
        public int Yc { get; }
        public int Radio { get; }
        public int TotalPuntosUnicos { get; }
        public IReadOnlyList<PasoParametricoCirculo> Pasos { get; }

        public ResultadoParametricoCirculo(int xc, int yc, int radio, int totalUnicos, IReadOnlyList<PasoParametricoCirculo> pasos)
        {
            Xc = xc;
            Yc = yc;
            Radio = radio;
            TotalPuntosUnicos = totalUnicos;
            Pasos = pasos;
        }
    }
}
