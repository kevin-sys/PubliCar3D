using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTITY;

namespace BLL
{
    public class PrincipalService
    {
        private readonly ConnectionManager conexion;
        private readonly PrincipalRepository repository;
        List<Principal> principals;
        public PrincipalService(string connectionString)
        {
            conexion = new ConnectionManager(connectionString);
            repository = new PrincipalRepository(conexion);
        }
        public string Guardar(Principal principal)
        {
            try
            {
                CalcularDescuento(principal);
                conexion.Open();
                repository.Guardar(principal);
                conexion.Close();
                return $"Se guardaron los datos satisfactoriamente";
            }
            catch (Exception e)
            {

                return $"ERROR: {e.Message}";
            }

            finally
            {
                conexion.Close();
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
            conexion.Open();
            principals = new List<Principal>();
            principals = repository.Consultar();
            conexion.Close();
            return principals;
        }

        public RespuestaBusqueda BuscarEmpresa(string cedula)
        {
            RespuestaBusqueda respuesta = new RespuestaBusqueda();
            try
            {
                conexion.Open();
                respuesta.pr = repository.Buscar(cedula);
                conexion.Close();
                respuesta.Mensaje = (respuesta.pr != null) ? "Hemos encontrado la empresa" : "La empresa buscada no existe";
                respuesta.Error = false;
                return respuesta;
            }
            catch (Exception e)
            {

                respuesta.Mensaje = $"Error de la Aplicacion: {e.Message}";
                respuesta.Error = true;
                return respuesta;
            }
            finally { conexion.Close(); }
        }

        public string EliminarEmpresa(string cedula)
        {
            try
            {
                conexion.Open();
                var empresa = repository.Buscar(cedula);
                if (empresa != null)
                {
                    repository.Eliminar(empresa);
                    conexion.Close();
                    return ($"El registro {empresa.Cedula} Ha sido eliminado correctamente");
                }
                else
                {
                    return ($"El registro {cedula} No se encuentra registrado");

                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { conexion.Close(); }
        }

        public string Modificar(Principal nuevacedula)
        {
            try
            {
                conexion.Open();
                var cedulavieja = repository.Buscar(nuevacedula.Cedula);
                if (cedulavieja != null)
                {
                    repository.Modificar(nuevacedula);
                    conexion.Close();
                    return ($"El registro {nuevacedula.Cedula} se ha modificado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {nuevacedula.Cedula} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { conexion.Close(); }
        }


    }
    public class RespuestaBusqueda
    {
        public string Mensaje { get; set; }
        public Principal pr { get; set; }
        public bool Error { get; set; }
    }

    public class RespuestaConsulta
    {
        public string Mensaje { get; set; }
        public IList<Principal> principals { get; set; }
        public bool Error { get; set; }
    }
}