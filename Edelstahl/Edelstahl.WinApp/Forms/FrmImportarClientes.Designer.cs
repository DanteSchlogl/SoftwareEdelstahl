namespace Edelstahl.WinApp.Forms
{
    partial class FrmImportarClientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpImportacion = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnSeleccionarArchivo = new System.Windows.Forms.Button();
            this.grpArchivo = new System.Windows.Forms.GroupBox();
            this.lblArchivo = new System.Windows.Forms.Label();
            this.txtRutaArchivo = new System.Windows.Forms.TextBox();
            this.lblCantidadRegistros = new System.Windows.Forms.Label();
            this.grpImportacion.SuspendLayout();
            this.grpArchivo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpImportacion
            // 
            this.grpImportacion.Controls.Add(this.lblCantidadRegistros);
            this.grpImportacion.Controls.Add(this.btnImportar);
            this.grpImportacion.Controls.Add(this.lblEstado);
            this.grpImportacion.Location = new System.Drawing.Point(20, 180);
            this.grpImportacion.Name = "grpImportacion";
            this.grpImportacion.Size = new System.Drawing.Size(740, 150);
            this.grpImportacion.TabIndex = 4;
            this.grpImportacion.TabStop = false;
            this.grpImportacion.Text = "Importación";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(20, 40);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(145, 13);
            this.lblEstado.TabIndex = 6;
            this.lblEstado.Text = "Ningún archivo seleccionado";
            // 
            // btnImportar
            // 
            this.btnImportar.Enabled = false;
            this.btnImportar.Location = new System.Drawing.Point(20, 104);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(180, 40);
            this.btnImportar.TabIndex = 7;
            this.btnImportar.Text = "Importar Clientes";
            this.btnImportar.UseVisualStyleBackColor = true;
            // 
            // btnSeleccionarArchivo
            // 
            this.btnSeleccionarArchivo.Location = new System.Drawing.Point(560, 68);
            this.btnSeleccionarArchivo.Name = "btnSeleccionarArchivo";
            this.btnSeleccionarArchivo.Size = new System.Drawing.Size(150, 35);
            this.btnSeleccionarArchivo.TabIndex = 8;
            this.btnSeleccionarArchivo.Text = "Seleccionar CSV";
            this.btnSeleccionarArchivo.UseVisualStyleBackColor = true;
            // 
            // grpArchivo
            // 
            this.grpArchivo.Controls.Add(this.txtRutaArchivo);
            this.grpArchivo.Controls.Add(this.lblArchivo);
            this.grpArchivo.Controls.Add(this.btnSeleccionarArchivo);
            this.grpArchivo.Location = new System.Drawing.Point(20, 20);
            this.grpArchivo.Name = "grpArchivo";
            this.grpArchivo.Size = new System.Drawing.Size(740, 140);
            this.grpArchivo.TabIndex = 3;
            this.grpArchivo.TabStop = false;
            this.grpArchivo.Text = "Archivo a importar";
            // 
            // lblArchivo
            // 
            this.lblArchivo.AutoSize = true;
            this.lblArchivo.Location = new System.Drawing.Point(20, 40);
            this.lblArchivo.Name = "lblArchivo";
            this.lblArchivo.Size = new System.Drawing.Size(161, 13);
            this.lblArchivo.TabIndex = 9;
            this.lblArchivo.Text = "Seleccione el archivo a importar:";
           
            // 
            // txtRutaArchivo
            // 
            this.txtRutaArchivo.Location = new System.Drawing.Point(20, 70);
            this.txtRutaArchivo.Name = "txtRutaArchivo";
            this.txtRutaArchivo.ReadOnly = true;
            this.txtRutaArchivo.Size = new System.Drawing.Size(520, 20);
            this.txtRutaArchivo.TabIndex = 10;
            // 
            // lblCantidadRegistros
            // 
            this.lblCantidadRegistros.AutoSize = true;
            this.lblCantidadRegistros.ForeColor = System.Drawing.Color.Navy;
            this.lblCantidadRegistros.Location = new System.Drawing.Point(20, 78);
            this.lblCantidadRegistros.Name = "lblCantidadRegistros";
            this.lblCantidadRegistros.Size = new System.Drawing.Size(119, 13);
            this.lblCantidadRegistros.TabIndex = 8;
            this.lblCantidadRegistros.Text = "Registros detectados: 0";
            // 
            // FrmImportarClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.grpImportacion);
            this.Controls.Add(this.grpArchivo);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FrmImportarClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Importación de Clientes";
            this.grpImportacion.ResumeLayout(false);
            this.grpImportacion.PerformLayout();
            this.grpArchivo.ResumeLayout(false);
            this.grpArchivo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpImportacion;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnSeleccionarArchivo;
        private System.Windows.Forms.GroupBox grpArchivo;
        private System.Windows.Forms.Label lblArchivo;
        private System.Windows.Forms.TextBox txtRutaArchivo;
        private System.Windows.Forms.Label lblCantidadRegistros;
    }
}