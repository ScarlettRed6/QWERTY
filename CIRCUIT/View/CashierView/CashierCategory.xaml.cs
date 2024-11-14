using System;
using System.Windows;
using System.Windows.Threading;

namespace CIRCUIT.View.CashierView
{
    public partial class CashierView : Window
    {

        //hahaha try
        public string name(string name) 
        {
        return name;
        
        }
        //hanggang rito

        public CashierView()
        {
            InitializeComponent();
            StartDateTimeUpdate();
        }

        private void StartDateTimeUpdate()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTimeTextBlock.Text = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss");
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



    }
}
