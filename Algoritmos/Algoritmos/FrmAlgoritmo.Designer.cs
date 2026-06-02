namespace Algoritmos
{
    partial class FrmAlgoritmo
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            dDAToolStripMenuItem = new ToolStripMenuItem();
            algoritmoDDAToolStripMenuItem = new ToolStripMenuItem();
            algoritmoToolStripMenuItem = new ToolStripMenuItem();
            algoritmoPuntoMedioLineaToolStripMenuItem = new ToolStripMenuItem();
            conicasToolStripMenuItem = new ToolStripMenuItem();
            puntoMedioDeCirculoToolStripMenuItem = new ToolStripMenuItem();
            parametricoDeCirculoToolStripMenuItem = new ToolStripMenuItem();
            polarDeCirculoToolStripMenuItem = new ToolStripMenuItem();
            rellenoToolStripMenuItem = new ToolStripMenuItem();
            rellenoPorSemillaToolStripMenuItem = new ToolStripMenuItem();
            rellenoPorPilaToolStripMenuItem = new ToolStripMenuItem();
            rellenoScanlineToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dDAToolStripMenuItem, conicasToolStripMenuItem, rellenoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1423, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // dDAToolStripMenuItem
            // 
            dDAToolStripMenuItem.BackColor = SystemColors.ButtonHighlight;
            dDAToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { algoritmoDDAToolStripMenuItem, algoritmoToolStripMenuItem, algoritmoPuntoMedioLineaToolStripMenuItem });
            dDAToolStripMenuItem.Name = "dDAToolStripMenuItem";
            dDAToolStripMenuItem.Size = new Size(64, 24);
            dDAToolStripMenuItem.Text = "Lineas";
            // 
            // algoritmoDDAToolStripMenuItem
            // 
            algoritmoDDAToolStripMenuItem.Name = "algoritmoDDAToolStripMenuItem";
            algoritmoDDAToolStripMenuItem.Size = new Size(280, 26);
            algoritmoDDAToolStripMenuItem.Text = "Algoritmo DDA";
            algoritmoDDAToolStripMenuItem.Click += algoritmoDDAToolStripMenuItem_Click;
            // 
            // algoritmoToolStripMenuItem
            // 
            algoritmoToolStripMenuItem.Name = "algoritmoToolStripMenuItem";
            algoritmoToolStripMenuItem.Size = new Size(280, 26);
            algoritmoToolStripMenuItem.Text = "Algoritmo Bresenham";
            algoritmoToolStripMenuItem.Click += algoritmoToolStripMenuItem_Click;
            // 
            // algoritmoPuntoMedioLineaToolStripMenuItem
            // 
            algoritmoPuntoMedioLineaToolStripMenuItem.Name = "algoritmoPuntoMedioLineaToolStripMenuItem";
            algoritmoPuntoMedioLineaToolStripMenuItem.Size = new Size(280, 26);
            algoritmoPuntoMedioLineaToolStripMenuItem.Text = "Algoritmo PuntoMedioLinea";
            algoritmoPuntoMedioLineaToolStripMenuItem.Click += algoritmoPuntoMedioLineaToolStripMenuItem_Click;
            // 
            // conicasToolStripMenuItem
            // 
            conicasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { puntoMedioDeCirculoToolStripMenuItem, parametricoDeCirculoToolStripMenuItem, polarDeCirculoToolStripMenuItem });
            conicasToolStripMenuItem.Name = "conicasToolStripMenuItem";
            conicasToolStripMenuItem.Size = new Size(74, 24);
            conicasToolStripMenuItem.Text = "Conicas";
            // 
            // puntoMedioDeCirculoToolStripMenuItem
            // 
            puntoMedioDeCirculoToolStripMenuItem.Name = "puntoMedioDeCirculoToolStripMenuItem";
            puntoMedioDeCirculoToolStripMenuItem.Size = new Size(248, 26);
            puntoMedioDeCirculoToolStripMenuItem.Text = "Punto Medio de Circulo";
            // 
            // parametricoDeCirculoToolStripMenuItem
            // 
            parametricoDeCirculoToolStripMenuItem.Name = "parametricoDeCirculoToolStripMenuItem";
            parametricoDeCirculoToolStripMenuItem.Size = new Size(248, 26);
            parametricoDeCirculoToolStripMenuItem.Text = "Parametrico de Circulo";
            // 
            // polarDeCirculoToolStripMenuItem
            // 
            polarDeCirculoToolStripMenuItem.Name = "polarDeCirculoToolStripMenuItem";
            polarDeCirculoToolStripMenuItem.Size = new Size(248, 26);
            polarDeCirculoToolStripMenuItem.Text = "Polar de Circulo";
            // 
            // rellenoToolStripMenuItem
            // 
            rellenoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { rellenoPorSemillaToolStripMenuItem, rellenoPorPilaToolStripMenuItem, rellenoScanlineToolStripMenuItem });
            rellenoToolStripMenuItem.Name = "rellenoToolStripMenuItem";
            rellenoToolStripMenuItem.Size = new Size(73, 24);
            rellenoToolStripMenuItem.Text = "Relleno";
            // 
            // rellenoPorSemillaToolStripMenuItem
            // 
            rellenoPorSemillaToolStripMenuItem.Name = "rellenoPorSemillaToolStripMenuItem";
            rellenoPorSemillaToolStripMenuItem.Size = new Size(224, 26);
            rellenoPorSemillaToolStripMenuItem.Text = "Relleno por Semilla";
            // 
            // rellenoPorPilaToolStripMenuItem
            // 
            rellenoPorPilaToolStripMenuItem.Name = "rellenoPorPilaToolStripMenuItem";
            rellenoPorPilaToolStripMenuItem.Size = new Size(224, 26);
            rellenoPorPilaToolStripMenuItem.Text = "Relleno por Pila";
            // 
            // rellenoScanlineToolStripMenuItem
            // 
            rellenoScanlineToolStripMenuItem.Name = "rellenoScanlineToolStripMenuItem";
            rellenoScanlineToolStripMenuItem.Size = new Size(224, 26);
            rellenoScanlineToolStripMenuItem.Text = "Relleno Scanline";
            // 
            // FrmAlgoritmo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1423, 680);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "FrmAlgoritmo";
            Text = "Algoritmo";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem dDAToolStripMenuItem;
        private ToolStripMenuItem conicasToolStripMenuItem;
        private ToolStripMenuItem rellenoToolStripMenuItem;
        private ToolStripMenuItem algoritmoDDAToolStripMenuItem;
        private ToolStripMenuItem algoritmoToolStripMenuItem;
        private ToolStripMenuItem algoritmoPuntoMedioLineaToolStripMenuItem;
        private ToolStripMenuItem puntoMedioDeCirculoToolStripMenuItem;
        private ToolStripMenuItem parametricoDeCirculoToolStripMenuItem;
        private ToolStripMenuItem polarDeCirculoToolStripMenuItem;
        private ToolStripMenuItem rellenoPorSemillaToolStripMenuItem;
        private ToolStripMenuItem rellenoPorPilaToolStripMenuItem;
        private ToolStripMenuItem rellenoScanlineToolStripMenuItem;
    }
}
