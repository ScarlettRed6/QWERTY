using CIRCUIT.ViewModel;
using System;
using System.Windows;
using System.Windows.Threading;

namespace CIRCUIT.View.CashierView
{
    public partial class CashierView : Window
    {

        public CashierView()
        {
            InitializeComponent();
            this.DataContext = new NewSaleViewModel();
        }


        private void NewSale_Click(object sender, RoutedEventArgs e)
        {
            NewSale newSaleWindow = new NewSale();
            newSaleWindow.Show();
            this.Close();
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            HistoryTransaction HistoryTransaction = new HistoryTransaction();
            HistoryTransaction.Show();
            this.Close();
        }

        private void Refund_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
