using CIRCUIT.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CIRCUIT.View
{
    public partial class UserLoginView : Window
    {
        public UserLoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.SecurePassword = passwordBox.SecurePassword;
            }
        }
    }
}
