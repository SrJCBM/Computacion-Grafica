namespace GeometriaComputacional
{
    partial class FrmPuntoMedioCirculo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            pnlEntrada = new Panel();
            btnSalir = new Button();
            btnLimpiar = new Button();
            btnCalcularRecta = new Button();
            txtY2 = new TextBox();
            txtX2 = new TextBox();
            txtY1 = new TextBox();
            txtX1 = new TextBox();
            lblY2 = new Label();
            lblX2 = new Label();
            lblY1 = new Label();
            lblX1 = new Label();
            lblPunto2 = new Label();
            lblPunto1 = new Label();
            lblParametros = new Label();
            pnlResumen = new Panel();
            cardK = new Panel();
            lblResultadoKPasos = new Label();
            lblKPasos = new Label();
            cardPendiente = new Panel();
            lblResultadoPendiente = new Label();
            lblPendiente = new Label();
            cardDeltaY = new Panel();
            lblResultadoDeltaY = new Label();
            lblDeltaY = new Label();
            cardDeltaX = new Panel();
            lblResultadoDeltaX = new Label();
            lblDeltaX = new Label();
            lblResumen = new Label();
            lblPlano = new Label();
            pnlContenedor = new Panel();
            lblTabla = new Label();
            dataGridView1 = new DataGridView();
            colX = new DataGridViewTextBoxColumn();
            colY = new DataGridViewTextBoxColumn();
            colP = new DataGridViewTextBoxColumn();
            colSimetrias = new DataGridViewTextBoxColumn();
            btnPaso = new Button();
            btnCompleto = new Button();
            btnZoomOut = new Button();
            btnZoomReset = new Button();
            btnZoomIn = new Button();
            lblZoom = new Label();
            pnlEntrada.SuspendLayout();
            pnlResumen.SuspendLayout();
            cardK.SuspendLayout();
            cardPendiente.SuspendLayout();
            cardDeltaY.SuspendLayout();
            cardDeltaX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitulo.Location = new Point(34, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(438, 50);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Circunferencia por punto medio";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            lblSubtitulo.ForeColor = Color.FromArgb(75, 85, 99);
            lblSubtitulo.Location = new Point(38, 76);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(465, 23);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Conicas: x^2 + y^2 = r^2, calculo por octantes y simetria.";
            // 
            // pnlEntrada
            // 
            pnlEntrada.BackColor = Color.White;
            pnlEntrada.BorderStyle = BorderStyle.FixedSingle;
            pnlEntrada.Controls.Add(btnSalir);
            pnlEntrada.Controls.Add(btnLimpiar);
            pnlEntrada.Controls.Add(btnCalcularRecta);
            pnlEntrada.Controls.Add(txtY2);
            pnlEntrada.Controls.Add(txtX2);
            pnlEntrada.Controls.Add(txtY1);
            pnlEntrada.Controls.Add(txtX1);
            pnlEntrada.Controls.Add(lblY2);
            pnlEntrada.Controls.Add(lblX2);
            pnlEntrada.Controls.Add(lblY1);
            pnlEntrada.Controls.Add(lblX1);
            pnlEntrada.Controls.Add(lblPunto2);
            pnlEntrada.Controls.Add(lblPunto1);
            pnlEntrada.Controls.Add(lblParametros);
            pnlEntrada.Location = new Point(34, 120);
            pnlEntrada.Name = "pnlEntrada";
            pnlEntrada.Size = new Size(720, 199);
            pnlEntrada.TabIndex = 2;
            // 
            // btnSalir
            // 
            btnSalir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSalir.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 9.5F);
            btnSalir.ForeColor = Color.FromArgb(31, 41, 55);
            btnSalir.Location = new Point(596, 122);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(92, 38);
            btnSalir.TabIndex = 13;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 9.5F);
            btnLimpiar.ForeColor = Color.FromArgb(31, 41, 55);
            btnLimpiar.Location = new Point(148, 122);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(110, 38);
            btnLimpiar.TabIndex = 12;
            btnLimpiar.Text = "Reiniciar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnCalcularRecta
            // 
            btnCalcularRecta.BackColor = Color.FromArgb(37, 99, 235);
            btnCalcularRecta.FlatAppearance.BorderSize = 0;
            btnCalcularRecta.FlatStyle = FlatStyle.Flat;
            btnCalcularRecta.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCalcularRecta.ForeColor = Color.White;
            btnCalcularRecta.Location = new Point(24, 122);
            btnCalcularRecta.Name = "btnCalcularRecta";
            btnCalcularRecta.Size = new Size(118, 38);
            btnCalcularRecta.TabIndex = 11;
            btnCalcularRecta.Text = "Calcular Circulo";
            btnCalcularRecta.UseVisualStyleBackColor = false;
            // 
            // txtY2
            // 
            txtY2.Font = new Font("Segoe UI", 10F);
            txtY2.Location = new Point(598, 70);
            txtY2.Name = "txtY2";
            txtY2.Size = new Size(88, 30);
            txtY2.TabIndex = 10;
            txtY2.Visible = false;
            // 
            // txtX2
            // 
            txtX2.Font = new Font("Segoe UI", 10F);
            txtX2.Location = new Point(492, 70);
            txtX2.Name = "txtX2";
            txtX2.Size = new Size(88, 30);
            txtX2.TabIndex = 9;
            // 
            // txtY1
            // 
            txtY1.Font = new Font("Segoe UI", 10F);
            txtY1.Location = new Point(174, 70);
            txtY1.Name = "txtY1";
            txtY1.Size = new Size(88, 30);
            txtY1.TabIndex = 8;
            // 
            // txtX1
            // 
            txtX1.Font = new Font("Segoe UI", 10F);
            txtX1.Location = new Point(68, 70);
            txtX1.Name = "txtX1";
            txtX1.Size = new Size(88, 30);
            txtX1.TabIndex = 7;
            // 
            // lblY2
            // 
            lblY2.AutoSize = true;
            lblY2.Font = new Font("Segoe UI", 9F);
            lblY2.ForeColor = Color.FromArgb(75, 85, 99);
            lblY2.Location = new Point(598, 48);
            lblY2.Name = "lblY2";
            lblY2.Size = new Size(25, 20);
            lblY2.TabIndex = 6;
            lblY2.Text = "Y2";
            lblY2.Visible = false;
            // 
            // lblX2
            // 
            lblX2.AutoSize = true;
            lblX2.Font = new Font("Segoe UI", 9F);
            lblX2.ForeColor = Color.FromArgb(75, 85, 99);
            lblX2.Location = new Point(492, 48);
            lblX2.Name = "lblX2";
            lblX2.Size = new Size(14, 20);
            lblX2.TabIndex = 5;
            lblX2.Text = "r";
            // 
            // lblY1
            // 
            lblY1.AutoSize = true;
            lblY1.Font = new Font("Segoe UI", 9F);
            lblY1.ForeColor = Color.FromArgb(75, 85, 99);
            lblY1.Location = new Point(174, 48);
            lblY1.Name = "lblY1";
            lblY1.Size = new Size(26, 20);
            lblY1.TabIndex = 4;
            lblY1.Text = "Yc";
            // 
            // lblX1
            // 
            lblX1.AutoSize = true;
            lblX1.Font = new Font("Segoe UI", 9F);
            lblX1.ForeColor = Color.FromArgb(75, 85, 99);
            lblX1.Location = new Point(68, 48);
            lblX1.Name = "lblX1";
            lblX1.Size = new Size(26, 20);
            lblX1.TabIndex = 3;
            lblX1.Text = "Xc";
            // 
            // lblPunto2
            // 
            lblPunto2.AutoSize = true;
            lblPunto2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPunto2.ForeColor = Color.FromArgb(17, 24, 39);
            lblPunto2.Location = new Point(408, 74);
            lblPunto2.Name = "lblPunto2";
            lblPunto2.Size = new Size(49, 20);
            lblPunto2.TabIndex = 2;
            lblPunto2.Text = "Radio";
            // 
            // lblPunto1
            // 
            lblPunto1.AutoSize = true;
            lblPunto1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPunto1.ForeColor = Color.FromArgb(17, 24, 39);
            lblPunto1.Location = new Point(3, 74);
            lblPunto1.Name = "lblPunto1";
            lblPunto1.Size = new Size(54, 20);
            lblPunto1.TabIndex = 1;
            lblPunto1.Text = "Centro";
            // 
            // lblParametros
            // 
            lblParametros.AutoSize = true;
            lblParametros.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblParametros.ForeColor = Color.FromArgb(17, 24, 39);
            lblParametros.Location = new Point(20, 18);
            lblParametros.Name = "lblParametros";
            lblParametros.Size = new Size(114, 25);
            lblParametros.TabIndex = 0;
            lblParametros.Text = "Parametros";
            // 
            // pnlResumen
            // 
            pnlResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlResumen.BackColor = Color.White;
            pnlResumen.BorderStyle = BorderStyle.FixedSingle;
            pnlResumen.Controls.Add(cardK);
            pnlResumen.Controls.Add(cardPendiente);
            pnlResumen.Controls.Add(cardDeltaY);
            pnlResumen.Controls.Add(cardDeltaX);
            pnlResumen.Controls.Add(lblResumen);
            pnlResumen.Location = new Point(774, 120);
            pnlResumen.Name = "pnlResumen";
            pnlResumen.Size = new Size(1048, 199);
            pnlResumen.TabIndex = 3;
            // 
            // cardK
            // 
            cardK.BackColor = Color.FromArgb(249, 250, 251);
            cardK.BorderStyle = BorderStyle.FixedSingle;
            cardK.Controls.Add(lblResultadoKPasos);
            cardK.Controls.Add(lblKPasos);
            cardK.Location = new Point(674, 61);
            cardK.Name = "cardK";
            cardK.Size = new Size(152, 73);
            cardK.TabIndex = 4;
            // 
            // lblResultadoKPasos
            // 
            lblResultadoKPasos.AutoSize = true;
            lblResultadoKPasos.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblResultadoKPasos.ForeColor = Color.FromArgb(17, 24, 39);
            lblResultadoKPasos.Location = new Point(18, 34);
            lblResultadoKPasos.Name = "lblResultadoKPasos";
            lblResultadoKPasos.Size = new Size(25, 35);
            lblResultadoKPasos.TabIndex = 1;
            lblResultadoKPasos.Text = "-";
            // 
            // lblKPasos
            // 
            lblKPasos.AutoSize = true;
            lblKPasos.Font = new Font("Segoe UI", 9F);
            lblKPasos.ForeColor = Color.FromArgb(107, 114, 128);
            lblKPasos.Location = new Point(18, 12);
            lblKPasos.Name = "lblKPasos";
            lblKPasos.Size = new Size(60, 20);
            lblKPasos.TabIndex = 0;
            lblKPasos.Text = "K pasos";
            // 
            // cardPendiente
            // 
            cardPendiente.BackColor = Color.FromArgb(249, 250, 251);
            cardPendiente.BorderStyle = BorderStyle.FixedSingle;
            cardPendiente.Controls.Add(lblResultadoPendiente);
            cardPendiente.Controls.Add(lblPendiente);
            cardPendiente.Location = new Point(467, 61);
            cardPendiente.Name = "cardPendiente";
            cardPendiente.Size = new Size(132, 73);
            cardPendiente.TabIndex = 3;
            // 
            // lblResultadoPendiente
            // 
            lblResultadoPendiente.AutoSize = true;
            lblResultadoPendiente.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblResultadoPendiente.ForeColor = Color.FromArgb(17, 24, 39);
            lblResultadoPendiente.Location = new Point(18, 34);
            lblResultadoPendiente.Name = "lblResultadoPendiente";
            lblResultadoPendiente.Size = new Size(25, 35);
            lblResultadoPendiente.TabIndex = 1;
            lblResultadoPendiente.Text = "-";
            // 
            // lblPendiente
            // 
            lblPendiente.AutoSize = true;
            lblPendiente.Font = new Font("Segoe UI", 9F);
            lblPendiente.ForeColor = Color.FromArgb(107, 114, 128);
            lblPendiente.Location = new Point(18, 12);
            lblPendiente.Name = "lblPendiente";
            lblPendiente.Size = new Size(91, 20);
            lblPendiente.TabIndex = 0;
            lblPendiente.Text = "Pendiente m";
            // 
            // cardDeltaY
            // 
            cardDeltaY.BackColor = Color.FromArgb(249, 250, 251);
            cardDeltaY.BorderStyle = BorderStyle.FixedSingle;
            cardDeltaY.Controls.Add(lblResultadoDeltaY);
            cardDeltaY.Controls.Add(lblDeltaY);
            cardDeltaY.Location = new Point(278, 61);
            cardDeltaY.Name = "cardDeltaY";
            cardDeltaY.Size = new Size(110, 73);
            cardDeltaY.TabIndex = 2;
            // 
            // lblResultadoDeltaY
            // 
            lblResultadoDeltaY.AutoSize = true;
            lblResultadoDeltaY.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblResultadoDeltaY.ForeColor = Color.FromArgb(17, 24, 39);
            lblResultadoDeltaY.Location = new Point(18, 34);
            lblResultadoDeltaY.Name = "lblResultadoDeltaY";
            lblResultadoDeltaY.Size = new Size(25, 35);
            lblResultadoDeltaY.TabIndex = 1;
            lblResultadoDeltaY.Text = "-";
            // 
            // lblDeltaY
            // 
            lblDeltaY.AutoSize = true;
            lblDeltaY.Font = new Font("Segoe UI", 9F);
            lblDeltaY.ForeColor = Color.FromArgb(107, 114, 128);
            lblDeltaY.Location = new Point(18, 12);
            lblDeltaY.Name = "lblDeltaY";
            lblDeltaY.Size = new Size(57, 20);
            lblDeltaY.TabIndex = 0;
            lblDeltaY.Text = "Delta Y";
            // 
            // cardDeltaX
            // 
            cardDeltaX.BackColor = Color.FromArgb(249, 250, 251);
            cardDeltaX.BorderStyle = BorderStyle.FixedSingle;
            cardDeltaX.Controls.Add(lblResultadoDeltaX);
            cardDeltaX.Controls.Add(lblDeltaX);
            cardDeltaX.Location = new Point(66, 61);
            cardDeltaX.Name = "cardDeltaX";
            cardDeltaX.Size = new Size(127, 73);
            cardDeltaX.TabIndex = 1;
            // 
            // lblResultadoDeltaX
            // 
            lblResultadoDeltaX.AutoSize = true;
            lblResultadoDeltaX.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblResultadoDeltaX.ForeColor = Color.FromArgb(17, 24, 39);
            lblResultadoDeltaX.Location = new Point(18, 34);
            lblResultadoDeltaX.Name = "lblResultadoDeltaX";
            lblResultadoDeltaX.Size = new Size(25, 35);
            lblResultadoDeltaX.TabIndex = 1;
            lblResultadoDeltaX.Text = "-";
            // 
            // lblDeltaX
            // 
            lblDeltaX.AutoSize = true;
            lblDeltaX.Font = new Font("Segoe UI", 9F);
            lblDeltaX.ForeColor = Color.FromArgb(107, 114, 128);
            lblDeltaX.Location = new Point(18, 12);
            lblDeltaX.Name = "lblDeltaX";
            lblDeltaX.Size = new Size(58, 20);
            lblDeltaX.TabIndex = 0;
            lblDeltaX.Text = "Delta X";
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblResumen.ForeColor = Color.FromArgb(17, 24, 39);
            lblResumen.Location = new Point(9, 18);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(93, 25);
            lblResumen.TabIndex = 0;
            lblResumen.Text = "Resumen";
            // 
            // lblPlano
            // 
            lblPlano.AutoSize = true;
            lblPlano.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPlano.ForeColor = Color.FromArgb(17, 24, 39);
            lblPlano.Location = new Point(38, 341);
            lblPlano.Name = "lblPlano";
            lblPlano.Size = new Size(160, 25);
            lblPlano.TabIndex = 4;
            lblPlano.Text = "Plano cartesiano";
            // 
            // pnlContenedor
            // 
            pnlContenedor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenedor.BackColor = Color.White;
            pnlContenedor.BorderStyle = BorderStyle.FixedSingle;
            pnlContenedor.Cursor = Cursors.Cross;
            pnlContenedor.Location = new Point(34, 379);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1180, 537);
            pnlContenedor.TabIndex = 9;
            pnlContenedor.TabStop = true;
            // 
            // lblTabla
            // 
            lblTabla.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTabla.AutoSize = true;
            lblTabla.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTabla.ForeColor = Color.FromArgb(17, 24, 39);
            lblTabla.Location = new Point(1242, 341);
            lblTabla.Name = "lblTabla";
            lblTabla.Size = new Size(163, 25);
            lblTabla.TabIndex = 10;
            lblTabla.Text = "Octantes y puntos";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colX, colY, colP, colSimetrias });
            dataGridView1.Location = new Point(1242, 379);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(447, 471);
            dataGridView1.TabIndex = 11;
            // 
            // colX
            // 
            colX.HeaderText = "x";
            colX.MinimumWidth = 6;
            colX.Name = "colX";
            colX.ReadOnly = true;
            colX.Width = 70;
            // 
            // colY
            // 
            colY.HeaderText = "y";
            colY.MinimumWidth = 6;
            colY.Name = "colY";
            colY.ReadOnly = true;
            colY.Width = 70;
            // 
            // colP
            // 
            colP.HeaderText = "p";
            colP.MinimumWidth = 6;
            colP.Name = "colP";
            colP.ReadOnly = true;
            colP.Width = 70;
            // 
            // colSimetrias
            // 
            colSimetrias.HeaderText = "8 simetrias";
            colSimetrias.MinimumWidth = 6;
            colSimetrias.Name = "colSimetrias";
            colSimetrias.ReadOnly = true;
            colSimetrias.Width = 200;
            // 
            // btnPaso
            // 
            btnPaso.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPaso.BackColor = Color.White;
            btnPaso.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnPaso.FlatStyle = FlatStyle.Flat;
            btnPaso.Font = new Font("Segoe UI", 10F);
            btnPaso.ForeColor = Color.FromArgb(31, 41, 55);
            btnPaso.Location = new Point(1242, 866);
            btnPaso.Name = "btnPaso";
            btnPaso.Size = new Size(172, 50);
            btnPaso.TabIndex = 12;
            btnPaso.Text = "Paso a paso";
            btnPaso.UseVisualStyleBackColor = false;
            // 
            // btnCompleto
            // 
            btnCompleto.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCompleto.BackColor = Color.FromArgb(17, 24, 39);
            btnCompleto.FlatAppearance.BorderSize = 0;
            btnCompleto.FlatStyle = FlatStyle.Flat;
            btnCompleto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCompleto.ForeColor = Color.White;
            btnCompleto.Location = new Point(1430, 866);
            btnCompleto.Name = "btnCompleto";
            btnCompleto.Size = new Size(200, 50);
            btnCompleto.TabIndex = 13;
            btnCompleto.Text = "Mostrar completo";
            btnCompleto.UseVisualStyleBackColor = false;
            // 
            // btnZoomOut
            // 
            btnZoomOut.BackColor = Color.White;
            btnZoomOut.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnZoomOut.FlatStyle = FlatStyle.Flat;
            btnZoomOut.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnZoomOut.ForeColor = Color.FromArgb(31, 41, 55);
            btnZoomOut.Location = new Point(236, 335);
            btnZoomOut.Name = "btnZoomOut";
            btnZoomOut.Size = new Size(38, 34);
            btnZoomOut.TabIndex = 5;
            btnZoomOut.Text = "-";
            btnZoomOut.UseVisualStyleBackColor = false;
            // 
            // btnZoomReset
            // 
            btnZoomReset.BackColor = Color.White;
            btnZoomReset.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnZoomReset.FlatStyle = FlatStyle.Flat;
            btnZoomReset.Font = new Font("Segoe UI", 8.5F);
            btnZoomReset.ForeColor = Color.FromArgb(31, 41, 55);
            btnZoomReset.Location = new Point(280, 335);
            btnZoomReset.Name = "btnZoomReset";
            btnZoomReset.Size = new Size(72, 34);
            btnZoomReset.TabIndex = 6;
            btnZoomReset.Text = "Reset";
            btnZoomReset.UseVisualStyleBackColor = false;
            // 
            // btnZoomIn
            // 
            btnZoomIn.BackColor = Color.White;
            btnZoomIn.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnZoomIn.FlatStyle = FlatStyle.Flat;
            btnZoomIn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnZoomIn.ForeColor = Color.FromArgb(31, 41, 55);
            btnZoomIn.Location = new Point(358, 335);
            btnZoomIn.Name = "btnZoomIn";
            btnZoomIn.Size = new Size(38, 34);
            btnZoomIn.TabIndex = 7;
            btnZoomIn.Text = "+";
            btnZoomIn.UseVisualStyleBackColor = false;
            // 
            // lblZoom
            // 
            lblZoom.AutoSize = true;
            lblZoom.Font = new Font("Segoe UI", 9F);
            lblZoom.ForeColor = Color.FromArgb(75, 85, 99);
            lblZoom.Location = new Point(418, 349);
            lblZoom.Name = "lblZoom";
            lblZoom.Size = new Size(89, 20);
            lblZoom.TabIndex = 8;
            lblZoom.Text = "Zoom 100%";
            // 
            // FrmPuntoMedioCirculo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 246, 250);
            ClientSize = new Size(1864, 956);
            Controls.Add(btnCompleto);
            Controls.Add(btnPaso);
            Controls.Add(dataGridView1);
            Controls.Add(lblTabla);
            Controls.Add(pnlContenedor);
            Controls.Add(lblZoom);
            Controls.Add(btnZoomIn);
            Controls.Add(btnZoomReset);
            Controls.Add(btnZoomOut);
            Controls.Add(lblPlano);
            Controls.Add(pnlResumen);
            Controls.Add(pnlEntrada);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblTitulo);
            MinimumSize = new Size(1200, 760);
            Name = "FrmPuntoMedioCirculo";
            Text = "Circunferencia por punto medio";
            WindowState = FormWindowState.Maximized;
            pnlEntrada.ResumeLayout(false);
            pnlEntrada.PerformLayout();
            pnlResumen.ResumeLayout(false);
            pnlResumen.PerformLayout();
            cardK.ResumeLayout(false);
            cardK.PerformLayout();
            cardPendiente.ResumeLayout(false);
            cardPendiente.PerformLayout();
            cardDeltaY.ResumeLayout(false);
            cardDeltaY.PerformLayout();
            cardDeltaX.ResumeLayout(false);
            cardDeltaX.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Panel pnlEntrada;
        private Label lblParametros;
        private Label lblPunto1;
        private Label lblPunto2;
        private Label lblX1;
        private Label lblY1;
        private Label lblX2;
        private Label lblY2;
        private TextBox txtX1;
        private TextBox txtY1;
        private TextBox txtX2;
        private TextBox txtY2;
        private Button btnCalcularRecta;
        private Button btnLimpiar;
        private Button btnSalir;
        private Panel pnlResumen;
        private Label lblResumen;
        private Panel cardDeltaX;
        private Label lblDeltaX;
        private Label lblResultadoDeltaX;
        private Panel cardDeltaY;
        private Label lblDeltaY;
        private Label lblResultadoDeltaY;
        private Panel cardPendiente;
        private Label lblPendiente;
        private Label lblResultadoPendiente;
        private Panel cardK;
        private Label lblKPasos;
        private Label lblResultadoKPasos;
        private Label lblPlano;
        private Button btnZoomOut;
        private Button btnZoomReset;
        private Button btnZoomIn;
        private Label lblZoom;
        private Panel pnlContenedor;
        private Label lblTabla;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colX;
        private DataGridViewTextBoxColumn colY;
        private DataGridViewTextBoxColumn colP;
        private DataGridViewTextBoxColumn colSimetrias;
        private Button btnPaso;
        private Button btnCompleto;
    }
}
