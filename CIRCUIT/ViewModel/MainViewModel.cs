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
        AccountRepository _accountRCon;
        private SalesTransactionsViewModel _salesTransactions;
        private StaffViewModel _staffViewModel;
        private ArchivedProductsViewModel _archivedProducts;
        private AnalyticsAndReportsVM _analyticsAndReports;
        private CatalogViewModel _catalog;
        private HomeViewModel _home;

        //Subscribing to window service interface
        private readonly IWindowService _windowService;

        //Commands
		public RelayCommand btnCloseCommand { get; set; }
        public RelayCommand<object> btnMinimizeCommand { get; set; }
        public RelayCommand<object> btnMaximizeCommand { get; set; }
        public RelayCommand LogOutBtnCommand { get; set; }
        //Commands for child views
        public RelayCommand ShowHomeCommand { get; set; }
        public RelayCommand ShowCatalogCommand { get; set; }
        public RelayCommand ShowAnalyticsCommand { get; set; }
        public RelayCommand ShowArchivedCommand { get; set; }
        public RelayCommand ShowEmployeeManagementCommand { get; set; }
        public RelayCommand ShowSalesCommand { get; set; }


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
        //Constructor
        public MainViewModel(IWindowService windowService, int userId)
        {
            _windowService = windowService;
            _userId = userId;
            _accountRCon = new AccountRepository();
            InitializeViewModels();
            InitializeCommands();

            //Default dashboard panel
            ExecuteShowHome();
            SetUserName();

        }

        //Initializes the viewmodels
        private void InitializeViewModels()
        {
            _salesTransactions = new SalesTransactionsViewModel();
            _staffViewModel = new StaffViewModel();
            _archivedProducts = new ArchivedProductsViewModel();
            _analyticsAndReports = new AnalyticsAndReportsVM();
            _catalog = new CatalogViewModel();
            _home = new HomeViewModel();
        }
        //Initialized the commands
        private void InitializeCommands()
        {
            //Initialize window control commands
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
        }

        //Execute logout function
        private void ExecuteLogout()
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Log out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            _accountRCon.LogUserOut(_userId);
            _windowService.OpenWindow(new UserLoginView());
            _windowService.CloseCurrentWindow();
        }
        //Sets the current logged in username
        private void SetUserName()
        {
            if (_currentAdminView != null)
            {
                var userList = _accountRCon.FetchUser(_userId);
                LoggedInAdmin = userList[0].FullName;

            }
        }

        //Execute command methods for panel switching
        private void ExecuteShowSales()
        {
            CurrentAdminView = _salesTransactions;
            Caption = "Sales Transactions";
            Icon = IconChar.MoneyBill1;
        }

        private void ExecuteShowEmployee()
        {
            CurrentAdminView = _staffViewModel;
            Caption = "Staff Management";
            Icon = IconChar.PeopleGroup;
        }

        private void ExecuteShowArchived()
        {
            CurrentAdminView = _archivedProducts;
            Caption = "Archives";
            Icon = IconChar.Archive;
        }

        private void ExecuteShowAnalytics()
        {
            CurrentAdminView = _analyticsAndReports;
            Caption = "Reports";
            Icon = IconChar.ChartLine;

        }

        private void ExecuteShowCatalog()
        {
            CurrentAdminView = _catalog;
            Caption = "Product Catalog";
            Icon = IconChar.ProductHunt;
        }

        private void ExecuteShowHome()
        {
            CurrentAdminView = _home;
            Caption = "Summary";
            Icon = IconChar.Home;
        }
        
        private void ExecuteClose()
        {
            var result = MessageBox.Show("Are you sure you want to close the app? Current account will be logged out","Close the application",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            _accountRCon.LogUserOut(_userId);
            Application.Current.Shutdown();
        }
    }
}
