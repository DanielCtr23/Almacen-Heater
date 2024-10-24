using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Heater
{
    public class DBMysql
    {
        private MySqlConnection con;
        private MySqlCommand cmd;
        private string server;
        private string database;
        private string uid;
        private string password;
        private static readonly object lockObject = new object();
        public DBMysql()
        {
            server = "localhost";
            database = "Heater";
            uid = "root";
            password = "Rheem";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            con = new MySqlConnection(connectionString);
        }

    }
}
