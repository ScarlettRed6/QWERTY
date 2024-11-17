using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class AddProductViewModel : PropertyChange
    {
        //Fields
        private string _productNameBox;
        private string _productModelBox;
        private string _brandBox;
        private string _categoryBox;
        private string _productDescriptionBox;
        private double _sellingPriceBox;
        private int _minStockLevel;
        private int _stockQuantityBox;
        private double _unitCost;
        private object _currentAddView;

        //Random number integer for sku placeholder
        private Random _random;

        //Db object
        private Db _dbCon;

        //Commands
        public RelayCommand ReturnBtnCommand { get; }
        public RelayCommand SaveProductCommand { get; }

        //Properties
        public object CurrentAddView
        {
            get { return _currentAddView; }
            set 
            { 
                _currentAddView = value;
                OnPropertyChange();
            }
        }

        public string ProductNameBox 
        { 
            get => _productNameBox; 
            set 
            { 
                _productNameBox = value;
                OnPropertyChange();
            } 
        }

        public string ProductModelBox 
        { 
            get => _productModelBox; 
            set
            {
                _productModelBox = value; 
                OnPropertyChange();
            } 
        }

        public string BrandBox 
        { 
            get => _brandBox; 
            set 
            { 
                _brandBox = value; 
                OnPropertyChange();
            } 
        }

        public string CategoryBox 
        { 
            get => _categoryBox; 
            set 
            { 
                _categoryBox = value; 
                OnPropertyChange();
            } 
        }

        public string ProductDescriptionBox 
        { 
            get => _productDescriptionBox; 
            set 
            { 
                _productDescriptionBox = value;
                OnPropertyChange();
            }
        }

        public double SellingPriceBox 
        { 
            get => _sellingPriceBox; 
            set 
            { 
                _sellingPriceBox = value;
                OnPropertyChange();
            }
        }

        public int MinStockLevelBox
        { 
            get => _minStockLevel; 
            set 
            { 
                _minStockLevel = value;
                OnPropertyChange();
            }
        }

        public int StockQuantityBox 
        { 
            get => _stockQuantityBox; 
            set 
            { 
                _stockQuantityBox = value;
                OnPropertyChange();
            }
        }

        public double UnitCostBox
        {
            get => _unitCost;
            set 
            { 
                _unitCost = value; 
            }
        }

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
                ProductDescriptionBox == null || SellingPriceBox <= 0 || MinStockLevelBox <= 0 || StockQuantityBox <= 0 || UnitCostBox <= 0)
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
                MinStockLevel = MinStockLevelBox,
                StockQuantity = StockQuantityBox,
                UnitCost = UnitCostBox,
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
