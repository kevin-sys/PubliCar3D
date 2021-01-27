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
        private SqlConnection connection;
        List<Principal> principals;

        public PrincipalRepository(SqlConnection connectionDb)
        {

            connection = connectionDb;
            principals = new List<Principal>();
        }
        public void Guardar(Principal principal)
        {
            using (var command = connection.CreateCommand())
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
        public void Eliminar(string principal)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Empresa WHERE Cedula=@Cedula";
                command.Parameters.AddWithValue("@Cedula", principal);
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

        public Principal Buscar(string cedula)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Empresa where Cedula=@Cedula";
                command.Parameters.AddWithValue("@Cedula", cedula);
               var dataReader = command.ExecuteReader();
                if (dataReader.HasRows==true)
                {
                    while (dataReader.Read())
                    {
                        return Mapear(dataReader);

                    }
                }
            }
            return null;

        }

        public void Modificar(Principal principal)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Empresa SET Nombre=@Nombre, Telefono=@Telefono, Direccion=@Direccion, TipoProducto=@TipoProducto, Producto=@Producto, Precio=@Precio, Afiliacion=@Afiliacion, Porcentaje=@Porcentaje, Descuento=@Descuento, TotalPagar=@TotalPagar, FechaRegistro=@FechaRegistro WHERE Cedula=@Cedula";
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




        public List<Principal> Consultar()
        {
            using (var command = connection.CreateCommand())
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