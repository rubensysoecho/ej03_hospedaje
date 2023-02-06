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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ej03_hospedaje
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static double IVA = 0.21;
        private bool datosCorrectos()
        {
            if (
                tb_apellidos.Text != "" && tb_nombre.Text != "" &&
                dp_llegada.SelectedDate.HasValue && dp_salida.SelectedDate.HasValue && dp_llegada.SelectedDate < dp_salida.SelectedDate && dp_llegada.SelectedDate > DateTime.Today
                ) return true;

            return false;
        }

        private int numNoches()
        {
            TimeSpan intervalo = (TimeSpan)(dp_salida.SelectedDate - dp_llegada.SelectedDate);
            int noches = intervalo.Days;
            return noches;
        }

        private void calcular()
        {
            double hospedaje = 0;
            double servicios = 0;

            if (rb_habEstandar.IsChecked == true) hospedaje = 60;
            else if (rb_habSuperior.IsChecked == true) hospedaje = 120;

            if (cb_buffet.IsChecked == true) servicios += 15;
            if (cb_wifi.IsChecked == true) servicios += 5;
            if (cb_spa.IsChecked == true) servicios += 30;

            double totalImporte = hospedaje + servicios;
            double iva = totalImporte * IVA;
            double total = totalImporte + iva;

            tb_importeHospedaje.Text = hospedaje.ToString("C");
            tb_importeServicios.Text = servicios.ToString("C");
            tb_importeTotal.Text = totalImporte.ToString("C");
            tb_iva.Text = iva.ToString("C");
            tb_total.Text = total.ToString("C");

            lbl_totalNoches.Content = numNoches();
        }

        private void reiniciarFechas()
        {
            dp_llegada.SelectedDate = DateTime.Today.AddDays(1);
            dp_salida.SelectedDate = null;
        }

        private void limpiar()
        {
            tb_apellidos.Text = "";
            tb_nombre.Text = "";
            reiniciarFechas();

            rb_habSuperior.IsChecked = false;
            rb_habEstandar.IsChecked = true;

            cb_buffet.IsChecked = false;
            cb_wifi.IsChecked = false;
            cb_spa.IsChecked = false;

            tb_importeHospedaje.Text = "";
            tb_importeServicios.Text = "";
            tb_total.Text = "";
            tb_iva.Text = "";
            tb_total.Text = "";
        }

        public MainWindow()
        {
            InitializeComponent();
            reiniciarFechas();
        }

        private void btn_calcular_Click(object sender, RoutedEventArgs e)
        {
            if (datosCorrectos())
            {
                calcular();
            }
            else
            {
                MessageBox.Show("Datos no introducidos");
            }
        }

        private void btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    
}
