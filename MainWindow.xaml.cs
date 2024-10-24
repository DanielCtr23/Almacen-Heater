using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Almacen_Heater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB DB = new DB();
        DataTable registroactual;
        bool Nuevo = false;
        public MainWindow()
        {
            InitializeComponent();
            CargarTabUsuarios();
            CargarTabInicio();
        }

        private void DGMovimientos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void DGMovimientos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
        }

        private void CargarImagen(string ruta)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(ruta, UriKind.Absolute);
            bmp.EndInit();
            ImgArticulo.Source = bmp;
        }


    }

    
}
