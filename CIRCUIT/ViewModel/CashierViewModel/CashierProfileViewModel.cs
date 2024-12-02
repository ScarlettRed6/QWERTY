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
        private string _currentPassword;
        public string CurrentPassword
        {
            get => _currentPassword;
            set
            {
                _currentPassword = value;
                OnPropertyChange(nameof(CurrentPassword));
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChange(nameof(NewPassword));
            }
        }

        private string _confirmNewPassword;
        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set
            {
                _confirmNewPassword = value;
                OnPropertyChange(nameof(ConfirmNewPassword));
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
            try
            {
                var userList = _accountRepository.FetchUser(_userId);

                _currentUser = userList.FirstOrDefault();
                if (_currentUser == null)
                {
                    MessageBox.Show("User data could not be loaded.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Populate properties for binding
                StaffName = _currentUser.Username;
                UserId = _currentUser.UserId;
                Username = _currentUser.Username;
                Fullname = _currentUser.FullName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading user data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles saving changes to update the password.
        /// </summary>
        private void SaveChanges()
        {
            if (string.IsNullOrWhiteSpace(CurrentPassword) || !VerifyCurrentPassword(CurrentPassword))
            {
                MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                MessageBox.Show("New password cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NewPassword == CurrentPassword)
            {
                MessageBox.Show("New password cannot be the same as the current password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NewPassword != ConfirmNewPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Hash the new password and save
                string newSalt;
                string hashedNewPassword = PasswordHelper.HashPassword(NewPassword, out newSalt);

                if (_currentUser != null)
                {
                    _currentUser.Password = hashedNewPassword;
                    _currentUser.Salt = newSalt;

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
