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
        private ObservableCollection<Usuario> _usuarios;
        private Usuario _nuevoUsuario;
        private void CargarTabUsuarios()
        {
            CancelarUsuarioBtn.IsEnabled = false;
            CancelarUsuarioBtn.Visibility = Visibility.Hidden;
            GuardarUsuarioBtn.IsEnabled = false;
            GuardarUsuarioBtn.Visibility = Visibility.Hidden;
            

            NuevoUsuarioBtn.IsEnabled = true;
            NuevoUsuarioBtn.Visibility = Visibility.Visible;
            ModificarUsuarioBtn.IsEnabled = true;
            ModificarUsuarioBtn.Visibility = Visibility.Visible;
            EliminarUsuarioBtn.IsEnabled = true;
            EliminarUsuarioBtn.Visibility = Visibility.Visible;

            try
            {
                var listaUsuarios = DB.CargarUsuarios();
                _usuarios = new ObservableCollection<Usuario>(listaUsuarios);
                UsuariosDG.ItemsSource = _usuarios;
            }
            catch (Exception)
            {

                throw;
            }
            UsuariosDG.CanUserAddRows = false;
            UsuariosDG.IsReadOnly = true;
            
        }

        private void NuevoUsuarioBtn_Click(object sender, RoutedEventArgs e)
        {
            UsuariosDG.IsReadOnly = false;
            UsuariosDG.CanUserAddRows = true;

            CancelarUsuarioBtn.IsEnabled = true;
            CancelarUsuarioBtn.Visibility = Visibility.Visible;
            GuardarUsuarioBtn.IsEnabled = true;
            GuardarUsuarioBtn.Visibility = Visibility.Visible;

            NuevoUsuarioBtn.IsEnabled = false;
            NuevoUsuarioBtn.Visibility = Visibility.Hidden;
            ModificarUsuarioBtn.IsEnabled = false;
            ModificarUsuarioBtn.Visibility = Visibility.Hidden;
            EliminarUsuarioBtn.IsEnabled = false;
            EliminarUsuarioBtn.Visibility = Visibility.Hidden;

            _nuevoUsuario = new Usuario();
            _usuarios.Add(_nuevoUsuario);

            UsuariosDG.SelectedItem = _nuevoUsuario;
            UsuariosDG.CurrentCell = new System.Windows.Controls.DataGridCellInfo(_nuevoUsuario, UsuariosDG.Columns[1]);
            UsuariosDG.BeginEdit();

        }

        private void GuardarUsuarioBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_nuevoUsuario != null)
            {
                try
                {
                    DB.InsertarUsuario(_nuevoUsuario);
                    CargarTabUsuarios();

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void CancelarUsuarioBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_nuevoUsuario != null)
            {
                _usuarios.Remove(_nuevoUsuario);
                _nuevoUsuario = null;

                CargarTabUsuarios();
            }
        }

    }
}
