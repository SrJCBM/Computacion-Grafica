using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExamenBlacioJulio
{
    public partial class FrmGema : Form
    {
        private float ladoBase;
        private float escala = 1f;
        private float desplazamientoX;
        private float desplazamientoY;
        private float rotacionGrados;
        private const float PasoTraslacion = 10f;
        private const float PasoRotacion = 5f;

        public FrmGema()
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

            lblDescripcion.Text = $"Lado del decágono: {ladoBase:0.##}";
            pnlCanvas.Invalidate();
        }

        private void BtnResetear_Click(object sender, EventArgs e)
        {
            txtAncho.Clear();
            lblDescripcion.Text = "Lado del decágono:";
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

            float sidePx = ladoBase * 10.3f * escala;
            float outerRadiusBySide = sidePx / (2f * (float)Math.Sin(Math.PI / 10f));
            float outerRadius = Math.Min(outerRadiusBySide, Math.Min(pnlCanvas.Width, pnlCanvas.Height) * 0.31f);

            PointF c = new PointF(pnlCanvas.Width / 2f, pnlCanvas.Height / 2f);
            PointF[] decagon = PolygonGeometry.CreateRegularPolygon(PointF.Empty, outerRadius, 10, -90f);

            float rMidOuter = outerRadius * 0.73f;
            float rMidInner = outerRadius * 0.50f;
            float rNearCore = outerRadius * 0.30f;
            float rCore = outerRadius * 0.08f;

            using (Pen penOut = new Pen(Color.Black, 2f))
            using (Pen penBlue = new Pen(Color.Blue, 2f))
            {
                GraphicsState state = g.Save();
                g.TranslateTransform(c.X + desplazamientoX, c.Y + desplazamientoY);
                g.RotateTransform(rotacionGrados);

                g.DrawPolygon(penOut, decagon);

                for (int i = 0; i < 10; i++)
                {
                    float a0 = -90f + (i * 36f);
                    float aMid = a0 + 18f;
                    float a2 = a0 + 36f;

                    PointF v0 = PolygonGeometry.PointOnCircle(PointF.Empty, outerRadius, a0);
                    PointF v2 = PolygonGeometry.PointOnCircle(PointF.Empty, outerRadius, a2);

                    PointF m0 = PolygonGeometry.PointOnCircle(PointF.Empty, rMidOuter, aMid);
                    PointF lKnee = PolygonGeometry.PointOnCircle(PointF.Empty, rMidInner, a0 + 10f);
                    PointF rKnee = PolygonGeometry.PointOnCircle(PointF.Empty, rMidInner, a2 - 10f);
                    PointF m1 = PolygonGeometry.PointOnCircle(PointF.Empty, rNearCore, aMid);
                    PointF core = PolygonGeometry.PointOnCircle(PointF.Empty, rCore, aMid);

                    g.DrawLine(penBlue, v0, m0);
                    g.DrawLine(penBlue, m0, lKnee);
                    g.DrawLine(penBlue, lKnee, m1);
                    g.DrawLine(penBlue, m1, core);

                    g.DrawLine(penBlue, core, m1);
                    g.DrawLine(penBlue, m1, rKnee);
                    g.DrawLine(penBlue, rKnee, m0);
                    g.DrawLine(penBlue, m0, v2);
                }

                PointF[] ring = PolygonGeometry.CreateRegularPolygon(PointF.Empty, rNearCore, 10, -72f);
                g.DrawPolygon(penBlue, ring);

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
