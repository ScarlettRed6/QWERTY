﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CIRCUIT.View.CashierView
{
    /// <summary>
    /// Interaction logic for RefundView.xaml
    /// </summary>
    public partial class RefundView : Window
    {
        public RefundView()
        {
            InitializeComponent();
        }
        private void close_clicked(object sender, RoutedEventArgs e)
        {
            CashierView CashierViewWindow = new CashierView();
            CashierViewWindow.Show();
            this.Close();
        }
    }
}
