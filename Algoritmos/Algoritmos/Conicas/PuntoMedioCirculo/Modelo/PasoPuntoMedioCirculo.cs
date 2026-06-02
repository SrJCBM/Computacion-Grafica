using System.Collections.Generic;

namespace GeometriaComputacional.Conicas.PuntoMedioCirculo.Modelo
{
    internal class PasoPuntoMedioCirculo
    {
        public int Iteracion { get; }
        public int X { get; }
        public int Y { get; }
        public int P { get; }
        public string SimetriasTexto { get; }
        public IReadOnlyList<(int X, int Y)> Puntos { get; }

        public PasoPuntoMedioCirculo(int iteracion, int x, int y, int p, string simetriasTexto, IReadOnlyList<(int X, int Y)> puntos)
        {
            Iteracion = iteracion;
            X = x;
            Y = y;
            P = p;
            SimetriasTexto = simetriasTexto;
            Puntos = puntos;
        }
    }
}
