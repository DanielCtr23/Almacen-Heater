using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Heater
{
    public class DB
    {
        private MySqlConnection con;
        private MySqlCommand cmd;
        private string server;
        private string database;
        private string uid;
        private string password;
        private static readonly object lockObject = new object();
        public DB()
        {
            server = "localhost";
            database = "almacen";
            uid = "root";
            password = "Rheem";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            con = new MySqlConnection(connectionString);
        }

        //READ

        public int UltimoRegistro()
        {
            int Ultimo = 0;
            lock(lockObject)
            {
                try
                {
                    con.Open();
                    using(cmd = new MySqlCommand("UltimoRegistro",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        Ultimo = int.Parse(cmd.ExecuteScalar().ToString());
                    }
                    con.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return Ultimo;
        }

        public DataTable Registro(int registro)
        {
            DataTable dt = new DataTable(); lock (lockObject)
            {
                try
                {
                    con.Open();
                    using (cmd = new MySqlCommand("registroInfo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@registro", registro);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                con.Close();
            }
            return dt;
        }


        public DataTable Movimientos(int registro)
        {
            DataTable dt = new DataTable();

            lock(lockObject)
            {
                try
                {
                    con.Open();
                    using (cmd = new MySqlCommand("CargarRegistros", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@movimiento", registro);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                con.Close();
            }

            return dt;
        }

        public DataTable Articulo(string Articulo)
        {
            DataTable dt = new DataTable();

            lock (lockObject)
            {
                try
                {
                    con.Open();
                    using (cmd = new MySqlCommand("CargarArticulo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codigo", Articulo);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                con.Close();
            }

            return dt;
        }


        public List<Usuario> CargarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            lock (lockObject)
            {
                try
                {
                    con.Open();
                    using (cmd = new MySqlCommand("CargarUsuarios", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usuarios.Add(
                                    new Usuario()
                                    {
                                        id = reader.GetInt32("id"),
                                        Nombre = reader.GetString("Nombre"),
                                        Nivel = reader.GetInt32("Nivel")
                                    });
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                con.Close();
            }
            return usuarios;
        }

        public void InsertarUsuario(Usuario usuario)
        {
            lock (lockObject)
            {
                try
                {
                    con.Open();
                    using (cmd = new MySqlCommand("InsertarUsuario",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", usuario.id);
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@Nivel", usuario.Nivel);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                con.Close();
            }
        }
    }
}
