using CIRCUIT.Model;
using CIRCUIT.ViewModel;
using CIRCUIT;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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


        private void ProductBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProductModel selectedProduct = (sender as FrameworkElement)?.DataContext as ProductModel;
            if (selectedProduct != null)
            {
                ProductDetailsModal productDetailsModalWindow = new ProductDetailsModal(selectedProduct);
                productDetailsModalWindow.ProductAddedToCart += (product) =>
                {
                    var viewModel = this.DataContext as NewSaleViewModel;
                    viewModel?.AddProductToCart(product);
                };
                productDetailsModalWindow.ShowDialog();
            }
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
        private void ApplyDiscount_Click(object sender, RoutedEventArgs e)
        {
            discountModalView discountModal = new discountModalView();
            discountModal.ShowDialog();
            this.Close();
        }

        private void ProcessOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
