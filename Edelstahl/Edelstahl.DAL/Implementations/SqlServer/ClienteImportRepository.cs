using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Edelstahl.DAL.Tools;

namespace Edelstahl.DAL.Implementations.SqlServer
{
    public class ClienteImportRepository
    {
        public void InsertarCliente(
            string nombre,
            string email,
            string telefono,
            string ciudad,
            string provincia,
            string pais,
            decimal totalConsumido,
            int cantidadCompras)
        {
            using (SqlConnection connection =
                SqlServerConnection.CreateConnection())
            {
                connection.Open();

                string sql =
                    @"INSERT INTO dbo.clientes
                    (
                        Nombre_y_Apellido,
                        E_mail,
                        Teléfono,
                        Ciudad,
                        Provincia_Estado,
                        País,
                        Total_consumido_USD,
                        Cantidad_de_compras
                    )
                    VALUES
                    (
                        @Nombre,
                        @Email,
                        @Telefono,
                        @Ciudad,
                        @Provincia,
                        @Pais,
                        @TotalConsumido,
                        @CantidadCompras
                    )";

                SqlCommand command =
                    new SqlCommand(sql, connection);

                command.Parameters.AddWithValue(
                    "@Nombre", nombre);

                command.Parameters.AddWithValue(
                    "@Email", email);

                command.Parameters.AddWithValue(
                    "@Telefono", telefono);

                command.Parameters.AddWithValue(
                    "@Ciudad", ciudad);

                command.Parameters.AddWithValue(
                    "@Provincia", provincia);

                command.Parameters.AddWithValue(
                    "@Pais", pais);

                command.Parameters.AddWithValue(
                    "@TotalConsumido",
                    totalConsumido);

                command.Parameters.AddWithValue(
                    "@CantidadCompras",
                    cantidadCompras);

                command.ExecuteNonQuery();
            }
        }
    }
}
