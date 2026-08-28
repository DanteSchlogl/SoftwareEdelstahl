using System;
using Edelstahl.BLL.Exceptions.Base;
using Edelstahl.BLL.Services;
using Edelstahl.Domain.Comercial;

namespace Edelstahl.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ClienteService clienteService =
                new ClienteService();

            Cliente cliente = new Cliente
            {
                CUIT = "20-29491211-9",
                RazonSocial = "Cliente de prueba",
                DireccionFacturacion = "Domicilio de prueba",
                DireccionEntrega = "Domicilio de entrega",
                Localidad = "Vicente Lopez",
                Provincia = "Buenos Aires",
                CodigoPostal = "1638",
                Email = "cliente@ejemplo.com",
                Telefono = "1112345678",
                LimiteCredito = 500000m
            };

            try
            {
                Cliente clienteRegistrado =
                    clienteService.Registrar(cliente);

                // Realizo un prueba fuera de catch
                //Cliente clienteDuplicado = new Cliente
                //{
                //    CUIT = "20294912119",
                //    RazonSocial = "Otro cliente",
                //    LimiteCredito = 100000m
                //};
                //clienteService.Registrar(clienteDuplicado);//
                Console.WriteLine(
                    "CLIENTE REGISTRADO MEDIANTE BLL");

                Console.WriteLine(
                    "------------------------------");

                Console.WriteLine(
                    $"Id: {clienteRegistrado.Id}");

                Console.WriteLine(
                    $"CUIT: {clienteRegistrado.CUIT}");

                Console.WriteLine(
                    $"Razon social: {clienteRegistrado.RazonSocial}");

                Console.WriteLine(
                    $"Activo: {clienteRegistrado.Activo}");

                Console.WriteLine(
                    $"Credito disponible: " +
                    $"{clienteRegistrado.CalcularCreditoDisponible():N2}");

                Console.WriteLine();

                Console.WriteLine(
                    $"Total de clientes: " +
                    $"{clienteService.ObtenerTodos().Count}");

                Cliente clienteEncontrado =
                    clienteService.ObtenerPorCUIT(
                        clienteRegistrado.CUIT);

                Console.WriteLine();
                Console.WriteLine("BUSQUEDA MEDIANTE BLL");
                Console.WriteLine("--------------------");

                Console.WriteLine(
                    clienteEncontrado != null
                        ? clienteEncontrado.RazonSocial
                        : "Cliente no encontrado");
            }
            catch (BusinessRuleException ex)
            {
                Console.WriteLine("REGLA DE NEGOCIO");
                Console.WriteLine("----------------");
                Console.WriteLine($"Codigo: {ex.Code}");
                Console.WriteLine($"Mensaje: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("VALIDACION RECHAZADA");
                Console.WriteLine("--------------------");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR INESPERADO");
                Console.WriteLine("----------------");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine(
                "Presione una tecla para finalizar.");

            Console.ReadKey();
        }
    }
}