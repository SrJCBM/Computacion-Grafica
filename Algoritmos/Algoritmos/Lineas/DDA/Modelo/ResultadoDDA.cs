using System.Collections.Generic;

namespace GeometriaComputacional.Lineas.DDA.Modelo
{
    internal class ResultadoDDA
    {
        public PuntoGeometrico Inicio { get; }
        public PuntoGeometrico Fin { get; }
        public float DeltaX { get; }
        public float DeltaY { get; }
        public float Pendiente { get; }
        public int KPasos { get; }
        public float IncrementoX { get; }
        public float IncrementoY { get; }
        public IReadOnlyList<PasoDDA> Pasos { get; }

        public ResultadoDDA(
            PuntoGeometrico inicio,
            PuntoGeometrico fin,
            float deltaX,
            float deltaY,
            float pendiente,
            int kPasos,
            float incrementoX,
            float incrementoY,
            IReadOnlyList<PasoDDA> pasos)
        {
            Inicio = inicio;
            Fin = fin;
            DeltaX = deltaX;
            DeltaY = deltaY;
            Pendiente = pendiente;
            KPasos = kPasos;
            IncrementoX = incrementoX;
            IncrementoY = incrementoY;
            Pasos = pasos;
        }
    }
}
