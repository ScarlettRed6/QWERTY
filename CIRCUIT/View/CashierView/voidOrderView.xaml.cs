using System.Windows;

namespace CIRCUIT.View.CashierView
{
    public partial class voidOrderView : Window
    {
        public voidOrderView()
        {
            InitializeComponent();
        }

        private void VoidOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Order has been voided.");
        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
