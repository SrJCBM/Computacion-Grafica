namespace GeometriaComputacional.Relleno.RellenoPorPila.Modelo
{
    internal class PasoRelleno
    {
        public int X { get; }
        public int Y { get; }
        public int Orden { get; }

        public PasoRelleno(int x, int y, int orden)
        {
            X = x;
            Y = y;
            Orden = orden;
        }
    }
}
