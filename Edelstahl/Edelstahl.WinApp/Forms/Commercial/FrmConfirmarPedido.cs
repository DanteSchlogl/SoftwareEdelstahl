using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
        private string _numeroPedidoGenerado;

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
            LimpiarValidacionCredito();
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
            // Paso 1: Cliente
            btnBuscarCliente.Click += btnBuscarCliente_Click;
            btnActualizarClientes.Click += btnActualizarClientes_Click;
            btnSiguienteCliente.Click += btnSiguienteCliente_Click;
            dgvSeleccionClientes.CellClick += dgvSeleccionClientes_CellClick;
            dgvSeleccionClientes.CellDoubleClick += dgvSeleccionClientes_CellDoubleClick;
            txtBuscarCliente.KeyDown += txtBuscarCliente_KeyDown;

            // Paso 2: Presupuesto
            btnBuscarPresupuesto.Click += btnBuscarPresupuesto_Click;
            btnActualizarPresupuestos.Click += btnActualizarPresupuestos_Click;
            btnAnteriorPresupuesto.Click += btnAnteriorPresupuesto_Click;
            btnSiguientePresupuesto.Click += btnSiguientePresupuesto_Click;
            dgvPresupuestos.CellClick += dgvPresupuestos_CellClick;
            txtBuscarPresupuesto.KeyDown += txtBuscarPresupuesto_KeyDown;

            // Paso 3: Productos
            btnAnteriorProductos.Click += btnAnteriorProductos_Click;
            btnSiguienteProductos.Click += btnSiguienteProductos_Click;

            // Paso 4: Validacion
            btnAnteriorValidacion.Click += btnAnteriorValidacion_Click;
            btnSiguienteValidacion.Click += btnSiguienteValidacion_Click;

            // Paso 6: Confirmacion
            btnAnteriorConfirmacion.Click += btnAnteriorConfirmacion_Click;
            btnConfirmarPedido.Click += btnConfirmarPedido_Click;

            // Paso 7: Resultado
            btnNuevoPedido.Click += btnNuevoPedido_Click;
            btnCerrarResultado.Click += btnCerrarResultado_Click;

            // Actualizacion general
            Activated += FrmConfirmarPedido_Activated;
        }

        // =====================================================
        // PASO 1: BUSCAR Y SELECCIONAR CLIENTE
        // =====================================================

        private void CargarClientes(string filtro = "")
        {
            List<Cliente> clientes = _clienteService.ObtenerTodos();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                string filtroTexto = filtro.Trim().ToLowerInvariant();
                string filtroCUIT = filtroTexto
                    .Replace("-", string.Empty)
                    .Replace(" ", string.Empty);

                clientes = clientes
                    .Where(cliente =>
                    {
                        string clienteCUIT = (cliente.CUIT ?? string.Empty)
                            .Replace("-", string.Empty)
                            .Replace(" ", string.Empty);

                        string clienteRazonSocial =
                            (cliente.RazonSocial ?? string.Empty).ToLowerInvariant();

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

        private void dgvSeleccionClientes_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SeleccionarClienteActual();
            }
        }

        private void dgvSeleccionClientes_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
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
                MostrarAdvertencia(
                    "Debe seleccionar un cliente para continuar.",
                    "Cliente requerido");
                return;
            }

            if (!_clienteSeleccionado.Activo)
            {
                MostrarAdvertencia(
                    "El cliente seleccionado no se encuentra activo.",
                    "Cliente inactivo");
                return;
            }

            lblClientePresupuestoCUIT.Text = _clienteSeleccionado.CUIT;
            lblClientePresupuestoRazon.Text = _clienteSeleccionado.RazonSocial;

            _presupuestoService.CrearPresupuestosDemostracion(
                _clienteSeleccionado.Id);

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

            List<PresupuestoSeleccionDto> presupuestosDto = presupuestos
                .Select(presupuesto => new PresupuestoSeleccionDto
                {
                    Id = presupuesto.Id,
                    Numero = presupuesto.Numero,
                    FechaEmision = presupuesto.FechaEmision,
                    FechaVencimiento = presupuesto.FechaVencimiento,
                    Moneda = presupuesto.Moneda == Moneda.DolaresEstadounidenses
                        ? "USD"
                        : "ARS",
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

        private void dgvPresupuestos_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
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

            _presupuestoSeleccionado =
                _presupuestoService.ObtenerPorId(presupuestoDto.Id);

            if (_presupuestoSeleccionado == null)
            {
                LimpiarSeleccionPresupuesto();
                return;
            }

            lblPresupuestoSeleccionado.Text =
                _presupuestoSeleccionado.Numero;

            lblTotalSeleccionado.Text =
                _presupuestoSeleccionado.CalcularTotal().ToString("N2");

            lblEstadoSeleccionado.Text =
                _presupuestoSeleccionado.Estado.ToString();

            btnSiguientePresupuesto.Enabled =
                _presupuestoSeleccionado.PuedeConfirmarse();
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
                MostrarAdvertencia(
                    "Debe seleccionar un presupuesto para continuar.",
                    "Presupuesto requerido");
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

        // =====================================================
        // PASO 3: REVISAR PRODUCTOS Y CANTIDADES
        // =====================================================

        private void CargarProductosPresupuesto()
        {
            if (_presupuestoSeleccionado == null ||
                _clienteSeleccionado == null)
            {
                LimpiarProductos();
                return;
            }

            List<DetallePresupuestoDto> detallesDto =
                _presupuestoSeleccionado.Detalles
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

            lblProductoPresupuestoNumero.Text =
                _presupuestoSeleccionado.Numero;

            lblProductoCliente.Text =
                _clienteSeleccionado.RazonSocial;

            lblProductosSubtotal.Text =
                _presupuestoSeleccionado.CalcularSubtotal().ToString("N2");

            lblProductosIVA.Text =
                _presupuestoSeleccionado.CalcularIVA().ToString("N2");

            lblProductosTotal.Text =
                _presupuestoSeleccionado.CalcularTotal().ToString("N2");

            lblProductosMoneda.Text =
                _presupuestoSeleccionado.Moneda ==
                Moneda.DolaresEstadounidenses
                    ? "USD"
                    : "ARS";

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
                MostrarAdvertencia(
                    "Debe seleccionar un presupuesto.",
                    "Presupuesto requerido");
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

            CargarValidacionCredito();
            TabPage.SelectedTab = tabValidacion;
        }

        // =====================================================
        // PASO 4: VALIDAR CREDITO
        // =====================================================

        private void CargarValidacionCredito()
        {
            if (_clienteSeleccionado == null ||
                _presupuestoSeleccionado == null)
            {
                LimpiarValidacionCredito();
                return;
            }

            decimal limiteCredito = _clienteSeleccionado.LimiteCredito;
            decimal deudaActual = _clienteSeleccionado.DeudaActual;
            decimal creditoDisponible =
                _clienteSeleccionado.CalcularCreditoDisponible();

            decimal totalPresupuesto =
                _presupuestoSeleccionado.CalcularTotal();

            decimal totalEnPesos =
                _presupuestoSeleccionado.CalcularTotalEnPesos();

            decimal anticipo =
                _presupuestoSeleccionado.CalcularAnticipo();

            decimal anticipoEnPesos =
                ConvertirImporteAPesos(anticipo);

            decimal creditoNecesario =
                _presupuestoSeleccionado.RequiereAnticipo()
                    ? totalEnPesos - anticipoEnPesos
                    : totalEnPesos;

            decimal saldoCredito =
                creditoDisponible - creditoNecesario;

            string moneda =
                _presupuestoSeleccionado.Moneda ==
                Moneda.DolaresEstadounidenses
                    ? "USD"
                    : "ARS";

            lblValidacionCliente.Text =
                _clienteSeleccionado.RazonSocial;

            lblValidacionPresupuesto.Text =
                _presupuestoSeleccionado.Numero;

            lblValidacionMoneda.Text = moneda;

            lblLimiteCreditoValidacion.Text =
                limiteCredito.ToString("N2") + " ARS";

            lblDeudaActualValidacion.Text =
                deudaActual.ToString("N2") + " ARS";

            lblCreditoDisponibleValidacion.Text =
                creditoDisponible.ToString("N2") + " ARS";

            lblTotalPresupuestoValidacion.Text =
                totalPresupuesto.ToString("N2") + " " + moneda;

            lblSaldoCreditoValidacion.Text =
                saldoCredito.ToString("N2") + " ARS";

            lblCondicionPagoValidacion.Text =
                _presupuestoSeleccionado.CondicionPago;

            lblAnticipoValidacion.Text =
                anticipo.ToString("N2") + " " + moneda;

            EvaluarResultadoCredito(
                creditoDisponible,
                creditoNecesario);
        }

        private decimal ConvertirImporteAPesos(decimal importe)
        {
            if (_presupuestoSeleccionado.Moneda ==
                Moneda.PesosArgentinos)
            {
                return importe;
            }

            return importe * _presupuestoSeleccionado.TipoCambio;
        }

        private void EvaluarResultadoCredito(
            decimal creditoDisponible,
            decimal creditoNecesario)
        {
            bool creditoSuficiente =
                creditoDisponible >= creditoNecesario;

            bool requiereAnticipo =
                _presupuestoSeleccionado.RequiereAnticipo();

            if (!creditoSuficiente)
            {
                lblResultadoValidacion.Text =
                    "CREDITO INSUFICIENTE";

                lblResultadoValidacion.ForeColor = Color.White;
                lblResultadoValidacion.BackColor = Color.Firebrick;
                lblSaldoCreditoValidacion.ForeColor = Color.Firebrick;
                btnSiguienteValidacion.Enabled = false;
                return;
            }

            lblSaldoCreditoValidacion.ForeColor = Color.DarkGreen;
            btnSiguienteValidacion.Enabled = true;

            if (requiereAnticipo)
            {
                lblResultadoValidacion.Text =
                    "REQUIERE ANTICIPO";

                lblResultadoValidacion.ForeColor = Color.Black;
                lblResultadoValidacion.BackColor = Color.Gold;
                return;
            }

            lblResultadoValidacion.Text =
                "CREDITO APROBADO";

            lblResultadoValidacion.ForeColor = Color.White;
            lblResultadoValidacion.BackColor = Color.SeaGreen;
        }

        private void LimpiarValidacionCredito()
        {
            lblValidacionCliente.Text = "Sin seleccionar";
            lblValidacionPresupuesto.Text = "Sin seleccionar";
            lblValidacionMoneda.Text = "Sin seleccionar";
            lblLimiteCreditoValidacion.Text = "0,00";
            lblDeudaActualValidacion.Text = "0,00";
            lblCreditoDisponibleValidacion.Text = "0,00";
            lblTotalPresupuestoValidacion.Text = "0,00";
            lblSaldoCreditoValidacion.Text = "0,00";
            lblCondicionPagoValidacion.Text = "Sin seleccionar";
            lblAnticipoValidacion.Text = "0,00";

            lblResultadoValidacion.Text = "Validacion pendiente";
            lblResultadoValidacion.ForeColor = Color.DimGray;
            lblResultadoValidacion.BackColor = Color.Gainsboro;

            btnSiguienteValidacion.Enabled = false;
        }

        private void btnAnteriorValidacion_Click(
            object sender,
            EventArgs e)
        {
            TabPage.SelectedTab = tabProductos;
        }

        private void btnSiguienteValidacion_Click(
            object sender,
            EventArgs e)
        {
            if (_clienteSeleccionado == null ||
                _presupuestoSeleccionado == null)
            {
                MostrarAdvertencia(
                    "Faltan datos del cliente o del presupuesto.",
                    "Validacion incompleta");
                return;
            }

            decimal creditoDisponible =
                _clienteSeleccionado.CalcularCreditoDisponible();

            decimal totalEnPesos =
                _presupuestoSeleccionado.CalcularTotalEnPesos();

            decimal anticipoEnPesos =
                ConvertirImporteAPesos(
                    _presupuestoSeleccionado.CalcularAnticipo());

            decimal creditoNecesario =
                _presupuestoSeleccionado.RequiereAnticipo()
                    ? totalEnPesos - anticipoEnPesos
                    : totalEnPesos;

            if (creditoDisponible < creditoNecesario)
            {
                MostrarAdvertencia(
                    "El credito disponible no alcanza para confirmar esta operacion.",
                    "Credito insuficiente");
                return;
            }

            if (_presupuestoSeleccionado.RequiereAnticipo())
            {
                TabPage.SelectedTab = tabSena;
                return;
            }

             CargarConfirmacion();

              TabPage.SelectedTab = tabConfirmacion;

        }

        private static void MostrarAdvertencia(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        // =====================================================
        // PASO 6: CONFIRMAR PEDIDO
        // =====================================================

        private void CargarConfirmacion()
        {
            if (_clienteSeleccionado == null ||
                _presupuestoSeleccionado == null)
            {
                MostrarAdvertencia(
                    "Faltan datos para mostrar la confirmacion.",
                    "Confirmacion incompleta");

                return;
            }

            string moneda =
                _presupuestoSeleccionado.Moneda ==
                Moneda.DolaresEstadounidenses
                    ? "USD"
                    : "ARS";

            lblConfirmacionCliente.Text =
                _clienteSeleccionado.RazonSocial;

            lblConfirmacionCUIT.Text =
                _clienteSeleccionado.CUIT;

            lblConfirmacionPresupuesto.Text =
                _presupuestoSeleccionado.Numero;

            lblConfirmacionCredito.Text =
                "Aprobado";

            lblConfirmacionCredito.ForeColor =
                Color.DarkGreen;

            lblConfirmacionItems.Text =
                _presupuestoSeleccionado
                    .Detalles
                    .Count
                    .ToString();

            lblConfirmacionSubtotal.Text =
                _presupuestoSeleccionado
                    .CalcularSubtotal()
                    .ToString("N2");

            lblConfirmacionIVA.Text =
                _presupuestoSeleccionado
                    .CalcularIVA()
                    .ToString("N2");

            lblConfirmacionTotal.Text =
                _presupuestoSeleccionado
                    .CalcularTotal()
                    .ToString("N2");

            lblConfirmacionMoneda.Text =
                moneda;

            lblConfirmacionCondicion.Text =
                _presupuestoSeleccionado.CondicionPago;

            lblConfirmacionAnticipo.Text =
                _presupuestoSeleccionado
                    .CalcularAnticipo()
                    .ToString("N2") +
                " " + moneda;

            btnConfirmarPedido.Enabled = true;
        }

        private void btnAnteriorConfirmacion_Click(
            object sender,
            EventArgs e)
        {
            TabPage.SelectedTab = tabValidacion;
        }

        private void btnConfirmarPedido_Click(
            object sender,
            EventArgs e)
        {
            if (_clienteSeleccionado == null ||
                _presupuestoSeleccionado == null)
            {
                MostrarAdvertencia(
                    "Faltan datos para confirmar el pedido.",
                    "Confirmacion incompleta");

                return;
            }

            DialogResult resultado =
                MessageBox.Show(
                    "¿Confirma la generacion del pedido?",
                    "Confirmar pedido",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            _numeroPedidoGenerado =
                GenerarNumeroPedido();

            _presupuestoSeleccionado.Estado =
                EstadoPresupuesto.ConvertidoEnPedido;

            btnConfirmarPedido.Enabled = false;

            MostrarResultadoExitoso();

            TabPage.SelectedTab = tabResultado;
        }

        private string GenerarNumeroPedido()
        {
            string codigo =
                Guid.NewGuid()
                    .ToString("N")
                    .Substring(0, 6)
                    .ToUpperInvariant();

            return string.Format(
                "PED-{0}-{1}",
                DateTime.Today.Year,
                codigo);
        }

        // =====================================================
        // PASO 7: MOSTRAR RESULTADO
        // =====================================================

        private void MostrarResultadoExitoso()
        {
            string moneda =
                _presupuestoSeleccionado.Moneda ==
                Moneda.DolaresEstadounidenses
                    ? "USD"
                    : "ARS";

            lblEstadoResultado.Text =
                "PEDIDO CONFIRMADO";

            lblEstadoResultado.ForeColor =
                Color.White;

            lblEstadoResultado.BackColor =
                Color.SeaGreen;

            lblResultadoNumeroPedido.Text =
                _numeroPedidoGenerado;

            lblResultadoFecha.Text =
                DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            lblResultadoCliente.Text =
                _clienteSeleccionado.RazonSocial;

            lblResultadoPresupuesto.Text =
                _presupuestoSeleccionado.Numero;

            lblResultadoTotal.Text =
                _presupuestoSeleccionado
                    .CalcularTotal()
                    .ToString("N2") +
                " " + moneda;

            lblResultadoEstado.Text =
                "Confirmado";

            lblResultadoEstado.ForeColor =
                Color.DarkGreen;

            lblMensajeResultado.Text =
                "El pedido fue generado correctamente. " +
                "El presupuesto quedo marcado como convertido.";

            btnNuevoPedido.Enabled = true;
        }

        private void btnNuevoPedido_Click(
            object sender,
            EventArgs e)
        {
            ReiniciarAsistente();

            TabPage.SelectedTab = tabCliente;
        }

        private void btnCerrarResultado_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        private void ReiniciarAsistente()
        {
            _clienteSeleccionado = null;
            _presupuestoSeleccionado = null;
            _numeroPedidoGenerado = string.Empty;

            txtBuscarCliente.Clear();
            txtBuscarPresupuesto.Clear();

            CargarClientes();
            LimpiarSeleccionCliente();
            LimpiarSeleccionPresupuesto();
            LimpiarProductos();
            LimpiarValidacionCredito();
            LimpiarConfirmacion();
            LimpiarResultado();

            
        }

        private void LimpiarConfirmacion()
        {
            lblConfirmacionCliente.Text =
                "Sin seleccionar";

            lblConfirmacionCUIT.Text =
                "Sin seleccionar";

            lblConfirmacionPresupuesto.Text =
                "Sin seleccionar";

            lblConfirmacionCredito.Text =
                "Sin validar";

            lblConfirmacionCredito.ForeColor =
                Color.DimGray;

            lblConfirmacionItems.Text =
                "0";

            lblConfirmacionSubtotal.Text =
                "0,00";

            lblConfirmacionIVA.Text =
                "0,00";

            lblConfirmacionTotal.Text =
                "0,00";

            lblConfirmacionMoneda.Text =
                "Sin seleccionar";

            lblConfirmacionCondicion.Text =
                "Sin seleccionar";

            lblConfirmacionAnticipo.Text =
                "0,00";

            btnConfirmarPedido.Enabled = false;
        }

        private void LimpiarResultado()
        {
            lblEstadoResultado.Text =
                "OPERACION PENDIENTE";

            lblEstadoResultado.ForeColor =
                Color.DimGray;

            lblEstadoResultado.BackColor =
                Color.Gainsboro;

            lblResultadoNumeroPedido.Text =
                "Sin generar";

            lblResultadoFecha.Text =
                "Sin generar";

            lblResultadoCliente.Text =
                "Sin seleccionar";

            lblResultadoPresupuesto.Text =
                "Sin seleccionar";

            lblResultadoTotal.Text =
                "0,00";

            lblResultadoEstado.Text =
                "Pendiente";

            lblResultadoEstado.ForeColor =
                Color.DimGray;

            lblMensajeResultado.Text =
                "El resultado de la operacion aparecera aqui.";

            btnNuevoPedido.Enabled = false;
        }
        // Eventos temporales conectados desde FrmConfirmarPedido.Designer.cs.
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

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void lblProductosTotal_Click(
            object sender,
            EventArgs e)
        {
        }

        private void lblDeudaActualValidacionTitulo_Click(
            object sender,
            EventArgs e)
        {
        }

        private void lblDeudaActualValidacionTitulo_Click_1(
            object sender,
            EventArgs e)
        {
        }
    }
}

