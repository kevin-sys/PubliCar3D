using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Principal
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string TipoProducto { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public string Afiliacion { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Descuento { get; set; }

        public decimal TotalPagar { get; set; }

        public DateTime FechaRegistro { get; set; }


    }
}
