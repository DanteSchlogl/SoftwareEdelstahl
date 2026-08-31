using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edelstahl.DAL.Factory;
using Edelstahl.DAL.Interfaces;
using Edelstahl.Domain.Comercial;

namespace Edelstahl.BLL.Services
{
    /// <summary>
    /// Coordina las operaciones relacionadas con presupuestos.
    /// </summary>
    public class PresupuestoService
    {
        private readonly IPresupuestoRepository _presupuestoRepository;

        public PresupuestoService()
        {
            _presupuestoRepository =
                FactoryDataAccess.PresupuestoRepository;
        }

        public PresupuestoService(
            IPresupuestoRepository presupuestoRepository)
        {
            _presupuestoRepository =
                presupuestoRepository
                ?? throw new ArgumentNullException(
                    nameof(presupuestoRepository));
        }

        public Presupuesto Registrar(Presupuesto presupuesto)
        {
            ValidarPresupuesto(presupuesto);

            presupuesto.Numero =
                presupuesto.Numero.Trim().ToUpperInvariant();

            if (_presupuestoRepository.ExistsByNumero(
                presupuesto.Numero))
            {
                throw new InvalidOperationException(
                    $"Ya existe el presupuesto " +
                    $"'{presupuesto.Numero}'.");
            }

            foreach (DetallePresupuesto detalle
                in presupuesto.Detalles)
            {
                detalle.PresupuestoId = presupuesto.Id;
            }

            _presupuestoRepository.Add(presupuesto);

            return presupuesto;
        }

        public Presupuesto ObtenerPorId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(
                    "El identificador del presupuesto no es válido.",
                    nameof(id));
            }

