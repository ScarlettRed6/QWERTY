using CIRCUIT.Model;
using CIRCUIT.ViewModel;
using CIRCUIT;
using System.Windows;
using System.Windows.Controls;

namespace CIRCUIT.View.CashierView
{
    /// <summary>
    /// Interaction logic for NewSale.xaml
    /// </summary>
    public partial class NewSale : Window
    {
        public NewSale()
        {
            InitializeComponent();
            this.DataContext = new NewSaleViewModel();
        }


        private void ProductBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var product = (sender as Border)?.DataContext as Product;
            if (product == null) return;

            var modal = new ProductDetailsModal
            {
                DataContext = product
            };
            modal.ShowDialog();
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CashierView CashierViewWindow = new CashierView();
            CashierViewWindow.Show();
            this.Close();
        }

        private void watermark_GotFocus(object sender, RoutedEventArgs e)
        {
            watermark.Visibility = Visibility.Collapsed;
            userinput.Visibility = Visibility.Visible;
            userinput.Focus();

        }

        private void userinput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userinput.Text))
            {
                userinput.Visibility = Visibility.Collapsed;
                watermark.Visibility = Visibility.Visible;
            
            }
        }
    }
}
