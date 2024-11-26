using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CIRCUIT.View;
using CIRCUIT.ViewModel.AdminDashboardViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FontAwesome.Sharp;
using System.Windows;
using System.Windows.Input;

namespace CIRCUIT.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        //Fields
        private int _userId;
        SessionRepository _sessionRCon;
        AccountRepository _accountRCon;

        //Subscribing to window service interface
        private readonly IWindowService _windowService;

        //Commands
		public RelayCommand btnCloseCommand { get; }
        public RelayCommand<object> btnMinimizeCommand { get; }
        public RelayCommand<object> btnMaximizeCommand { get; }
        public RelayCommand LogOutBtnCommand { get; }
        //Commands for child views
        public RelayCommand ShowHomeCommand { get; }
        public RelayCommand ShowCatalogCommand { get; }
        public RelayCommand ShowAnalyticsCommand { get; }
        public RelayCommand ShowArchivedCommand { get; }
        public RelayCommand ShowEmployeeManagementCommand { get; }
        public RelayCommand ShowSalesCommand { get; }


        //For side nav content control for current right panel/child view
        [ObservableProperty]
        private object _currentAdminView;

        [ObservableProperty]
        private string _caption;

        [ObservableProperty]
        private IconChar _icon;

        //Properties
        [ObservableProperty]
        private string _loggedInAdmin;

        //public MainViewModel() : this(null) { }

        public MainViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            _sessionRCon = new SessionRepository();
            _accountRCon = new AccountRepository();
            btnCloseCommand = new RelayCommand(ExecuteClose);
            btnMinimizeCommand = new RelayCommand<object>(_ => _windowService.Minimize());
            btnMaximizeCommand = new RelayCommand<object>(_ => _windowService.Maximize());
            LogOutBtnCommand = new RelayCommand(ExecuteLogout);

            //Initialize menu button commands
            ShowAnalyticsCommand = new RelayCommand(ExecuteShowAnalytics);
            ShowCatalogCommand = new RelayCommand(ExecuteShowCatalog);
            ShowHomeCommand = new RelayCommand(ExecuteShowHome);
            ShowArchivedCommand = new RelayCommand(ExecuteShowArchived);
            ShowEmployeeManagementCommand = new RelayCommand(ExecuteShowEmployee);
            ShowSalesCommand = new RelayCommand(ExecuteShowSales);

            //Default dashboard panel
            ExecuteShowHome();
            SetUserName();

        }

        private void ExecuteLogout()
        {
            _sessionRCon.LogSessionEnd(_userId);
        }

        private void SetUserName()
        {
            if (_currentAdminView != null)
            {
                _userId = (int)_sessionRCon.GetLoggedInUserId();
                var userList = _accountRCon.FetchUser(_userId);
                LoggedInAdmin = userList[0].Username;
            }
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