            return _presupuestoRepository.GetById(id);
        }

        public Presupuesto ObtenerPorNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
            {
                return null;
            }

            return _presupuestoRepository.GetByNumero(
                numero.Trim().ToUpperInvariant());
        }

        public List<Presupuesto> ObtenerPorCliente(
            Guid clienteId)
        {
            if (clienteId == Guid.Empty)
            {
                throw new ArgumentException(
                    "El identificador del cliente no es válido.",
                    nameof(clienteId));
            }

            return _presupuestoRepository
                .GetByClienteId(clienteId)
                .OrderByDescending(
                    presupuesto => presupuesto.FechaEmision)
                .ToList();
        }

        public List<Presupuesto> BuscarPorCliente(
            Guid clienteId,
            string filtro)
        {
            List<Presupuesto> presupuestos =
                ObtenerPorCliente(clienteId);

            if (string.IsNullOrWhiteSpace(filtro))
            {
                return presupuestos;
            }

            string filtroNormalizado =
                filtro.Trim().ToLowerInvariant();

            return presupuestos
                .Where(presupuesto =>
                    (presupuesto.Numero ?? string.Empty)
                        .ToLowerInvariant()
                        .Contains(filtroNormalizado)
                    ||
                    presupuesto.Estado
                        .ToString()
                        .ToLowerInvariant()
                        .Contains(filtroNormalizado))
                .ToList();
        }

        public void CrearPresupuestosDemostracion(
            Guid clienteId)
        {
            if (clienteId == Guid.Empty)
            {
                throw new ArgumentException(
                    "El identificador del cliente no es válido.",
                    nameof(clienteId));
            }

            if (_presupuestoRepository
                .GetByClienteId(clienteId)
                .Any())
            {
                return;
            }

            Presupuesto ventaEstandar =
                CrearVentaEstandar(clienteId);

            Presupuesto fabricacionEspecial =
                CrearFabricacionEspecial(clienteId);

            Registrar(ventaEstandar);
            Registrar(fabricacionEspecial);
        }

        private static Presupuesto CrearVentaEstandar(
            Guid clienteId)
        {
            Presupuesto presupuesto = new Presupuesto
            {
                Numero = CrearNumero(
                    "VEN",
                    clienteId),

                ClienteId = clienteId,

                FechaEmision = DateTime.Today,

                FechaVencimiento =
                    DateTime.Today.AddDays(15),

                Moneda =
                    Moneda.DolaresEstadounidenses,

                TipoCambio = 1515m,

                PorcentajeIVA = 21m,

                PorcentajeAnticipo = 0m,

                CondicionPago = "Contado",

                PlazoEntrega =
                    "Entrega inmediata según disponibilidad",

                EntregaIncluida = false,

                Estado = EstadoPresupuesto.Aceptado,

                Observaciones =
                    "Cotización de venta estándar."
            };

            presupuesto.Detalles.Add(
                new DetallePresupuesto
                {
                    Codigo = "MP304-15",

                    Descripcion =
                        "Mirilla plana para soldar AISI 304",

                    TipoItem =
                        TipoItemPresupuesto.Producto,

                    Cantidad = 4m,

                    PrecioUnitario = 33.47m
                });

            presupuesto.Detalles.Add(
                new DetallePresupuesto
                {
                    Codigo = "MCPS304-15",

                    Descripcion =
                        "Manguito Clamp largo AISI 304",

                    TipoItem =
                        TipoItemPresupuesto.Producto,

                    Cantidad = 4m,

                    PrecioUnitario = 4.23m
                });

            presupuesto.Detalles.Add(
                new DetallePresupuesto
                {
                    Codigo = "SERV-SOLD",

                    Descripcion =
                        "Servicio de soldadura y pulido sanitario",

                    TipoItem =
                        TipoItemPresupuesto.Servicio,

                    Cantidad = 4m,

                    PrecioUnitario = 45m
                });

            return presupuesto;
        }

        private static Presupuesto CrearFabricacionEspecial(
            Guid clienteId)
        {
            Presupuesto presupuesto = new Presupuesto
            {
                Numero = CrearNumero(
                    "FAB",
                    clienteId),

                ClienteId = clienteId,

                FechaEmision = DateTime.Today,

                FechaVencimiento =
                    DateTime.Today.AddDays(30),

                Moneda =
                    Moneda.DolaresEstadounidenses,

                TipoCambio = 1515m,

                PorcentajeIVA = 10.5m,

                PorcentajeAnticipo = 50m,

                CondicionPago =
                    "50% de anticipo y 50% contra entrega",

                PlazoEntrega =
                    "45 a 60 días desde la recepción del anticipo",

                EntregaIncluida = false,

                Estado = EstadoPresupuesto.Aceptado,

                Observaciones =
                    "Cotización de fabricación especial."
            };

            presupuesto.Detalles.Add(
                new DetallePresupuesto
                {
                    Codigo = "FILTRO-10",

                    Descripcion =
                        "Filtro de ranura continua de 10 pulgadas",

                    DescripcionTecnica =
                        "Construcción sanitaria en acero inoxidable, " +
                        "configuración en L y conexión 226.",

                    TipoItem =
                        TipoItemPresupuesto.FabricacionEspecial,

                    Cantidad = 2m,

                    PrecioUnitario = 1240m
                });

            presupuesto.Detalles.Add(
                new DetallePresupuesto
                {
                    Codigo = "FILTRO-20",

                    Descripcion =
                        "Filtro de ranura continua de 20 pulgadas",

                    DescripcionTecnica =
                        "Construcción sanitaria en acero inoxidable, " +
                        "configuración en L y conexión 226.",

                    TipoItem =
                        TipoItemPresupuesto.FabricacionEspecial,

                    Cantidad = 2m,

                    PrecioUnitario = 1492m
                });

            return presupuesto;
        }

        private static string CrearNumero(
            string prefijo,
            Guid clienteId)
        {
            string identificadorCorto =
                clienteId
                    .ToString("N")
                    .Substring(0, 6)
                    .ToUpperInvariant();

            return string.Format(
                "PRE-{0}-{1}-{2}",
                DateTime.Today.Year,
                prefijo,
                identificadorCorto);
        }

        private static void ValidarPresupuesto(
            Presupuesto presupuesto)
        {
            if (presupuesto == null)
            {
                throw new ArgumentNullException(
                    nameof(presupuesto),
                    "El presupuesto no puede ser nulo.");
            
            }


        }
    }
}
