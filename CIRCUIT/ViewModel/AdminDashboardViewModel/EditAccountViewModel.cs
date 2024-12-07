using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Security;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class EditAccountViewModel : ObservableObject
    {
        //Fields and properties
        private string _imageToDatabase = "";

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

        [ObservableProperty]
        private string _userImagePath;

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
        public RelayCommand UploadImageCommand { get; }

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
            UploadImageCommand = new RelayCommand(ExecuteUploadImage);
            LoadAccountById(UserId);
        }

        private void ExecuteUploadImage()
        {
            Microsoft.Win32.OpenFileDialog oFd = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Upload an Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff|All Files|*.*"

            };

            if (oFd.ShowDialog() == true)
            {
                string originalImagePath = oFd.FileName;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(originalImagePath);
                string imagesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Images", "AccountImages");

                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                string newImagePath = Path.Combine(imagesFolderPath, fileName);
                File.Copy(originalImagePath, newImagePath);

                UserImagePath = Path.Combine("Assets", "Images", "AccountImages", fileName);
                _imageToDatabase = UserImagePath;
                MessageBox.Show("Image uploaded successfully", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            UserImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UserImagePath);
            OnPropertyChanged(nameof(UserImagePath));
        }

        private void ExecuteReturnCommand()
        {
            CurrentView = new StaffViewModel();
        }

        private void ExecuteAccountUpdate()
        {
            var result = MessageBox.Show("Do you want to proceed with the Update?", "Proceed to update", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            string plainOld = PasswordHelper.ConvertSecureString(OldSecurePassword);
            string plainNew = PasswordHelper.ConvertSecureString(NewSecurePassword);

            // Case 1: Both passwords are empty
            if (string.IsNullOrEmpty(plainOld) && string.IsNullOrEmpty(plainNew))
            {
                UpdateWithoutNewPassword();
                return;
            }

            // Case 2: Old password is empty but new password is provided
            if (string.IsNullOrEmpty(plainOld) && !string.IsNullOrEmpty(plainNew))
            {
                MessageBox.Show("Please input the old password!");
                return;
            }

            // Case 3: Old password is provided but new password is empty
            if (!string.IsNullOrEmpty(plainOld) && string.IsNullOrEmpty(plainNew))
            {
                MessageBox.Show("Please input a new password!");
                return;
            }

            // Case 4: Both passwords are provided
            UpdateWithNewPassword(plainOld, plainNew);


        }
        
        //Method to update without new password
        private void UpdateWithoutNewPassword()
        {
            var updateAccount = new UsersModel
            {
                FullName = UserFullName,
                UserId = UserId,
                Username = UserName,
                Role = RoleBox,
                UserImagePath = _imageToDatabase

            };

            //dbConn.UpdateUserAccount(updateAccount);
            acRCon.UpdateAccountWithoutPassword(updateAccount);

            CurrentView = new StaffViewModel();
        }

        //Method to update with new password
        private void UpdateWithNewPassword(string plainOld, string plainNew)
        {
            bool compare = PasswordHelper.VerifyPassword(plainOld, ComparePassword, CompareSalt);

            if (compare)
            {

                var updateAccount = new UsersModel
                {
                    FullName = UserFullName,
                    UserId = UserId,
                    Username = UserName,
                    Role = RoleBox,
                    Password = plainNew,
                    UserImagePath = _imageToDatabase

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
                UserFullName = user.FullName;
                UserName = user.Username;
                RoleBox = user.Role;
                ComparePassword = user.Password;
                CompareSalt = user.Salt;
                UserImagePath = user.UserImagePath;
            }
            else
                MessageBox.Show("Product not found.");

        }

    }
}
