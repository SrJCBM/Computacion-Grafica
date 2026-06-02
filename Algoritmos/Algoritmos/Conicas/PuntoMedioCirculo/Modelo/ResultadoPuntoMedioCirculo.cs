using System.Collections.Generic;

namespace GeometriaComputacional.Conicas.PuntoMedioCirculo.Modelo
{
    internal class ResultadoPuntoMedioCirculo
    {
        public int Xc { get; }
        public int Yc { get; }
        public int Radio { get; }
        public IReadOnlyList<PasoPuntoMedioCirculo> Pasos { get; }

        public ResultadoPuntoMedioCirculo(int xc, int yc, int radio, IReadOnlyList<PasoPuntoMedioCirculo> pasos)
        {
            Xc = xc;
            Yc = yc;
            Radio = radio;
            Pasos = pasos;
        }
    }
}
