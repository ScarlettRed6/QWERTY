using CIRCUIT.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CIRCUIT.ViewModel;
using CIRCUIT.Model;
namespace CIRCUIT.View.CashierView
{
    public partial class quickpayView : Window
    {
        public NewSaleViewModel ViewModel { get; private set; }
        public ObservableCollection<CartItem> OrderItems { get; private set; }
        public decimal TotalAmount { get; private set; }

        public quickpayView(ObservableCollection<CartItem> cartItems, decimal totalAmount, NewSaleViewModel viewModel)
        {
            InitializeComponent();

            // Store references
            ViewModel = viewModel;
            OrderItems = cartItems;
            TotalAmount = totalAmount;

            // Set complex DataContext
            DataContext = new
            {
                ViewModel = ViewModel,
                OrderItems = OrderItems,
                TotalAmount = TotalAmount
            };
        }

        private void ProcessPayment_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are the items finalized? Do you want to proceed with payment?",
                                                      "Confirm Payment",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }


        private void ItemsDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("Enter key pressed in ItemsDataGrid.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
