using GeometriaComputacional;
using System.Windows.Forms;

namespace Algoritmos
{
    public partial class FrmAlgoritmo : Form
    {
        public FrmAlgoritmo()
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
        }

        private void AbrirFormulario<T>() where T : Form, new()
        {
            Form? formularioActual = null;

            foreach (Form hijo in MdiChildren)
            {
                if (hijo is T)
                {
                    formularioActual = hijo;
                    continue;
                }

                hijo.Close();
            }

            if (formularioActual != null)
            {
                formularioActual.WindowState = FormWindowState.Maximized;
                formularioActual.BringToFront();
                formularioActual.Activate();
                return;
            }

            T formulario = new T();
            formulario.MdiParent = this;
            formulario.WindowState = FormWindowState.Maximized;
            formulario.Show();
        }


        private void algoritmoDDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmDDA>();
        }

        private void algoritmoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmBresenham>();
        }

        private void algoritmoPuntoMedioLineaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FrmPuntoMedioLinea>();
        }
    }
}
