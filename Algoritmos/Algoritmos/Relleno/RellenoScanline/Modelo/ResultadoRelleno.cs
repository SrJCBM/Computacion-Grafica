using System.Collections.Generic;

namespace GeometriaComputacional.Relleno.RellenoScanline.Modelo
{
    internal class ResultadoRelleno
    {
        public int SemillaX { get; }
        public int SemillaY { get; }
        public int TotalCeldas { get; }
        public IReadOnlyList<PasoRelleno> Pasos { get; }

        public ResultadoRelleno(int semillaX, int semillaY, IReadOnlyList<PasoRelleno> pasos)
        {
            SemillaX = semillaX;
            SemillaY = semillaY;
            TotalCeldas = pasos.Count;
            Pasos = pasos;
        }
    }
}
