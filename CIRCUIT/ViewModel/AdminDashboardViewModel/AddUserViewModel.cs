using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Security;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class AddUserViewModel : ObservableObject
    {
        //Properties and Fields
        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private string _userFullName;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _roleBox;

        private SecureString _securePassword;

        public SecureString SecurePassword
        {
            get => _securePassword;
            set => SetProperty(ref _securePassword, value);
        }


        //Commands
        public RelayCommand ReturnBtnCommand { get; }
        public RelayCommand CreateUserCommand { get; }

        //Class objects
        //Db dbCon = new Db();
        AccountRepository acRCon = new AccountRepository();

        public AddUserViewModel()
        {
            ReturnBtnCommand = new RelayCommand(ExecuteReturnBtn);
            CreateUserCommand = new RelayCommand(ExecuteCreateUser);

        }

        private void ExecuteCreateUser()
        {
            string plainPass = PasswordHelper.ConvertSecureString(SecurePassword);


            if (string.IsNullOrEmpty(UserFullName) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(RoleBox)
                || string.IsNullOrEmpty(plainPass))
            {
                MessageBox.Show("Please fill all credentials!", "Missing user credentials!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = AddUser();
            //bool check = dbCon.InsertUser(user);
            bool check = acRCon.InsertUser(user);

            if (!check)
            {
                return;
            }
            CurrentView = new StaffViewModel();

        }

        private UsersModel AddUser()
        {
            string plainPass = PasswordHelper.ConvertSecureString(SecurePassword);
            var users = new UsersModel
            {
                Username = UserName,
                Role = RoleBox,
                Password = plainPass,

            };
            return users;

        }

        private void ExecuteReturnBtn()
        {
            CurrentView = new StaffViewModel();
            
        }


    }
}
