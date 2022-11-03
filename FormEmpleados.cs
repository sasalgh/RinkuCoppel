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
        clsConDat scm;
        SqlCommand SQLComm;
        public SqlDataReader Dtr;
        int Modo;
        string NumE;
        public FormEmpleados()
        {
            InitializeComponent();
            llenargrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            
            llenarcatalogo();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
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
            
            //dataGridView1.Columns[0].Visible = false;

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString);
            conn.Open();
            using (conn)
            {
                // Create a DataTable with the modified rows.  
                //DataTable addedCategories = CategoriesDataTable.GetChanges(DataRowState.Added);

                // Configure the SqlCommand and SqlParameter.  
                SqlCommand insertCommand = new SqlCommand("spGraEmpleado", conn);
                insertCommand.CommandType = CommandType.StoredProcedure;

                //
                //SqlTransaction Trans = new SqlConnection().BeginTransaction();
                //SQLComm.Transaction = Trans;

                //insertCommand.Parameters.Add
                //insertCommand.Parameters.Add("@tpMov", SqlDbType.Int).Value = Modo;
                insertCommand.Parameters.Add("@NumEmpleado", SqlDbType.VarChar, 50).Value = txtNumEmp.Text;
                insertCommand.Parameters.Add("@NomEmpleado", SqlDbType.VarChar, 100).Value = txtNomEmp.Text;
                insertCommand.Parameters.Add("@fkCatRol", SqlDbType.Int).Value = cmbRol.SelectedValue;
                //insertCommand.Parameters.Add("@Ide", SqlDbType.Int).Value = 0;
                //


                //SqlParameter Param = insertCommand.Parameters.AddWithValue("@tpMov", Modo);

                //insertCommand.SqlDbType = SqlDbType.Structured;
                
                // Execute the command.  
                insertCommand.ExecuteNonQuery();
            }

                //Modo = 1;
                ////clsConDat.con.Open();
                //SQLComm.Connection = clsConDat.con.
                //SQLComm.Parameters.Clear();
                ////Stored procedure para ejecutar las altas y la edición de los datos
                //SQLComm.CommandText = "spGraEmpleado";
                //SQLComm.CommandType = CommandType.StoredProcedure;

                //SqlTransaction Trans = new SqlConnection().BeginTransaction();
                //SQLComm.Transaction = Trans;

                //try
                //{

                //    SQLComm.Parameters.Add("@tpMov", SqlDbType.Int).Value = Modo;
                //    SQLComm.Parameters.Add("@NumEmpleado", SqlDbType.VarChar, 50).Value = txtNumEmp.Text;
                //    SQLComm.Parameters.Add("@NomEmpleado", SqlDbType.VarChar, 100).Value = txtNomEmp.Text;
                //    SQLComm.Parameters.Add("@fkCatRol", SqlDbType.Int).Value = cmbRol.SelectedValue;
                //    SQLComm.Parameters.Add("@Ide", SqlDbType.Int).Value = 0;


                //    Dtr = SQLComm.ExecuteReader(CommandBehavior.SingleRow);


                //    Dtr.Close();

                //    Trans.Commit();
                //}
                //catch
                //{
                //    Trans.Rollback();
                //}
                //finally
                //{
                //    Trans.Dispose();
                //    SQLComm.Dispose();
                //    clsConDat.con.Close();
                //}
            }

    

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {            
                NumE = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select * from vwEmpleados where [Numero Empleado] = '" + NumE + "'";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            foreach(DataRow dato in dt.Rows)
            {
                txtNumEmp.Text = dato.ItemArray[1].ToString();
            }
            txtNumEmp.Text = dt.Columns[1].ToString();
            
        }
    }
}
