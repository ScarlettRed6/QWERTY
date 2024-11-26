using CIRCUIT.Utilities;
using CIRCUIT.ViewModel.AdminDashboardViewModel;
using CommunityToolkit.Mvvm.Input;
using FontAwesome.Sharp;
using System.Windows;
using System.Windows.Input;

namespace CIRCUIT.ViewModel
{
    internal class MainViewModel : PropertyChange
    {   
        //Fields

        //Subscribing to window service interface
        private readonly IWindowService _windowService;

        //Commands
		public RelayCommand btnCloseCommand { get; }
        public RelayCommand<object> btnMinimizeCommand { get; }
        public RelayCommand<object> btnMaximizeCommand { get; }
        //Commands for child views
        public RelayCommand ShowHomeCommand { get; }
        public RelayCommand ShowCatalogCommand { get; }
        public RelayCommand ShowAnalyticsCommand { get; }
        public RelayCommand ShowArchivedCommand { get; }
        public RelayCommand ShowEmployeeManagementCommand { get; }
        public RelayCommand ShowSalesCommand { get; }


        //For side nav content control for current right panel/child view
        private object _currentAdminView;
        private string _caption;
        private IconChar _icon;

        //Properties

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
			btnCloseCommand = new RelayCommand(ExecuteClose);
            btnMinimizeCommand = new RelayCommand<object>(_ => _windowService.Minimize());
            btnMaximizeCommand = new RelayCommand<object>(_ => _windowService.Maximize());
            ShowAnalyticsCommand = new RelayCommand(ExecuteShowAnalytics);
            ShowCatalogCommand = new RelayCommand(ExecuteShowCatalog);
            ShowHomeCommand = new RelayCommand(ExecuteShowHome);
            ShowArchivedCommand = new RelayCommand(ExecuteShowArchived);
            ShowEmployeeManagementCommand = new RelayCommand(ExecuteShowEmployee);
            ShowSalesCommand = new RelayCommand(ExecuteShowSales);

            //Default dashboard panel
            ExecuteShowHome();


        }

        private void ExecuteShowSales()
        {
            CurrentAdminView = new SalesTransactionsViewModel();
            Caption = "Sales Transactions";
            Icon = IconChar.MoneyBill1;
        }

        private void ExecuteShowEmployee()
        {
            CurrentAdminView = new StaffViewModel();
            Caption = "Staff Management";
            Icon = IconChar.PeopleGroup;
        }

        private void ExecuteShowArchived()
        {
            CurrentAdminView = new ArchivedProductsViewModel();
            Caption = "Archives";
            Icon = IconChar.Archive;
        }

        private void ExecuteShowAnalytics()
        {
            CurrentAdminView = new  AnalyticsAndReportsVM();
            Caption = "Reports";
            Icon = IconChar.ChartLine;

        }

        private void ExecuteShowCatalog()
        {
            CurrentAdminView = new CatalogViewModel();
            Caption = "Product Catalog";
            Icon = IconChar.ProductHunt;
        }

        private void ExecuteShowHome()
        {
            CurrentAdminView = new HomeViewModel();
            Caption = "Summary";
            Icon = IconChar.Home;
        }

        private void ExecuteClose()
        {
            Application.Current.Shutdown();
        }
    }
}
