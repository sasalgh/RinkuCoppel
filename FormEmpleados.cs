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
    public partial class FormEmpleados : Form
    {
        string NumE;
        int ide = 0;
        public FormEmpleados()
        {
            InitializeComponent();
            llenarcatalogo();
            llenargrid();
        }

        private void limpiarcampos()
        {
            txtNomEmp.Text = "";
            txtNumEmp.Text = "";
            cmbRol.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;
            panel1.Visible = true;
            limpiarcampos();
            lbltitulo.Text = button1.Text;
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarcampos();
            panel1.Visible = false;
            dataGridView1.Enabled = true;
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
                string query = "select [Numero Empleado], [Nombre Empleado], Rol from vwEmpleados";
                //string query = "select * from vwEmpleados";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView1.DataSource = dt;
            
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;

            if (cmbRol.Text == "Sin Especificar")
            {
                MessageBox.Show("Debe de Seleccionar un Rol", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (lbltitulo.Text == "Modificar")
                {
                    SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString);
                    conn.Open();
                    using (conn)
                    {

                        SqlCommand UpdateCommand = new SqlCommand("spModEmpleado", conn);
                        UpdateCommand.CommandType = CommandType.StoredProcedure;

                        UpdateCommand.Parameters.Add("@NumEmpleado", SqlDbType.VarChar, 50).Value = txtNumEmp.Text;
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
                
                        SqlCommand insertCommand = new SqlCommand("spGraEmpleado", conn);
                        insertCommand.CommandType = CommandType.StoredProcedure;

                        insertCommand.Parameters.Add("@NumEmpleado", SqlDbType.VarChar, 50).Value = txtNumEmp.Text;
                        insertCommand.Parameters.Add("@NomEmpleado", SqlDbType.VarChar, 100).Value = txtNomEmp.Text;
                        insertCommand.Parameters.Add("@fkCatRol", SqlDbType.Int).Value = cmbRol.SelectedValue;
                
                        insertCommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("Información Guardada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                llenargrid();
                limpiarcampos();
                panel1.Visible = false;

            }

            dataGridView1.Enabled = true;

        }

    
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;

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
                    ide = (int)dato.ItemArray[0];
                    txtNumEmp.Text = dato.ItemArray[1].ToString();
                    txtNomEmp.Text = dato.ItemArray[2].ToString();
                    cmbRol.SelectedIndex = (int)dato.ItemArray[3] - 1;
                }
            }
            dataGridView1.Enabled = true;

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
                NumE = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
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
            ttpict.SetToolTip(button1, "Agrega un nuevo Empleado");
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            ToolTip ttpict = new ToolTip();
            ttpict.AutoPopDelay = 10000;
            ttpict.InitialDelay = 300;
            ttpict.IsBalloon = true;
            ttpict.ToolTipIcon = ToolTipIcon.Info;
            ttpict.SetToolTip(button2, "Modifica los datos del Empleado");
        }

        
    }
}
