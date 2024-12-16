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

        private void ReceiptDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                MessageBox.Show("F2 key pressed in ReceiptDataGrid.");
            }
        }
        private void ReceiptDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Selection changed in ReceiptDataGrid.");
        }
        private void ReprintButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reprinting receipt...");
        }
    }
}
