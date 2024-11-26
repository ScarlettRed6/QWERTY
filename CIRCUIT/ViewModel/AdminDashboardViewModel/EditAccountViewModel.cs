using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Security;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class EditAccountViewModel : ObservableObject
    {
        //Fields
        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private int _userId;

        [ObservableProperty]
        private string _userFullName;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _roleBox;

        [ObservableProperty]
        private string _comparePassword;

        [ObservableProperty]
        private string _compareSalt;

        private SecureString _NewSecurePassword;
        private SecureString _oldSecurePassword;

        public SecureString OldSecurePassword
        {
            get => _oldSecurePassword;
            set => SetProperty(ref _oldSecurePassword, value);
        }

        public SecureString NewSecurePassword
        {
            get => _NewSecurePassword;
            set => SetProperty(ref _NewSecurePassword, value);
        }

        //Class objects
        //Db dbConn;
        AccountRepository acRCon;

        //Commands
        public RelayCommand UpdateAccountCommand { get; }
        public RelayCommand ReturnBtnCommand { get; }

        //Default Constructor
        public EditAccountViewModel() => acRCon = new AccountRepository();

        //Overloaded constructor
        public EditAccountViewModel(UsersModel data)
        {
            UserId = data.UserId;
            //dbConn = new Db();
            acRCon = new AccountRepository();
            UpdateAccountCommand = new RelayCommand(ExecuteAccountUpdate);
            ReturnBtnCommand = new RelayCommand(ExecuteReturnCommand);
            LoadAccountById(UserId);
        }

        private void ExecuteReturnCommand()
        {
            CurrentView = new StaffViewModel();
        }

        private void ExecuteAccountUpdate()
        {
            var result = MessageBox.Show("Do you want to proceed with the Update?", "Proceed to update", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            string plainOld = PasswordHelper.ConvertSecureString(OldSecurePassword);
            string plainNew = PasswordHelper.ConvertSecureString(NewSecurePassword);
            bool compare = PasswordHelper.VerifyPassword(plainOld, ComparePassword, CompareSalt);

            if(compare)
            {

                var updateAccount = new UsersModel
                {
                    UserId = UserId,
                    Username = UserName,
                    Role = RoleBox,
                    Password = plainNew

                };

                //dbConn.UpdateUserAccount(updateAccount);
                acRCon.UpdateUserAccount(updateAccount);

               CurrentView = new StaffViewModel();
            }
            else
            {
                MessageBox.Show("Wrong password!");
                return;
            }

        }

        //Loads the UI with data of the account
        private void LoadAccountById(int userId)
        {
            //var account = dbConn.FetchUser(userId);
            var account = acRCon.FetchUser(userId);

            if (account != null && account.Count > 0)
            {
                var user = account[0];
                UserName = user.Username;
                RoleBox = user.Role;
                ComparePassword = user.Password;
                CompareSalt = user.Salt;
            }
            else
                MessageBox.Show("Product not found.");

        }

    }
}
