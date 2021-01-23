using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using System.Data.SqlClient;

namespace DAL
{
    public class PrincipalRepository
    {
        private readonly SqlConnection _connection;
        private readonly List<Principal> principals = new List<Principal>();
        public PrincipalRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
        public void Guardar(Principal principal)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Empresa (Cedula, Nombre, Telefono, Direccion, TipoProducto, Producto, Precio, Afiliacion, Porcentaje, Descuento, TotalPagar, FechaRegistro) VALUES (@Cedula, @Nombre, @Telefono, @Direccion, @TipoProducto, @Producto, @Precio, @Afiliacion, @Porcentaje, @Descuento, @TotalPagar, @FechaRegistro)";
                command.Parameters.AddWithValue("@Cedula", principal.Cedula);
                command.Parameters.AddWithValue("@Nombre", principal.Nombre);
                command.Parameters.AddWithValue("@Telefono", principal.Telefono);
                command.Parameters.AddWithValue("@Direccion", principal.Direccion);
                command.Parameters.AddWithValue("@TipoProducto", principal.TipoProducto);
                command.Parameters.AddWithValue("@Producto", principal.Producto);
                command.Parameters.AddWithValue("@Precio", principal.Precio);
                command.Parameters.AddWithValue("@Afiliacion", principal.Afiliacion);
                command.Parameters.AddWithValue("@Porcentaje", principal.Porcentaje);
                command.Parameters.AddWithValue("@Descuento", principal.Descuento);
                command.Parameters.AddWithValue("@TotalPagar", principal.TotalPagar);
                command.Parameters.AddWithValue("@FechaRegistro", principal.FechaRegistro);
                command.ExecuteNonQuery();
            }
        }
        private Principal Mapear(SqlDataReader reader)
        {
            Principal principal = new Principal();
            principal.Cedula = (string)reader["Cedula"];
            principal.Nombre = (string)reader["Nombre"];
            principal.Telefono = (string)reader["Telefono"];
            principal.Direccion = (string)reader["Direccion"];
            principal.TipoProducto = (string)reader["TipoProducto"];
            principal.Producto = (string)reader["Producto"];
            principal.Precio = (decimal)reader["Precio"];

            principal.Afiliacion = (string)reader["Afiliacion"];
            principal.Porcentaje = (decimal)reader["Porcentaje"];
            principal.Descuento = (decimal)reader["Descuento"];
            principal.TotalPagar = (decimal)reader["TotalPagar"];

            principal.FechaRegistro = (DateTime)reader["FechaRegistro"];
            return principal;
        }
        public List<Principal> Consultar()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Empresa";
                var Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Principal principal = new Principal();
                    principal = Mapear(Reader);
                    principals.Add(principal);
                }
            }
            return principals;
        }




    }
}