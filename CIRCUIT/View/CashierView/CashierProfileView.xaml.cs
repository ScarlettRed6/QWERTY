using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using CIRCUIT.ViewModel.CashierViewModel;

namespace CIRCUIT.View.CashierView
{
    public partial class CashierProfileView : Window
    {
        public CashierProfileView()
        {
            InitializeComponent();
        }

        private void CurrentPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            ((CashierProfileViewModel)DataContext).CurrentPassword = passwordBox.Password;
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            ((CashierProfileViewModel)DataContext).NewPassword = passwordBox.Password;
        }

        private void ConfirmNewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            ((CashierProfileViewModel)DataContext).ConfirmNewPassword = passwordBox.Password;
        }


    }
}
