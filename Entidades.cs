using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public decimal Costo;
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

    public class Movimiento : INotifyPropertyChanged
    {
        private int _cantidad;
        private decimal _costo;
        private decimal _importe;
        public int id { get; set;}
        public Articulo Articulo { get; set;}

        public event PropertyChangedEventHandler PropertyChanged;
        public int Cantidad 
        {
            get { return _cantidad; }
            set
            {
                if (_cantidad != value)
                {
                    _cantidad = value;
                    OnPropertyChanged(nameof(Cantidad));
                    CalcularImporte();
                }
            }
        }
        public decimal Costo 
        {
            get { return _costo; }
            set
            {
                if (_costo != value)
                {
                    _costo = value;
                    OnPropertyChanged(nameof(Costo));
                    CalcularImporte();
                }
            }
        }
        public decimal Importe
        {
            get { return _importe; }
            private set
            {
                if (_importe != value)
                {
                    _importe = value;
                    OnPropertyChanged(nameof(Importe));
                }
            }
        }

        private void CalcularImporte()
        {
            Importe = Cantidad * Costo;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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
