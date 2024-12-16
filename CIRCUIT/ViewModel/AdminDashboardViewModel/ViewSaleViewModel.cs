using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class ViewSaleViewModel : ObservableObject
    {
        //Fields
        private int _currentPage = 1;
        private int _itemsPerPage = 3; // Adjust number of items per page
        Db dbCon = new Db();
        SalesRepository saleConn = new SalesRepository();
        SalesTransactionsViewModel _prevViewModel;
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);

        //Collections
        public ObservableCollection<SalesItemModel> PagedSales { get; set; }
        public ObservableCollection<SalesItemModel> Sales { get; set; }

        //Commands
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }
        public RelayCommand ReturnBtnCommand { get; }

        //Properties
        [ObservableProperty]
        private int _saleIdGet; //From sales and for fetching data

        [ObservableProperty]
        private int _salesIdBox; //For UI display

        [ObservableProperty]
        private DateTime _dateTimeBox;

        [ObservableProperty]
        private string _cashierNameBox; // Implement this later 

        [ObservableProperty]
        private int _cashierIdBox;

        [ObservableProperty]
        private decimal _totalAmountBox;

        [ObservableProperty]
        private string _paymentMethodBox;

        [ObservableProperty]
        private decimal _customerPaymentBox;

        [ObservableProperty]
        private decimal _changeGivenBox;

        [ObservableProperty]
        private object _currentView;

        //For total items in the dataset Product collection
        [ObservableProperty]
        private int _totalItems;

        //For listing selectedall
        [ObservableProperty]
        private List<SalesItemModel> _items;

        [ObservableProperty]
        private int _totalUsers;

        //For pagination
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value && value > 0 && value <= TotalPages)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                    UpdatePagedUsers();
                }
            }
        }
        //For pagination number of items per page
        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set
            {
                _itemsPerPage = value;
                OnPropertyChanged();
                UpdatePagedUsers();
            }
        }

        //Default constructor no parameters
        public ViewSaleViewModel(){}

        //Constructor
        public ViewSaleViewModel(SaleModel model, SalesTransactionsViewModel prev)
        {
            _prevViewModel = prev;
            SaleIdGet = model.SaleId;
            Sales = new ObservableCollection<SalesItemModel>();
            PagedSales = new ObservableCollection<SalesItemModel>();
            FirstPageCommand = new RelayCommand(() => CurrentPage = 1);
            LastPageCommand = new RelayCommand(() => CurrentPage = TotalPages);
            NextPageCommand = new RelayCommand(() =>
            {
                if (CurrentPage < TotalPages)
                    CurrentPage++;
            });
            PreviousPageCommand = new RelayCommand(() =>
            {
                if (CurrentPage > 1)
                    CurrentPage--;
            });
            ReturnBtnCommand = new RelayCommand(ExecuteReturnCommand);


            SetDetailBoxes(model);
            LoadDetails();
        }

        //Return command
        private void ExecuteReturnCommand()
        {
            CurrentView = _prevViewModel;
        }

        //Refreshes the products in the datagrid
        public void LoadDetails()
        {
            var fetchedSales = saleConn.FetchSaleItems(SaleIdGet);
            
            Sales.Clear();
            Items = new List<SalesItemModel>();
            foreach (var saleI in fetchedSales)
            {
                Sales.Add(saleI);
                Items.Add(saleI);
                TotalUsers++;
            }
            TotalItems = Sales.Count;
            UpdatePagedUsers();

        }

        //The the groupbox details
        private void SetDetailBoxes(SaleModel model)
        {
            SalesIdBox = model.SaleId;
            DateTimeBox = model.DateTime;
            CashierIdBox = model.CashierId;
            TotalAmountBox = model.TotalAmount;
            PaymentMethodBox = model.PaymentMethod;
            CustomerPaymentBox = model.CustomerPayment;
            ChangeGivenBox = model.ChangeGiven;
            CashierNameBox = model.CashierName;
        }

        //Updates items per page
        private void UpdatePagedUsers()
        {
            IEnumerable<SalesItemModel> filteredProducts = Sales;

            // Update TotalItems dynamically
            TotalItems = filteredProducts.Count();

            // Paginate the results
            PagedSales = new ObservableCollection<SalesItemModel>(
                filteredProducts.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            OnPropertyChanged(nameof(PagedSales));
        }

    }
}
