namespace GeometriaComputacional.Lineas.Bresenham.Modelo
{
    internal class PasoBresenham
    {
        public int Indice { get; }
        public int XReal { get; }
        public int YReal { get; }
        public int XPixel => XReal;
        public int YPixel => YReal;
        public int Error { get; }
        public string Decision { get; }

        public PasoBresenham(int indice, int x, int y, int error, string decision)
        {
            Indice = indice;
            XReal = x;
            YReal = y;
            Error = error;
            Decision = decision;
        }

        public string PixelFormateado => $"({XPixel}, {YPixel})";
    }
}
