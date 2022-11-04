using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

/*
 * Desarrollador: L.I. Sandro Sarlis López
 * Fecha 03/11/2022
 * Clase que se utliza para conectar a la BD de SQL Express.
 */
namespace RinkuCoppel.Clase
{
    class clsConDat
    {
        public static SqlConnection con = new SqlConnection("server=(localdb)\\MSSQLLocalDB; database=RinkuCoppel ; integrated security = true");

    }
}
