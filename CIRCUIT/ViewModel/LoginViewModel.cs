using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Model.SingleTons;
using CIRCUIT.Utilities;
using CIRCUIT.View;
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
            propChange = new PropertyChange();
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
        }

        private void ExecuteLoginCommand()
        {
            string plainPass = PasswordHelper.ConvertSecureString(SecurePassword);

            _users = acRCon.FetchUserPassAndSalt(Username);

            // Verifies user
            if (_users == null)
            {
                MessageBox.Show($"{Username} does not exist!");
                return;
            }

            // Verifies password
            bool isPasswordValid = PasswordHelper.VerifyPassword(plainPass, _users.Password, _users.Salt);

            if (isPasswordValid)
            {
                //Verifies if user is active or inactive
                if (_users.UserStatus == "Inactive")
                {
                    MessageBox.Show("This account has been disabled.", "Login Denied", MessageBoxButton.OK,MessageBoxImage.Information);
                    return;
                }

                //Get and set the user id for use
                GetUserId = _users.UserId;
                UserSessionService.Instance.UserId = GetUserId;
                //Check  if the user account is currently logged in on another device
                if (acRCon.IsUserLoggedIn(GetUserId))
                {
                    MessageBox.Show("User currently logged in on another device");
                    return;
                }

                //Show a successful login
                MessageBox.Show("Login successful!");

                //Updates the users table to set the logged in user by userid
                acRCon.LogUserIn(GetUserId);

                //Declare a new window
                Window newWindow = null;

                //Condition to check if the logging user account is an admin or a staff/cashier
                if (_users.Role == "Admin")
                {
                    newWindow = new AdminDashboardView(GetUserId);
                }
                else if (_users.Role == "Cashier")
                {
                    newWindow = new CashierView();
                }

                //If log in is successful open the new window and close the login window
                if (newWindow != null)
                {
                    // Set the new window as the main window
                    Application.Current.MainWindow = newWindow;
                    newWindow.Show();

                    // Close the login window
                    Application.Current.Windows.OfType<UserLoginView>().FirstOrDefault()?.Close();
                }
            }
            else
            {
                //Message box if password is invalid
                MessageBox.Show("Invalid password.");
            }

        }

       
    }
}
