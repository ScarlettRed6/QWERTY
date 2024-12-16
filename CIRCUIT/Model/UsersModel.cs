using CIRCUIT.Utilities;

namespace CIRCUIT.Model
{
    public class UsersModel : PropertyChange
    {
        public int UserId {  get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }
        public string FullName { get; set; }
        public string UserImagePath { get; set; }

        private string _userStatus;
        public string UserStatus
        {
            get { return _userStatus; }
            set
            {
                _userStatus = value;
                OnPropertyChange();
                OnPropertyChange(nameof(IsActivated));
            }
        }

        public bool IsActivated => UserStatus.Equals("Active", StringComparison.OrdinalIgnoreCase);

        private bool _isSelected;
        public bool IsSelected 
        { 
            get { return _isSelected; } 
            set
            {
                _isSelected = value;
                OnPropertyChange();
            }
        
        }

    }
}
