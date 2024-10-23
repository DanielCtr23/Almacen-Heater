using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Almacen_Heater
{
    public partial class MainWindow
    {
		private Registro _registro;
		private ObservableCollection<Movimiento> _movientos;

        private void CargarTabInicio()
        {
			try
			{
				var RegistroActual = DB.CargarRegistro((DB.UltimoRegistro()));
				_registro = RegistroActual;
				var Movimientos = RegistroActual.Movimientos;
				_movientos = new ObservableCollection<Movimiento>(Movimientos);
				DGMovimientos.ItemsSource = _movientos;
				TBFechaRegistro.Text = _registro.Fecha.ToString();
				TBIdRegistro.Text = _registro.id.ToString();
				TBUsuario.Text = _registro.Usuario.id.ToString()+ ": " + _registro.Usuario.Nombre;
			}
			catch (Exception)
			{

			}
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
			
        }

    }
}
