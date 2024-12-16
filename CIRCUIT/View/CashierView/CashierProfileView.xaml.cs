using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using CIRCUIT.ViewModel.CashierViewModel;
using CIRCUIT.ViewModel;
using CIRCUIT.ViewModel.AdminDashboardViewModel;

namespace CIRCUIT.View.CashierView
{
    public partial class CashierProfileView : Window
    {
        public CashierProfileView()
        {
            InitializeComponent();
            DataContext = new CashierProfileViewModel();
        }

        private void CurrentPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is CashierProfileViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.CurrentPassword = passwordBox.SecurePassword;
            }
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is CashierProfileViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.NewPassword = passwordBox.SecurePassword;
            }
        }

        private void ConfirmNewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is CashierProfileViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.ConfirmNewPassword = passwordBox.SecurePassword;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
