using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using GeometriaComputacional.Conicas.ParametricoCirculo.Controlador;
using GeometriaComputacional.Conicas.ParametricoCirculo.Modelo;

namespace GeometriaComputacional
{
    public partial class FrmParametricoCirculo : Form
    {
        private readonly ParametricoCirculoController controlador = new();
        private int? xCentro, yCentro, radio;
        private bool arrastrando, arrastandoCentro, desplazandoPlano;
        private int pasosVisibles, escala = 24;
        private Point ultimoPuntoMouse;
        private PointF desplazamientoPlano;
        private ResultadoParametricoCirculo? resultado;

        private const int EscalaInicial = 24;
        private const int EscalaMinima = 10;
        private const int EscalaMaxima = 72;
        private const float ToleranciaClick = 0.8f;

        public FrmParametricoCirculo()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ConfigurarVista();
            RenombrarCards();
            LimpiarResultados();
        }

        private void RenombrarCards()
        {
            lblDeltaX.Text = "Xc";
            lblDeltaY.Text = "Yc";
            lblPendiente.Text = "Radio";
            lblKPasos.Text = "Puntos";
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

            btnCalcularRecta.Click += BtnCalcular_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            btnPaso.Click += BtnPaso_Click;
            btnCompleto.Click += BtnCompleto_Click;
            btnZoomIn.Click += (_, _) => CambiarZoom(4);
            btnZoomOut.Click += (_, _) => CambiarZoom(-4);
            btnZoomReset.Click += (_, _) => RestablecerZoom();
            btnSalir.Click += (_, _) => Close();
        }

        private PointF ObtenerOrigen()
            => new(pnlContenedor.ClientSize.Width / 2f + desplazamientoPlano.X,
                   pnlContenedor.ClientSize.Height / 2f + desplazamientoPlano.Y);

        private PointF CartesianoAPantalla(float x, float y)
        {
            PointF o = ObtenerOrigen();
            return new(o.X + x * escala, o.Y - y * escala);
        }

        private PointF PantallaACartesiano(Point p)
        {
            PointF o = ObtenerOrigen();
            return new((p.X - o.X) / escala, (o.Y - p.Y) / escala);
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

            if (e.Button != MouseButtons.Left) return;

            PointF coord = PantallaACartesiano(e.Location);
            int cx = (int)Math.Round(coord.X), cy = (int)Math.Round(coord.Y);

            if (xCentro == null)
            {
                xCentro = cx;
                yCentro = cy;
            }
            else if (radio == null)
            {
                int dx = cx - xCentro.Value, dy = cy - yCentro!.Value;
                radio = Math.Max(1, (int)Math.Round(Math.Sqrt(dx * dx + dy * dy)));
            }
            else
            {
                bool cercaCentro = EstaProximo(coord.X, coord.Y, xCentro.Value, yCentro!.Value);
                bool cercaHandle = EstaProximo(coord.X, coord.Y, xCentro.Value + radio.Value, yCentro.Value);
                if (cercaCentro) { arrastrando = true; arrastandoCentro = true; }
                else if (cercaHandle) { arrastrando = true; arrastandoCentro = false; }
            }

            SincronizarEntradas();
            CalcularSiHayDatos(false);
            pnlContenedor.Invalidate();
        }

        private static bool EstaProximo(float ax, float ay, float bx, float by)
            => Math.Abs(ax - bx) < ToleranciaClick && Math.Abs(ay - by) < ToleranciaClick;

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

            if (!arrastrando) { ActualizarCursor(e.Location); return; }

            PointF coord = PantallaACartesiano(e.Location);
            int cx = (int)Math.Round(coord.X), cy = (int)Math.Round(coord.Y);

            if (arrastandoCentro)
            {
                xCentro = cx;
                yCentro = cy;
            }
            else
            {
                int dx = cx - xCentro!.Value, dy = cy - yCentro!.Value;
                radio = Math.Max(1, (int)Math.Round(Math.Sqrt(dx * dx + dy * dy)));
            }

