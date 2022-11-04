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
        string Mes;
        int ideMes;
        int ide = 0;
        public FormEntregas()
        {
            InitializeComponent();
            llenarcatalogo();
            llenarMes();
            llenarNumEmp();
            llenargrid();
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

        private void llenargrid()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select NumEmpleado as [Numero de Empleado], NomEmpleado as [Nombre del Empleado], DescRol as Rol, Mes, CantidadEntregas from vwEntregasEmpleados";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView1.DataSource = dt;

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCantEnt.Text == "")
            {
                MessageBox.Show("La cantidad de Entrega es requerida", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (lbltitulo.Text == "Modificar")
                {
                    SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString);
                    conn.Open();
                    using (conn)
                    {

                        SqlCommand UpdateCommand = new SqlCommand("spModEntrega", conn);
                        UpdateCommand.CommandType = CommandType.StoredProcedure;

                        //UpdateCommand.Parameters.Add("@NumEmpleado", SqlDbType.VarChar, 50).Value = txtNumEmp.Text;
                        UpdateCommand.Parameters.Add("@NomEmpleado", SqlDbType.VarChar, 100).Value = txtNomEmp.Text;
                        UpdateCommand.Parameters.Add("@fkCatRol", SqlDbType.Int).Value = cmbRol.SelectedValue;
                        UpdateCommand.Parameters.Add("@ide", SqlDbType.Int).Value = ide;
                        UpdateCommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("Modificación Realizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString);
                    conn.Open();
                    using (conn)
                    {

                        SqlCommand insertCommand = new SqlCommand("spGraEntrega", conn);
                        insertCommand.CommandType = CommandType.StoredProcedure;

                        insertCommand.Parameters.Add("@fkEmpleados", SqlDbType.Int).Value = ide;
                        insertCommand.Parameters.Add("@fkMes", SqlDbType.Int).Value = cmbMes.SelectedValue;
                        insertCommand.Parameters.Add("@CantidadEntregas", SqlDbType.Int).Value = txtCantEnt.Text;

                        insertCommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("Información Guardada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                llenargrid();
                limpiarcampos();
                panel1.Visible = false;

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarcampos();
            panel1.Visible = false;
        }

        private void buscarMes()
        {
            switch (Mes)
            {
                case "Enero":
                    ideMes = 1;
                    break;
                case "Febrero":
                    ideMes = 2;
                    break;
                case "Marzo":
                    ideMes = 3;
                    break;
                case "Abril":
                    ideMes = 4;
                    break;
                case "Mayo":
                    ideMes = 5;
                    break;
                case "Junio":
                    ideMes = 6;
                    break;
                case "Julio":
                    ideMes = 7;
                    break;
                case "Agosto":
                    ideMes = 8;
                    break;
                case "Septiembre":
                    ideMes = 9;
                    break;
                case "Octubre":
                    ideMes = 10;
                    break;
                case "Noviembre":
                    ideMes = 11;
                    break;
                case "Diciembre":
                    ideMes = 12;
                    break;
                
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                NumE = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                Mes = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }

            buscarMes();
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            limpiarcampos();
            lbltitulo.Text = button2.Text;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select * from Empleados where NumEmpleado = '" + NumE + "'";

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
