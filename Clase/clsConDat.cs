using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RinkuCoppel.Clase
{
    class clsConDat
    {
        public static SqlConnection con = new SqlConnection("server=(localdb)\\MSSQLLocalDB; database=RinkuCoppel ; integrated security = true");

        //public void conectar()
        //{
        //    try
        //    {
        //        con.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public void desconectar()
        //{
        //    con.Close();
        //}

        ////funcion que devuelve la conexion sqlserver
        //public SqlConnection conex()
        //{
        //    SqlConnection conexi = con;
        //    return conexi;
        //}
    }
}
