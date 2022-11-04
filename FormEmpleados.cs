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

/*
 * Desarrollador: L.I. Sandro Sarlis López
 * Fecha 04/11/2022
 * Se utiliza para:
 * Agregar un Nuevo Empleado a la BD.
 * Modificar la iformación del empleado de la BD.
 */

namespace RinkuCoppel
{
    public partial class FormEmpleados : Form
    {
        //****** Variables globales del formulario
        string NumE;
        int ide = 0;
        public FormEmpleados()
        {
            InitializeComponent();
            //****** Al cargar el formulario se llena el catalogo del Rol con la información de la BD y el Grid se llena con los registros que estan en la BD.
            llenarcatalogo();
            llenargrid();
        }

        private void limpiarcampos()
        {
            //****** Limpia los campos para que cuando se vuelvan a desplegar no tengan información previamente mostrada.
            txtNomEmp.Text = "";
            txtNumEmp.Text = "";
            cmbRol.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //****** Muestra el Panel que permite Agregar a un nuevo Empleado

            dataGridView1.Enabled = false;
            panel1.Visible = true;
            limpiarcampos();
            lbltitulo.Text = button1.Text;
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //****** Oculta el Panel que permite Agregar o Modificar la información del Empleado, no permitiendo guardar.

            limpiarcampos();
            panel1.Visible = false;
            dataGridView1.Enabled = true;
        }

        private void llenarcatalogo()
        {
            //****** Llena el combobox del Rol con la información que esta en el catalogo de la BD.

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
            //****** Llena el Grid que sirve como Consulta, con la información de los Empleados, la cual esta en la BD, utilizando la Vista vwEmpleados

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select [Numero Empleado], [Nombre Empleado], Rol from vwEmpleados order by [Numero Empleado]";
                //string query = "select * from vwEmpleados";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView1.DataSource = dt;
            
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            /* Guarda la información del Empleado en la BD.
             * Si no se especifica un Rol, se le manda un mensaje al usuario, para que lo realice.
             * Si la Opcion elegida fue Agregar realiza un INSERT a la BD con la información del nuevo Empleado.
             * Si la Opción elegida fue Modificar realiza un UPDATE a la BD con la nueva información del Empleado seleccionado.
             * Los campos son enviados como parametros a los Procedimientos Almacenados que estan en la BD.
             * spModEmpleado es utilizado para realizar UPDATE.
             * spGraEmpleado es utilizado para realizar INSERT.
            */

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
                    if (BuscarNumEmp(txtNumEmp.Text))
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
                    else
                    {
                        MessageBox.Show("El Número de Empleado ya Existe en un Registro", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                llenargrid();
                limpiarcampos();
                panel1.Visible = false;

            }

            dataGridView1.Enabled = true;

        }
                
        private static bool BuscarNumEmp(string NE)
        {
            bool encon = false;

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select * from Empleados where NumEmpleado = '" + NE + "'";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }


            if (dt.Rows.Count > 0)
                encon = false;
            else
                encon = true;
            
            return encon;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            /* Muestra el Panel que permite Modificar la información del Empleado seleccionado.
             * Busca al Empleado en la BD utilizando el Numero de empleado para desplegar la información que podra ser modificada.
            */

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
            //****** Guarda en la variable Global NumE el valor del Numero de empleado que esta en la celda de este campo correspondiente a la fila seleccionado con doble Clic

            if (dataGridView1.SelectedRows.Count > 0)
                NumE = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            //****** Se Agrega un Tooltip en el Grid para que sea mostrado cuando el cursor pase sobre el Grid.

                if ((e.ColumnIndex > -1) && (e.RowIndex > -1))
                    e.ToolTipText = "Para Modificar un registro dar dos clics en la primer columna del renglón.";
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            //****** Agrega un Tooltip en el Boton "Agregar" para que sea mostrado cuando el cursor pase sobre el Boton.

            ToolTip ttpict = new ToolTip();
            ttpict.AutoPopDelay = 10000;
            ttpict.InitialDelay = 300;
            ttpict.IsBalloon = true;
            ttpict.ToolTipIcon = ToolTipIcon.Info;
            ttpict.SetToolTip(button1, "Agrega un nuevo Empleado");
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            //****** Agrega un Tooltip en el Boton "Modificar" para que sea mostrado cuando el cursor pase sobre el Boton.

            ToolTip ttpict = new ToolTip();
            ttpict.AutoPopDelay = 10000;
            ttpict.InitialDelay = 300;
            ttpict.IsBalloon = true;
            ttpict.ToolTipIcon = ToolTipIcon.Info;
            ttpict.SetToolTip(button2, "Modifica los datos del Empleado");
        }

        
    }
}
