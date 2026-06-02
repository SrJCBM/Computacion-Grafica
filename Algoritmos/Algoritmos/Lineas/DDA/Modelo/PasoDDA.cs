using System;

namespace GeometriaComputacional.Lineas.DDA.Modelo
{
    internal class PasoDDA
    {
        public int Indice { get; }
        public float XReal { get; }
        public float YReal { get; }
        public int XPixel { get; }
        public int YPixel { get; }

        public PasoDDA(int indice, float xReal, float yReal)
        {
            Indice = indice;
            XReal = xReal;
            YReal = yReal;
            XPixel = (int)Math.Round(xReal);
            YPixel = (int)Math.Round(yReal);
        }

        public string PixelFormateado => $"({XPixel}, {YPixel})";
    }
}
