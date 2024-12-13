using System.Configuration;
using System.Data;
using System.Windows;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.View;
using CIRCUIT.View.AdminDashboardView;
using CIRCUIT.ViewModel.AdminDashboardViewModel;

namespace CIRCUIT
{
  
    public partial class App : Application
    {
        private AccountRepository _accountRepository;   

        protected void ApplicationStart(object sender, EventArgs e)
        {
            _accountRepository = new AccountRepository();

            // Check if there are admin accounts
            bool hasAdminAccounts = _accountRepository.HasAdminAccounts();

            if (hasAdminAccounts)
            {
                // Proceed to UserLoginView if admin accounts exist
                var loginWindow = new UserLoginView();
                loginWindow.Show();
            }
            else
            {
                // Open SetupWindow if no admin accounts exist
                var setupWindow = new SetupWindow();
                setupWindow.Show();
            }



        }

    }

}
