using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.View.AdminDashboardViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class ReviewOrderViewModel : ObservableObject
    {
        //Fields and Properties
        private StockControlRepository _sControlRepo;

        [ObservableProperty]
        private string _supplierSelected;

        [ObservableProperty]
        private int _supplierID;

        [ObservableProperty]
        private decimal _subTotal;

        [ObservableProperty]
        private decimal _shippingFee;

        [ObservableProperty]
        private decimal _totalAmount;

        private ObservableCollection<SuppliersModel> _suppliersDetails;

        private ObservableCollection<ProductModel> _filteredProducts;

        public ObservableCollection<ProductModel> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
            }
        }

        //Commands
        public ICommand ConfirmOrderCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        //Constructor
        public ReviewOrderViewModel()
        {
            _sControlRepo = new StockControlRepository();
            _suppliersDetails = new ObservableCollection<SuppliersModel>();
            FilteredProducts = new ObservableCollection<ProductModel>();
            
        }

        public void ConfirmOrder(Window window)
        {
            LoadSupplierDetails(); // Ensure supplier details are loaded

            if (_suppliersDetails.Count > 0)
            {
                var supplierId = _suppliersDetails[0].SupplierID; // Get the SupplierID

                // Insert the main order and retrieve the OrderID
                int orderId = _sControlRepo.InsertOrder(supplierId, TotalAmount, ShippingFee);

                // Insert the product details
                _sControlRepo.InsertOrderDetails(orderId, FilteredProducts);

                MessageBox.Show("Order has been successfully placed along with its details!",
                                "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                window?.Close();
            }
            else
            {
                MessageBox.Show("Supplier details not found. Please check your selection.",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Load supplier data
        private void LoadSupplierDetails()
        {
            string query = "SELECT * FROM suppliers WHERE SupplierName = @SupplierName";
            var supplierDetails = _sControlRepo.FetchSuppliers(query, SupplierSelected);

            _suppliersDetails.Clear();
            foreach (var supplier in supplierDetails)
            {
                _suppliersDetails.Add(supplier);
            }
            SupplierID = _suppliersDetails[0].SupplierID;

        }

        public void CancelOrder(Window window)
        {
            window?.Close();
        }

        public void LoadTotalAmount()
        {
            int totalItems = 0;
            foreach (var product in FilteredProducts)
            {
                SubTotal += product.TotalCost;
                totalItems += product.OrderQuantity;
            }

            ShippingFee = totalItems * 120;
            TotalAmount = SubTotal + ShippingFee;
        }

    }
}
