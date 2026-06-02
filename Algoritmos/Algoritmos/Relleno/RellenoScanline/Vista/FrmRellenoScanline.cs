using System;
using System.Drawing;
using System.Windows.Forms;
using GeometriaComputacional.Relleno.RellenoScanline.Controlador;
using GeometriaComputacional.Relleno.RellenoScanline.Modelo;

namespace GeometriaComputacional
{
    public partial class FrmRellenoScanline : Form
    {
        private readonly RellenoScanlineController controlador = new();

        private byte[,]? matriz;
        private ResultadoRelleno? resultado;
        private int pasoActual;
        private int semX, semY;

        private const int COLS = RellenoScanlineController.COLS;
        private const int ROWS = RellenoScanlineController.ROWS;

        private static readonly Color ColorBorde   = Color.FromArgb(17, 24, 39);
        private static readonly Color ColorRelleno = Color.FromArgb(233, 213, 255);   // lila claro
        private static readonly Color ColorSemilla = Color.FromArgb(147, 51, 234);
        private static readonly Color ColorLineas  = Color.FromArgb(209, 213, 219);
        private static readonly Color ColorTexto   = Color.FromArgb(30, 41, 59);

        public FrmRellenoScanline()
        {
            InitializeComponent();
            DoubleBuffered = true;
            semX = (int)nudX.Value;
            semY = (int)nudY.Value;
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            pnlContenedor.Paint      += PnlContenedor_Paint;
            pnlContenedor.Resize     += (_, _) => pnlContenedor.Invalidate();
            pnlContenedor.MouseClick += PnlContenedor_MouseClick;

            btnAplicarInicio.Click += (_, _) => AplicarSemilla();
            btnDibujarFigura.Click += (_, _) => DibujarFigura();
            btnRellenar.Click      += (_, _) => IniciarRelleno();
            btnParar.Click         += (_, _) => DetenerAnimacion();
            btnLimpiar.Click       += (_, _) => Limpiar();
            btnSalir.Click         += (_, _) => Close();
            timer1.Tick            += Timer_Tick;
        }

        private void AplicarSemilla()
        {
            semX = (int)nudX.Value;
            semY = (int)nudY.Value;
            pnlContenedor.Invalidate();
            ActualizarEstado("Semilla aplicada.");
        }

        private void DibujarFigura()
        {
            DetenerAnimacion();
            resultado = null; pasoActual = 0;
            var tipo = rbPentagono.Checked ? TipoFiguraScanline.Pentagono : TipoFiguraScanline.Hexagono;
            matriz = controlador.GenerarMatriz(tipo);
            ActualizarEstado("Figura dibujada.");
            ActualizarPasos(0);
            pnlContenedor.Invalidate();
        }

        private void IniciarRelleno()
        {
            if (matriz == null) DibujarFigura();
            var tipo = rbPentagono.Checked ? TipoFiguraScanline.Pentagono : TipoFiguraScanline.Hexagono;
            var copia = controlador.GenerarMatriz(tipo);
            resultado = controlador.Calcular(copia, semX, semY);
            matriz = copia;
            pasoActual = 0;

            if (resultado.TotalCeldas == 0)
            { ActualizarEstado("Semilla sobre borde o fuera de figura."); return; }

            timer1.Interval = Math.Max(1, (int)nudDelay.Value);
            timer1.Start();
            ActualizarEstado("Rellenando...");
        }

        private void DetenerAnimacion()
        {
            timer1.Stop();
            ActualizarEstado(resultado != null
                ? $"Animacion detenida. Celdas pintadas: {pasoActual}"
                : "Animacion detenida.");
        }

        private void Limpiar()
        {
            DetenerAnimacion();
            matriz = null; resultado = null; pasoActual = 0;
            ActualizarEstado("Listo."); ActualizarPasos(0);
            pnlContenedor.Invalidate();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (resultado == null || matriz == null || pasoActual >= resultado.Pasos.Count)
            {
                timer1.Stop();
                ActualizarEstado($"Relleno completo. Celdas pintadas: {resultado?.TotalCeldas}");
                return;
            }
            var paso = resultado.Pasos[pasoActual];
            if (paso.Y < ROWS && paso.X < COLS) matriz[paso.Y, paso.X] = 2;
            pasoActual++;
            ActualizarPasos(pasoActual);
            pnlContenedor.Invalidate();
        }

