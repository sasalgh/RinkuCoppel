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
 * Agregar las Entregas realizadas por el Empleado en cada mes a la BD.
 * Modificar la iformación de las Entregas del empleado de la BD.
 */

namespace RinkuCoppel
{
    public partial class FormEntregas : Form
    {
        //****** Variables globales del formulario
        string NumE;
        string ide = "";
        string ideguardar = "";
        public FormEntregas()
        {
            InitializeComponent();
            /* Al cargar el formulario se llena el catalogo del Rol con la información de la BD.
             * Se llena el catalogo de los meses con la información de la BD.
             * Se llena el catalogo del Número de los Empleados con la información de la BD.
             * Se llena el Grid con los registros que estan en la BD.
            */
            llenarcatalogo();
            llenarMes();
            llenarNumEmp();
            llenargrid();
        }

        private void limpiarcampos()
        {
            //****** Limpia los campos para que cuando se vuelvan a desplegar no tengan información previamente mostrada.

            txtNomEmp.Text = "";
            cmbNumEmp.SelectedIndex = -1;
            cmbRol.SelectedIndex = -1;
            cmbMes.SelectedIndex = -1;
            txtCantEnt.Text = "";
        }

        private void llenarNumEmp()
        {

            /* Llena el combobox de los Numeros de empleado con la información que esta en la BD.
             * La utilida de este combobox es que el usuario no teclee numeros de empleado, en su lugar seleccionara un numero de empleado de la lista.
            */

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
            //****** Llena el combobox de los Meses con la información que esta en el catalogo de la BD.

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
            //****** Llena el Grid que sirve como Consulta, con la información de las Entregas de los Empleados, las cuales estan en la BD, utilizando la Vista vwEntregasEmpleados.

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
            //****** Muestra el Panel que permite Agregar las Entregas por mes del Empleado

            panel1.Visible = true;
            limpiarcampos();
            lbltitulo.Text = button1.Text;
        }

        private void cmbNumEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //****** Busca al Empleado en la BD utilizando el Numero de empleado seleccionado para desplegar la información del Empleado.
             
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
            /* Guarda la información de las Entregas realizadas por el Empleado en la BD.
             * Si no se especifica una Cantidad de Entregas, se le manda un mensaje al usuario, para que lo realice.
             * Si la Opcion elegida fue Agregar realiza un INSERT a la BD con la información de las Entregas del Empleado.
             * Si la Opción elegida fue Modificar realiza un UPDATE a la BD con la nueva información de las Entregas del Empleado seleccionado.
             * Los campos son enviados como parametros a los Procedimientos Almacenados que estan en la BD.
             * spModEntrega es utilizado para realizar UPDATE.
             * spGraEntrega es utilizado para realizar INSERT.
            */

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
            //****** Oculta el Panel que permite Agregar o Modificar la información de las Entregas del Empleado, no permitiendo guardar.

            limpiarcampos();
            panel1.Visible = false;
        }

        
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            /* Guarda en la variable Global NumE el valor del Numero de empleado que esta en la celda de este campo correspondiente a la fila seleccionado con doble Clic
             * Guarda en la variable Global ide el valor del ide de la tabla "EntregasEmpleados".
            */

            if (dataGridView1.SelectedRows.Count > 0)
            {
                NumE = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                ide = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* Muestra el Panel que permite Modificar la información de las Entregas del Empleado seleccionado.
             * Busca al Empleado en la BD utilizando el Numero de empleado para desplegar la información del Empleado.
             * Tambien utiliza el ide de la tabla "EntregasEmpleados" para desplegar la información de las Entregas del Empleado.
            */

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
            ttpict.SetToolTip(button1, "Agrega el total de Entregas en un mes del empleado");
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            //****** Agrega un Tooltip en el Boton "Modificar" para que sea mostrado cuando el cursor pase sobre el Boton.

            ToolTip ttpict = new ToolTip();
            ttpict.AutoPopDelay = 10000;
            ttpict.InitialDelay = 300;
            ttpict.IsBalloon = true;
            ttpict.ToolTipIcon = ToolTipIcon.Info;
            ttpict.SetToolTip(button2, "Modifica la cantidad de Entregas de un mes del empleado");
        }
    }
}
