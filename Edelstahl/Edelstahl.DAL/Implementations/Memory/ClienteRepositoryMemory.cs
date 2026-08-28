using System;
using System.Collections.Generic;
using System.Linq;
using Edelstahl.DAL.Interfaces;
using Edelstahl.Domain.Comercial;

namespace Edelstahl.DAL.Implementations.Memory
{
    /// <summary>
    /// Implementación temporal del repositorio de clientes.
    /// Mantiene la información en memoria mientras la aplicación está abierta.
    /// </summary>
    public sealed class ClienteRepositoryMemory : IClienteRepository
    {
        private readonly List<Cliente> _clientes;

        public ClienteRepositoryMemory()
        {
            _clientes = new List<Cliente>();
        }

        public void Add(Cliente entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _clientes.Add(entity);
        }

        public void Update(Cliente entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Cliente clienteExistente = GetById(entity.Id);

            if (clienteExistente == null)
            {
                throw new InvalidOperationException(
                    "No se encontró el cliente que se desea modificar.");
            }

            clienteExistente.CUIT = entity.CUIT;
            clienteExistente.RazonSocial = entity.RazonSocial;
            clienteExistente.DireccionFacturacion = entity.DireccionFacturacion;
            clienteExistente.DireccionEntrega = entity.DireccionEntrega;
            clienteExistente.Localidad = entity.Localidad;
            clienteExistente.Provincia = entity.Provincia;
            clienteExistente.CodigoPostal = entity.CodigoPostal;
            clienteExistente.Email = entity.Email;
            clienteExistente.Telefono = entity.Telefono;
            clienteExistente.LimiteCredito = entity.LimiteCredito;
            clienteExistente.DeudaActual = entity.DeudaActual;
            clienteExistente.Activo = entity.Activo;
        }

        public void Delete(Guid id)
        {
            Cliente clienteExistente = GetById(id);

            if (clienteExistente == null)
            {
                throw new InvalidOperationException(
                    "No se encontró el cliente que se desea eliminar.");
            }

            _clientes.Remove(clienteExistente);
        }

        public Cliente GetById(Guid id)
        {
            return _clientes.FirstOrDefault(cliente => cliente.Id == id);
        }

        public List<Cliente> GetAll()
        {
            return new List<Cliente>(_clientes);
        }

        public Cliente GetByCUIT(string cuit)
        {
            return _clientes.FirstOrDefault(
                cliente => string.Equals(
                    cliente.CUIT,
                    cuit,
                    StringComparison.OrdinalIgnoreCase));
        }

        public bool ExistsByCUIT(string cuit)
        {
            return _clientes.Any(
                cliente => string.Equals(
                    cliente.CUIT,
                    cuit,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}