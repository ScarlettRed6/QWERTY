using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class ReceiveStockOrdersViewModel : ObservableObject
    {
        //Fields and properties
        private StockOrdersViewModel _stockOrdersViewModel;
        private StockControlRepository _sControlRepo;

        [ObservableProperty]
        private int _orderId;

        [ObservableProperty]
        private decimal _subTotal;

        [ObservableProperty]
        private decimal _totalAmount;

        [ObservableProperty]
        private decimal _shippingFee;

        [ObservableProperty]
        private object _currentView;

        //Collections
        [ObservableProperty]
        private ObservableCollection<PurchaseOrderDetailModel> _purchaseOrdersItems;

        //Commands
        public RelayCommand ConfirmReceiveCommand { get; set; }

        //Constructor
        public ReceiveStockOrdersViewModel(PurchaseOrderModel order, StockOrdersViewModel prev)
        {
            _sControlRepo = new StockControlRepository();
            _orderId = order.OrderID;
            PurchaseOrdersItems = new ObservableCollection<PurchaseOrderDetailModel>();
            _stockOrdersViewModel = prev;
            ConfirmReceiveCommand = new RelayCommand(ExecuteConfirmCommand);
            LoadDetails();
        }

        private void ExecuteConfirmCommand()
        {
            _sControlRepo.UpdateStock(PurchaseOrdersItems, OrderId);
            MessageBox.Show("Orders received!, Stocks updated!");
            CurrentView = new StockOrdersViewModel();

        }

        //Loads the details in the datagrid and subtotal, shipping and totalamount
        public void LoadDetails()
        {
            var fetchedItems = _sControlRepo.FetchPurchaseOrderItems(OrderId);
            int numItems = 0;

            PurchaseOrdersItems.Clear();
            foreach (var item in fetchedItems)
            {
                PurchaseOrdersItems.Add(item);
                SubTotal += item.Quantity * item.UnitPrice;
                numItems += item.Quantity;
            }

            ShippingFee = numItems * 120;
            TotalAmount = ShippingFee + SubTotal;

        }

    }
}
