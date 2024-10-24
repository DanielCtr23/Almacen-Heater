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
        int ConteoLinea = 1;

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

        private void DGMovimientos_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var UltimoItem = DGMovimientos.Items.OfType<Movimiento>().LastOrDefault();
            int LastId = UltimoItem?.id ?? 0;
            var newItem = new Movimiento
            {
                id = LastId + 1
            };
            e.NewItem = newItem;
        }
        private void DGMovimientos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(e.Column.DisplayIndex == 1)
            {
                string CodigoIngresado = (e.EditingElement as TextBox).Text;
                Articulo _articuloEncontrado = DB.BusquedaArticulo(CodigoIngresado);
                if (_articuloEncontrado != null)
                {
                    movimientoActual = e.Row.Item as Movimiento;
                    movimientoActual.Articulo = new Articulo();
                    movimientoActual.Articulo = _articuloEncontrado;
                    movimientoActual.Costo = movimientoActual.Articulo.Costo;
                    e.Row.Item = movimientoActual;
                    
                }
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
