namespace GeometriaComputacional.Lineas.PuntoMedioLinea.Modelo
{
    internal class PasoPuntoMedioLinea
    {
        public int Indice { get; }
        public int XReal { get; }
        public int YReal { get; }
        public int XPixel => XReal;
        public int YPixel => YReal;
        public int Decision { get; }
        public string Criterio { get; }

        public PasoPuntoMedioLinea(int indice, int x, int y, int decision, string criterio)
        {
            Indice = indice;
            XReal = x;
            YReal = y;
            Decision = decision;
            Criterio = criterio;
        }

        public string PixelFormateado => $"({XPixel}, {YPixel})";
    }
}