        private void PnlContenedor_MouseClick(object? sender, MouseEventArgs e)
        {
            if (timer1.Enabled || matriz == null) return;
            ObtenerTamanoCelda(out int tam, out int ox, out int oy);
            int col = (e.X - ox) / tam, row = (e.Y - oy) / tam;
            if (col < 0 || col >= COLS || row < 0 || row >= ROWS) return;
            if (matriz[row, col] != RellenoScanlineController.VACIA) return;
            semX = col; semY = row;
            nudX.Value = semX; nudY.Value = semY;
            pnlContenedor.Invalidate();
            ActualizarEstado($"Semilla movida a ({semX}, {semY}).");
        }

        private void PnlContenedor_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            ObtenerTamanoCelda(out int tam, out int ox, out int oy);
            DibujarTitulo(g, ox);
            DibujarCeldas(g, tam, ox, oy);
            DibujarOrden(g, tam, ox, oy);
            DibujarEjes(g, tam, ox, oy);
        }

        private void ObtenerTamanoCelda(out int tam, out int ox, out int oy)
        {
            int mi = 32, ms = 50, md = 10, mib = 28;
            tam = Math.Max(10, Math.Min(
                (pnlContenedor.ClientSize.Width - mi - md) / COLS,
                (pnlContenedor.ClientSize.Height - ms - mib) / ROWS));
            ox = mi; oy = ms;
        }

        private void DibujarTitulo(Graphics g, int ox)
        {
            using Font f = new("Segoe UI", 10, FontStyle.Bold);
            g.DrawString($"Matriz {COLS} x {ROWS}", f, new SolidBrush(ColorTexto), ox, 8);
            using Font f2 = new("Segoe UI", 8.5F);
            g.DrawString("Click en una celda vacia para cambiar la semilla.",
                f2, new SolidBrush(Color.FromArgb(107, 114, 128)), ox, 28);
        }

        private void DibujarCeldas(Graphics g, int tam, int ox, int oy)
        {
            using Pen pen = new(ColorLineas, 1);
            using Brush bBorde = new SolidBrush(ColorBorde);
            using Brush bRell  = new SolidBrush(ColorRelleno);
            using Brush bVacia = new SolidBrush(Color.White);
            using Brush bSem   = new SolidBrush(ColorSemilla);

            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS; c++)
                {
                    int px = ox + c * tam, py = oy + r * tam;
                    var rect = new Rectangle(px, py, tam, tam);
                    byte st = matriz != null ? matriz[r, c] : (byte)0;
                    Brush br = st switch { 1 => bBorde, 2 => bRell, _ => bVacia };
                    if (st == 0 && c == semX && r == semY && !timer1.Enabled && resultado == null)
                        br = bSem;
                    g.FillRectangle(br, rect);
                    g.DrawRectangle(pen, rect);
                }

            if (resultado != null)
            {
                using Pen pSem = new(ColorSemilla, 2);
                g.DrawRectangle(pSem, ox + semX * tam + 1, oy + semY * tam + 1, tam - 2, tam - 2);
            }
        }

        private void DibujarOrden(Graphics g, int tam, int ox, int oy)
        {
            if (resultado == null || pasoActual == 0) return;
            using Font f = new("Segoe UI", tam >= 22 ? 7.5F : 6F);
            using Brush b = new SolidBrush(ColorTexto);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            int mostrar = Math.Min(pasoActual, resultado.Pasos.Count);
            for (int i = 0; i < mostrar; i++)
            {
                var p = resultado.Pasos[i];
                g.DrawString(p.Orden.ToString(), f, b,
                    new RectangleF(ox + p.X * tam, oy + p.Y * tam, tam, tam), sf);
            }
        }

        private void DibujarEjes(Graphics g, int tam, int ox, int oy)
        {
            using Font f = new("Segoe UI", 7.5F);
            using Brush b = new SolidBrush(Color.FromArgb(100, 116, 139));
            for (int c = 0; c < COLS; c++)
                g.DrawString(c.ToString(), f, b, ox + c * tam + tam / 2 - 6, oy + ROWS * tam + 4);
            for (int r = 0; r < ROWS; r++)
                g.DrawString(r.ToString(), f, b, ox - 24, oy + r * tam + tam / 2 - 8);
        }

        private void ActualizarEstado(string t) => lblEstado.Text = t;
        private void ActualizarPasos(int n) => lblPasos.Text = $"Pasos: {n}";
    }
}
