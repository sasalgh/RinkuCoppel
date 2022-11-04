using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RinkuCoppel.Clase;

namespace RinkuCoppel
{
    public partial class FormEntregas : Form
    {
        SqlCommand SQLComm;
        string NumE;
        int ide = 0;
        public FormEntregas()
        {
            InitializeComponent();
            llenarcatalogo();
            llenarMes();
            llenarNumEmp();
            //llenargrid();
        }

        private void limpiarcampos()
        {
            txtNomEmp.Text = "";
            cmbNumEmp.SelectedIndex = 0;
            cmbRol.SelectedIndex = 0;
            cmbMes.SelectedIndex = 0;
            txtCantEnt.Text = "";
        }

        private void llenarNumEmp()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select ide, NumEmpleado from Empleados";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            cmbNumEmp.DisplayMember = "NumEmpleado";
            cmbNumEmp.ValueMember = "ide";
            cmbNumEmp.DataSource = dt;
        }
        private void llenarMes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select ide, Mes from CatMes";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            cmbMes.DisplayMember = "Mes";
            cmbMes.ValueMember = "ide";
            cmbMes.DataSource = dt;
        }
        private void llenarcatalogo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select ide, DescRol from CatRol";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            cmbRol.DisplayMember = "DescRol";
            cmbRol.ValueMember = "ide";
            cmbRol.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            limpiarcampos();
            lbltitulo.Text = button1.Text;
        }

        private void cmbNumEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select * from Empleados where NumEmpleado = '" + cmbNumEmp.Text + "'";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }


            foreach (DataRow dato in dt.Rows)
            {
                ide = (int)dato.ItemArray[0];
                //txtNumEmp.Text = dato.ItemArray[1].ToString();
                txtNomEmp.Text = dato.ItemArray[2].ToString();
                cmbRol.SelectedIndex = (int)dato.ItemArray[3] - 1;
            }
        }
    }
}
