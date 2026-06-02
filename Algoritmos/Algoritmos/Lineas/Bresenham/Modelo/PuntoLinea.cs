using System;

namespace GeometriaComputacional.Lineas.Bresenham.Modelo
{
    internal class PuntoLinea
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Nombre { get; set; }

        public PuntoLinea(int x, int y, string nombre)
        {
            X = x;
            Y = y;
            Nombre = nombre;
        }

        public bool EstaProximo(float x, float y, float tolerancia = 0.5f)
        {
            return Math.Abs(X - x) < tolerancia && Math.Abs(Y - y) < tolerancia;
        }
    }
}
