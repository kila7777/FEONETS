using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEONET
{
    internal class BD
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-K8TL1AD\SQLEXPRESS;Initial Catalog=FEONET;Integrated Security = True");
        public void opcon()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();
        }
        public void clscon()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
        }
        public SqlConnection getcon()
        {
            return sqlConnection;
        }
    }
}
