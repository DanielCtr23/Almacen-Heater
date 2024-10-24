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

        private void CargarTabInicio()
        {
            try
            {
                var RegistroActual = DB.CargarRegistro(DB.UltimoRegistro());
                _registro = RegistroActual;
                var Movimientos = RegistroActual.Movimientos;
                _movimientos = new ObservableCollection<Movimiento>(Movimientos);
                DGMovimientos.ItemsSource = _movimientos;
                TBFechaRegistro.Text = _registro.Fecha.ToString("g"); // Formato de fecha
                TBIdRegistro.Text = _registro.idRegistro.ToString();
                TBUsuario.Text = $"{_registro.Usuario.id}: {_registro.Usuario.Nombre}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el registro: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LoginDialog loginDialog = new LoginDialog();
            bool? result = loginDialog.ShowDialog();
            if (result == true)
            {
                _registro = new Registro
                {
                    idRegistro = 0,
                    Fecha = DateTime.Now,
                    Usuario = loginDialog.UsuarioAutenticado,
                    Movimientos = new List<Movimiento>(),
                    Comentario = " "
                };
                TBFechaRegistro.Text = _registro.Fecha.ToString("g");
                TBIdRegistro.Text = " ";
                TBUsuario.Text = $"{_registro.Usuario.id}: {_registro.Usuario.Nombre}";
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
                    CargarRegistro(RegistroActual);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el registro anterior: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(TBIdRegistro.Text) + 1 <= DB.UltimoRegistro())
                {
                    var RegistroActual = DB.CargarRegistro(int.Parse(TBIdRegistro.Text) + 1);
                    CargarRegistro(RegistroActual);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el registro siguiente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarRegistro(Registro registroActual)
        {
            _registro = registroActual;
            var Movimientos = registroActual.Movimientos;
            _movimientos = new ObservableCollection<Movimiento>(Movimientos);
            DGMovimientos.ItemsSource = _movimientos;
            TBFechaRegistro.Text = _registro.Fecha.ToString("g");
            TBIdRegistro.Text = _registro.idRegistro.ToString();
            TBUsuario.Text = $"{_registro.Usuario.id}: {_registro.Usuario.Nombre}";
        }

        private void DGMovimientos_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            e.NewItem = new Movimiento(); // Crear un nuevo objeto Movimiento
        }

        private void DGMovimientos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (e.Column.DisplayIndex == 1) // Suponiendo que es la columna del código
                {
                    var tb = e.EditingElement as TextBox;
                    string codigoIngresado = tb.Text;
                    Articulo articuloEncontrado = DB.BusquedaArticulo(codigoIngresado);

                    if (articuloEncontrado != null)
                    {
                        movimientoActual = e.Row.Item as Movimiento;
                        if (movimientoActual == null)
                        {
                            movimientoActual = new Movimiento();
                            _movimientos.Add(movimientoActual);
                            DGMovimientos.ItemsSource = _movimientos;
                        }

                        // Asignar los valores del artículo encontrado
                        movimientoActual.Articulo = articuloEncontrado;
                        movimientoActual.Costo = articuloEncontrado.Costo;
                        movimientoActual.Cantidad = 1; // Asignar valor predeterminado
                    }
                    else
                    {
                        MessageBox.Show("Artículo no encontrado. Verifique el código ingresado.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else if (e.Column.Header.ToString() == "Cantidad")
                {
                    if (movimientoActual != null)
                    {
                        var tb = e.EditingElement as TextBox;
                        if (int.TryParse(tb.Text, out int nuevaCantidad))
                        {
                            movimientoActual.Cantidad = nuevaCantidad;
                        }
                        else
                        {
                            MessageBox.Show("La cantidad debe ser un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
