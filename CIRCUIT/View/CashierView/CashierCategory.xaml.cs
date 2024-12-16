using CIRCUIT.Utilities;
using CIRCUIT.ViewModel;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
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
                UserLoginView loginView = new UserLoginView();
                loginView.Show();
                this.Close();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void ControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void ControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
    }

}
