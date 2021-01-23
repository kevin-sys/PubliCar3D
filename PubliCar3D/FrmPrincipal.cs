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
            CmbTipoProducto.Text ="";
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
    }
}
