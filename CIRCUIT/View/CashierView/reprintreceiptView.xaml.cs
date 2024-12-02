using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CIRCUIT.View.CashierView
{
    public partial class reprintreceiptView : Window
    {
        public reprintreceiptView()
        {
            InitializeComponent();
        }

        // Event handler for the ReceiptDataGrid PreviewKeyDown event
        private void ReceiptDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Handle key press logic here (e.g., for keyboard navigation or editing)
            if (e.Key == Key.F2)
            {
                // Handle specific key press (e.g., F2)
                MessageBox.Show("F2 key pressed in ReceiptDataGrid.");
            }
        }

        // Event handler for the ReceiptDataGrid SelectionChanged event
        private void ReceiptDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change logic here
            MessageBox.Show("Selection changed in ReceiptDataGrid.");
        }

        // Event handler for the Reprint Button click
        private void ReprintButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle reprinting logic here
            MessageBox.Show("Reprinting receipt...");
        }
    }
}
