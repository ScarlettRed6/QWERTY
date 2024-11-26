using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CIRCUIT.View.AdminDashboardView;
using CIRCUIT.View.CashierView;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Security;
using System.Windows;

namespace CIRCUIT.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        //Fields
        private string _username;
        private SecureString _securePassword;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private PropertyChange propChange;

        //Class objects 
        private UsersModel _users;
        //Db dbConn;
        AccountRepository acRCon;
        SessionRepository _sessionRCon;

        //Properties
        [ObservableProperty]
        private int _getUserId;

        public string Username
        {
            get { return _username; }
            set 
            { 
                _username = value;
                propChange.OnPropertyChange();
            }

        }

        public SecureString SecurePassword
        {
            get => _securePassword;
            set => SetProperty(ref _securePassword, value);
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            {
                _errorMessage = value;
                propChange.OnPropertyChange();
            }

        }
        
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set 
            {
                _isViewVisible = value;
                propChange.OnPropertyChange();
            }

        }

        //Commands
        public RelayCommand LoginCommand { get; }
        public RelayCommand ShowPasswordCommand { get; }

        //Default Constructor
        public LoginViewModel()
        {
            //dbConn = new Db();
            acRCon = new AccountRepository();
            _sessionRCon = new SessionRepository();
            propChange = new PropertyChange();
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
        }

        private void ExecuteLoginCommand()
        {
            string plainPass = PasswordHelper.ConvertSecureString(SecurePassword);

            _users = acRCon.FetchUserPassAndSalt(Username);

            //Verifies user
            if (_users == null)
            {
                MessageBox.Show($"{Username} does not exist!");
                return;
            }

            //Verifies password
            bool isPasswordValid = PasswordHelper.VerifyPassword(plainPass, _users.Password, _users.Salt);

            if (isPasswordValid)
            {
                MessageBox.Show("Login successful!");
                GetUserId = _users.UserId;
                _sessionRCon.LogSessionStart(GetUserId);

                if (_users.Role == "Admin")
                {
                    AdminDashboardView adminDashboardView = new AdminDashboardView();
                    adminDashboardView.Show();  // Show the AdminDashboardView
                    Application.Current.MainWindow.Close();
                    
                }
                else if (_users.Role == "Cashier")
                {
                    CashierView cashierView = new CashierView();
                    cashierView.Show();
                    Application.Current.MainWindow.Close();
                }

            }
            else
            {
                MessageBox.Show("Invalid password.");
            }

        }

       
    }
}
