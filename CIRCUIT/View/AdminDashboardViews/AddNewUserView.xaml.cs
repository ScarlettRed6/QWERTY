using CIRCUIT.ViewModel.AdminDashboardViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CIRCUIT.View.AdminDashboardViews
{
    /// <summary>
    /// Interaction logic for AddNewUserView.xaml
    /// </summary>
    public partial class AddNewUserView : UserControl
    {
        public AddNewUserView()
        {
            InitializeComponent();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddUserViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.SecurePassword = passwordBox.SecurePassword;
            }
        }
    }
}
