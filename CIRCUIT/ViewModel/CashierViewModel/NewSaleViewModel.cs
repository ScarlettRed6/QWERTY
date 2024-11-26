using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CIRCUIT.Utilities;
using CIRCUIT.Model;
using CIRCUIT.View.CashierView;
using CommunityToolkit.Mvvm.Input;

namespace CIRCUIT.ViewModel
{
    public class NewSaleViewModel : PropertyChange
    {
        #region Private Fields
        private readonly Db _db;
        private string _searchQuery;
        private string _amountReceived;
        private string _amountGiven;
        private ObservableCollection<ProductModel> _allProducts = new ObservableCollection<ProductModel>();
        #endregion

        #region Public Properties
        // Time and Staff Information
        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChange(nameof(CurrentDate));
            }
        }
        private string _currentDate;

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChange(nameof(CurrentTime));
            }
        }
        private string _currentTime;

        public string StaffName
        {
            get => _staffName;
            set
            {
                _staffName = value;
                OnPropertyChange(nameof(StaffName));
            }
        }
        private string _staffName;

        // Search and Products
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChange(nameof(SearchQuery));
                FilterProducts();
            }
        }

        public ObservableCollection<ProductModel> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChange(nameof(FilteredProducts));
            }
        }
        private ObservableCollection<ProductModel> _filteredProducts = new ObservableCollection<ProductModel>();

        // Cart and Order Information
        public ObservableCollection<CartItem> CartItems { get; set; } = new ObservableCollection<CartItem>();
        public string ConfirmationText { get; set; } = "Please review your order before processing.";

        public decimal TotalAmount => CartItems.Sum(item => item.Price * item.Quantity);

        // Payment Information
        public string AmountReceived
        {
            get => _amountReceived;
            set
            {
                _amountReceived = value;
                OnPropertyChange(nameof(AmountReceived));
            }
        }

        public string AmountGiven
        {
            get => _amountGiven;
            set
            {
                _amountGiven = value;
                OnPropertyChange(nameof(AmountGiven));
            }
        }
        #endregion

        #region Commands
        public ICommand BackCommand { get; set; }
        public ICommand IncrementQuantityCommand { get; set; }
        public ICommand DecrementQuantityCommand { get; set; }
        public ICommand CancelOrderCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand CheckoutCommand { get; set; }
        public ICommand ProcessOrderCommand { get; set; }
        public ICommand ConfirmOrderCommand { get; private set; }
        #endregion

        #region Constructor
        public NewSaleViewModel()
        {
            InitializeBasicInfo();
            _db = new Db();
            InitializeCommands();
            LoadProductsFromDatabase();
        }
        #endregion

        #region Initialization Methods
        private void InitializeBasicInfo()
        {
            CurrentDate = DateTime.Now.ToString("MM/dd/yyyy");
            CurrentTime = DateTime.Now.ToString("hh:mm tt");
            StaffName = "Rhenz Masarap";
        }

        private void InitializeCommands()
        {
            //BackCommand = new RelayCommand(OnBackButtonClicked);
            IncrementQuantityCommand = new RelayCommand<CartItem>(IncrementQuantity);
            DecrementQuantityCommand = new RelayCommand<CartItem>(DecrementQuantity);
            CancelOrderCommand = new RelayCommand(CancelOrder);
            DeleteItemCommand = new RelayCommand<CartItem>(DeleteItem);
            CheckoutCommand = new RelayCommand(Checkout);
            ProcessOrderCommand = new RelayCommand(ExecuteProcessOrder);
            ProcessOrderCommand = new RelayCommand(ExecuteProcessOrder);
        }
        #endregion

        #region Database Operations
        private void LoadProductsFromDatabase()
        {
            try
            {
                string query = "SELECT * FROM Products WHERE is_archived = 0";
                var productsFromDb = _db.FetchData(query);

                _allProducts.Clear();
                foreach (var product in productsFromDb)
                {
                    _allProducts.Add(new ProductModel
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Category = product.Category,
                        Brand = product.Brand,
                        ModelNumber = product.ModelNumber,
                        SKU = product.SKU,
                        Description = product.Description,
                        StockQuantity = product.StockQuantity,
                        UnitCost = product.UnitCost,
                        SellingPrice = (decimal)product.SellingPrice,
                        MinStockLevel = product.MinStockLevel,
                        IsArchived = product.IsArchived,
                    });
                }
                FilteredProducts = new ObservableCollection<ProductModel>(_allProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                // TODO: Implement proper error handling
            }
        }
        #endregion

        #region Product and Search Operations
        private void FilterProducts()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                FilteredProducts = new ObservableCollection<ProductModel>(_allProducts);
                return;
            }

            var filtered = _allProducts
                .Where(p => p.ProductName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredProducts.Clear();
            foreach (var product in filtered)
            {
                FilteredProducts.Add(product);
            }
        }

        public void AddProductToCart(ProductModel product)
        {
            var existingCartItem = CartItems.FirstOrDefault(item => item.ProductId == product.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Quantity = 1,
                    SellingPrice = product.SellingPrice,
                    Price = product.SellingPrice,
                    Description = product.Description // Description should be set here
                });
            }

            OnPropertyChange(nameof(TotalAmount));
        }
        #endregion

        #region Cart Operations
        private void IncrementQuantity(CartItem item)
        {
            var product = _allProducts.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (product != null && item.Quantity < product.StockQuantity)
            {
                item.Quantity++;
                OnPropertyChange(nameof(TotalAmount));
            }
            else
            {
                MessageBox.Show("Cannot add more. Stock is limited.", "Stock Limit Reached",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DecrementQuantity(CartItem item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
                OnPropertyChange(nameof(TotalAmount));
            }
            else
            {
                MessageBox.Show("Quantity cannot be less than 1.", "Minimum Quantity Reached",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteItem(CartItem item)
        {
            if (item != null)
            {
                CartItems.Remove(item);
                OnPropertyChange(nameof(TotalAmount));
            }
        }

        private void CancelOrder()
        {
            if (CartItems.Count == 0)
            {
                MessageBox.Show("The cart is already empty.", "No items to cancel",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel the order?",
                "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                CartItems.Clear();
                OnPropertyChange(nameof(TotalAmount));
            }
        }
        #endregion

        #region Order Processing
        private void Checkout()
        {
            if (CartItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty. Please add items to the cart.", "Empty Cart",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var confirmationModal = new ConfirmationModal(CartItems, TotalAmount, this); // 'this' refers to NewSaleViewModel
            confirmationModal.ShowDialog();
        }

        public void ExecuteProcessOrder()
        {
            try
            {
                if (!ValidatePayment(out decimal receivedAmount))
                {
                    return;
                }

                decimal change = receivedAmount - TotalAmount;

                SaleModel newSale = new SaleModel
                {
                    DateTime = DateTime.Now,
                    TotalAmount = TotalAmount,
                    PaymentMethod = "Cash",
                    CustomerPaid = receivedAmount,
                    ChangeGiven = change
                };

                // Insert the sale record into the database
                _db.InsertSale(newSale);

                // Notify the user that the order was processed successfully
                MessageBox.Show("Order processed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear the cart items after successful processing
                CartItems.Clear();
            }
            catch (Exception ex)
            {
                // Handle any exception that occurs during order processing
                MessageBox.Show($"An error occurred while processing the order: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidatePayment(out decimal receivedAmount)
        {
            receivedAmount = 0;
            if (!decimal.TryParse(AmountReceived, out receivedAmount) || receivedAmount <= 0)
            {
                MessageBox.Show("Please enter a valid amount for payment.", "Invalid Payment",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (receivedAmount < TotalAmount)
            {
                MessageBox.Show("Amount received is less than the total amount. Please provide sufficient funds.",
                    "Insufficient Payment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
        #endregion
    }
}
