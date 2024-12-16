using CIRCUIT.ViewModel.AdminDashboardViewModel;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CIRCUIT.View.AdminDashboardViews
{
    /// <summary>
    /// Interaction logic for EditAccountView.xaml
    /// </summary>
    public partial class EditAccountView : UserControl
    {
        public EditAccountView()
        {
            InitializeComponent();
        }

        private void OldPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditAccountViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.OldSecurePassword = passwordBox.SecurePassword;
            }
        }

        private void NewPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditAccountViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.NewSecurePassword = passwordBox.SecurePassword;
            }
        }
    }
}
