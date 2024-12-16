using CIRCUIT.Model;
using CIRCUIT.ViewModel;
using CIRCUIT;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Windows.Interop;

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
                    viewModel.ImagePath = selectedProduct.ImagePath;
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

        private void ProcessOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?",
                 "Confirm Logout",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                UserLoginView loginView = new UserLoginView();
                loginView.Show();
                this.Close();
            }

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void ControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void ControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

    } 
}
