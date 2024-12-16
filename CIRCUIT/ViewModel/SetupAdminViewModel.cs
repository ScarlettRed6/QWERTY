using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.View;
using CIRCUIT.ViewModel.AdminDashboardViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Security;
using System.Windows;

namespace CIRCUIT.ViewModel
{
    public partial class SetupAdminViewModel : ObservableObject
    {
        //Properties and Fields
        private string _imageToDatabase = "";

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private string _userFullName;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _roleBox;

        [ObservableProperty]
        private string _userImagePath;

        private SecureString _securePassword;

        public SecureString SecurePassword
        {
            get => _securePassword;
            set => SetProperty(ref _securePassword, value);
        }

        AccountRepository acRCon = new AccountRepository();

        //Commands
        public RelayCommand CreateUserCommand { get; }
        public RelayCommand UploadImageCommand { get; }

        public SetupAdminViewModel()
        {
            CreateUserCommand = new RelayCommand(ExecuteCreateUser);
            UploadImageCommand = new RelayCommand(ExecuteUploadImage);
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
            UserImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UserImagePath);
            OnPropertyChanged(nameof(UserImagePath));

        }

        private void ExecuteCreateUser()
        {
            string plainPass = PasswordHelper.ConvertSecureString(SecurePassword);

            MessageBox.Show($"test {UserFullName} {UserName} {RoleBox} {plainPass}");

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

            MessageBox.Show("Account created Successfully!", "Setup Complete!");

            Window newWindow = new UserLoginView();
            newWindow.Show();
            Application.Current.Windows.OfType<SetupWindow>().FirstOrDefault()?.Close();

        }

        private UsersModel AddUser()
        {
            string plainPass = PasswordHelper.ConvertSecureString(SecurePassword);
            var users = new UsersModel
            {
                FullName = UserFullName,
                Username = UserName,
                Role = RoleBox,
                Password = plainPass,
                UserImagePath = _imageToDatabase,

            };
            return users;

        }

    }
}
