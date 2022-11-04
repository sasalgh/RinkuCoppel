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
        string NumE;
        string ide = "";
        string ideguardar = "";
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
            cmbNumEmp.SelectedIndex = -1;
            cmbRol.SelectedIndex = -1;
            cmbMes.SelectedIndex = -1;
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
                string query = "select ide, ideEntregas, NumEmpleado as [Numero de Empleado], NomEmpleado as [Nombre del Empleado], DescRol as Rol, Mes, CantidadEntregas from vwEntregasEmpleados";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
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
                ideguardar = dato.ItemArray[0].ToString();
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
                        UpdateCommand.Parameters.Add("@fkEmpleados", SqlDbType.Int).Value = int.Parse(ideguardar);
                        UpdateCommand.Parameters.Add("@fkMes", SqlDbType.Int).Value = cmbMes.SelectedValue;
                        UpdateCommand.Parameters.Add("@CantidadEntregas", SqlDbType.Int).Value = txtCantEnt.Text;
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

                        insertCommand.Parameters.Add("@fkEmpleados", SqlDbType.Int).Value = int.Parse(ideguardar);
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

        
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                NumE = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                ide = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NumE == null)
                MessageBox.Show("Seleccione un Registro para Modificar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
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
                    cmbNumEmp.SelectedIndex = cmbNumEmp.FindStringExact(NumE);
                    txtNomEmp.Text = dato.ItemArray[2].ToString();
                    cmbRol.SelectedIndex = (int)dato.ItemArray[3] - 1;
                
                }

                using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
                {
                    string query = "select * from vwEntregasEmpleados where ideEntregas = " + ide;

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                foreach (DataRow dato in dt.Rows)
                {
                    cmbMes.SelectedIndex = cmbMes.FindStringExact(dato.ItemArray[6].ToString());
                    txtCantEnt.Text = dato.ItemArray[7].ToString();
                }
            }
            
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if ((e.ColumnIndex > -1) && (e.RowIndex > -1))                
                e.ToolTipText = "Para Modificar un registro dar dos clics en la primer columna del renglón.";
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            ToolTip ttpict = new ToolTip();
            ttpict.AutoPopDelay = 10000;
            ttpict.InitialDelay = 300;
            ttpict.IsBalloon = true;
            ttpict.ToolTipIcon = ToolTipIcon.Info;            
            ttpict.SetToolTip(button1, "Agrega el total de Entregas en un mes del empleado");
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            ToolTip ttpict = new ToolTip();
            ttpict.AutoPopDelay = 10000;
            ttpict.InitialDelay = 300;
            ttpict.IsBalloon = true;
            ttpict.ToolTipIcon = ToolTipIcon.Info;
            ttpict.SetToolTip(button2, "Modifica la cantidad de Entregas de un mes del empleado");
        }
    }
}
