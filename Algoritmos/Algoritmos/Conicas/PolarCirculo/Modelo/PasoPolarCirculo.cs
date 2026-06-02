namespace GeometriaComputacional.Conicas.PolarCirculo.Modelo
{
    internal class PasoPolarCirculo
    {
        public int Theta { get; }
        public int R { get; }
        public int PixelX { get; }
        public int PixelY { get; }

        public string PuntoFormateado => $"({PixelX}, {PixelY})";

        public PasoPolarCirculo(int theta, int r, int pixelX, int pixelY)
        {
            Theta = theta;
            R = r;
            PixelX = pixelX;
            PixelY = pixelY;
        }
    }
}
