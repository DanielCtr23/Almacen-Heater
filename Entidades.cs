using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Heater
{
    public class Usuario
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
    }

    public class Articulo
    {
        public int id { get; set; }
        public string Codigo { get; set;}
        public string Descripcion { get; set;}
        //public int Equipo { get; set;}
        public int Cantidad { get; set;}
        public int StockObjetivo { get; set;}
        public int StockMinimo { get; set;}
        //public int Ubicacion { get; set;}
        public string url { get; set;}

        public Equipo Equipo { get; set;}
        public Ubicacion Ubicacion { get; set;}
    }

    public class Equipo
    {
        public int id { get; set; }
        public string Nombre { get; set;}
        public string Ubicacion { get; set;}
    }
    public class Ubicacion
    {
        public int id { get; set; }
        public int Rack { get; set;}
        public string Seccion { get; set;}
        public string Comentario { get; set;}
    }

    public class Movimiento
    {
        public int id { get; set;}
        public Articulo Articulo { get; set;}
        public int Cantidad { get; set;}
        public decimal Costo { get; set;}
    }

    public class Registro
    {
        public int id { get; set;}
        public DateTime Fecha { get; set;}
        public Usuario Usuario { get; set;}
        public string Comentario { get; set;}
        public List<Movimiento> Movimientos { get; set;}
    }


}
