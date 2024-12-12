using System.Collections.ObjectModel;
using System.Windows;
using CIRCUIT.Model;
using System.Text.RegularExpressions;
using CIRCUIT.ViewModel;
using System.Windows.Input;

namespace CIRCUIT.View.CashierView
{
    public partial class ConfirmationModal : Window
    {
        public NewSaleViewModel ViewModel { get; private set; }
        public ObservableCollection<CartItem> OrderItems { get; private set; }
        public decimal TotalAmount { get; private set; }

        public ConfirmationModal(ObservableCollection<CartItem> cartItems, decimal totalAmount, NewSaleViewModel viewModel)
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

        private void AmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+(\.\d{0,2})?$");
        }

        private void ReturnToCart(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void order_click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            //AmountGivenTextBox.Clear();
            //AmountReceivedTextBox.Clear();
        }

        private void PaymentMethodComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void processOrder_Click(object sender, RoutedEventArgs e)
        {
            /*
            MessageBoxResult result = MessageBox.Show("Do you want to proceed with payment?",
                              "Confirm Payment",
                              MessageBoxButton.YesNo,
                              MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }*/
        }
    }
}