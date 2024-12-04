using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class StockOrdersViewModel : ObservableObject
    {
        //Fields and properties
        //Fields
        private int _currentPage = 1;
        private int _itemsPerPage = 10; // Adjust number of items per page
        private string _searchTerm;
        private StockControlRepository _sControlRepo;
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
        private ViewSaleViewModel _saleViewModel;

        [ObservableProperty]
        private object _currentView;

        //For total items in the dataset Product collection
        [ObservableProperty]
        private int _totalItems;

        //For listing selectedall
        [ObservableProperty]
        private List<PurchaseOrderModel> _items;

        [ObservableProperty]
        private int _totalProductSold;

        [ObservableProperty]
        private int _totalTransactions;

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
                    UpdatePagedSales();
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
                UpdatePagedSales();
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    OnPropertyChanged();
                    UpdatePagedSales();
                }
            }
        }

        //Collections
        public ObservableCollection<PurchaseOrderModel> PagedOrders { get; set; }
        public ObservableCollection<PurchaseOrderModel> Orders { get; set; }

        //Commands
        public RelayCommand OrderStockCommand { get; set; }
        public RelayCommand<PurchaseOrderModel> ViewCommand { get; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand<PurchaseOrderModel> ReceiveCommand { get; }

        public StockOrdersViewModel()
        {
            OrderStockCommand = new RelayCommand(ExecuteOrderNewStock);
            _sControlRepo = new StockControlRepository();
            Orders = new ObservableCollection<PurchaseOrderModel>();
            PagedOrders = new ObservableCollection<PurchaseOrderModel>();
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
            ReceiveCommand = new RelayCommand<PurchaseOrderModel>(ExecuteReceive);

            UpdateList();
        }

        private void ExecuteReceive(PurchaseOrderModel? model)
        {
            if (model != null && model.Status != "Completed")
            {
                model.Status = "Completed";  // Mark the status as received/completed
                                             // You might also want to handle updating the status in the database here

                // Update the UI by notifying that the status has changed
                OnPropertyChanged(nameof(Orders)); // Refresh the Orders collection if needed
            }

            CurrentView = new ReceiveStockOrdersViewModel(model, this);
        }

        private void ExecuteOrderNewStock()
        {
            CurrentView = new OrderNewStockViewModel(); 

        }

        //Refreshes the products in the datagrid
        public void UpdateList()
        {
            string query = "SELECT po.OrderID, po.SupplierID, s.SupplierName, po.OrderDate, po.Status, po.TotalAmount, po.ShippingFee FROM purchaseorders po INNER JOIN suppliers s ON po.SupplierID = s.SupplierID";
            var fetchedSales = _sControlRepo.FetchPurchaseOrders(query);

            Orders.Clear();
            foreach (var saleI in fetchedSales)
            {
                Orders.Add(saleI);
                TotalTransactions++;
            }

            TotalItems = Orders.Count;
            UpdatePagedSales();
        }

        //Updates data per page
        private void UpdatePagedSales()
        {
            IEnumerable<PurchaseOrderModel> filteredItems = Orders;

            // For search term filter
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filteredItems = filteredItems.Where(p =>
                    p.Status.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            TotalItems = filteredItems.Count();
            PagedOrders = new ObservableCollection<PurchaseOrderModel>(
                filteredItems.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            OnPropertyChanged(nameof(PagedOrders));
        }

        //Search and filters data by Product name, filter can be modified later
        public void SearchItems(string searchTerm)
        {
            // Filter the product list based on the search term
            var filteredItems = Orders
                .Where(p => p.Status.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            TotalItems = filteredItems.Count;
            PagedOrders = new ObservableCollection<PurchaseOrderModel>(
                filteredItems.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );
            OnPropertyChanged(nameof(PagedOrders));
        }
    }
}
