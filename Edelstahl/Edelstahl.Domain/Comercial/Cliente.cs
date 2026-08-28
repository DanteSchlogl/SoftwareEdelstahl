using System;
using Edelstahl.Domain.Common;

namespace Edelstahl.Domain.Comercial
{
    /// <summary>
    /// Representa a un cliente de Edelstahl.
    /// </summary>
    public class Cliente : Entity
    {
        public string CUIT { get; set; }

        public string RazonSocial { get; set; }
        
        public string DireccionFacturacion { get; set; }

        public string DireccionEntrega { get; set; }

        public string Localidad { get; set; }

        public string Provincia { get; set; }

        public string CodigoPostal { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public decimal LimiteCredito { get; set; }

        public decimal DeudaActual { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaAlta { get; set; }

        public Cliente()
        {
            Activo = true;
            DeudaActual = 0;
            FechaAlta = DateTime.Now;
        }

        public decimal CalcularCreditoDisponible()
        {
            return LimiteCredito - DeudaActual;
        }
    }
}
