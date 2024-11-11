using CIRCUIT.Utilities;
using CIRCUIT.ViewModel.AdminDashboardViewModel;
using CIRCUIT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CIRCUIT.ViewModel
{
    internal class MainViewModel : PropertyChange
    {

		private object _currentView;

        public object CurrentView
		{
			get { return _currentView; }
			set 
			{
				_currentView = value;
				OnPropertyChange();

            }
		}

        public MainViewModel()
        {
         
        }

		


	}
}
