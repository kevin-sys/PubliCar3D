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


    }
}