using System;

namespace GeometriaComputacional.Lineas.DDA.Modelo
{
    internal class PuntoGeometrico
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string Nombre { get; set; }

        public PuntoGeometrico(float x, float y, string nombre)
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
