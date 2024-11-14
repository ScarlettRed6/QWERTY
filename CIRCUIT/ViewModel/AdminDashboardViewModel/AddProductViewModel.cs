using CIRCUIT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class AddProductViewModel : PropertyChange
    {

        public ICommand ReturnBtnCommand { get; }

        private object _currentAddView;

        public object CurrentAddView
        {
            get { return _currentAddView; }
            set 
            { 
                _currentAddView = value;
                OnPropertyChange();
            }
        }

        public AddProductViewModel()
        {
            ReturnBtnCommand = new CommandRelay(ExecuteReturnBtn);

        }

        private void ExecuteReturnBtn(object obj)
        {
            CurrentAddView = new CatalogViewModel();
        }
    }
}
