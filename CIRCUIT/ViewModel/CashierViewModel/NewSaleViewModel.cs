using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Model.SingleTons;
using CIRCUIT.Utilities;
using CIRCUIT.View.CashierView;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Numerics;
using System.Windows;

using System.Windows.Input;

namespace CIRCUIT.ViewModel
{
    public partial class NewSaleViewModel : ObservableObject
    {

        #region Private Fields
        private readonly Db _db;
        private readonly AccountRepository _accountRepository;
        private readonly SalesRepository _productService;



        private quickpayView quickpayWindow;
        private ConfirmationModal confirmationModal;
        private int _userId;
        private string _searchQuery;
        private string _amountReceived;
        private string _amountGiven;
        private string _paymentMethod;
        private ObservableCollection<ProductModel> _allProducts = new ObservableCollection<ProductModel>();
        public ObservableCollection<string> PaymentMethods { get; } = new ObservableCollection<string> { "Card", "Cash" };

        private List<UsersModel> _users = new List<UsersModel>();
        private decimal _totalAmountBeforeDiscount;
        private decimal _adjustedTotalAmount;
        private bool _isDiscountApplied;
        private decimal _discountPercentage = 20m;

        [ObservableProperty]
        private string _imagePath;

        #endregion




        private decimal _originalTotalAmount;
        private bool _discountAlreadyUsed = false;

