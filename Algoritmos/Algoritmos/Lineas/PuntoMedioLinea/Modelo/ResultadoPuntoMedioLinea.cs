using System.Collections.Generic;

namespace GeometriaComputacional.Lineas.PuntoMedioLinea.Modelo
{
    internal class ResultadoPuntoMedioLinea
    {
        public PuntoLinea Inicio { get; }
        public PuntoLinea Fin { get; }
        public int DeltaX { get; }
        public int DeltaY { get; }
        public float Pendiente { get; }
        public int KPasos { get; }
        public IReadOnlyList<PasoPuntoMedioLinea> Pasos { get; }

        public ResultadoPuntoMedioLinea(PuntoLinea inicio, PuntoLinea fin, int deltaX, int deltaY, float pendiente, int kPasos, IReadOnlyList<PasoPuntoMedioLinea> pasos)
        {
            Inicio = inicio;
            Fin = fin;
            DeltaX = deltaX;
            DeltaY = deltaY;
            Pendiente = pendiente;
            KPasos = kPasos;
            Pasos = pasos;
        }
    }
}
