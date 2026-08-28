namespace Edelstahl.WinApp.Forms.Commercial
{
    partial class FrmClientes
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.grpDatosCliente = new System.Windows.Forms.GroupBox();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.txtCUIT = new System.Windows.Forms.TextBox();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.lblLimiteCredito = new System.Windows.Forms.Label();
            this.txtLocalidad = new System.Windows.Forms.TextBox();
            this.nudLimiteCredito = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.lblListado = new System.Windows.Forms.Label();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.colCUIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLimiteCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocalidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpDatosCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimiteCredito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblTitulo.ForeColor = System.Drawing.Color.Crimson;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(221, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Clientes";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(298, 15);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Registre, consulte y administre los clientes de Edelstahl.";
            // 
            // grpDatosCliente
            // 
            this.grpDatosCliente.Controls.Add(this.nudLimiteCredito);
            this.grpDatosCliente.Controls.Add(this.txtLocalidad);
            this.grpDatosCliente.Controls.Add(this.lblLimiteCredito);
            this.grpDatosCliente.Controls.Add(this.lblLocalidad);
            this.grpDatosCliente.Controls.Add(this.txtTelefono);
            this.grpDatosCliente.Controls.Add(this.txtEmail);
            this.grpDatosCliente.Controls.Add(this.txtRazonSocial);
            this.grpDatosCliente.Controls.Add(this.lblTelefono);
            this.grpDatosCliente.Controls.Add(this.lblEmail);
            this.grpDatosCliente.Controls.Add(this.lblRazonSocial);
            this.grpDatosCliente.Controls.Add(this.txtCUIT);
            this.grpDatosCliente.Controls.Add(this.lblCUIT);
            this.grpDatosCliente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpDatosCliente.Location = new System.Drawing.Point(25, 90);
            this.grpDatosCliente.Name = "grpDatosCliente";
            this.grpDatosCliente.Size = new System.Drawing.Size(1120, 210);
            this.grpDatosCliente.TabIndex = 2;
            this.grpDatosCliente.TabStop = false;
            this.grpDatosCliente.Text = "Datos del cliente";
            // 
            // lblCUIT
            // 
            this.lblCUIT.AutoSize = true;
            this.lblCUIT.Location = new System.Drawing.Point(25, 38);
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.Size = new System.Drawing.Size(36, 15);
            this.lblCUIT.TabIndex = 0;
            this.lblCUIT.Text = "CUIT:";
            // 
            // txtCUIT
            // 
            this.txtCUIT.Location = new System.Drawing.Point(150, 34);
            this.txtCUIT.MaxLength = 13;
            this.txtCUIT.Name = "txtCUIT";
            this.txtCUIT.Size = new System.Drawing.Size(230, 23);
            this.txtCUIT.TabIndex = 0;
            this.txtCUIT.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.Location = new System.Drawing.Point(25, 78);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(75, 15);
            this.lblRazonSocial.TabIndex = 2;
            this.lblRazonSocial.Text = "Razón social:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(25, 118);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(46, 15);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Correo:";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(25, 158);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(56, 15);
            this.lblTelefono.TabIndex = 4;
            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Location = new System.Drawing.Point(150, 74);
            this.txtRazonSocial.MaxLength = 150;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(400, 23);
            this.txtRazonSocial.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(150, 114);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 23);
            this.txtEmail.TabIndex = 2;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(150, 154);
            this.txtTelefono.MaxLength = 50;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(230, 23);
            this.txtTelefono.TabIndex = 3;
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.Location = new System.Drawing.Point(590, 34);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(61, 15);
            this.lblLocalidad.TabIndex = 8;
            this.lblLocalidad.Text = "Localidad:\n";
            // 
            // lblLimiteCredito
            // 
            this.lblLimiteCredito.AutoSize = true;
            this.lblLimiteCredito.Location = new System.Drawing.Point(590, 78);
            this.lblLimiteCredito.Name = "lblLimiteCredito";
            this.lblLimiteCredito.Size = new System.Drawing.Size(99, 15);
            this.lblLimiteCredito.TabIndex = 9;
            this.lblLimiteCredito.Text = "Límite de crédito:";
            // 
            // txtLocalidad
            // 
            this.txtLocalidad.Location = new System.Drawing.Point(720, 34);
            this.txtLocalidad.MaxLength = 100;
            this.txtLocalidad.Name = "txtLocalidad";
            this.txtLocalidad.Size = new System.Drawing.Size(350, 23);
            this.txtLocalidad.TabIndex = 4;
            // 
            // nudLimiteCredito
            // 
            this.nudLimiteCredito.Location = new System.Drawing.Point(720, 74);
            this.nudLimiteCredito.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudLimiteCredito.Name = "nudLimiteCredito";
            this.nudLimiteCredito.Size = new System.Drawing.Size(350, 23);
            this.nudLimiteCredito.TabIndex = 5;
            this.nudLimiteCredito.ThousandsSeparator = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(90)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.btnGuardar.Location = new System.Drawing.Point(25, 335);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(150, 38);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar cliente";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.btnRefrescar.Location = new System.Drawing.Point(315, 335);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(120, 38);
            this.btnRefrescar.TabIndex = 8;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            // 
            // lblListado
            // 
            this.lblListado.AutoSize = true;
            this.lblListado.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblListado.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.lblListado.Location = new System.Drawing.Point(25, 395);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(139, 20);
            this.lblListado.TabIndex = 9;
            this.lblListado.Text = "Clientes registrados";
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.AllowUserToResizeRows = false;
            this.dgvClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCUIT,
            this.colActivo,
            this.colLimiteCredito,
            this.colTelefono,
            this.colEmail,
            this.colLocalidad,
            this.colRazonSocial});
            this.dgvClientes.Location = new System.Drawing.Point(25, 425);
            this.dgvClientes.MultiSelect = false;
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersVisible = false;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(1120, 211);
            this.dgvClientes.TabIndex = 9;
            // 
            // colCUIT
            // 
            this.colCUIT.DataPropertyName = "CUIT";
            this.colCUIT.HeaderText = "CUIT";
            this.colCUIT.Name = "colCUIT";
            this.colCUIT.ReadOnly = true;
            // 
            // colActivo
            // 
            this.colActivo.DataPropertyName = "Activo";
            this.colActivo.HeaderText = "Activo";
            this.colActivo.Name = "colActivo";
            this.colActivo.ReadOnly = true;
            // 
            // colLimiteCredito
            // 
            this.colLimiteCredito.DataPropertyName = "LimiteCredito";
            this.colLimiteCredito.HeaderText = "Límite de crédito";
            this.colLimiteCredito.Name = "colLimiteCredito";
            this.colLimiteCredito.ReadOnly = true;
            // 
            // colTelefono
            // 
            this.colTelefono.DataPropertyName = "Telefono";
            this.colTelefono.HeaderText = "Teléfono";
            this.colTelefono.Name = "colTelefono";
            this.colTelefono.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "Email";
            this.colEmail.HeaderText = "Correo electrónico";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // colLocalidad
            // 
            this.colLocalidad.DataPropertyName = "Localidad";
            this.colLocalidad.HeaderText = "Localidad";
            this.colLocalidad.Name = "colLocalidad";
            this.colLocalidad.ReadOnly = true;
            // 
            // colRazonSocial
            // 
            this.colRazonSocial.DataPropertyName = "RazonSocial";
            this.colRazonSocial.HeaderText = "Razón social";
            this.colRazonSocial.Name = "colRazonSocial";
            this.colRazonSocial.ReadOnly = true;
            // 
            // FrmClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 648);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.lblListado);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.grpDatosCliente);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FrmClientes";
            this.Text = "FrmClientes";
            this.grpDatosCliente.ResumeLayout(false);
            this.grpDatosCliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimiteCredito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.GroupBox grpDatosCliente;
        private System.Windows.Forms.TextBox txtCUIT;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.TextBox txtLocalidad;
        private System.Windows.Forms.Label lblLimiteCredito;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.NumericUpDown nudLimiteCredito;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Label lblListado;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCUIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLimiteCredito;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRazonSocial;
    }
}