            SincronizarEntradas();
            CalcularSiHayDatos(false);
            pnlContenedor.Invalidate();
        }

        private void PnlContenedor_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) desplazandoPlano = false;
            if (e.Button == MouseButtons.Left) arrastrando = false;
            ActualizarCursor(e.Location);
        }

        private void PnlContenedor_MouseWheel(object? sender, MouseEventArgs e)
            => CambiarZoom(e.Delta > 0 ? 4 : -4);

        private void BtnCalcular_Click(object? sender, EventArgs e)
        {
            if (!LeerDatosDesdeTexto())
            {
                MessageBox.Show("Ingresa Xc, Yc y radio antes de graficar.", "Datos incompletos");
                return;
            }
            CalcularSiHayDatos(true);
            pnlContenedor.Invalidate();
        }

        private void BtnPaso_Click(object? sender, EventArgs e)
        {
            if (resultado == null)
            {
                if (!LeerDatosDesdeTexto())
                {
                    MessageBox.Show("Ingresa los datos antes de avanzar paso a paso.", "Datos incompletos");
                    return;
                }
                CalcularSiHayDatos(false);
            }

            if (resultado == null) return;
            pasosVisibles = Math.Min(pasosVisibles + 1, resultado.Pasos.Count);
            LlenarTabla();
            pnlContenedor.Invalidate();
        }

        private void BtnCompleto_Click(object? sender, EventArgs e)
        {
            if (!LeerDatosDesdeTexto())
            {
                MessageBox.Show("Ingresa los datos antes de mostrar el circulo.", "Datos incompletos");
                return;
            }
            CalcularSiHayDatos(true);
            pnlContenedor.Invalidate();
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            xCentro = yCentro = radio = null;
            resultado = null;
            arrastrando = desplazandoPlano = false;
            pasosVisibles = 0;
            escala = EscalaInicial;
            desplazamientoPlano = PointF.Empty;
            txtX1.Clear(); txtY1.Clear(); txtX2.Clear();
            LimpiarResultados();
            pnlContenedor.Invalidate();
        }

        private bool LeerDatosDesdeTexto()
        {
            if (!TryParseInt(txtX1.Text, out int xc) ||
                !TryParseInt(txtY1.Text, out int yc) ||
                !TryParseInt(txtX2.Text, out int r) ||
                r <= 0)
                return false;

            xCentro = xc;
            yCentro = yc;
            radio = r;
            return true;
        }

        private static bool TryParseInt(string texto, out int valor)
        {
            if (int.TryParse(texto, out valor)) return true;
            if (float.TryParse(texto, NumberStyles.Float, CultureInfo.InvariantCulture, out float f))
            {
                valor = (int)Math.Round(f);
                return true;
            }
            valor = 0;
            return false;
        }

        private void CalcularSiHayDatos(bool mostrarCompleto)
        {
            if (xCentro == null || yCentro == null || radio == null) return;
            resultado = controlador.Calcular(xCentro.Value, yCentro.Value, radio.Value);
            pasosVisibles = mostrarCompleto ? resultado.Pasos.Count : Math.Min(pasosVisibles, resultado.Pasos.Count);
            ActualizarResumen();
            LlenarTabla();
        }

        private void SincronizarEntradas()
        {
            if (xCentro.HasValue) txtX1.Text = xCentro.Value.ToString();
            if (yCentro.HasValue) txtY1.Text = yCentro.Value.ToString();
            if (radio.HasValue) txtX2.Text = radio.Value.ToString();
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
            if (resultado == null) { LimpiarResultados(); return; }
            lblResultadoDeltaX.Text = resultado.Xc.ToString();
            lblResultadoDeltaY.Text = resultado.Yc.ToString();
            lblResultadoPendiente.Text = resultado.Radio.ToString();
            lblResultadoKPasos.Text = resultado.TotalPuntosUnicos.ToString();
        }

        private void LlenarTabla()
        {
            dataGridView1.Rows.Clear();
            if (resultado == null) return;

            int cantidad = Math.Min(pasosVisibles, resultado.Pasos.Count);
            for (int i = 0; i < cantidad; i++)
            {
                var paso = resultado.Pasos[i];
                dataGridView1.Rows.Add(paso.XReal.ToString("F2"), paso.YReal.ToString("F2"),
                    paso.Angulo, paso.PuntoFormateado);
            }
        }

        private void PnlContenedor_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            DibujarPlanoCartesiano(g);
            DibujarCirculo(g);
            DibujarLeyenda(g);
        }

        private void DibujarPlanoCartesiano(Graphics g)
        {
            int ancho = pnlContenedor.ClientSize.Width;
            int alto = pnlContenedor.ClientSize.Height;
            PointF origen = ObtenerOrigen();

            using Pen penGrilla = new(Color.FromArgb(226, 232, 240), 1);
            for (float x = origen.X % escala; x <= ancho; x += escala)
                g.DrawLine(penGrilla, x, 0, x, alto);
            for (float y = origen.Y % escala; y <= alto; y += escala)
                g.DrawLine(penGrilla, 0, y, ancho, y);

            using Pen penEje = new(Color.FromArgb(100, 116, 139), 1.5f);
            g.DrawLine(penEje, origen.X, 0, origen.X, alto);
            g.DrawLine(penEje, 0, origen.Y, ancho, origen.Y);

            using Font font = new("Segoe UI", 8);
            using Brush texto = new SolidBrush(Color.FromArgb(71, 85, 105));
            g.DrawString("X", font, texto, ancho - 22, origen.Y + 6);
            g.DrawString("Y", font, texto, origen.X + 6, 8);
        }

        private void DibujarCirculo(Graphics g)
        {
            if (xCentro == null) { DibujarAyuda(g); return; }

            DibujarPuntoControl(g, xCentro.Value, yCentro!.Value, Color.FromArgb(22, 163, 74), "Centro");

            if (radio == null) return;

            PointF pCentro = CartesianoAPantalla(xCentro.Value, yCentro.Value);
            float radioPixeles = radio.Value * escala;
            using Pen penIdeal = new(Color.FromArgb(160, 37, 99, 235), 1.5f);
            g.DrawEllipse(penIdeal, pCentro.X - radioPixeles, pCentro.Y - radioPixeles, radioPixeles * 2, radioPixeles * 2);

            PointF pHandle = CartesianoAPantalla(xCentro.Value + radio.Value, yCentro.Value);
            using Pen penLinea = new(Color.FromArgb(150, 100, 116, 139), 1);
            g.DrawLine(penLinea, pCentro, pHandle);
            DibujarPuntoControl(g, xCentro.Value + radio.Value, yCentro.Value, Color.FromArgb(245, 158, 11), $"r={radio}");

            if (resultado == null) return;

            int cantidad = Math.Min(pasosVisibles, resultado.Pasos.Count);
            using Brush pixel = new SolidBrush(Color.FromArgb(200, 22, 163, 74));
            using Pen bordePixel = new(Color.White, 1);

            for (int i = 0; i < cantidad; i++)
            {
                var paso = resultado.Pasos[i];
                PointF pt = CartesianoAPantalla(paso.PixelX, paso.PixelY);
                RectangleF celda = new(pt.X - escala / 2f, pt.Y - escala / 2f, escala, escala);
                g.FillRectangle(pixel, celda);
                g.DrawRectangle(bordePixel, celda.X, celda.Y, celda.Width, celda.Height);
            }
        }

        private void DibujarPuntoControl(Graphics g, int x, int y, Color color, string etiqueta)
        {
            PointF pt = CartesianoAPantalla(x, y);
            using Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, pt.X - 6, pt.Y - 6, 12, 12);
            using Font font = new("Segoe UI", 9, FontStyle.Bold);
            using Brush texto = new SolidBrush(Color.FromArgb(15, 23, 42));
            g.DrawString($"{etiqueta} ({x}, {y})", font, texto, pt.X + 10, pt.Y - 22);
        }

        private void DibujarLeyenda(Graphics g)
        {
            RectangleF caja = new(pnlContenedor.ClientSize.Width - 350, 18, 322, 58);
            if (caja.X < 18) caja.X = 18;

            using Brush fondo = new SolidBrush(Color.FromArgb(245, 248, 252));
            using Pen borde = new(Color.FromArgb(203, 213, 225), 1);
            g.FillRectangle(fondo, caja);
            g.DrawRectangle(borde, caja.X, caja.Y, caja.Width, caja.Height);

            using Font titulo = new("Segoe UI", 8, FontStyle.Bold);
            using Brush texto = new SolidBrush(Color.FromArgb(30, 41, 59));
            g.DrawString("Leyenda", titulo, texto, caja.X + 14, caja.Y + 8);

            DibujarItemLeyenda(g, caja.X + 14, caja.Y + 34, Color.FromArgb(22, 163, 74), "Centro");
            DibujarItemLeyenda(g, caja.X + 94, caja.Y + 34, Color.FromArgb(245, 158, 11), "Handle radio");
            DibujarItemLeyenda(g, caja.X + 214, caja.Y + 34, Color.FromArgb(200, 22, 163, 74), "Pixel t");
        }

        private static void DibujarItemLeyenda(Graphics g, float x, float y, Color color, string textoItem)
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
            g.DrawString("Clic para colocar el centro. Luego clic para definir el radio.", font, texto, 24, 24);
        }

        private void ActualizarCursor(Point ubicacion)
        {
            if (xCentro == null || radio == null) { pnlContenedor.Cursor = Cursors.Cross; return; }
            PointF coord = PantallaACartesiano(ubicacion);
            bool cerca = EstaProximo(coord.X, coord.Y, xCentro.Value, yCentro!.Value) ||
                         EstaProximo(coord.X, coord.Y, xCentro.Value + radio.Value, yCentro.Value);
            pnlContenedor.Cursor = cerca ? Cursors.Hand : Cursors.Cross;
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
            => lblZoom.Text = $"Zoom {escala * 100 / EscalaInicial}%";
    }
}
