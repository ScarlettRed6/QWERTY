using System.Configuration;
using System.Data;
using System.Windows;
using CIRCUIT.View;
using CIRCUIT.View.AdminDashboardView;
using CIRCUIT.ViewModel.AdminDashboardViewModel;

namespace CIRCUIT
{
  
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, EventArgs e)
        {
            //var loginView = new LoginView();
            //loginView.Show();
            //loginView.IsVisibleChanged += (s, ev) =>
            //{
                //if (loginView.IsVisible == false && loginView.IsLoaded)
                //{
                   //var adminView = new AdminDashboardView();
                    //adminView.Show();
                    //loginView.Close();
                //}
            //};

        }

    }

}
