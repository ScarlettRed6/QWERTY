using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CIRCUIT.View.CashierView
{
    public partial class preorderView : Window
    {
        public preorderView()
        {
            InitializeComponent();
        }

        // Event handler to allow only numeric input for Quantity
        private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if the input character is a digit (number)
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;  // Prevent non-numeric characters from being input
            }
        }

        // Event handler for Submit Button
        private void SubmitPreorder_Click(object sender, RoutedEventArgs e)
        {
            // Handle the preorder submission logic here
            string itemName = ItemNameTextBox.Text;
            int quantity = int.TryParse(QuantityTextBox.Text, out var result) ? result : 0;
            string paymentMethod = ((ComboBoxItem)PaymentMethodComboBox.SelectedItem)?.Content.ToString();

            // Perform your submission logic here (e.g., save to database or display confirmation)
            MessageBox.Show($"Preorder submitted for {itemName} (Quantity: {quantity}, Payment Method: {paymentMethod})");
        }
    }
}
