using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Edelstahl.DAL.Implementations.SqlServer;

namespace Edelstahl.BLL.Services
{
    public class ClienteImportService
    {
        private readonly ClienteImportRepository _repositorio;

        public ClienteImportService()
        {
            _repositorio =
                new ClienteImportRepository();
        }

        public int ImportarClientes(
            string rutaArchivo)
        {
            string[] lineas =
                File.ReadAllLines(rutaArchivo);

            int importados = 0;

            for (int i = 1; i < lineas.Length; i++)
            {
                string[] datos =
                    lineas[i].Split(';');

                if (datos.Length < 15)
                {
                    continue;
                }

                string nombre =
                    datos[0];

                string email =
                    datos[1];

                string telefono =
                    datos[2];

                string ciudad =
                    datos[8];

                string provincia =
                    datos[10];

                string pais =
                    datos[12];

                decimal totalConsumido = 0m;

                decimal.TryParse(
                    datos[13]
                        .Replace('.', ','),
                    out totalConsumido);

                int cantidadCompras = 0;

                int.TryParse(
                    datos[14],
                    out cantidadCompras);

                _repositorio.InsertarCliente(
                    nombre,
                    email,
                    telefono,
                    ciudad,
                    provincia,
                    pais,
                    totalConsumido,
                    cantidadCompras);

                importados++;
            }

            return importados;
        }
    }
}