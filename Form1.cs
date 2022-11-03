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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //SqlConnection conexion = new SqlConnection("server=(localdb)\\MSSQLLocalDB; database=Prueba ; integrated security = true");
            //conexion.Open();
            
            clsConDat.con.Open();
            MessageBox.Show("Se abrió la conexión con el servidor SQL Server y se seleccionó la base de datos");
            //conexion.Close();
            clsConDat.con.Close();
            MessageBox.Show("Se cerró la conexión.");
            
        }
    }
}
