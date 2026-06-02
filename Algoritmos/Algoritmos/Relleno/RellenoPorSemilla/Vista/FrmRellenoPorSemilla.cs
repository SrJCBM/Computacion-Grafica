using System;
using System.Drawing;
using System.Windows.Forms;
using GeometriaComputacional.Relleno.RellenoPorSemilla.Controlador;
using GeometriaComputacional.Relleno.RellenoPorSemilla.Modelo;

namespace GeometriaComputacional
{
    public partial class FrmRellenoPorSemilla : Form
    {
        private readonly RellenoPorSemillaController controlador = new();

        private byte[,]? matriz;
        private ResultadoRelleno? resultado;
        private int pasoActual;
        private int semX, semY;

        private const int COLS = RellenoPorSemillaController.COLS;
        private const int ROWS = RellenoPorSemillaController.ROWS;

        // Colores
        private static readonly Color ColorBorde   = Color.FromArgb(17, 24, 39);
        private static readonly Color ColorRelleno = Color.FromArgb(186, 230, 253);
        private static readonly Color ColorSemilla = Color.FromArgb(8, 145, 178);
        private static readonly Color ColorLineas  = Color.FromArgb(209, 213, 219);
        private static readonly Color ColorTexto   = Color.FromArgb(30, 41, 59);

        public FrmRellenoPorSemilla()
        {
            InitializeComponent();
            DoubleBuffered = true;
            semX = (int)nudX.Value;
            semY = (int)nudY.Value;
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            pnlContenedor.Paint   += PnlContenedor_Paint;
            pnlContenedor.Resize  += (_, _) => pnlContenedor.Invalidate();
            pnlContenedor.MouseClick += PnlContenedor_MouseClick;

            btnAplicarInicio.Click += (_, _) => AplicarSemilla();
            btnDibujarFigura.Click += (_, _) => DibujarFigura();
            btnRellenar.Click      += (_, _) => IniciarRelleno();
            btnParar.Click         += (_, _) => DetenerAnimacion();
            btnLimpiar.Click       += (_, _) => Limpiar();
            btnSalir.Click         += (_, _) => Close();
            timer1.Tick            += Timer_Tick;
        }

        // ─── Acciones ───────────────────────────────────────────────────

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
            resultado = null;
            pasoActual = 0;
            var tipo = rbCuadrado.Checked ? TipoFiguraSemilla.Cuadrado : TipoFiguraSemilla.Circulo;
            matriz = controlador.GenerarMatriz(tipo);
            ActualizarEstado("Figura dibujada. Haz clic en una celda vacia o presiona Rellenar.");
            ActualizarPasos(0);
            pnlContenedor.Invalidate();
        }

        private void IniciarRelleno()
        {
            if (matriz == null) DibujarFigura();

            var tipo = rbCuadrado.Checked ? TipoFiguraSemilla.Cuadrado : TipoFiguraSemilla.Circulo;
            var matrizCopia = controlador.GenerarMatriz(tipo);
            resultado = controlador.Calcular(matrizCopia, semX, semY);
            matriz = matrizCopia;
            pasoActual = 0;

            if (resultado.TotalCeldas == 0)
            {
                ActualizarEstado("La semilla esta sobre un borde o fuera de la figura.");
                return;
            }

            timer1.Interval = Math.Max(1, (int)nudDelay.Value);
            timer1.Start();
            ActualizarEstado("Rellenando...");
        }

        private void DetenerAnimacion()
        {
            timer1.Stop();
            if (resultado != null)
                ActualizarEstado($"Animacion detenida. Celdas pintadas: {pasoActual}");
            else
                ActualizarEstado("Animacion detenida.");
        }

        private void Limpiar()
        {
            DetenerAnimacion();
            matriz = null;
            resultado = null;
            pasoActual = 0;
            ActualizarEstado("Listo.");
            ActualizarPasos(0);
            pnlContenedor.Invalidate();
        }

        // ─── Timer ──────────────────────────────────────────────────────

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (resultado == null || matriz == null || pasoActual >= resultado.Pasos.Count)
            {
                timer1.Stop();
                ActualizarEstado($"Relleno completo. Celdas pintadas: {resultado?.TotalCeldas}");
                return;
            }

            var paso = resultado.Pasos[pasoActual];
            if (paso.Y < ROWS && paso.X < COLS)
                matriz[paso.Y, paso.X] = 2;

