using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Almacen_Heater
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        Usuario _usuario;
        DB db = new DB();
        public LoginDialog()
        {
            InitializeComponent();
            OkBtn.IsEnabled = false;
            idTB.Focus();
            
        }

        public Usuario UsuarioAutenticado { get; private set; }


        private void idTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                _usuario = db.CargarUsuario(int.Parse(idTB.Text));
                NombreTB.Text = _usuario.Nombre;
                OkBtn.IsEnabled = true;
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            UsuarioAutenticado = _usuario;
            this.DialogResult = true;
        }
    }
}
