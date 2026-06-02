using System;
using System.Collections.Generic;
using GeometriaComputacional.Lineas.DDA.Modelo;

namespace GeometriaComputacional.Lineas.DDA.Controlador
{
    internal class DdaController
    {
        public ResultadoDDA Calcular(PuntoGeometrico inicio, PuntoGeometrico fin)
        {
            ArgumentNullException.ThrowIfNull(inicio);
            ArgumentNullException.ThrowIfNull(fin);

            float deltaX = fin.X - inicio.X;
            float deltaY = fin.Y - inicio.Y;
            int kPasos = (int)Math.Ceiling(Math.Max(Math.Abs(deltaX), Math.Abs(deltaY)));
            float pendiente = deltaX == 0 ? float.PositiveInfinity : deltaY / deltaX;

            if (kPasos == 0)
            {
                return new ResultadoDDA(
                    inicio,
                    fin,
                    deltaX,
                    deltaY,
                    pendiente,
                    0,
                    0,
                    0,
                    new List<PasoDDA> { new PasoDDA(0, inicio.X, inicio.Y) });
            }

            float incrementoX = deltaX / kPasos;
            float incrementoY = deltaY / kPasos;
            float x = inicio.X;
            float y = inicio.Y;
            var pasos = new List<PasoDDA>();

            for (int i = 0; i <= kPasos; i++)
            {
                pasos.Add(new PasoDDA(i, x, y));
                x += incrementoX;
                y += incrementoY;
            }

            return new ResultadoDDA(
                inicio,
                fin,
                deltaX,
                deltaY,
                pendiente,
                kPasos,
                incrementoX,
                incrementoY,
                pasos);
        }

        public string CrearExplicacion(ResultadoDDA resultado)
        {
            string pendiente = float.IsPositiveInfinity(resultado.Pendiente)
                ? "indefinida"
                : resultado.Pendiente.ToString("F2");

            return
                $"DDA calcula Delta X = X2 - X1 = {resultado.DeltaX:F2} y Delta Y = Y2 - Y1 = {resultado.DeltaY:F2}.\r\n" +
                $"La pendiente m = Delta Y / Delta X = {pendiente}.\r\n" +
                $"K toma el mayor valor absoluto entre Delta X y Delta Y (redondeo hacia arriba): K = {resultado.KPasos}.\r\n" +
                $"En cada paso se suma Xinc = Delta X / K = {resultado.IncrementoX:F2} y Yinc = Delta Y / K = {resultado.IncrementoY:F2}.\r\n" +
                "El pixel pintado se obtiene redondeando las coordenadas reales de cada iteracion.";
        }
    }
}
