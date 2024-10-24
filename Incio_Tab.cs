using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Almacen_Heater
{
    public partial class MainWindow
    {
		private Registro _registro;
		private ObservableCollection<Movimiento> _movimientos;
        Movimiento movimientoActual;
        int ConteoLinea = 0;

        private void CargarTabInicio()
        {
			try
			{
				var RegistroActual = DB.CargarRegistro((DB.UltimoRegistro()));
				_registro = RegistroActual;
				var Movimientos = RegistroActual.Movimientos;
				_movimientos = new ObservableCollection<Movimiento>(Movimientos);
				DGMovimientos.ItemsSource = _movimientos;
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
            _movimientos.Clear();
			LoginDialog loginDialog = new LoginDialog();
			bool? result = loginDialog.ShowDialog();
			if(result == true)
			{
                _registro = new Registro()
                {
                    id = 0,
                    Fecha = DateTime.Now,
                    Usuario = loginDialog.UsuarioAutenticado,
                    Movimientos = new List<Movimiento>(),
                    Comentario = " "

                };
                TBFechaRegistro.Text = _registro.Fecha.ToString();
                TBIdRegistro.Text = " ";
                TBUsuario.Text = _registro.Usuario.id.ToString() + ": " + _registro.Usuario.Nombre;
                _movimientos = new ObservableCollection<Movimiento>();
                DGMovimientos.ItemsSource = _movimientos;

                // Habilitar la edición del DataGrid
                DGMovimientos.IsReadOnly = false;
                DGMovimientos.CanUserAddRows = true;
            }
        }

        private void BtnAnterior_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(TBIdRegistro.Text) - 1 > 0)
                {
                    var RegistroActual = DB.CargarRegistro(int.Parse(TBIdRegistro.Text) - 1);
                    _registro = RegistroActual;
                    var Movimientos = RegistroActual.Movimientos;
                    _movimientos = new ObservableCollection<Movimiento>(Movimientos);
                    DGMovimientos.ItemsSource = _movimientos;
                    TBFechaRegistro.Text = _registro.Fecha.ToString();
                    TBIdRegistro.Text = _registro.id.ToString();
                    TBUsuario.Text = _registro.Usuario.id.ToString() + ": " + _registro.Usuario.Nombre;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(TBIdRegistro.Text) + 1 <= DB.UltimoRegistro())
                {
                    var RegistroActual = DB.CargarRegistro(int.Parse(TBIdRegistro.Text) + 1);
                    _registro = RegistroActual;
                    var Movimientos = RegistroActual.Movimientos;
                    _movimientos = new ObservableCollection<Movimiento>(Movimientos);
                    DGMovimientos.ItemsSource = _movimientos;
                    TBFechaRegistro.Text = _registro.Fecha.ToString();
                    TBIdRegistro.Text = _registro.id.ToString();
                    TBUsuario.Text = _registro.Usuario.id.ToString() + ": " + _registro.Usuario.Nombre;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
