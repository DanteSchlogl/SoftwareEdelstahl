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

            PresupuestoService presupuestoService =
                new PresupuestoService();

            Cliente cliente = new Cliente
            {
                CUIT = "30-87654321-0",
                RazonSocial = "Industrias Delta SRL",
                Email = "ventas@industriasdelta.test",
                Telefono = "1145678901",
                Localidad = "San Martín",
                LimiteCredito = 850000m
            };

            clienteService.Registrar(cliente);

            presupuestoService
                .CrearPresupuestosDemostracion(cliente.Id);

            Console.WriteLine("PRESUPUESTOS DEL CLIENTE");
            Console.WriteLine("------------------------");

            foreach (Presupuesto presupuesto
                in presupuestoService.ObtenerPorCliente(
                    cliente.Id))
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"Número: {presupuesto.Numero}");

                Console.WriteLine(
                    $"Estado: {presupuesto.Estado}");

                Console.WriteLine(
                    $"Moneda: {presupuesto.Moneda}");

                Console.WriteLine(
                    $"Cantidad de detalles: " +
                    $"{presupuesto.Detalles.Count}");

                Console.WriteLine(
                    $"Subtotal: " +
                    $"{presupuesto.CalcularSubtotal():N2}");

                Console.WriteLine(
                    $"IVA: {presupuesto.CalcularIVA():N2}");

                Console.WriteLine(
                    $"Total: {presupuesto.CalcularTotal():N2}");

                Console.WriteLine(
                    $"Anticipo: " +
                    $"{presupuesto.CalcularAnticipo():N2}");

                Console.WriteLine(
                    $"Puede confirmarse: " +
                    $"{presupuesto.PuedeConfirmarse()}");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Presione una tecla para finalizar.");

            Console.ReadKey();
        }
    }
}