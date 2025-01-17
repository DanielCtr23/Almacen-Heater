﻿using MySql.Data.MySqlClient;
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



        //Usuario
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

        public Usuario CargarUsuario(int id)
        {
            Usuario _usuario = null;
            lock (lockObject)
            {
                con.Open();
                using (cmd = new MySqlCommand("ObtenerUsuario",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _usuario = new Usuario()
                                {
                                    id = reader.GetInt32("id"),
                                    Nombre = reader.GetString("Nombre"),
                                    Nivel = reader.GetInt32("Nivel")
                                };
                            }
                            con.Close() ;
                            return _usuario;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        //Movimientos
        

        //Registros
        public Registro CargarRegistro(int RegistroId)
        {
            lock(lockObject)
            {
                con.Open();
                using (cmd = new MySqlCommand("ObtenerMovimientos",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Registro", RegistroId);
                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Registro registro = null;
                        List<Movimiento> movimientos = new List<Movimiento>();
                        while (reader.Read())
                        {
                            if (registro == null)
                            {
                                registro = new Registro()
                                {
                                    idRegistro = reader.GetInt32("Registroid"),
                                    Fecha = reader.GetDateTime("Fecha"),
                                    Comentario = reader.GetString("RegistroComentario"),
                                    Usuario = new Usuario()
                                    {
                                        id = reader.GetInt32("Usuarioid"),
                                        Nombre = reader.GetString("UsuarioNombre"),
                                        Nivel = reader.GetInt32("UsuarioNivel")
                                    },
                                    Movimientos = movimientos
                                };
                            }
                            Movimiento movimiento = new Movimiento()
                            {
                                Movimientoid = reader.GetInt32("Linea"),
                                Cantidad = reader.GetInt32("Cantidad"),
                                Costo = reader.GetDecimal("Costo"),
                                Articulo = new Articulo
                                {
                                    id = reader.GetInt32("Articuloid"),
                                    Codigo = reader.GetString("Codigo"),
                                    Descripcion = reader.GetString("Descripcion"),
                                    Cantidad = reader.GetInt32("Stock"),
                                    Costo = reader.GetDecimal("Costo"),
                                    StockObjetivo = reader.GetInt32("StockObjetivo"),
                                    StockMinimo = reader.GetInt32("StockMinimo"),
                                    url = reader.GetString("Url"),
                                    Equipo = new Equipo
                                    {
                                        id = reader.GetInt32("Equipoid"),
                                        Nombre = reader.GetString("EquipoNombre"),
                                        Ubicacion = reader.GetString("EquipoUbicacion")
                                    },
                                    Ubicacion = new Ubicacion
                                    {
                                        id = reader.GetInt32("Ubicacionid"),
                                        Rack = reader.GetInt32("Rack"),
                                        Seccion = reader.GetString("Seccion"),
                                        Comentario = reader.GetString("UbicacionComentario")
                                    }
                                }
                            };
                            movimientos.Add(movimiento);
                        }
                        con.Close();
                        return registro;
                    }
                }
            }
        }

        //Articulos
        public Articulo BusquedaArticulo(string busqueda)
        {
            Articulo _articulo = null;
            lock (lockObject)
            {
                con.Open();
                using (cmd = new MySqlCommand("BuscarArticulo",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Busqueda", busqueda);
                    try
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (_articulo == null)
                            {
                                while (reader.Read())
                                {
                                    _articulo = new Articulo
                                    {
                                        id = reader.GetInt32("id"),
                                        Codigo = reader.GetString("Codigo"),
                                        Descripcion = reader.GetString("Descripción"),
                                        Cantidad = reader.GetInt32("Cantidad"),
                                        Costo = reader.GetDecimal("Costo"),
                                        StockObjetivo = reader.GetInt32("Stock_Objetivo"),
                                        StockMinimo = reader.GetInt32("Stock_Minimo"),
                                        url = reader.GetString("url"),
                                        Equipo = new Equipo
                                        {
                                            id = reader.GetInt32("equipoid"),
                                            Nombre = reader.GetString("Nombre"),
                                            Ubicacion = reader.GetString("equipoUbicacion")
                                        },
                                        Ubicacion = new Ubicacion
                                        {
                                            id = reader.GetInt32("ubicacionid"),
                                            Rack = reader.GetInt32("Rack"),
                                            Seccion = reader.GetString("Seccion"),
                                            Comentario = reader.GetString("Comentario")
                                        }
                                    };
                                }
                            }
                            con.Close();
                            return _articulo;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
        
    }

}
