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

        public FrmConfirmarPedido()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _clienteService = new ClienteService();
            _presupuestoService = new PresupuestoService();

            ConfigurarGrillaClientes();
            ConfigurarGrillaPresupuestos();
            ConfigurarGrillaProductos();
            ConfigurarEventos();

            CargarClientes();
            LimpiarSeleccionPresupuesto();
            LimpiarProductos();
        }

        private void ConfigurarGrillaClientes()
        {
            ConfigurarGrillaBase(dgvSeleccionClientes);
        }

        private void ConfigurarGrillaPresupuestos()
        {
            ConfigurarGrillaBase(dgvPresupuestos);
        }

        private void ConfigurarGrillaProductos()
        {
            ConfigurarGrillaBase(dgvDetallePresupuesto);
        }

        private static void ConfigurarGrillaBase(DataGridView grilla)
        {
            grilla.AutoGenerateColumns = false;
            grilla.ReadOnly = true;
            grilla.MultiSelect = false;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.AllowUserToResizeRows = false;
            grilla.RowHeadersVisible = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.DefaultCellStyle.ForeColor = Color.Black;
            grilla.DefaultCellStyle.BackColor = Color.White;
            grilla.DefaultCellStyle.SelectionForeColor = Color.White;
            grilla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
        }

        private void ConfigurarEventos()
        {
            btnBuscarCliente.Click += btnBuscarCliente_Click;
            btnActualizarClientes.Click += btnActualizarClientes_Click;
            btnSiguienteCliente.Click += btnSiguienteCliente_Click;
            dgvSeleccionClientes.CellClick += dgvSeleccionClientes_CellClick;
            dgvSeleccionClientes.CellDoubleClick += dgvSeleccionClientes_CellDoubleClick;
            txtBuscarCliente.KeyDown += txtBuscarCliente_KeyDown;

            btnBuscarPresupuesto.Click += btnBuscarPresupuesto_Click;
            btnActualizarPresupuestos.Click += btnActualizarPresupuestos_Click;
            btnAnteriorPresupuesto.Click += btnAnteriorPresupuesto_Click;
            btnSiguientePresupuesto.Click += btnSiguientePresupuesto_Click;
            dgvPresupuestos.CellClick += dgvPresupuestos_CellClick;
            txtBuscarPresupuesto.KeyDown += txtBuscarPresupuesto_KeyDown;

            btnAnteriorProductos.Click += btnAnteriorProductos_Click;
            btnSiguienteProductos.Click += btnSiguienteProductos_Click;

            Activated += FrmConfirmarPedido_Activated;
        }

        private void CargarClientes(string filtro = "")
        {
            List<Cliente> clientes = _clienteService.ObtenerTodos();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                string filtroTexto = filtro.Trim().ToLowerInvariant();
                string filtroCUIT = filtroTexto.Replace("-", string.Empty).Replace(" ", string.Empty);

                clientes = clientes
                    .Where(cliente =>
                    {
                        string clienteCUIT = (cliente.CUIT ?? string.Empty)
                            .Replace("-", string.Empty)
                            .Replace(" ", string.Empty);

                        string clienteRazonSocial = (cliente.RazonSocial ?? string.Empty)
                            .ToLowerInvariant();

                        return clienteCUIT.Contains(filtroCUIT) ||
                               clienteRazonSocial.Contains(filtroTexto);
                    })
                    .ToList();
            }

            List<ClienteSeleccionDto> clientesDto = clientes
                .Select(cliente => new ClienteSeleccionDto
                {
                    Id = cliente.Id,
                    CUIT = cliente.CUIT,
                    RazonSocial = cliente.RazonSocial,
                    Localidad = cliente.Localidad,
                    CreditoDisponible = cliente.CalcularCreditoDisponible(),
                    Activo = cliente.Activo
                })
                .ToList();

            dgvSeleccionClientes.DataSource = null;
            dgvSeleccionClientes.DataSource = clientesDto;
            dgvSeleccionClientes.ClearSelection();
            dgvSeleccionClientes.CurrentCell = null;
            LimpiarSeleccionCliente();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            CargarClientes(txtBuscarCliente.Text);
        }

        private void btnActualizarClientes_Click(object sender, EventArgs e)
        {
            txtBuscarCliente.Clear();
            CargarClientes();
            txtBuscarCliente.Focus();
        }

        private void txtBuscarCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            CargarClientes(txtBuscarCliente.Text);
        }

        private void dgvSeleccionClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SeleccionarClienteActual();
            }
        }

        private void dgvSeleccionClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SeleccionarClienteActual();
            }
        }

        private void SeleccionarClienteActual()
        {
            ClienteSeleccionDto clienteDto =
                dgvSeleccionClientes.CurrentRow?.DataBoundItem as ClienteSeleccionDto;

            if (clienteDto == null)
            {
                LimpiarSeleccionCliente();
                return;
            }

            _clienteSeleccionado = _clienteService.ObtenerPorId(clienteDto.Id);

            if (_clienteSeleccionado == null)
            {
                LimpiarSeleccionCliente();
                return;
            }

            lblCUITSeleccionado.Text = _clienteSeleccionado.CUIT;
            lblRazonSeleccionada.Text = _clienteSeleccionado.RazonSocial;
            btnSiguienteCliente.Enabled = _clienteSeleccionado.Activo;
        }

        private void LimpiarSeleccionCliente()
        {
            _clienteSeleccionado = null;
            lblCUITSeleccionado.Text = "Sin seleccionar";
            lblRazonSeleccionada.Text = "Sin seleccionar";
            btnSiguienteCliente.Enabled = false;
        }

        private void btnSiguienteCliente_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                MostrarAdvertencia("Debe seleccionar un cliente para continuar.", "Cliente requerido");
                return;
            }

            if (!_clienteSeleccionado.Activo)
            {
                MostrarAdvertencia("El cliente seleccionado no se encuentra activo.", "Cliente inactivo");
                return;
            }

            lblClientePresupuestoCUIT.Text = _clienteSeleccionado.CUIT;
            lblClientePresupuestoRazon.Text = _clienteSeleccionado.RazonSocial;

            _presupuestoService.CrearPresupuestosDemostracion(_clienteSeleccionado.Id);
            _presupuestoSeleccionado = null;

            txtBuscarPresupuesto.Clear();
            CargarPresupuestos();
            TabPage.SelectedTab = tabPresupuesto;
        }

        private void FrmConfirmarPedido_Activated(object sender, EventArgs e)
        {
            if (_clienteService != null && TabPage.SelectedTab == tabCliente)
            {
                CargarClientes(txtBuscarCliente.Text);
            }
        }

        private void CargarPresupuestos(string filtro = "")
        {
            if (_clienteSeleccionado == null)
            {
                dgvPresupuestos.DataSource = null;
                LimpiarSeleccionPresupuesto();
                return;
            }

            List<Presupuesto> presupuestos = _presupuestoService.BuscarPorCliente(
                _clienteSeleccionado.Id,
                filtro);

            List<PresupuestoSeleccionDto> presupuestosDto = presupuestos
                .Select(presupuesto => new PresupuestoSeleccionDto
                {
                    Id = presupuesto.Id,
                    Numero = presupuesto.Numero,
                    FechaEmision = presupuesto.FechaEmision,
                    FechaVencimiento = presupuesto.FechaVencimiento,
                    Moneda = presupuesto.Moneda == Moneda.DolaresEstadounidenses ? "USD" : "ARS",
                    Total = presupuesto.CalcularTotal(),
                    Anticipo = presupuesto.CalcularAnticipo(),
                    Estado = presupuesto.Estado.ToString(),
                    Vigente = presupuesto.EstaVigente(),
                    PuedeConfirmarse = presupuesto.PuedeConfirmarse()
                })
                .ToList();

            dgvPresupuestos.DataSource = null;
            dgvPresupuestos.DataSource = presupuestosDto;
            dgvPresupuestos.ClearSelection();
            dgvPresupuestos.CurrentCell = null;
            LimpiarSeleccionPresupuesto();
        }

        private void btnBuscarPresupuesto_Click(object sender, EventArgs e)
        {
            CargarPresupuestos(txtBuscarPresupuesto.Text);
        }

        private void btnActualizarPresupuestos_Click(object sender, EventArgs e)
        {
            txtBuscarPresupuesto.Clear();
            CargarPresupuestos();
            txtBuscarPresupuesto.Focus();
        }

        private void txtBuscarPresupuesto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            CargarPresupuestos(txtBuscarPresupuesto.Text);
        }

        private void dgvPresupuestos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SeleccionarPresupuestoActual();
            }
        }

        private void SeleccionarPresupuestoActual()
        {
            PresupuestoSeleccionDto presupuestoDto =
                dgvPresupuestos.CurrentRow?.DataBoundItem as PresupuestoSeleccionDto;

            if (presupuestoDto == null)
            {
                LimpiarSeleccionPresupuesto();
                return;
            }

            _presupuestoSeleccionado = _presupuestoService.ObtenerPorId(presupuestoDto.Id);

            if (_presupuestoSeleccionado == null)
            {
                LimpiarSeleccionPresupuesto();
                return;
            }

            lblPresupuestoSeleccionado.Text = _presupuestoSeleccionado.Numero;
            lblTotalSeleccionado.Text = _presupuestoSeleccionado.CalcularTotal().ToString("N2");
            lblEstadoSeleccionado.Text = _presupuestoSeleccionado.Estado.ToString();
            btnSiguientePresupuesto.Enabled = _presupuestoSeleccionado.PuedeConfirmarse();
        }

        private void LimpiarSeleccionPresupuesto()
        {
            _presupuestoSeleccionado = null;
            lblPresupuestoSeleccionado.Text = "Sin seleccionar";
            lblTotalSeleccionado.Text = "0,00";
            lblEstadoSeleccionado.Text = "Sin seleccionar";
            btnSiguientePresupuesto.Enabled = false;
        }

        private void btnAnteriorPresupuesto_Click(object sender, EventArgs e)
        {
            LimpiarSeleccionPresupuesto();
            TabPage.SelectedTab = tabCliente;
        }

        private void btnSiguientePresupuesto_Click(object sender, EventArgs e)
        {
            if (_presupuestoSeleccionado == null)
            {
                MostrarAdvertencia("Debe seleccionar un presupuesto para continuar.", "Presupuesto requerido");
                return;
            }

            if (!_presupuestoSeleccionado.PuedeConfirmarse())
            {
                MostrarAdvertencia(
                    "El presupuesto no esta vigente, no fue aceptado o no contiene detalles.",
                    "Presupuesto no confirmable");
                return;
            }

            CargarProductosPresupuesto();
            TabPage.SelectedTab = tabProductos;
        }

        private void CargarProductosPresupuesto()
        {
            if (_presupuestoSeleccionado == null || _clienteSeleccionado == null)
            {
                LimpiarProductos();
                return;
            }

            List<DetallePresupuestoDto> detallesDto = _presupuestoSeleccionado.Detalles
                .Select(detalle => new DetallePresupuestoDto
                {
                    Codigo = detalle.Codigo,
                    Descripcion = detalle.Descripcion,
                    Tipo = detalle.TipoItem.ToString(),
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    Subtotal = detalle.CalcularSubtotal()
                })
                .ToList();

            dgvDetallePresupuesto.DataSource = null;
            dgvDetallePresupuesto.DataSource = detallesDto;
            dgvDetallePresupuesto.ClearSelection();
            dgvDetallePresupuesto.CurrentCell = null;

            lblProductoPresupuestoNumero.Text = _presupuestoSeleccionado.Numero;
            lblProductoCliente.Text = _clienteSeleccionado.RazonSocial;
            lblProductosSubtotal.Text = _presupuestoSeleccionado.CalcularSubtotal().ToString("N2");
            lblProductosIVA.Text = _presupuestoSeleccionado.CalcularIVA().ToString("N2");
            lblProductosTotal.Text = _presupuestoSeleccionado.CalcularTotal().ToString("N2");
            lblProductosMoneda.Text =
                _presupuestoSeleccionado.Moneda == Moneda.DolaresEstadounidenses ? "USD" : "ARS";
            btnSiguienteProductos.Enabled = detallesDto.Count > 0;
        }

        private void LimpiarProductos()
        {
            dgvDetallePresupuesto.DataSource = null;
            lblProductoPresupuestoNumero.Text = "Sin seleccionar";
            lblProductoCliente.Text = "Sin seleccionar";
            lblProductosSubtotal.Text = "0,00";
            lblProductosIVA.Text = "0,00";
            lblProductosTotal.Text = "0,00";
            lblProductosMoneda.Text = "Sin seleccionar";
            btnSiguienteProductos.Enabled = false;
        }

        private void btnAnteriorProductos_Click(object sender, EventArgs e)
        {
            TabPage.SelectedTab = tabPresupuesto;
        }

        private void btnSiguienteProductos_Click(object sender, EventArgs e)
        {
            if (_presupuestoSeleccionado == null)
            {
                MostrarAdvertencia("Debe seleccionar un presupuesto.", "Presupuesto requerido");
                return;
            }

            if (_presupuestoSeleccionado.Detalles == null ||
                _presupuestoSeleccionado.Detalles.Count == 0)
            {
                MostrarAdvertencia(
                    "El presupuesto no contiene productos o servicios.",
                    "Presupuesto sin detalles");
                return;
            }

            TabPage.SelectedTab = tabValidacion;
        }

        private static void MostrarAdvertencia(string mensaje, string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        // Eventos temporales conectados desde FrmConfirmarPedido.Designer.cs.
        private void dgvSeleccionClientes_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
        }

        private void grpClienteSeleccionado_Enter(object sender, EventArgs e)
        {
        }

        private void lblClientePresupuestoCUITTitulo_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void lblProductosTotal_Click(object sender, EventArgs e)
        {
        }
    }
}






