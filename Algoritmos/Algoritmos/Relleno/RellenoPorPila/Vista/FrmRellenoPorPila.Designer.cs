namespace GeometriaComputacional
{
    partial class FrmRellenoPorPila
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            pnlControles = new Panel();
            lblFigura = new Label();
            rbTriangulo = new RadioButton();
            rbRombo = new RadioButton();
            lblDelay = new Label();
            nudDelay = new NumericUpDown();
            lblPuntoInicial = new Label();
            lblX = new Label();
            nudX = new NumericUpDown();
            lblY = new Label();
            nudY = new NumericUpDown();
            btnAplicarInicio = new Button();
            btnDibujarFigura = new Button();
            btnRellenar = new Button();
            btnParar = new Button();
            btnLimpiar = new Button();
            lblSeparador = new Label();
            lblPasos = new Label();
            lblEstado = new Label();
            btnSalir = new Button();
            pnlContenedor = new Panel();
            timer1 = new System.Windows.Forms.Timer(components);

            pnlControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            SuspendLayout();

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitulo.Location = new Point(18, 18);
            lblTitulo.Text = "Relleno por pila";

            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 9.5F);
            lblSubtitulo.ForeColor = Color.FromArgb(75, 85, 99);
            lblSubtitulo.Location = new Point(22, 62);
            lblSubtitulo.Text = "Relleno por pila (DFS) con animacion paso a paso.";

            pnlControles.BackColor = Color.White;
            pnlControles.BorderStyle = BorderStyle.FixedSingle;
            pnlControles.Location = new Point(18, 95);
            pnlControles.Size = new Size(270, 600);
            pnlControles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlControles.Controls.AddRange(new Control[] {
                lblFigura, rbTriangulo, rbRombo, lblDelay, nudDelay,
                lblPuntoInicial, lblX, nudX, lblY, nudY,
                btnAplicarInicio, btnDibujarFigura, btnRellenar, btnParar,
                btnLimpiar, lblSeparador, lblPasos, lblEstado, btnSalir });

            lblFigura.AutoSize = true;
            lblFigura.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFigura.Location = new Point(16, 18);
            lblFigura.Text = "Figura";

            rbTriangulo.AutoSize = true;
            rbTriangulo.Checked = true;
            rbTriangulo.Font = new Font("Segoe UI", 9.5F);
            rbTriangulo.Location = new Point(20, 46);
            rbTriangulo.Text = "Triangulo";

            rbRombo.AutoSize = true;
            rbRombo.Font = new Font("Segoe UI", 9.5F);
            rbRombo.Location = new Point(120, 46);
            rbRombo.Text = "Rombo";

            lblDelay.AutoSize = true;
            lblDelay.Font = new Font("Segoe UI", 9.5F);
            lblDelay.ForeColor = Color.FromArgb(55, 65, 81);
            lblDelay.Location = new Point(16, 88);
            lblDelay.Text = "Delay por celda (ms)";

            nudDelay.Font = new Font("Segoe UI", 10F);
            nudDelay.Location = new Point(20, 112);
            nudDelay.Maximum = 2000; nudDelay.Minimum = 0; nudDelay.Value = 80;
            nudDelay.Size = new Size(100, 30);

            lblPuntoInicial.AutoSize = true;
            lblPuntoInicial.Font = new Font("Segoe UI", 9.5F);
            lblPuntoInicial.ForeColor = Color.FromArgb(55, 65, 81);
            lblPuntoInicial.Location = new Point(16, 158);
            lblPuntoInicial.Text = "Punto inicial";

            lblX.AutoSize = true; lblX.Font = new Font("Segoe UI", 9.5F);
            lblX.Location = new Point(20, 184); lblX.Text = "X";

            nudX.Font = new Font("Segoe UI", 10F);
            nudX.Location = new Point(36, 180);
            nudX.Maximum = 22; nudX.Minimum = 0; nudX.Value = 11;
            nudX.Size = new Size(72, 30);

            lblY.AutoSize = true; lblY.Font = new Font("Segoe UI", 9.5F);
            lblY.Location = new Point(124, 184); lblY.Text = "Y";

            nudY.Font = new Font("Segoe UI", 10F);
            nudY.Location = new Point(140, 180);
            nudY.Maximum = 14; nudY.Minimum = 0; nudY.Value = 7;
            nudY.Size = new Size(72, 30);

            btnAplicarInicio.BackColor = Color.FromArgb(8, 145, 178);
            btnAplicarInicio.FlatAppearance.BorderSize = 0;
            btnAplicarInicio.FlatStyle = FlatStyle.Flat;
            btnAplicarInicio.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAplicarInicio.ForeColor = Color.White;
            btnAplicarInicio.Location = new Point(16, 226);
            btnAplicarInicio.Size = new Size(236, 38);
            btnAplicarInicio.Text = "Aplicar inicio";

            btnDibujarFigura.BackColor = Color.FromArgb(37, 99, 235);
            btnDibujarFigura.FlatAppearance.BorderSize = 0;
            btnDibujarFigura.FlatStyle = FlatStyle.Flat;
            btnDibujarFigura.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDibujarFigura.ForeColor = Color.White;
            btnDibujarFigura.Location = new Point(16, 274);
            btnDibujarFigura.Size = new Size(236, 38);
            btnDibujarFigura.Text = "Dibujar figura";

            btnRellenar.BackColor = Color.FromArgb(22, 163, 74);
            btnRellenar.FlatAppearance.BorderSize = 0;
            btnRellenar.FlatStyle = FlatStyle.Flat;
            btnRellenar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRellenar.ForeColor = Color.White;
            btnRellenar.Location = new Point(16, 322);
            btnRellenar.Size = new Size(236, 38);
            btnRellenar.Text = "Rellenar con delay";

            btnParar.BackColor = Color.FromArgb(220, 38, 38);
            btnParar.FlatAppearance.BorderSize = 0;
            btnParar.FlatStyle = FlatStyle.Flat;
            btnParar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnParar.ForeColor = Color.White;
            btnParar.Location = new Point(16, 370);
            btnParar.Size = new Size(236, 38);
            btnParar.Text = "Parar";

            btnLimpiar.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 9.5F);
            btnLimpiar.ForeColor = Color.FromArgb(31, 41, 55);
            btnLimpiar.Location = new Point(16, 418);
            btnLimpiar.Size = new Size(236, 38);
            btnLimpiar.Text = "Limpiar";

            lblSeparador.BorderStyle = BorderStyle.Fixed3D;
            lblSeparador.Location = new Point(16, 470);
            lblSeparador.Size = new Size(236, 2);

            lblPasos.AutoSize = true;
            lblPasos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPasos.ForeColor = Color.FromArgb(17, 24, 39);
            lblPasos.Location = new Point(16, 482);
            lblPasos.Text = "Pasos: 0";

            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 9F);
            lblEstado.ForeColor = Color.FromArgb(107, 114, 128);
            lblEstado.Location = new Point(16, 510);
            lblEstado.Text = "Animacion detenida.";

            btnSalir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSalir.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 9.5F);
            btnSalir.ForeColor = Color.FromArgb(31, 41, 55);
            btnSalir.Location = new Point(16, 550);
            btnSalir.Size = new Size(100, 34);
            btnSalir.Text = "Salir";

            pnlContenedor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenedor.BackColor = Color.White;
            pnlContenedor.BorderStyle = BorderStyle.FixedSingle;
            pnlContenedor.Cursor = Cursors.Hand;
            pnlContenedor.Location = new Point(304, 95);
            pnlContenedor.Size = new Size(880, 600);
            pnlContenedor.TabStop = true;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 246, 250);
            ClientSize = new Size(1200, 720);
            MinimumSize = new Size(960, 600);
            WindowState = FormWindowState.Maximized;
            Name = "FrmRellenoPorPila";
            Text = "Relleno por pila";
            Controls.AddRange(new Control[] { lblTitulo, lblSubtitulo, pnlControles, pnlContenedor });

            pnlControles.ResumeLayout(false);
            pnlControles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo, lblSubtitulo, lblFigura, lblDelay, lblPuntoInicial, lblX, lblY;
        private Label lblSeparador, lblPasos, lblEstado;
        private RadioButton rbTriangulo, rbRombo;
        private NumericUpDown nudDelay, nudX, nudY;
        private Button btnAplicarInicio, btnDibujarFigura, btnRellenar, btnParar, btnLimpiar, btnSalir;
        private Panel pnlControles, pnlContenedor;
        private System.Windows.Forms.Timer timer1;
    }
}
