using System;
using System.IO;
using System.Windows.Forms;
using Edelstahl.BLL.Services;

namespace Edelstahl.WinApp.Forms
{
    public partial class FrmImportarClientes : Form
    {
        public FrmImportarClientes()
        {
            InitializeComponent();

            btnSeleccionarArchivo.Click +=
                BtnSeleccionarArchivo_Click;

            btnImportar.Click +=
                BtnImportar_Click;

            btnImportar.Enabled = false;
        }

        private void BtnSeleccionarArchivo_Click(
            object sender,
            EventArgs e)
        {
            OpenFileDialog dialogo =
                new OpenFileDialog();

            dialogo.Filter =
                "Archivos CSV (*.csv)|*.csv";

            if (dialogo.ShowDialog() ==
                DialogResult.OK)
            {
                txtRutaArchivo.Text =
                    dialogo.FileName;

                string[] lineas =
                    File.ReadAllLines(
                        dialogo.FileName);

                int cantidad =
                    Math.Max(0, lineas.Length - 1);

                lblEstado.Text =
                    "Archivo cargado correctamente";

                lblCantidadRegistros.Text =
                    "Registros detectados: "
                    + cantidad;

                btnImportar.Enabled = true;
            }
        }

        private void BtnImportar_Click(
    object sender,
    EventArgs e)
        {
            MessageBox.Show(
                "Importación pendiente de implementar.",
                "Importar clientes",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}