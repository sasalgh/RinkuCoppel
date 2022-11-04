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
 * Consultar la Información mensual de cada Empleado, mostrando:
 * Horas Trabajadas, Pago Total por Entregas, Pago Total por Bonos, Retenciones, Vales y Sueldo Total.
 */

namespace RinkuCoppel
{
    public partial class FormReporte : Form
    {
        public FormReporte()
        {
            InitializeComponent();
            llenarMes();
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

            cmbMes.SelectedIndex = -1;
        }
        private void llenargrid()
        {
            /* Llena el Grid que sirve como Consulta, con la información de los Pagos y Retenciones de los Empleados, la cual esta en la BD, utilizando la Vista vwReporte
             * La cual de las vistas: vwPagos, vwRetenciones, vwSdoMensual_Vales.
            */

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsConDat.con.ConnectionString))
            {
                string query = "select * from vwReporte where Mes = '" + cmbMes.Text + "'";

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
            //****** Al seleccionar un Mes de la lista llena el Grid.

            llenargrid();
        }
    }
}