        #region Public Properties
        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }
        private string _currentDate;

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }
        private string _currentTime;

        public string StaffName
        {
            get => _staffName;
            set
            {
                _staffName = value;
                OnPropertyChanged(nameof(StaffName));
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
                OnPropertyChanged(nameof(SearchQuery));
                FilterProducts();
            }
        }

        public ObservableCollection<ProductModel> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
            }
        }
        private ObservableCollection<ProductModel> _filteredProducts = new ObservableCollection<ProductModel>();

        // Cart and Order Information
        public ObservableCollection<CartItem> CartItems { get; set; } = new ObservableCollection<CartItem>();


        public string ConfirmationText { get; set; } = "Please review your order before processing.";


        public decimal TotalAmount
        {
            get => _totalAmountBeforeDiscount;
            private set
            {
                SetProperty(ref _totalAmountBeforeDiscount, value);
                _adjustedTotalAmount = value;
                _isDiscountApplied = false;
                OnPropertyChanged(nameof(AdjustedTotalAmount));
            }
        }

        public decimal AdjustedTotalAmount
        {
            get
            {
                if (_isDiscountApplied)
                {
                    decimal discountAmount = _originalTotalAmount * (_discountPercentage / 100m);
                    return _originalTotalAmount - discountAmount;
                }
                return TotalAmount;
            }
        }


        private void CalculateChange()
        {
            if (decimal.TryParse(AmountReceived, out decimal amountReceived))
            {
                decimal totalAmount = AdjustedTotalAmount > 0 ? AdjustedTotalAmount : TotalAmount;
                decimal change = amountReceived - totalAmount;

                // Ensure change is not negative
                AmountGiven = change >= 0 ? change.ToString("N2") : "0.00";
            }
            else
            {
                AmountGiven = "0.00";
            }
        }





        #region Discount Method
        private void ApplyDiscount()
        {
            if (_discountAlreadyUsed)
            {
                MessageBox.Show("Discount has already been applied.", "Discount Limit",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (CartItems.Count == 0)
            {
                MessageBox.Show("Cannot apply discount. Cart is empty.", "No Items",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Activate discount flag
            _isDiscountApplied = true;
            _discountAlreadyUsed = true;

            // Recalculate total with discount
            RecalculateTotalAmount();

            MessageBox.Show($"A {_discountPercentage}% discount has been applied!", "Discount Applied",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        private void RecalculateTotalAmount()
        {
            decimal newCartTotal = CartItems.Sum(i => i.Price * i.Quantity);

            if (_isDiscountApplied)
            {
                // Always recalculate the discount based on the full new cart total
                decimal discountAmount = newCartTotal * (_discountPercentage / 100m);
                TotalAmount = newCartTotal - discountAmount;
            }
            else
            {
                // Normal calculation if no discount applied
                TotalAmount = newCartTotal;
                _originalTotalAmount = TotalAmount;
            }

            OnPropertyChanged(nameof(TotalAmount));
            OnPropertyChanged(nameof(AdjustedTotalAmount));
        }

        public string AmountReceived
        {
            get => _amountReceived;
            set
            {
                _amountReceived = value;
                OnPropertyChanged(nameof(AmountReceived));
                CalculateChange();
            }
        }

        public string AmountGiven
        {
            get => _amountGiven;
            set
            {
                _amountGiven = value;
                OnPropertyChanged(nameof(AmountGiven));
            }
        }

        public string PaymentMethod
        {
            get => _paymentMethod;
            set
            {
                _paymentMethod = value;
                OnPropertyChanged(nameof(PaymentMethod));
            }
        }

        #endregion

        #region Commands
        public ICommand ApplyDiscountCommand { get; }
        public ICommand BackCommand { get; set; }
        public ICommand IncrementQuantityCommand { get; set; }
        public ICommand DecrementQuantityCommand { get; set; }
        public ICommand CancelOrderCommand { get; set; }
        public ICommand returncartCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand CheckoutCommand { get; set; }
        public ICommand ProcessOrderCommand { get; set; }
        public ICommand ConfirmOrderCommand { get; private set; }
        //new added features
        public ICommand MyProfileCommand { get; set; }
        public ICommand LayawayCommand { get; set; }
        public ICommand QuickPayCommand { get; set; }
        public ICommand PreorderCommand { get; set; }
        public ICommand VoidCommand { get; set; }
        public ICommand ReprintReceiptCommand { get; set; }
        public ICommand ViewInventoryCommand { get; set; }
        public ICommand ResetOrderDetailsCommand { get; set; }
        public ICommand ExecuteQuickOrderCommand { get; set; }
        //Button close command
        public RelayCommand BtnCloseCommand { get; set; }



        public RelayCommand LogOutBtnCommand { get; set; }
        #endregion

        #region Constructor
        public NewSaleViewModel()
        {

            _db = new Db();
            _userId = UserSessionService.Instance.UserId;
            _accountRepository = new AccountRepository();
            InitializeBasicInfo();
            InitializeStaffName();
            InitializeCommands();
            ApplyDiscountCommand = new RelayCommand(ApplyDiscount);
            LoadProductsFromDatabase();

        }
        #endregion

        #region Initialization Methods
        private void InitializeBasicInfo()
        {
            CurrentDate = DateTime.Now.ToString("MM/dd/yyyy");
            CurrentTime = DateTime.Now.ToString("hh:mm tt");

        }
        //Initialize staff name
        private void InitializeStaffName()
        {
            var userList = _accountRepository.FetchUser(_userId);
            foreach (var user in userList)
            {
                _users.Add(user);
                StaffName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.Username.ToLower());

            }

        }

        private void InitializeCommands()
        {
            //BackCommand = new RelayCommand(OnBackButtonClicked);
            IncrementQuantityCommand = new RelayCommand<CartItem>(IncrementQuantity);
            DecrementQuantityCommand = new RelayCommand<CartItem>(DecrementQuantity);
            CancelOrderCommand = new RelayCommand(CancelOrder);
            returncartCommand = new RelayCommand(returncart);
            DeleteItemCommand = new RelayCommand<CartItem>(DeleteItem);
            CheckoutCommand = new RelayCommand(Checkout);
            ProcessOrderCommand = new RelayCommand(ExecuteProcessOrder);
            ExecuteQuickOrderCommand = new RelayCommand(ExecuteQuickOrder);
            LogOutBtnCommand = new RelayCommand(ExecuteLogout);
            //new added features
            MyProfileCommand = new RelayCommand(OpenMyProfile);
            QuickPayCommand = new RelayCommand(ExecuteQuickPay);
            VoidCommand = new RelayCommand(VoidOrder);
            ViewInventoryCommand = new RelayCommand(ViewInventory);
            ResetOrderDetailsCommand = new RelayCommand(ResetOrderDetails);
            BtnCloseCommand = new RelayCommand(ExecuteClose);
        }
        #endregion

        private void OpenMyProfile()
        {
            CashierProfileView cashierProfileView = new CashierProfileView();
            cashierProfileView.Show();
        }

        private void ExecuteQuickPay()
        {
            if (CartItems.Count == 0)
            {
                MessageBox.Show("The cart is empty.", "No items can process",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (CartItems.Count > 5)
            {
                MessageBox.Show("Only 5 items can be processed.", "Too many items",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            quickpayWindow = new quickpayView(CartItems, TotalAmount, this);
            quickpayWindow.Show();

        }

        private void VoidOrder()
        {
            voidOrderView voidOrderWindow = new voidOrderView();
            voidOrderWindow.Show();
        }

        private void ViewInventory()
        {
            inventoryView inventoryWindow = new inventoryView();
            inventoryWindow.Show();
        }

        //Button close method
        private void ExecuteClose()
        {
            var result = MessageBox.Show("Are you sure you want to close the app? Current account will be logged out", "Close the application", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            _accountRepository.LogUserOut(_userId);
            Application.Current.Shutdown();

        }


        #region Database Operations


        //etong database operations  kailangan ilipat sa iserviceProduct
        //Dependency Injection

        //Refactor o nalang siguro 
        private void LoadProductsFromDatabase()

        {

            string query = "SELECT * FROM tbl_Products WHERE is_archived = 0 AND stock_quantity <= min_stock_level";
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
                    ImagePath = product.ImagePath,
                });
            }

            FilteredProducts = new ObservableCollection<ProductModel>(_allProducts);

            if (_allProducts.Count == 0)
            {
                MessageBox.Show("No products available", "Low Stock",
                    MessageBoxButton.OK, MessageBoxImage.Information);
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
                    Description = product.Description
                });
            }

            RecalculateTotalAmount();
        }
        #endregion

        #region Cart Operations
        private void IncrementQuantity(CartItem item)
        {
            var product = _allProducts.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (product != null && item.Quantity < product.StockQuantity)
            {
                item.Quantity++;
                RecalculateTotalAmount();
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
                RecalculateTotalAmount();
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
                RecalculateTotalAmount();
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
                TotalAmount = 0;
                _isDiscountApplied = false;
                _discountAlreadyUsed = false;
                OnPropertyChanged(nameof(AdjustedTotalAmount));
            }
        }

        private void returncart()
        {

            _isDiscountApplied = false;
            _discountAlreadyUsed = false;
            OnPropertyChanged(nameof(AdjustedTotalAmount));
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

            confirmationModal = new ConfirmationModal(CartItems, TotalAmount, this);
            confirmationModal.ShowDialog();
        }



        private void ExecuteLogout()
        {
            _accountRepository.LogUserOut(_userId);
        }

        //Modified executeprocessorder to print a receipt
        private void ExecuteProcessOrder()
        {
            MessageBoxResult result = MessageBox.Show("Are the items finalized? Do you want to proceed with payment?",
                                                      "Confirm Payment",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                if (!ValidatePayment(out decimal receivedAmount))
                {
                    return;
                }

                decimal change = receivedAmount - TotalAmount;

                if (string.IsNullOrEmpty(PaymentMethod))
                {
                    MessageBox.Show("Please select a payment method.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create a new sale record
                SaleModel newSale = new SaleModel
                {
                    CashierId = _userId,
                    DateTime = DateTime.Now,
                    TotalAmount = TotalAmount,
                    PaymentMethod = PaymentMethod,
                    CustomerPaid = receivedAmount,
                    ChangeGiven = change
                };
                int newSaleId = _db.InsertSale(newSale);

                foreach (var cartItem in CartItems)
                {
                    SaleItemModel saleItem = new SaleItemModel
                    {
                        SaleId = newSaleId,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        ItemTotalPrice = cartItem.Quantity * cartItem.SellingPrice
                    };
                    _db.InsertSaleItem(saleItem);
                    _db.UpdateProductStockQuantity(cartItem.ProductId, cartItem.Quantity);
                }

                // Open a SaveFileDialog to save the receipt
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = $"Receipt_{newSaleId}.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string exportPath = saveFileDialog.FileName;

                    // Generate the receipt PDF
                    PdfGenerator.GenerateReceiptPdf(newSale, newSaleId, CartItems.ToList(), exportPath);

                    MessageBox.Show("Receipt generated and order processed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Reset the order details
                LoadProductsFromDatabase();
                ResetOrderDetails();
                AmountReceived = string.Empty;
                AmountGiven = string.Empty;
                PaymentMethod = string.Empty;
                CartItems.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing the order: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            confirmationModal?.Close();
        }


        private void ExecuteQuickOrder()
        {
            MessageBoxResult result = MessageBox.Show("Are the items finalized? Do you want to proceed with payment?",
                                                      "Confirm Payment",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                // Create a new sale record
                SaleModel newSale = new SaleModel
                {
                    CashierId = _userId,
                    DateTime = DateTime.Now,
                    PaymentMethod = "Cash",
                    TotalAmount = TotalAmount,
                };
                int newSaleId = _db.InsertSale(newSale);

                foreach (var cartItem in CartItems)
                {
                    SaleItemModel saleItem = new SaleItemModel
                    {
                        SaleId = newSaleId,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        ItemTotalPrice = cartItem.Quantity * cartItem.SellingPrice
                    };
                    _db.InsertSaleItem(saleItem);
                    _db.UpdateProductStockQuantity(cartItem.ProductId, cartItem.Quantity);
                }

                // Open a SaveFileDialog to save the receipt
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = $"Receipt_{newSaleId}.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string exportPath = saveFileDialog.FileName;

                    // Generate the receipt PDF
                    PdfGenerator.GenerateReceiptPdf(newSale, newSaleId, CartItems.ToList(), exportPath);

                    MessageBox.Show("Receipt generated and order processed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Receipt generation canceled by user.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Reset the order details
                LoadProductsFromDatabase();
                ResetOrderDetails();
                TotalAmount = 0;
                CartItems.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing the order: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            quickpayWindow?.Close();
        }


        private void ResetOrderDetails()
        {
            CartItems.Clear();
            TotalAmount = 0;
            _isDiscountApplied = false;
            _discountAlreadyUsed = false;
            PaymentMethod = string.Empty;
            PaymentMethod = "";
            OnPropertyChanged(nameof(AdjustedTotalAmount));

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

            decimal totalToCompare = AdjustedTotalAmount > 0 ? AdjustedTotalAmount : TotalAmount;
            if (receivedAmount < totalToCompare)
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
