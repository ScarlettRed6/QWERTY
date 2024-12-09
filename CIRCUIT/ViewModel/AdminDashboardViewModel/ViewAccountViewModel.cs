using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class ViewAccountViewModel : ObservableObject
    {
        //Fields
        private int _accountId;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty] 
        private string _userFullName;

        [ObservableProperty]
        private string _roleBox;

        [ObservableProperty]
        private string _userImage;

        [ObservableProperty]
        private object _currentView;

        //Db dbConn;
        AccountRepository acRCon;
        StaffViewModel _staffViewModel;

        //Commands
        public RelayCommand ReturnBtnCommand { get; }

        //Default constructor
        public ViewAccountViewModel() => acRCon = new AccountRepository();

        public ViewAccountViewModel(UsersModel data, StaffViewModel model)
        {
            //dbConn = new Db();
            acRCon = new AccountRepository();
            _accountId = data.UserId;
            LoadAccountById(_accountId);
            _staffViewModel = model;
            ReturnBtnCommand = new RelayCommand(ExecuteReturnCommand);

        }

        private void ExecuteReturnCommand()
        {
            CurrentView = _staffViewModel;
        }

        //Loads the UI with data of the account
        private void LoadAccountById(int userId)
        {
            var account = acRCon.FetchUser(userId);

            if (account != null && account.Count > 0)
            {
                var user = account[0];
                UserFullName = user.FullName;
                UserName = user.Username;
                RoleBox = user.Role;
                UserImage = user.UserImagePath;
            }
            else
                MessageBox.Show("Product not found.");

        }

    }
}
