using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_Heater
{
    public class Movimiento
    {
        public string Codigo { get; set; }
        public string Descripción { get; set;}

        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Importe { get; set; }
    }
}
