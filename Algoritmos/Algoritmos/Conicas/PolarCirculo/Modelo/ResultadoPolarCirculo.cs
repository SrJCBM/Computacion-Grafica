using System.Collections.Generic;

namespace GeometriaComputacional.Conicas.PolarCirculo.Modelo
{
    internal class ResultadoPolarCirculo
    {
        public int Xc { get; }
        public int Yc { get; }
        public int Radio { get; }
        public int TotalPuntosUnicos { get; }
        public IReadOnlyList<PasoPolarCirculo> Pasos { get; }

        public ResultadoPolarCirculo(int xc, int yc, int radio, int totalUnicos, IReadOnlyList<PasoPolarCirculo> pasos)
        {
            Xc = xc;
            Yc = yc;
            Radio = radio;
            TotalPuntosUnicos = totalUnicos;
            Pasos = pasos;
        }
    }
}
