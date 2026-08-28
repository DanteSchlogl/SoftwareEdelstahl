using System;
using System.Windows.Forms;
using Edelstahl.WinApp.Forms.Commercial;

namespace Edelstahl.WinApp
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // Inicialización del formulario principal.
        }

        private void menuStrip1_ItemClicked(
            object sender,
            ToolStripItemClickedEventArgs e)
        {
            // Evento general del menú.
        }

        private void mnuClientes_Click(
            object sender,
            EventArgs e)
        {
            foreach (Form formularioAbierto in MdiChildren)
            {
                if (formularioAbierto is FrmClientes)
                {
                    formularioAbierto.Activate();
                    return;
                }
            }

            FrmClientes formulario = new FrmClientes
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };

            formulario.Show();
        }
    }
}
