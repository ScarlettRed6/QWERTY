using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.Input;
using CIRCUIT.Model.DataRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CIRCUIT.Model.SingleTons;
using System.Security;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CIRCUIT.ViewModel.CashierViewModel
{
    public class CashierProfileViewModel : PropertyChange
    {
        // Public properties for binding
        public string StaffName { get; private set; }
        public int UserId { get; private set; }
        public string Username { get; set; }
        public string Fullname { get; set; }

        // Password Properties
        private SecureString _currentPassword;
        public SecureString CurrentPassword
        {
            get => _currentPassword;
            set
            {
                _currentPassword = value;
                OnPropertyChange(nameof(CurrentPassword));
            }
        }

        private SecureString _newPassword;
        public SecureString NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChange(nameof(NewPassword));
            }
        }

        private SecureString _confirmNewPassword;
        public SecureString ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set
            {
                _confirmNewPassword = value;
                OnPropertyChange(nameof(ConfirmNewPassword));
            }
        }

        private string _comparePassword;
        public string ComparePassword
        {
            get 
            { 
                return _comparePassword; 
            }
            set 
            { 
                _comparePassword = value; 
                OnPropertyChange(nameof(ComparePassword));
            }
        }
        
        private string _compareSalt;
        public string CompareSalt
        {
            get 
            { 
                return _compareSalt; 
            }
            set 
            {
                _compareSalt = value; 
                OnPropertyChange(nameof(CompareSalt));
            }
        }

        private string _imagePathDisplay;

        public string ImagePathDisplay
        {
            get 
            { 
                return _imagePathDisplay; 
            }
            set 
            { 
                _imagePathDisplay = value; 
            }
        }



        // Private fields
        private readonly int _userId;
        private readonly AccountRepository _accountRepository;
        private UsersModel _currentUser;

        // Commands
        public ICommand SaveChangesCommand { get; }

        public CashierProfileViewModel()
        {
            // Initialize fields
            _userId = UserSessionService.Instance.UserId;
            _accountRepository = new AccountRepository();

            InitializeAccount();

            // Initialize commands
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }

        /// <summary>
        /// Initializes user account data by fetching it from the repository.
        /// </summary>
        private void InitializeAccount()
        {
           
             var userList = _accountRepository.FetchUser(_userId);

             if (userList == null)
                {
                MessageBox.Show("User test data could not be loaded.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
             }

              _currentUser = userList.FirstOrDefault();

              // Populate properties for binding
              StaffName = _currentUser.Username;
              UserId = _currentUser.UserId;
              Username = _currentUser.Username;
              Fullname = _currentUser.FullName;
              ComparePassword = _currentUser.Password;
              CompareSalt = _currentUser.Salt;
              ImagePathDisplay = _currentUser.UserImagePath;

        }

        /// <summary>
        /// Handles saving changes to update the password.
        /// </summary>
        private void SaveChanges()
        {
            string plainOld = PasswordHelper.ConvertSecureString(CurrentPassword);
            string plainNew = PasswordHelper.ConvertSecureString(NewPassword);
            string plainConfirmNew = PasswordHelper.ConvertSecureString(ConfirmNewPassword);


            bool compare = PasswordHelper.VerifyPassword(plainOld, ComparePassword, CompareSalt);

            if (!compare)
            {
                MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(plainNew))
            {
                MessageBox.Show("New password cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (plainNew == plainOld)
            {
                MessageBox.Show("New password cannot be the same as the current password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (plainNew != plainConfirmNew)
            {
                MessageBox.Show("New password and confirm password do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Hash the new password and save
                //string newSalt;
                //string hashedNewPassword = PasswordHelper.HashPassword(plainNew, out newSalt);

                if (_currentUser != null)
                {
                    _currentUser.Password = plainNew;
                    //_currentUser.Salt = newSalt;

                    _accountRepository.UpdateUserAccount(_currentUser);

                    MessageBox.Show("Password updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("User data could not be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the password: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Verifies if the entered current password matches the stored password.
        /// </summary>
        private bool VerifyCurrentPassword(string enteredPassword)
        {
            // Hash the entered password to compare with the stored password hash
            string hashedEnteredPassword = PasswordHelper.HashPassword(enteredPassword, out string salt);
            return hashedEnteredPassword == _currentUser.Password;
        }
    }
}
