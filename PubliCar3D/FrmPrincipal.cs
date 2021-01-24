using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENTITY;
using BLL;
using System.Configuration;



namespace PubliCar3D
{
    public partial class FrmPrincipal : Form
    {
        Principal principal;
        PrincipalService service;
        List<Principal> principals = new List<Principal>();
        public FrmPrincipal()
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            service = new PrincipalService(connectionString);
        }

        private Principal MapearDatos()
        {
            principal = new Principal();
            principal.Nombre = TxtNombre.Text.Trim();
            principal.Cedula = TxtCedula.Text.Trim();
            principal.Telefono = TxtTelefono.Text.Trim();
            principal.Direccion = TxtDireccion.Text.Trim();
            principal.TipoProducto = CmbTipoProducto.Text.Trim();
            principal.Afiliacion = CmbAfiliacion.Text.Trim();

            principal.Producto = TxtProducto.Text.Trim();
            principal.Precio = Decimal.Parse(TxtPrecio.Text.Trim());
            principal.FechaRegistro = DtpFechaRegistro.Value;
            return principal;

        }
        private void Limpiar()
        {
            TxtNombre.Text = "";
            TxtCedula.Text = "";
            TxtTelefono.Text = "";
            TxtDireccion.Text = "";
            TxtProducto.Text = "";
            CmbTipoProducto.Text = "";
            CmbAfiliacion.Text = "";
            TxtPrecio.Text = "";
        }



        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Principal principal = MapearDatos();
            string mensaje = service.Guardar(principal);
            MessageBox.Show(mensaje, "Mensaje de Guardado", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            Limpiar();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void BtnListado_Click(object sender, EventArgs e)
        {
            FrmListado frmListado = new FrmListado();
            frmListado.ShowDialog();

        }

        private void TxtNombre_Validated(object sender, EventArgs e)
        {
            //if (TxtNombre.Text.Trim() == "")
            //{
            //    ErrorProvider.SetError(TxtNombre, "Campo Obligatorio");
            //    TxtNombre.Focus();
            //}
            //else
            //{
            //    ErrorProvider.Clear();
            //}
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        private void Buscar()
        {
            RespuestaBusqueda respuesta = new RespuestaBusqueda();
            string cedula = TxtCedula.Text.Trim();
            if (cedula!="")
            {
                respuesta = service.BuscarEmpresa(cedula);
                if (respuesta.pr!=null)
                {
                    TxtCedula.Text = respuesta.pr.Cedula;
                    TxtNombre.Text = respuesta.pr.Nombre;
                    TxtTelefono.Text = respuesta.pr.Telefono;
                    TxtDireccion.Text = respuesta.pr.Direccion;

                    CmbTipoProducto.Text = respuesta.pr.TipoProducto;
                    TxtProducto.Text = respuesta.pr.Producto;
                    TxtPrecio.Text = respuesta.pr.Precio.ToString();
                    CmbAfiliacion.Text = respuesta.pr.Afiliacion;
                    MessageBox.Show(respuesta.Mensaje, "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(respuesta.Mensaje, "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor digite una cedula Valida", "Registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EliminarEmpresa()
        {
            string cedula = TxtCedula.Text.Trim();
            if (cedula!=null)
            {
                var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Mensaje de Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    string mensaje = service.EliminarEmpresa(cedula);
                    MessageBox.Show(mensaje, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor digite la cedula de la empresa a eliminar y presione el boton buscar", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            EliminarEmpresa();
        }

        private void ModificarEmpresa()
        {
            var respuesta = MessageBox.Show("Está seguro de Modificar la información", "Mensaje de Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta == DialogResult.Yes)
            {
                Principal principal = MapearDatos();
                string mensaje = service.Modificar(principal);
            }
        }


        private void BtnModificar_Click(object sender, EventArgs e)
        {
            ModificarEmpresa();
        }
    }
}
