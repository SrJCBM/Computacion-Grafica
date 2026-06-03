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
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitulo.Location = new Point(18, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(272, 46);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Relleno por pila";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 9.5F);
            lblSubtitulo.ForeColor = Color.FromArgb(75, 85, 99);
            lblSubtitulo.Location = new Point(22, 62);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(355, 21);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Relleno por pila (DFS) con animacion paso a paso.";
            // 
            // pnlControles
            // 
            pnlControles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlControles.BackColor = Color.White;
            pnlControles.BorderStyle = BorderStyle.FixedSingle;
            pnlControles.Controls.Add(lblFigura);
            pnlControles.Controls.Add(rbTriangulo);
            pnlControles.Controls.Add(rbRombo);
            pnlControles.Controls.Add(lblDelay);
            pnlControles.Controls.Add(nudDelay);
            pnlControles.Controls.Add(lblPuntoInicial);
            pnlControles.Controls.Add(lblX);
            pnlControles.Controls.Add(nudX);
            pnlControles.Controls.Add(lblY);
            pnlControles.Controls.Add(nudY);
            pnlControles.Controls.Add(btnAplicarInicio);
            pnlControles.Controls.Add(btnDibujarFigura);
            pnlControles.Controls.Add(btnRellenar);
            pnlControles.Controls.Add(btnParar);
            pnlControles.Controls.Add(btnLimpiar);
            pnlControles.Controls.Add(lblSeparador);
            pnlControles.Controls.Add(lblPasos);
            pnlControles.Controls.Add(lblEstado);
            pnlControles.Controls.Add(btnSalir);
            pnlControles.Location = new Point(18, 95);
            pnlControles.Name = "pnlControles";
            pnlControles.Size = new Size(280, 600);
            pnlControles.TabIndex = 2;
            // 
            // lblFigura
            // 
            lblFigura.AutoSize = true;
            lblFigura.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFigura.Location = new Point(16, 18);
            lblFigura.Name = "lblFigura";
            lblFigura.Size = new Size(61, 23);
            lblFigura.TabIndex = 0;
            lblFigura.Text = "Figura";
            // 
            // rbTriangulo
            // 
            rbTriangulo.AutoSize = true;
            rbTriangulo.Checked = true;
            rbTriangulo.Font = new Font("Segoe UI", 9.5F);
            rbTriangulo.Location = new Point(20, 46);
            rbTriangulo.Name = "rbTriangulo";
            rbTriangulo.Size = new Size(96, 25);
            rbTriangulo.TabIndex = 1;
            rbTriangulo.TabStop = true;
            rbTriangulo.Text = "Triangulo";
            // 
            // rbRombo
            // 
            rbRombo.AutoSize = true;
            rbRombo.Font = new Font("Segoe UI", 9.5F);
            rbRombo.Location = new Point(120, 46);
            rbRombo.Name = "rbRombo";
            rbRombo.Size = new Size(82, 25);
            rbRombo.TabIndex = 2;
            rbRombo.Text = "Rombo";
            // 
            // lblDelay
            // 
            lblDelay.AutoSize = true;
            lblDelay.Font = new Font("Segoe UI", 9.5F);
            lblDelay.ForeColor = Color.FromArgb(55, 65, 81);
            lblDelay.Location = new Point(16, 88);
            lblDelay.Name = "lblDelay";
            lblDelay.Size = new Size(152, 21);
            lblDelay.TabIndex = 3;
            lblDelay.Text = "Delay por celda (ms)";
            // 
            // nudDelay
            // 
            nudDelay.Font = new Font("Segoe UI", 10F);
            nudDelay.Location = new Point(20, 112);
            nudDelay.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            nudDelay.Name = "nudDelay";
            nudDelay.Size = new Size(100, 30);
            nudDelay.TabIndex = 4;
            nudDelay.Value = new decimal(new int[] { 80, 0, 0, 0 });
            // 
            // lblPuntoInicial
            // 
            lblPuntoInicial.AutoSize = true;
            lblPuntoInicial.Font = new Font("Segoe UI", 9.5F);
            lblPuntoInicial.ForeColor = Color.FromArgb(55, 65, 81);
            lblPuntoInicial.Location = new Point(16, 158);
            lblPuntoInicial.Name = "lblPuntoInicial";
            lblPuntoInicial.Size = new Size(95, 21);
            lblPuntoInicial.TabIndex = 5;
            lblPuntoInicial.Text = "Punto inicial";
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.Font = new Font("Segoe UI", 9.5F);
            lblX.Location = new Point(20, 184);
            lblX.Name = "lblX";
            lblX.Size = new Size(19, 21);
            lblX.TabIndex = 6;
            lblX.Text = "X";
            // 
            // nudX
            // 
            nudX.Font = new Font("Segoe UI", 10F);
            nudX.Location = new Point(36, 180);
            nudX.Maximum = new decimal(new int[] { 22, 0, 0, 0 });
            nudX.Name = "nudX";
            nudX.Size = new Size(72, 30);
            nudX.TabIndex = 7;
            nudX.Value = new decimal(new int[] { 11, 0, 0, 0 });
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.Font = new Font("Segoe UI", 9.5F);
            lblY.Location = new Point(124, 184);
            lblY.Name = "lblY";
            lblY.Size = new Size(19, 21);
            lblY.TabIndex = 8;
            lblY.Text = "Y";
            // 
            // nudY
            // 
            nudY.Font = new Font("Segoe UI", 10F);
            nudY.Location = new Point(140, 180);
            nudY.Maximum = new decimal(new int[] { 14, 0, 0, 0 });
            nudY.Name = "nudY";
            nudY.Size = new Size(72, 30);
            nudY.TabIndex = 9;
            nudY.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // btnAplicarInicio
            // 
            btnAplicarInicio.BackColor = Color.FromArgb(8, 145, 178);
            btnAplicarInicio.FlatAppearance.BorderSize = 0;
            btnAplicarInicio.FlatStyle = FlatStyle.Flat;
            btnAplicarInicio.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAplicarInicio.ForeColor = Color.White;
            btnAplicarInicio.Location = new Point(16, 226);
            btnAplicarInicio.Name = "btnAplicarInicio";
            btnAplicarInicio.Size = new Size(236, 38);
            btnAplicarInicio.TabIndex = 10;
            btnAplicarInicio.Text = "Aplicar inicio";
            btnAplicarInicio.UseVisualStyleBackColor = false;
            // 
            // btnDibujarFigura
            // 
            btnDibujarFigura.BackColor = Color.FromArgb(37, 99, 235);
            btnDibujarFigura.FlatAppearance.BorderSize = 0;
            btnDibujarFigura.FlatStyle = FlatStyle.Flat;
            btnDibujarFigura.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDibujarFigura.ForeColor = Color.White;
            btnDibujarFigura.Location = new Point(16, 274);
            btnDibujarFigura.Name = "btnDibujarFigura";
            btnDibujarFigura.Size = new Size(236, 38);
            btnDibujarFigura.TabIndex = 11;
            btnDibujarFigura.Text = "Dibujar figura";
            btnDibujarFigura.UseVisualStyleBackColor = false;
            // 
            // btnRellenar
            // 
            btnRellenar.BackColor = Color.FromArgb(22, 163, 74);
            btnRellenar.FlatAppearance.BorderSize = 0;
            btnRellenar.FlatStyle = FlatStyle.Flat;
            btnRellenar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRellenar.ForeColor = Color.White;
            btnRellenar.Location = new Point(16, 322);
            btnRellenar.Name = "btnRellenar";
            btnRellenar.Size = new Size(236, 38);
            btnRellenar.TabIndex = 12;
            btnRellenar.Text = "Rellenar con delay";
            btnRellenar.UseVisualStyleBackColor = false;
            // 
            // btnParar
            // 
            btnParar.BackColor = Color.FromArgb(220, 38, 38);
            btnParar.FlatAppearance.BorderSize = 0;
            btnParar.FlatStyle = FlatStyle.Flat;
            btnParar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnParar.ForeColor = Color.White;
            btnParar.Location = new Point(16, 370);
            btnParar.Name = "btnParar";
            btnParar.Size = new Size(236, 38);
            btnParar.TabIndex = 13;
            btnParar.Text = "Parar";
            btnParar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            btnLimpiar.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 9.5F);
            btnLimpiar.ForeColor = Color.FromArgb(31, 41, 55);
            btnLimpiar.Location = new Point(16, 418);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(236, 38);
            btnLimpiar.TabIndex = 14;
            btnLimpiar.Text = "Limpiar";
            // 
            // lblSeparador
            // 
            lblSeparador.BorderStyle = BorderStyle.Fixed3D;
            lblSeparador.Location = new Point(16, 470);
            lblSeparador.Name = "lblSeparador";
            lblSeparador.Size = new Size(236, 2);
            lblSeparador.TabIndex = 15;
            // 
            // lblPasos
            // 
            lblPasos.AutoSize = true;
            lblPasos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPasos.ForeColor = Color.FromArgb(17, 24, 39);
            lblPasos.Location = new Point(16, 482);
            lblPasos.Name = "lblPasos";
            lblPasos.Size = new Size(83, 25);
            lblPasos.TabIndex = 16;
            lblPasos.Text = "Pasos: 0";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 9F);
            lblEstado.ForeColor = Color.FromArgb(107, 114, 128);
            lblEstado.Location = new Point(16, 510);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(146, 20);
            lblEstado.TabIndex = 17;
            lblEstado.Text = "Animacion detenida.";
            // 
            // btnSalir
            // 
            btnSalir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSalir.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 9.5F);
            btnSalir.ForeColor = Color.FromArgb(31, 41, 55);
            btnSalir.Location = new Point(16, 550);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(100, 34);
            btnSalir.TabIndex = 18;
            btnSalir.Text = "Salir";
            // 
            // pnlContenedor
            // 
            pnlContenedor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenedor.BackColor = Color.White;
            pnlContenedor.BorderStyle = BorderStyle.FixedSingle;
            pnlContenedor.Cursor = Cursors.Hand;
            pnlContenedor.Location = new Point(304, 95);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(880, 600);
            pnlContenedor.TabIndex = 3;
            pnlContenedor.TabStop = true;
            // 
            // FrmRellenoPorPila
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 246, 250);
            ClientSize = new Size(1200, 720);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(pnlControles);
            Controls.Add(pnlContenedor);
            MinimumSize = new Size(960, 600);
            Name = "FrmRellenoPorPila";
            Text = "Relleno por pila";
            WindowState = FormWindowState.Maximized;
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
