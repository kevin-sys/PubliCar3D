using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.SqlClient;
using ENTITY;

namespace BLL
{
    public class PrincipalService
    {
        List<Principal> principals;
        SqlConnection connection;
        string CadenaConexion = @"Data Source=ALEXANDER;Initial Catalog=Publicar3D;Integrated Security=True";

        PrincipalRepository repository;
        public PrincipalService()
        {
            connection = new SqlConnection(CadenaConexion);
            repository = new PrincipalRepository(connection);
        }
        public string Guardar(Principal principal)
        {
            try
            {
                CalcularDescuento(principal);
                connection.Open();
                repository.Guardar(principal);
                return $"Se guardaron los datos satisfactoriamente";
            }
            catch (Exception e)
            {

                return $"ERROR: {e.Message}";
            }

            finally
            {
                connection.Close();
            }
        }

        public void CalcularDescuento(Principal principal)
        {
            if (principal.Afiliacion.Equals("Si"))
            {
                principal.Porcentaje = 20;
                principal.Descuento = (principal.Precio * principal.Porcentaje) / 100;
                principal.TotalPagar = principal.Precio - principal.Descuento;
            }
            else if (principal.Afiliacion.Equals("No"))
            {
                principal.Porcentaje = 0;
                principal.Descuento = (principal.Precio * principal.Porcentaje) / 100;
                principal.TotalPagar = principal.Precio - principal.Descuento;
            }
        }

        public List<Principal> Consultar()
        {
            connection.Open();
            principals = new List<Principal>();
            principals = repository.Consultar();
            connection.Close();
            return principals;
        }

        public Principal BuscarEmpresa(string cedula)
        {
            Principal principal = new Principal();
            try
            {
                connection.Open();
                
                return repository.Buscar(cedula);
            }
            catch (Exception e)
            {
                string mensaje = " ERROR EN LA BASE DE DATOS " + e.Message;
                return null;
            }
            finally { connection.Close(); }
        }

        public string EliminarEmpresa(string cedula)
        {
            try
            {
                connection.Open();
                repository.Eliminar(cedula);
                return "SE ELIMINO CORRECTAMENTE";

            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { connection.Close(); }
        }

        public Principal Modificar(Principal nuevacedula)
        {
            try
            {
                connection.Open();
                repository.Modificar(nuevacedula);
                return nuevacedula;

            }
            catch (Exception e)
            {

                string mensaje = "ERROR!" + e.Message;
                return null;
            }
            finally { connection.Close(); }
        }


    }
  
}