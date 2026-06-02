using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using GeometriaComputacional.Lineas.DDA.Controlador;
using GeometriaComputacional.Lineas.DDA.Modelo;

namespace GeometriaComputacional
{
    public partial class FrmDDA : Form
    {
        private readonly DdaController controlador = new();
        private PuntoGeometrico? p1;
        private PuntoGeometrico? p2;
        private PuntoGeometrico? puntoCapturado;
        private ResultadoDDA? resultado;
        private bool arrastrando;
        private bool desplazandoPlano;
        private int pasosVisibles;
        private int escala = 24;
        private Point ultimoPuntoMouse;
        private PointF desplazamientoPlano;

        private const int EscalaInicial = 24;
        private const int EscalaMinima = 10;
        private const int EscalaMaxima = 72;
        private const float ToleranciaClick = 0.8f;

        public FrmDDA()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ConfigurarVista();
            LimpiarResultados();
        }

        private void ConfigurarVista()
        {
            pnlContenedor.Paint += PnlContenedor_Paint;
            pnlContenedor.MouseDown += PnlContenedor_MouseDown;
            pnlContenedor.MouseMove += PnlContenedor_MouseMove;
            pnlContenedor.MouseUp += PnlContenedor_MouseUp;
            pnlContenedor.MouseWheel += PnlContenedor_MouseWheel;
            pnlContenedor.MouseEnter += (_, _) => pnlContenedor.Focus();
            pnlContenedor.Resize += (_, _) => pnlContenedor.Invalidate();

            btnCalcularRecta.Click += BtnCalcularRecta_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            btnPaso.Click += BtnPaso_Click;
            btnCompleto.Click += BtnCompleto_Click;
            btnZoomIn.Click += (_, _) => CambiarZoom(4);
            btnZoomOut.Click += (_, _) => CambiarZoom(-4);
            btnZoomReset.Click += (_, _) => RestablecerZoom();
            btnSalir.Click += (_, _) => Close();
        }

        private PointF ObtenerOrigen()
        {
            return new PointF(
                pnlContenedor.ClientSize.Width / 2f + desplazamientoPlano.X,
                pnlContenedor.ClientSize.Height / 2f + desplazamientoPlano.Y);
        }

        private PointF CartesianoAPantalla(float x, float y)
        {
            PointF origen = ObtenerOrigen();
            return new PointF(origen.X + x * escala, origen.Y - y * escala);
        }

        private PointF PantallaACartesiano(Point p)
        {
            PointF origen = ObtenerOrigen();
            return new PointF((p.X - origen.X) / escala, (origen.Y - p.Y) / escala);
        }

        private void PnlContenedor_MouseDown(object? sender, MouseEventArgs e)
        {
            pnlContenedor.Focus();

            if (e.Button == MouseButtons.Right)
            {
                desplazandoPlano = true;
                ultimoPuntoMouse = e.Location;
                pnlContenedor.Cursor = Cursors.SizeAll;
                return;
            }

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            PointF coordActual = PantallaACartesiano(e.Location);

            if (p1 == null)
            {
                p1 = new PuntoGeometrico((float)Math.Round(coordActual.X), (float)Math.Round(coordActual.Y), "P1");
            }
            else if (p2 == null)
            {
                p2 = new PuntoGeometrico((float)Math.Round(coordActual.X), (float)Math.Round(coordActual.Y), "P2");
            }
            else if (p1.EstaProximo(coordActual.X, coordActual.Y, ToleranciaClick))
            {
                puntoCapturado = p1;
                arrastrando = true;
            }
            else if (p2.EstaProximo(coordActual.X, coordActual.Y, ToleranciaClick))
            {
                puntoCapturado = p2;
                arrastrando = true;
            }

            SincronizarEntradas();
            CalcularSiHayDosPuntos(mostrarCompleto: false);
            pnlContenedor.Invalidate();
        }

