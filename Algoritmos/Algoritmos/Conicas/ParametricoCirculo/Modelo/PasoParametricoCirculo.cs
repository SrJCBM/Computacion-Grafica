namespace GeometriaComputacional.Conicas.ParametricoCirculo.Modelo
{
    internal class PasoParametricoCirculo
    {
        public int Angulo { get; }
        public float XReal { get; }
        public float YReal { get; }
        public int PixelX { get; }
        public int PixelY { get; }

        public string PuntoFormateado => $"({PixelX}, {PixelY})";

        public PasoParametricoCirculo(int angulo, float xReal, float yReal, int pixelX, int pixelY)
        {
            Angulo = angulo;
            XReal = xReal;
            YReal = yReal;
            PixelX = pixelX;
            PixelY = pixelY;
        }
    }
}
