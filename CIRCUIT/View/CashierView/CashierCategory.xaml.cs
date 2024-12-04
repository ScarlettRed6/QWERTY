﻿using CIRCUIT.ViewModel;
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
        }

        private void NewSale_Click(object sender, RoutedEventArgs e)
        {
            NewSale newSaleWindow = new NewSale();
            newSaleWindow.Show();
            this.Close();
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            HistoryTransaction historyTransaction = new HistoryTransaction();
            historyTransaction.Show();
            this.Close();
        }

        private void Refund_Click(object sender, RoutedEventArgs e)
        {
            RefundView refundView = new RefundView();
            refundView.Show();
            this.Close();
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?",
                 "Confirm Logout",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }

}