        private void PnlContenedor_MouseMove(object? sender, MouseEventArgs e)
        {
            if (desplazandoPlano)
            {
                desplazamientoPlano.X += e.X - ultimoPuntoMouse.X;
                desplazamientoPlano.Y += e.Y - ultimoPuntoMouse.Y;
                ultimoPuntoMouse = e.Location;
                pnlContenedor.Invalidate();
                return;
            }

            if (!arrastrando || puntoCapturado == null)
            {
                ActualizarCursor(e.Location);
                return;
            }

            PointF coordActual = PantallaACartesiano(e.Location);
            puntoCapturado.X = (float)Math.Round(coordActual.X);
            puntoCapturado.Y = (float)Math.Round(coordActual.Y);

            SincronizarEntradas();
            CalcularSiHayDosPuntos(mostrarCompleto: false);
            pnlContenedor.Invalidate();
        }

        private void PnlContenedor_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                desplazandoPlano = false;
            }

            if (e.Button == MouseButtons.Left)
            {
                arrastrando = false;
                puntoCapturado = null;
            }

            ActualizarCursor(e.Location);
        }

        private void PnlContenedor_MouseWheel(object? sender, MouseEventArgs e)
        {
            CambiarZoom(e.Delta > 0 ? 4 : -4);
        }

        private void BtnCalcularRecta_Click(object? sender, EventArgs e)
        {
            if (!LeerPuntosDesdeTexto())
            {
                MessageBox.Show("Ingresa ambos puntos antes de graficar.", "Datos incompletos");
                return;
            }

            CalcularSiHayDosPuntos(mostrarCompleto: true);
            pnlContenedor.Invalidate();
        }

        private void BtnPaso_Click(object? sender, EventArgs e)
        {
            if (resultado == null)
            {
                if (!LeerPuntosDesdeTexto())
                {
                    MessageBox.Show("Ingresa ambos puntos antes de avanzar paso a paso.", "Datos incompletos");
                    return;
                }

                CalcularSiHayDosPuntos(mostrarCompleto: false);
            }

            if (resultado == null)
            {
                return;
            }

            pasosVisibles = Math.Min(pasosVisibles + 1, resultado.Pasos.Count);
            LlenarTablaDDA();
            pnlContenedor.Invalidate();
        }

        private void BtnCompleto_Click(object? sender, EventArgs e)
        {
            if (!LeerPuntosDesdeTexto())
            {
                MessageBox.Show("Ingresa ambos puntos antes de mostrar la recta.", "Datos incompletos");
                return;
            }

            CalcularSiHayDosPuntos(mostrarCompleto: true);
            pnlContenedor.Invalidate();
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            p1 = null;
            p2 = null;
            puntoCapturado = null;
            resultado = null;
            arrastrando = false;
            desplazandoPlano = false;
            pasosVisibles = 0;
            escala = EscalaInicial;
            desplazamientoPlano = PointF.Empty;

            txtX1.Clear();
            txtY1.Clear();
            txtX2.Clear();
            txtY2.Clear();
            LimpiarResultados();
            pnlContenedor.Invalidate();
        }

        private bool LeerPuntosDesdeTexto()
        {
            if (!TryParse(txtX1.Text, out float x1) ||
                !TryParse(txtY1.Text, out float y1) ||
                !TryParse(txtX2.Text, out float x2) ||
                !TryParse(txtY2.Text, out float y2))
            {
                return false;
            }

            p1 = new PuntoGeometrico(x1, y1, "P1");
            p2 = new PuntoGeometrico(x2, y2, "P2");
            return true;
        }

        private static bool TryParse(string texto, out float valor)
        {
            return float.TryParse(texto, NumberStyles.Float, CultureInfo.CurrentCulture, out valor) ||
                   float.TryParse(texto, NumberStyles.Float, CultureInfo.InvariantCulture, out valor);
        }

        private void CalcularSiHayDosPuntos(bool mostrarCompleto)
        {
            if (p1 == null || p2 == null)
            {
                return;
            }

            resultado = controlador.Calcular(p1, p2);
            pasosVisibles = mostrarCompleto ? resultado.Pasos.Count : Math.Min(pasosVisibles, resultado.Pasos.Count);
            ActualizarResumen();
            LlenarTablaDDA();
        }

        private void SincronizarEntradas()
        {
            if (p1 != null)
            {
                txtX1.Text = p1.X.ToString("0.##");
                txtY1.Text = p1.Y.ToString("0.##");
            }

            if (p2 != null)
            {
                txtX2.Text = p2.X.ToString("0.##");
                txtY2.Text = p2.Y.ToString("0.##");
            }
        }

        private void LimpiarResultados()
        {
            lblResultadoDeltaX.Text = "-";
            lblResultadoDeltaY.Text = "-";
            lblResultadoPendiente.Text = "-";
            lblResultadoKPasos.Text = "-";
            lblZoom.Text = $"Zoom {escala * 100 / EscalaInicial}%";
            dataGridView1.Rows.Clear();
        }

        private void ActualizarResumen()
        {
            if (resultado == null)
            {
                LimpiarResultados();
                return;
            }

            lblResultadoDeltaX.Text = resultado.DeltaX.ToString("F2");
            lblResultadoDeltaY.Text = resultado.DeltaY.ToString("F2");
            lblResultadoPendiente.Text = float.IsPositiveInfinity(resultado.Pendiente)
                ? "Indef."
                : resultado.Pendiente.ToString("F2");
            lblResultadoKPasos.Text = resultado.KPasos.ToString();
        }

        private void LlenarTablaDDA()
        {
            dataGridView1.Rows.Clear();

            if (resultado == null)
            {
                return;
            }

            int cantidad = Math.Min(pasosVisibles, resultado.Pasos.Count);
            for (int i = 0; i < cantidad; i++)
            {
                PasoDDA paso = resultado.Pasos[i];
                dataGridView1.Rows.Add(
                    paso.Indice,
                    paso.XReal.ToString("F2"),
                    paso.YReal.ToString("F2"),
                    paso.PixelFormateado);
            }
        }

        private void PnlContenedor_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            DibujarPlanoCartesiano(g);
            DibujarRectaDDA(g);
            DibujarLeyenda(g);
        }

        private void DibujarPlanoCartesiano(Graphics g)
        {
            int ancho = pnlContenedor.ClientSize.Width;
            int alto = pnlContenedor.ClientSize.Height;
            PointF origen = ObtenerOrigen();

            using Pen penGrilla = new(Color.FromArgb(226, 232, 240), 1);
            for (float x = origen.X % escala; x <= ancho; x += escala)
            {
                g.DrawLine(penGrilla, x, 0, x, alto);
            }

            for (float y = origen.Y % escala; y <= alto; y += escala)
            {
                g.DrawLine(penGrilla, 0, y, ancho, y);
            }

            using Pen penEje = new(Color.FromArgb(100, 116, 139), 1.5f);
            g.DrawLine(penEje, origen.X, 0, origen.X, alto);
            g.DrawLine(penEje, 0, origen.Y, ancho, origen.Y);

            using Font font = new("Segoe UI", 8);
            using Brush texto = new SolidBrush(Color.FromArgb(71, 85, 105));
            g.DrawString("X", font, texto, ancho - 22, origen.Y + 6);
            g.DrawString("Y", font, texto, origen.X + 6, 8);
        }

        private void DibujarRectaDDA(Graphics g)
        {
            if (p1 == null)
            {
                DibujarAyuda(g);
                return;
            }

            DibujarPuntoControl(g, p1, Color.FromArgb(22, 163, 74));

            if (p2 == null)
            {
                return;
            }

            DibujarPuntoControl(g, p2, Color.FromArgb(245, 158, 11));

            PointF pt1 = CartesianoAPantalla(p1.X, p1.Y);
            PointF pt2 = CartesianoAPantalla(p2.X, p2.Y);
            using Pen ideal = new(Color.FromArgb(37, 99, 235), 2);
            g.DrawLine(ideal, pt1, pt2);

            if (resultado == null)
            {
                return;
            }

            int cantidad = Math.Min(pasosVisibles, resultado.Pasos.Count);
            using Brush pixel = new SolidBrush(Color.FromArgb(220, 38, 38));
            using Pen bordePixel = new(Color.White, 1);

            for (int i = 0; i < cantidad; i++)
            {
                PasoDDA paso = resultado.Pasos[i];
                PointF punto = CartesianoAPantalla(paso.XPixel, paso.YPixel);
                RectangleF celda = new(punto.X - escala / 2f, punto.Y - escala / 2f, escala, escala);
                g.FillRectangle(pixel, celda);
                g.DrawRectangle(bordePixel, celda.X, celda.Y, celda.Width, celda.Height);
            }
        }

        private void DibujarPuntoControl(Graphics g, PuntoGeometrico punto, Color color)
        {
            PointF pantalla = CartesianoAPantalla(punto.X, punto.Y);
            using Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, pantalla.X - 6, pantalla.Y - 6, 12, 12);

            using Font font = new("Segoe UI", 9, FontStyle.Bold);
            using Brush texto = new SolidBrush(Color.FromArgb(15, 23, 42));
            g.DrawString($"{punto.Nombre} ({punto.X:0.##}, {punto.Y:0.##})", font, texto, pantalla.X + 10, pantalla.Y - 22);
        }

        private void DibujarLeyenda(Graphics g)
        {
            RectangleF caja = new(pnlContenedor.ClientSize.Width - 370, 18, 342, 58);
            if (caja.X < 18)
            {
                caja.X = 18;
            }

            using Brush fondo = new SolidBrush(Color.FromArgb(245, 248, 252));
            using Pen borde = new(Color.FromArgb(203, 213, 225), 1);
            g.FillRectangle(fondo, caja);
            g.DrawRectangle(borde, caja.X, caja.Y, caja.Width, caja.Height);

            using Font titulo = new("Segoe UI", 8, FontStyle.Bold);
            using Font item = new("Segoe UI", 8);
            using Brush texto = new SolidBrush(Color.FromArgb(30, 41, 59));
            g.DrawString("Leyenda", titulo, texto, caja.X + 14, caja.Y + 8);

            DibujarItemLeyenda(g, caja.X + 14, caja.Y + 34, Color.FromArgb(22, 163, 74), "P1");
            DibujarItemLeyenda(g, caja.X + 74, caja.Y + 34, Color.FromArgb(245, 158, 11), "P2");
            DibujarItemLeyenda(g, caja.X + 136, caja.Y + 34, Color.FromArgb(220, 38, 38), "Pixel DDA");

            using Pen linea = new(Color.FromArgb(37, 99, 235), 2);
            g.DrawLine(linea, caja.X + 246, caja.Y + 41, caja.X + 272, caja.Y + 41);
            g.DrawString("Recta ideal", item, texto, caja.X + 280, caja.Y + 32);
        }

        private void DibujarItemLeyenda(Graphics g, float x, float y, Color color, string textoItem)
        {
            using Brush marca = new SolidBrush(color);
            using Brush texto = new SolidBrush(Color.FromArgb(30, 41, 59));
            using Font font = new("Segoe UI", 8);
            g.FillRectangle(marca, x, y + 3, 10, 10);
            g.DrawString(textoItem, font, texto, x + 16, y - 1);
        }

        private void DibujarAyuda(Graphics g)
        {
            using Font font = new("Segoe UI", 10);
            using Brush texto = new SolidBrush(Color.FromArgb(71, 85, 105));
            g.DrawString("Haz clic en el plano para colocar Punto 1 y Punto 2.", font, texto, 24, 24);
        }

        private void ActualizarCursor(Point ubicacion)
        {
            if (arrastrando)
            {
                pnlContenedor.Cursor = Cursors.SizeAll;
                return;
            }

            PointF coordActual = PantallaACartesiano(ubicacion);
            bool sobrePunto = (p1 != null && p1.EstaProximo(coordActual.X, coordActual.Y, ToleranciaClick)) ||
                              (p2 != null && p2.EstaProximo(coordActual.X, coordActual.Y, ToleranciaClick));
            pnlContenedor.Cursor = sobrePunto ? Cursors.Hand : Cursors.Cross;
        }

        private void CambiarZoom(int delta)
        {
            escala = Math.Clamp(escala + delta, EscalaMinima, EscalaMaxima);
            ActualizarTextoZoom();
            pnlContenedor.Invalidate();
        }

        private void RestablecerZoom()
        {
            escala = EscalaInicial;
            desplazamientoPlano = PointF.Empty;
            ActualizarTextoZoom();
            pnlContenedor.Invalidate();
        }

        private void ActualizarTextoZoom()
        {
            lblZoom.Text = $"Zoom {escala * 100 / EscalaInicial}%";
        }

    }
}
