using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExamenBlacioJulio
{
    public partial class FrmMargarita : Form
    {
        private float ladoBase;
        private float escala = 1f;
        private float desplazamientoX;
        private float desplazamientoY;
        private float rotacionGrados;
        private const float PasoTraslacion = 10f;
        private const float PasoRotacion = 5f;

        public FrmMargarita()
        {
            InitializeComponent();
            this.KeyPreview = true;
            trackBar1.Minimum = 2;
            trackBar1.Maximum = 30;
            trackBar1.Value = 10;
            trackBar1.TickFrequency = 2;
            lblEscalaValor.Text = "1.0x";
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(txtAncho.Text, out ladoBase) || ladoBase <= 0)
            {
                MessageBox.Show("Ingresa un tamaño válido mayor que 0.");
                return;
            }

            lblResultado.Text = $"Tamaño base: {ladoBase:0.##}";
            pnlCanvas.Invalidate();
        }

        private void BtnResetear_Click(object sender, EventArgs e)
        {
            txtAncho.Clear();
            lblResultado.Text = "Tamaño base:";
            ladoBase = 0;
            escala = 1f;
            desplazamientoX = 0f;
            desplazamientoY = 0f;
            rotacionGrados = 0f;
            trackBar1.Value = 10;
            lblEscalaValor.Text = "1.0x";
            pnlCanvas.Invalidate();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            escala = trackBar1.Value / 10f;
            lblEscalaValor.Text = $"{escala:0.0}x";
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            if (ladoBase <= 0)
            {
                return;
            }

            float squareSize = Math.Min(pnlCanvas.Width, pnlCanvas.Height) * 0.78f;
            RectangleF square = new RectangleF(
                (pnlCanvas.Width - squareSize) / 2f,
                (pnlCanvas.Height - squareSize) / 2f,
                squareSize,
                squareSize);

            PointF centro = new PointF(square.Left + square.Width / 2f, square.Top + square.Height / 2f);

            float sidePx = ladoBase * 11.4f * escala;
            float radioPetaloEntrada = sidePx / (2f * (float)Math.Sin(Math.PI / 5f));
            float radioPetalo = Math.Min(radioPetaloEntrada, squareSize * 0.19f);
            float radioEstructura = radioPetalo * 1.10f;
            float radioCentroVacio = radioPetalo * 0.66f;

            using (Pen penCuadro = new Pen(Color.Gray, 2f))
            using (Brush red = new SolidBrush(Color.Red))
            using (Brush white = new SolidBrush(Color.White))
            {
                g.DrawRectangle(penCuadro, square.X, square.Y, square.Width, square.Height);

                GraphicsState state = g.Save();
                g.TranslateTransform(centro.X + desplazamientoX, centro.Y + desplazamientoY);
                g.RotateTransform(rotacionGrados);

                PointF centroLocal = PointF.Empty;
                PointF[] centrosPetalos = PolygonGeometry.CreateRegularPolygon(centroLocal, radioEstructura, 5, -90f);

                for (int i = 0; i < 5; i++)
                {
                    float orientacion = -90f + (i * 72f);
                    PointF[] petalo = PolygonGeometry.CreateRegularPolygon(centrosPetalos[i], radioPetalo, 5, orientacion);
                    g.FillPolygon(red, petalo);
                }

                PointF[] huecoCentral = PolygonGeometry.CreateRegularPolygon(centroLocal, radioCentroVacio, 5, -54f);
                g.FillPolygon(white, huecoCentral);

                g.Restore(state);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool handled = true;

            switch (keyData)
            {
                case Keys.W:
                    desplazamientoY -= PasoTraslacion;
                    break;
                case Keys.S:
                    desplazamientoY += PasoTraslacion;
                    break;
                case Keys.A:
                    desplazamientoX -= PasoTraslacion;
                    break;
                case Keys.D:
                    desplazamientoX += PasoTraslacion;
                    break;
                case Keys.Left:
                case Keys.Up:
                    rotacionGrados -= PasoRotacion;
                    break;
                case Keys.Right:
                case Keys.Down:
                    rotacionGrados += PasoRotacion;
                    break;
                default:
                    handled = false;
                    break;
            }

            if (handled)
            {
                pnlCanvas.Invalidate();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
