using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIRCUIT.Model
{
    public class UsersModel : PropertyChange
    {
        public int UserId {  get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }

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
