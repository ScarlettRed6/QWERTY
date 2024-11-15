using CIRCUIT.ViewModel;
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


        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var product = (sender as Border)?.DataContext as Product;
            if (product == null) return;

            var modal = new ProductDetailsModal
            {
                DataContext = product 
            };
            modal.ShowDialog();
        }
    }
}
