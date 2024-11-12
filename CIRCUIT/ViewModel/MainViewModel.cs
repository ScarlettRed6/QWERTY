using CIRCUIT.Utilities;
using CIRCUIT.ViewModel.AdminDashboardViewModel;
using CIRCUIT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CIRCUIT.View.AdminDashboardView;
using FontAwesome.Sharp;

namespace CIRCUIT.ViewModel
{
    internal class MainViewModel : PropertyChange
    {   
        //Fields

        //Current view
		//private object _currentView;

        //Subscribing to window service interface
        private readonly IWindowService _windowService;

        //Commands
		public ICommand btnCloseCommand { get; }
        public ICommand btnMinimizeCommand { get; }
        public ICommand btnMaximizeCommand { get; }
        //Commands for child views
        public ICommand ShowHomeCommand { get; }
        public ICommand ShowCatalogCommand { get; }
        public ICommand ShowAnalyticsCommand { get; }


        //For side nav content control for current right panel/child view
        private object _currentAdminView;
        private string _caption;
        private IconChar _icon;

        //Properties
        /*
        public object CurrentView
		{
			get { return _currentView; }
			set 
			{
				_currentView = value;
				OnPropertyChange();

            }
		}
        */

        public object CurrentAdminView 
        { 
            get => _currentAdminView;
            set
            {
                _currentAdminView = value;
                OnPropertyChange();
            }
        }

        public string Caption 
        { 
            get => _caption; 
            set
            {
                _caption = value;
                OnPropertyChange();
            }
        }

        public IconChar Icon 
        { 
            get => _icon; 
            set
            {
                _icon = value;
                OnPropertyChange();
            }
        }

        //public MainViewModel() : this(null) { }

        public MainViewModel(IWindowService windowService)
        {
            _windowService = windowService;
			btnCloseCommand = new CommandRelay(ExecuteClose);
            btnMinimizeCommand = new CommandRelay(_ => _windowService.Minimize());
            btnMaximizeCommand = new CommandRelay(_ => _windowService.Maximize());
            ShowAnalyticsCommand = new CommandRelay(ExecuteShowAnalytics);
            ShowCatalogCommand = new CommandRelay(ExecuteShowCatalog);
            ShowHomeCommand = new CommandRelay(ExecuteShowHome);

            //Default dashboard panel
            ExecuteShowHome(null);


        }

        private void ExecuteShowAnalytics(object obj)
        {
            CurrentAdminView = new  AnalyticsAndReportsVM();
            Caption = "Reports";
            Icon = IconChar.ChartLine;

        }

        private void ExecuteShowCatalog(object obj)
        {
            CurrentAdminView = new CatalogViewModel();
            Caption = "Product Catalog";
            Icon = IconChar.ProductHunt;
        }

        private void ExecuteShowHome(object obj)
        {
            CurrentAdminView = new HomeViewModel();
            Caption = "Summary";
            Icon = IconChar.Home;
        }

        private void ExecuteClose(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}
