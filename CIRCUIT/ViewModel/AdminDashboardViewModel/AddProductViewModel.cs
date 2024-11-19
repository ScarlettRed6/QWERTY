using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class AddProductViewModel : ObservableObject
    {
        //Properties
        [ObservableProperty]
        private string _productNameBox;

        [ObservableProperty]
        private string _productModelBox;

        [ObservableProperty]
        private string _brandBox;

        [ObservableProperty]
        private string _categoryBox;

        [ObservableProperty]
        private string _productDescriptionBox;

        [ObservableProperty]
        private double _sellingPriceBox;

        [ObservableProperty]
        private int _minStockLevel;

        [ObservableProperty]
        private int _stockQuantityBox;

        [ObservableProperty]
        private double _unitCost;

        [ObservableProperty]
        private object _currentAddView;

        //Random number integer for sku placeholder
        private Random _random;

        //Db object
        private Db _dbCon;

        //Commands
        public RelayCommand ReturnBtnCommand { get; }
        public RelayCommand SaveProductCommand { get; }

        //Constructor
        public AddProductViewModel()
        {
            _dbCon = new Db();
            ReturnBtnCommand = new RelayCommand(ExecuteReturnBtn);
            SaveProductCommand = new RelayCommand(ExecuteSaveProductCommand);

        }

        private void ExecuteSaveProductCommand()
        {
            if (ProductNameBox == null || ProductModelBox == null || BrandBox == null || CategoryBox == null ||
                ProductDescriptionBox == null || SellingPriceBox <= 0 || MinStockLevel <= 0 || StockQuantityBox <= 0 || UnitCost <= 0)
            {
                MessageBox.Show("Empty details is not allowed!", "Missing Product Details", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _random = new Random();
            var _products = AddProduct(_random);

            _dbCon = new Db();
            _dbCon.InsertProduct(_products);
            CurrentAddView = new CatalogViewModel();

        }

        private ProductModel AddProduct(Random _random)
        {
            var _products = new ProductModel
            {
                ProductName = ProductNameBox,
                ModelNumber = ProductModelBox,
                Brand = BrandBox,
                Category = CategoryBox,
                Description = ProductDescriptionBox,
                SellingPrice = SellingPriceBox,
                MinStockLevel = MinStockLevel,
                StockQuantity = StockQuantityBox,
                UnitCost = UnitCost,
                SKU = _random.Next(1, 9),
                IsArchived = false
            };
            return _products;

        }

        private void ExecuteReturnBtn()
        {
            CurrentAddView = new CatalogViewModel();
        }
    }
}
