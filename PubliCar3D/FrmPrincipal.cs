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



namespace PubliCar3D
{
    public partial class FrmPrincipal : Form
    {
        Principal principal;
        public FrmPrincipal()
        {
            InitializeComponent();
           
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
            PrincipalService service = new PrincipalService();
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
            PrincipalService service = new PrincipalService();
            string cedula = TxtCedula.Text.Trim();
            if (cedula!="")
            {
                Principal principal = service.BuscarEmpresa(cedula);
                if (cedula!=null)
                {
                    TxtCedula.Text = principal.Cedula;
                    TxtNombre.Text = principal.Nombre;
                    TxtTelefono.Text = principal.Telefono;
                    TxtDireccion.Text = principal.Direccion;

                    CmbTipoProducto.Text = principal.TipoProducto;
                    TxtProducto.Text = principal.Producto;
                    TxtPrecio.Text = principal.Precio.ToString();
                    CmbAfiliacion.Text = principal.Afiliacion;
                }
                else
                {
                    MessageBox.Show($"La empresa con cedula:  {cedula} NO SE ENCUENTRA EN NUESTRA BASE DE DATOS");
                }
            }
            else
            {
                MessageBox.Show("Por favor digite una cedula Valida", "Registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EliminarEmpresa()
        {
            var respuesta = MessageBox.Show("Esta seguro de eliminar el registro, PARA SIEMPRE!", "Eliminar Registro", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                PrincipalService service = new PrincipalService();
                string cedula = TxtCedula.Text;
                string msjeliminado = service.EliminarEmpresa(cedula);
                MessageBox.Show(msjeliminado);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            EliminarEmpresa();
            Limpiar();
        }

        private void ModificarEmpresa()
        {

            if (TxtCedula.Text != "" && TxtDireccion.Text != "" && TxtNombre.Text != "" && TxtPrecio != null)
            {
                PrincipalService service = new PrincipalService();
                Principal principal = new Principal();
                principal.Cedula = TxtCedula.Text.Trim();
                principal.Nombre = TxtNombre.Text;
                principal.Direccion = TxtDireccion.Text;
                principal.FechaRegistro = DtpFechaRegistro.Value.Date;
                service.Modificar(principal);
                MessageBox.Show("SE MODIFICO CORRECTAMENTE EL REGISTRO");
            }
            else
            {
                MessageBox.Show("ALGUNOS CAMPOS ESTAN VACIOS");
            }
        }


        private void BtnModificar_Click(object sender, EventArgs e)
        {
            ModificarEmpresa();
        }
    }
}