            pasoActual++;
            ActualizarPasos(pasoActual);
            pnlContenedor.Refresh();
        }

        // ─── Mouse en matriz ────────────────────────────────────────────

        private void PnlContenedor_MouseClick(object? sender, MouseEventArgs e)
        {
            if (timer1.Enabled || matriz == null) return;

            ObtenerTamanoCelda(out int tam, out int oxm, out int oym);
            int col = (e.X - oxm) / tam;
            int row = (e.Y - oym) / tam;

            if (col < 0 || col >= COLS || row < 0 || row >= ROWS) return;
            if (matriz[row, col] != RellenoPorSemillaController.VACIA) return;

            semX = col; semY = row;
            nudX.Value = semX; nudY.Value = semY;
            pnlContenedor.Invalidate();
            ActualizarEstado($"Semilla movida a ({semX}, {semY}).");
        }

        // ─── Dibujo ─────────────────────────────────────────────────────

        private void PnlContenedor_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            ObtenerTamanoCelda(out int tam, out int oxm, out int oym);

            DibujarTitulo(g, oxm, oym);
            DibujarCeldas(g, tam, oxm, oym);
            DibujarOrden(g, tam, oxm, oym);
            DibujarEjes(g, tam, oxm, oym);
        }

        private void ObtenerTamanoCelda(out int tam, out int originX, out int originY)
        {
            int margenIzq = 32, margenSup = 50, margenDer = 10, margenInf = 28;
            int anchoDisponible = pnlContenedor.ClientSize.Width - margenIzq - margenDer;
            int altoDisponible  = pnlContenedor.ClientSize.Height - margenSup - margenInf;
            tam = Math.Max(10, Math.Min(anchoDisponible / COLS, altoDisponible / ROWS));
            originX = margenIzq;
            originY = margenSup;
        }

        private void DibujarTitulo(Graphics g, int ox, int oy)
        {
            using Font f = new("Segoe UI", 10, FontStyle.Bold);
            using Brush b = new SolidBrush(ColorTexto);
            g.DrawString($"Matriz {COLS} x {ROWS}", f, b, ox, 8);
            using Font f2 = new("Segoe UI", 8.5F);
            using Brush b2 = new SolidBrush(Color.FromArgb(107, 114, 128));
            g.DrawString("Click en una celda vacia para cambiar la semilla.", f2, b2, ox, 28);
        }

        private void DibujarCeldas(Graphics g, int tam, int ox, int oy)
        {
            using Pen penGrilla = new(ColorLineas, 1);
            using Brush brBorde   = new SolidBrush(ColorBorde);
            using Brush brRelleno = new SolidBrush(ColorRelleno);
            using Brush brVacia   = new SolidBrush(Color.White);
            using Brush brSemilla = new SolidBrush(ColorSemilla);

            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    int px = ox + c * tam;
                    int py = oy + r * tam;
                    var rect = new Rectangle(px, py, tam, tam);

                    byte estado = matriz != null ? matriz[r, c] : (byte)0;
                    Brush br = estado switch
                    {
                        1 => brBorde,
                        2 => brRelleno,
                        _ => brVacia
                    };

                    // Semilla sin relleno activo
                    if (estado == 0 && c == semX && r == semY && !timer1.Enabled && resultado == null)
                        br = brSemilla;

                    g.FillRectangle(br, rect);
                    g.DrawRectangle(penGrilla, rect);
                }
            }

            // Marcar semilla cuando hay resultado
            if (resultado != null)
            {
                int px = ox + semX * tam + 1;
                int py = oy + semY * tam + 1;
                using Pen penSem = new(ColorSemilla, 2);
                g.DrawRectangle(penSem, px, py, tam - 2, tam - 2);
            }
        }

        private void DibujarOrden(Graphics g, int tam, int ox, int oy)
        {
            if (resultado == null || pasoActual == 0) return;

            using Font f = new("Segoe UI", tam >= 22 ? 7.5F : 6F);
            using Brush b = new SolidBrush(ColorTexto);
            StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            int mostrar = Math.Min(pasoActual, resultado.Pasos.Count);
            for (int i = 0; i < mostrar; i++)
            {
                var p = resultado.Pasos[i];
                var rect = new RectangleF(ox + p.X * tam, oy + p.Y * tam, tam, tam);
                g.DrawString(p.Orden.ToString(), f, b, rect, sf);
            }
        }

        private void DibujarEjes(Graphics g, int tam, int ox, int oy)
        {
            using Font f = new("Segoe UI", 7.5F);
            using Brush b = new SolidBrush(Color.FromArgb(100, 116, 139));

            // Eje X (columnas) — debajo de la grilla
            for (int c = 0; c < COLS; c++)
            {
                int px = ox + c * tam + tam / 2;
                int py = oy + ROWS * tam + 4;
                g.DrawString(c.ToString(), f, b, px - 6, py);
            }

            // Eje Y (filas) — a la izquierda
            for (int r = 0; r < ROWS; r++)
            {
                int px = ox - 24;
                int py = oy + r * tam + tam / 2 - 8;
                g.DrawString(r.ToString(), f, b, px, py);
            }
        }

        // ─── Helpers ────────────────────────────────────────────────────

        private void ActualizarEstado(string texto) => lblEstado.Text = texto;
        private void ActualizarPasos(int n) => lblPasos.Text = $"Pasos: {n}";
    }
}
