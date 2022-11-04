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
    public partial class FormReporte : Form
    {
        public FormReporte()
        {
            InitializeComponent();
            llenarMes();
            //llenargrid();
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

            cmbMes.SelectedIndex = -1;
        }
        private void llenargrid()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select * from vwReporte where Mes = '" + cmbMes.Text + "'";
                //string query = "select * from vwEmpleados";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenargrid();
        }
    }
}
