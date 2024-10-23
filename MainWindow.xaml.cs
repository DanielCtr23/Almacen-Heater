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
            if (!Nuevo)
            {
                DataRowView valor = DGMovimientos.SelectedItem as DataRowView;
                if (e.AddedCells.Count > 0)
                {
                    try
                    {
                        DataTable Articulo = DB.Articulo(valor.DataView.Table.Rows[0]["Codigo"].ToString());
                        CargarImagen(Articulo.Rows[0]["Url"].ToString());
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void CargarImagen(string ruta)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(ruta, UriKind.Absolute);
            bmp.EndInit();
            ImgArticulo.Source = bmp;
        }


        private void BtnAnterior_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DGMovimientos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(e.Column.Header.ToString() == "Codigo")
            {
                TextBox TB = e.EditingElement as TextBox;
                string CodigoIngresado = TB.Text;
                if (!string.IsNullOrEmpty(CodigoIngresado))
                {
                    string descripcion = DB.Articulo(CodigoIngresado).Rows[0]["Descripción"].ToString();
                    var movimiento = e.Row.Item as Movimiento;
                    //movimiento.Descripción = descripcion;
                }
            }
        }

        private void DGMovimientos_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            
        }

        private void DGMovimientos_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            DGMovimientos.Items.Refresh();
        }


    }

    
}
