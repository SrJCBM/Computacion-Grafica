using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenBlacioJulio
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        private void AbrirFormulario<T>() where T : Form, new()
        {
            Form formularioActual = null;

            foreach (Form hijo in this.MdiChildren)
            {
                if (hijo is T)
                {
                    formularioActual = hijo;
                }
                else
                {
                    hijo.Close();
                }
            }

            if (formularioActual != null)
            {
                formularioActual.BringToFront();
                return;
            }

            T formulario = new T();
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void margaritaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmMargarita>();
        }

        private void gemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmGema>();
        }
    }
}
