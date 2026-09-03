using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edelstahl.DAL.Interfaces;
using Edelstahl.Domain.Comercial;
using System.Data.SqlClient;
using Edelstahl.DAL.Tools;

namespace Edelstahl.DAL.Implementations.SqlServer

{
    public class ClienteRepositorySqlServer
        : IClienteRepository
    {
        public void Add(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Cliente GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> GetAll()
        {
            List<Cliente> clientes =
                new List<Cliente>();

            using (SqlConnection connection =
                SqlServerConnection.CreateConnection())
            {
                connection.Open();

                string sql =
                    "SELECT TOP 5 Nombre_y_Apellido " +
                    "FROM dbo.clientes";

                SqlCommand command =
                    new SqlCommand(sql, connection);

                SqlDataReader reader =
                    command.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.RazonSocial =
                        reader["Nombre_y_Apellido"]
                            .ToString();

                    cliente.Email =
                        reader["E_mail"]
                            .ToString();

                    cliente.Localidad =
                        reader["Ciudad"]
                            .ToString();

                    cliente.Activo = true;

                    clientes.Add(cliente);
                }
            }

            return clientes;
        }
        public Cliente GetByCUIT(string cuit)
        {
            throw new NotImplementedException();
        }

        public bool ExistsByCUIT(string cuit)
        {
            throw new NotImplementedException();
        }
    }
}
