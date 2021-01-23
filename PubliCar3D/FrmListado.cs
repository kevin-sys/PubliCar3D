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
using System.Configuration;
using BLL;

namespace PubliCar3D
{
    public partial class FrmListado : Form
    {

        PrincipalService service;
        List<Principal> principals = new List<Principal>();
        public FrmListado()
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            service = new PrincipalService(connectionString);
            DgvListado.DataSource = null;
            principals.Clear();
            principals = service.Consultar();
            DgvListado.DataSource = principals;
        }


        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            FrmPrincipal frmPrincipal = new FrmPrincipal();
            frmPrincipal.ShowDialog();
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmListado_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DgvListado.DataSource = null;
            principals.Clear();
            principals = service.Consultar();
            DgvListado.DataSource = principals;
        }
    }
}
