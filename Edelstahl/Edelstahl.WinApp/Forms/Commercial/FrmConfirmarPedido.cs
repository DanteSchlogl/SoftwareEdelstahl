using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Edelstahl.BLL.DTOs;
using Edelstahl.BLL.Services;
using Edelstahl.Domain.Comercial;


namespace Edelstahl.WinApp.Forms.Commercial
{
    public partial class FrmConfirmarPedido : Form
    {
        private ClienteService _clienteService;
        private PresupuestoService _presupuestoService;

        private Cliente _clienteSeleccionado;
        private Presupuesto _presupuestoSeleccionado;

        // Modifique este contructor para que no reciba parámetros y se pueda instanciar desde el diseñador de Visual Studio.
        public FrmConfirmarPedido()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode ==
                LicenseUsageMode.Designtime)
            {
                return;
            }

            _clienteService = new ClienteService();
            _presupuestoService = new PresupuestoService();

            ConfigurarGrillaClientes();
            ConfigurarGrillaPresupuestos();
            ConfigurarEventos();
            CargarClientes();
            ConfigurarGrillaProductos();
        }
        private void ConfigurarGrillaProductos()
        {
            dgvDetallePresupuesto.AutoGenerateColumns = false;
            dgvDetallePresupuesto.ReadOnly = true;
            dgvDetallePresupuesto.MultiSelect = false;
            dgvDetallePresupuesto.AllowUserToAddRows = false;
            dgvDetallePresupuesto.AllowUserToDeleteRows = false;
            dgvDetallePresupuesto.AllowUserToResizeRows = false;
            dgvDetallePresupuesto.RowHeadersVisible = false;

            dgvDetallePresupuesto.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDetallePresupuesto.DefaultCellStyle.ForeColor =
                Color.Black;

            dgvDetallePresupuesto.DefaultCellStyle.BackColor =
                Color.White;

            dgvDetallePresupuesto.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvDetallePresupuesto.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 120, 215);
        }

        private void ConfigurarGrillaClientes()
        {
            dgvSeleccionClientes.AutoGenerateColumns = false;
            dgvSeleccionClientes.ReadOnly = true;
            dgvSeleccionClientes.MultiSelect = false;
            dgvSeleccionClientes.AllowUserToAddRows = false;
            dgvSeleccionClientes.AllowUserToDeleteRows = false;
            dgvSeleccionClientes.AllowUserToResizeRows = false;
            dgvSeleccionClientes.RowHeadersVisible = false;

            dgvSeleccionClientes.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvSeleccionClientes.DefaultCellStyle.ForeColor =
                Color.Black;

            dgvSeleccionClientes.DefaultCellStyle.BackColor =
                Color.White;

            dgvSeleccionClientes.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvSeleccionClientes.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 120, 215);
        }

        private void ConfigurarGrillaPresupuestos()
        {
            dgvPresupuestos.AutoGenerateColumns = false;
            dgvPresupuestos.ReadOnly = true;
            dgvPresupuestos.MultiSelect = false;
            dgvPresupuestos.AllowUserToAddRows = false;
            dgvPresupuestos.AllowUserToDeleteRows = false;
            dgvPresupuestos.AllowUserToResizeRows = false;
            dgvPresupuestos.RowHeadersVisible = false;

            dgvPresupuestos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPresupuestos.DefaultCellStyle.ForeColor =
                Color.Black;

            dgvPresupuestos.DefaultCellStyle.BackColor =
                Color.White;

            dgvPresupuestos.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvPresupuestos.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 120, 215);
        }

        private void ConfigurarEventos()
        {
            // Paso 1: Cliente
            btnBuscarCliente.Click +=
                btnBuscarCliente_Click;

            btnActualizarClientes.Click +=
                btnActualizarClientes_Click;

            btnSiguienteCliente.Click +=
                btnSiguienteCliente_Click;

            dgvSeleccionClientes.CellClick +=
                dgvSeleccionClientes_CellClick;

            dgvSeleccionClientes.CellDoubleClick +=
                dgvSeleccionClientes_CellDoubleClick;

            txtBuscarCliente.KeyDown +=
                txtBuscarCliente_KeyDown;

            // Paso 2: Presupuesto
            btnBuscarPresupuesto.Click +=
                btnBuscarPresupuesto_Click;

            btnActualizarPresupuestos.Click +=
                btnActualizarPresupuestos_Click;

            btnAnteriorPresupuesto.Click +=
                btnAnteriorPresupuesto_Click;

            btnSiguientePresupuesto.Click +=
                btnSiguientePresupuesto_Click;

            dgvPresupuestos.CellClick +=
                dgvPresupuestos_CellClick;

            txtBuscarPresupuesto.KeyDown +=
                txtBuscarPresupuesto_KeyDown;

            // Actualización general del formulario
            Activated +=
                FrmConfirmarPedido_Activated;
            // Paso 3: Productos
            btnAnteriorProductos.Click +=
                btnAnteriorProductos_Click;

            btnSiguienteProductos.Click +=
                btnSiguienteProductos_Click;

        }
        private void btnAnteriorProductos_Click(
    object sender,
    EventArgs e)
        {
            TabPage.SelectedTab = tabPresupuesto;
        }

        private void btnSiguienteProductos_Click(
            object sender,
            EventArgs e)
        {
            if (_presupuestoSeleccionado == null)
            {
                MessageBox.Show(
                    "Debe seleccionar un presupuesto.",
                    "Presupuesto requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (_presupuestoSeleccionado.Detalles == null ||
                _presupuestoSeleccionado.Detalles.Count == 0)
            {
                MessageBox.Show(
                    "El presupuesto no contiene productos o servicios.",
                    "Presupuesto sin detalles",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            TabPage.SelectedTab = tabValidacion;
        }

        private void CargarClientes(string filtro = "")
        {
            List<Cliente> clientes =
                _clienteService.ObtenerTodos();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                string filtroTexto =
                    filtro.Trim().ToLowerInvariant();

                string filtroCUIT =
                    filtroTexto
                        .Replace("-", string.Empty)
                        .Replace(" ", string.Empty);

                clientes = clientes
                    .Where(cliente =>
                    {
                        string clienteCUIT =
                            (cliente.CUIT ?? string.Empty)
                                .Replace("-", string.Empty)
                                .Replace(" ", string.Empty);

                        string clienteRazonSocial =
                            (cliente.RazonSocial ?? string.Empty)
                                .ToLowerInvariant();

                        bool coincideCUIT =
                            clienteCUIT.Contains(filtroCUIT);

                        bool coincideRazonSocial =
                            clienteRazonSocial.Contains(filtroTexto);

                        return coincideCUIT || coincideRazonSocial;
                    })
                    .ToList();
            }

            List<ClienteSeleccionDto> clientesDto =
                clientes
                    .Select(cliente => new ClienteSeleccionDto
                    {
                        Id = cliente.Id,
                        CUIT = cliente.CUIT,
                        RazonSocial = cliente.RazonSocial,
                        Localidad = cliente.Localidad,
                        CreditoDisponible =
                            cliente.CalcularCreditoDisponible(),
                        Activo = cliente.Activo
                    })
                    .ToList();

            dgvSeleccionClientes.DataSource = null;
            dgvSeleccionClientes.DataSource = clientesDto;

            dgvSeleccionClientes.ClearSelection();
            dgvSeleccionClientes.CurrentCell = null;

            LimpiarSeleccionCliente();
        }

        private void btnBuscarCliente_Click(
            object sender,
            EventArgs e)
        {
            CargarClientes(txtBuscarCliente.Text);
        }

        private void btnActualizarClientes_Click(
            object sender,
            EventArgs e)
        {
            txtBuscarCliente.Clear();
            CargarClientes();
            txtBuscarCliente.Focus();
        }

        private void txtBuscarCliente_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            CargarClientes(txtBuscarCliente.Text);
        }

        private void dgvSeleccionClientes_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            SeleccionarClienteActual();
        }

        private void dgvSeleccionClientes_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            SeleccionarClienteActual();
        }

        private void SeleccionarClienteActual()
        {
            if (dgvSeleccionClientes.CurrentRow == null)
            {
                LimpiarSeleccionCliente();
                return;
            }

            ClienteSeleccionDto clienteDto =
                dgvSeleccionClientes.CurrentRow.DataBoundItem
                    as ClienteSeleccionDto;

            if (clienteDto == null)
            {
                LimpiarSeleccionCliente();
                return;
            }

            _clienteSeleccionado =
                _clienteService.ObtenerPorId(clienteDto.Id);

            if (_clienteSeleccionado == null)
            {
                LimpiarSeleccionCliente();
                return;
            }

            lblCUITSeleccionado.Text =
                _clienteSeleccionado.CUIT;

            lblRazonSeleccionada.Text =
                _clienteSeleccionado.RazonSocial;

            btnSiguienteCliente.Enabled =
                _clienteSeleccionado.Activo;
        }

        private void LimpiarSeleccionCliente()
        {
            _clienteSeleccionado = null;

            lblCUITSeleccionado.Text =
                "Sin seleccionar";

            lblRazonSeleccionada.Text =
                "Sin seleccionar";

            btnSiguienteCliente.Enabled = false;
        }

        private void btnSiguienteCliente_Click(
            object sender,
            EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                MessageBox.Show(
                    "Debe seleccionar un cliente para continuar.",
                    "Cliente requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!_clienteSeleccionado.Activo)
            {
                MessageBox.Show(
                    "El cliente seleccionado no se encuentra activo.",
                    "Cliente inactivo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            lblClientePresupuestoCUIT.Text =
                _clienteSeleccionado.CUIT;

            lblClientePresupuestoRazon.Text =
                _clienteSeleccionado.RazonSocial;

            _presupuestoService.CrearPresupuestosDemostracion(
                _clienteSeleccionado.Id);

            _presupuestoSeleccionado = null;

            txtBuscarPresupuesto.Clear();
            CargarPresupuestos();

            TabPage.SelectedTab = tabPresupuesto;
        }

        private void FrmConfirmarPedido_Activated(
            object sender,
            EventArgs e)
        {
            if (TabPage.SelectedTab == tabCliente)
            {
                CargarClientes(txtBuscarCliente.Text);
            }
        }
        // =====================================================
        // PASO 2: BUSCAR Y SELECCIONAR PRESUPUESTO
        // =====================================================

        private void CargarPresupuestos(string filtro = "")
        {
            if (_clienteSeleccionado == null)
            {
                dgvPresupuestos.DataSource = null;
                LimpiarSeleccionPresupuesto();
                return;
            }

            List<Presupuesto> presupuestos =
                _presupuestoService.BuscarPorCliente(
                    _clienteSeleccionado.Id,
                    filtro);

            List<PresupuestoSeleccionDto> presupuestosDto =
                presupuestos
                    .Select(presupuesto =>
                        new PresupuestoSeleccionDto
                        {
                            Id = presupuesto.Id,
                            Numero = presupuesto.Numero,
                            FechaEmision = presupuesto.FechaEmision,
                            FechaVencimiento = presupuesto.FechaVencimiento,

                            Moneda =
                                presupuesto.Moneda ==
                                Moneda.DolaresEstadounidenses
                                    ? "USD"
                                    : "ARS",

                            Total = presupuesto.CalcularTotal(),

                            Anticipo =
                                presupuesto.CalcularAnticipo(),

                            Estado =
                                presupuesto.Estado.ToString(),

                            Vigente =
                                presupuesto.EstaVigente(),

                            PuedeConfirmarse =
                                presupuesto.PuedeConfirmarse()
                        })
                    .ToList();

            dgvPresupuestos.DataSource = null;
            dgvPresupuestos.DataSource = presupuestosDto;

            dgvPresupuestos.ClearSelection();
            dgvPresupuestos.CurrentCell = null;

            LimpiarSeleccionPresupuesto();
        }

        private void btnBuscarPresupuesto_Click(
            object sender,
            EventArgs e)
        {
            CargarPresupuestos(
                txtBuscarPresupuesto.Text);
        }

        private void btnActualizarPresupuestos_Click(
            object sender,
            EventArgs e)
        {
            txtBuscarPresupuesto.Clear();

            CargarPresupuestos();

            txtBuscarPresupuesto.Focus();
        }

        private void txtBuscarPresupuesto_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;

            CargarPresupuestos(
                txtBuscarPresupuesto.Text);
        }

        private void dgvPresupuestos_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            SeleccionarPresupuestoActual();
        }

        private void SeleccionarPresupuestoActual()
        {
            if (dgvPresupuestos.CurrentRow == null)
            {
                LimpiarSeleccionPresupuesto();
                return;
            }

            PresupuestoSeleccionDto presupuestoDto =
                dgvPresupuestos.CurrentRow.DataBoundItem
                    as PresupuestoSeleccionDto;

            if (presupuestoDto == null)
            {
                LimpiarSeleccionPresupuesto();
                return;
            }

            _presupuestoSeleccionado =
                _presupuestoService.ObtenerPorId(
                    presupuestoDto.Id);

            if (_presupuestoSeleccionado == null)
            {
                LimpiarSeleccionPresupuesto();
                return;
            }

            lblPresupuestoSeleccionado.Text =
                _presupuestoSeleccionado.Numero;

            lblTotalSeleccionado.Text =
                _presupuestoSeleccionado
                    .CalcularTotal()
                    .ToString("N2");

            lblEstadoSeleccionado.Text =
                _presupuestoSeleccionado.Estado.ToString();

            btnSiguientePresupuesto.Enabled =
                _presupuestoSeleccionado.PuedeConfirmarse();
        }

        private void LimpiarSeleccionPresupuesto()
        {
            _presupuestoSeleccionado = null;

            lblPresupuestoSeleccionado.Text =
                "Sin seleccionar";

            lblTotalSeleccionado.Text =
                "0,00";

            lblEstadoSeleccionado.Text =
                "Sin seleccionar";

            btnSiguientePresupuesto.Enabled = false;
        }

        private void btnAnteriorPresupuesto_Click(
            object sender,
            EventArgs e)
        {
            LimpiarSeleccionPresupuesto();

            TabPage.SelectedTab =
                tabCliente;
        }

        private void btnSiguientePresupuesto_Click(
            object sender,
            EventArgs e)
        {
            if (_presupuestoSeleccionado == null)
            {
                MessageBox.Show(
                    "Debe seleccionar un presupuesto para continuar.",
                    "Presupuesto requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!_presupuestoSeleccionado.PuedeConfirmarse())
            {
                MessageBox.Show(
                    "El presupuesto no está vigente, no fue aceptado " +
                    "o no contiene detalles.",
                    "Presupuesto no confirmable",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            TabPage.SelectedTab =
                tabProductos;
        }

        /*
         * Estos métodos permanecen temporalmente porque están
         * conectados desde FrmConfirmarPedido.Designer.cs.
         */

        private void dgvSeleccionClientes_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
        }

        private void grpClienteSeleccionado_Enter(
            object sender,
            EventArgs e)
        {
        }

        private void lblClientePresupuestoCUITTitulo_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label6_Click(
            object sender,
            EventArgs e)
        {
        }

        private void lblProductosTotal_Click(object sender, EventArgs e)
        {

        }
    }

}


