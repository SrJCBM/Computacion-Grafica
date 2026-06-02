using System.Collections.Generic;

namespace GeometriaComputacional.Lineas.Bresenham.Modelo
{
    internal class ResultadoBresenham
    {
        public PuntoLinea Inicio { get; }
        public PuntoLinea Fin { get; }
        public int DeltaX { get; }
        public int DeltaY { get; }
        public float Pendiente { get; }
        public int KPasos { get; }
        public IReadOnlyList<PasoBresenham> Pasos { get; }

        public ResultadoBresenham(PuntoLinea inicio, PuntoLinea fin, int deltaX, int deltaY, float pendiente, int kPasos, IReadOnlyList<PasoBresenham> pasos)
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
