using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class ViewProductViewModel : ObservableObject
    {

        //Fields and Properties
        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private string _viewProductName;

        [ObservableProperty]
        private string _viewCategory;

        [ObservableProperty]
        private int _viewStockQuantity;

        [ObservableProperty]
        private string _viewDescription;

        [ObservableProperty]
        private int _viewSKU;

        [ObservableProperty]
        private string _viewModel;

        [ObservableProperty]
        private string _viewBrand;

        [ObservableProperty]
        private decimal _viewPrice;

        [ObservableProperty]
        private decimal _viewUnitCost;

        [ObservableProperty]
        private int _viewMinStockLevel;

        [ObservableProperty]
        private int _viewProductId;

        [ObservableProperty]
        private int _lastView;

        //Class objects
        Db _dbConn;
        CatalogViewModel _catalogViewModel;
        ArchivedProductsViewModel _archivedProductsViewModel;


        //Commands
        public RelayCommand ReturnBtnCommand { get; }

        //Default constructor
        public ViewProductViewModel() => _dbConn = new Db();

        //Override constructor with parameters
        public ViewProductViewModel(ProductModel data, int lastView)
        {
            _dbConn = new Db();
            ViewProductId = data.ProductId;
            LastView = lastView;
            ReturnBtnCommand = new RelayCommand(ExecuteReturnCommand);
            LoadProductById(ViewProductId);

        }

        //Execute return command to return to catalog view
        private void ExecuteReturnCommand()
        {
            //Returns to catalogview
            if (LastView == 1)
            {
                CurrentView = new CatalogViewModel();
            }
            //Returns to archived view
            if(LastView == 2)
            {
                CurrentView = new ArchivedProductsViewModel();
            }

            
        }

        // Method to load product data by ID
        public void LoadProductById(int productId)
        {
            //Messagebox for testing purposes
            //MessageBox.Show($"Loading product with ID: {productId}");
            var products = _dbConn.GetProductById(productId);

            if (products != null && products.Count > 0)
            {
                var prod = products[0];
                ViewProductName = prod.ProductName;
                ViewModel = prod.ModelNumber;
                ViewBrand = prod.Brand;
                ViewCategory = prod.Category;
                ViewDescription = prod.Description;
                ViewPrice = prod.SellingPrice;
                ViewMinStockLevel = prod.MinStockLevel;
                ViewStockQuantity = prod.StockQuantity;
                ViewUnitCost = prod.UnitCost;
                ViewSKU = prod.SKU;
            }
            else
                MessageBox.Show("Product not found.");

        }

    }
}
