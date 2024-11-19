using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class EditProductViewModel : ObservableObject
    {
        //Fields and Properties using ObservableProperty for quality of life
        [ObservableProperty]
        private int _productID;

        [ObservableProperty]
        private string _editName;

        [ObservableProperty]
        private string _editModel;

        [ObservableProperty]
        private string _editBrand;

        [ObservableProperty]
        private string _editCategory;

        [ObservableProperty]
        private string _editDescription;

        [ObservableProperty]
        private double _editSellingPrice;

        [ObservableProperty]
        private int _editMinStockLevel;

        [ObservableProperty]
        private int _editStockQuantity;

        [ObservableProperty]
        private double _editUnitCost;

        [ObservableProperty]
        private object _currentAddView;

        private Db _dbCon;

        //Commands
        public RelayCommand UpdateProductCommand { get; }
        public RelayCommand ReturnBtnCommand { get; }

        // Default constructor
        public EditProductViewModel() => _dbCon = new Db();

        // Constructor with a parameter
        public EditProductViewModel(ProductModel data)
        {
            //InitializeBoxes(data); 
            _dbCon = new Db();
            ProductID = data.ProductId;
            ReturnBtnCommand = new RelayCommand(ExecuteReturnBtn);
            UpdateProductCommand = new RelayCommand(ExecuteUpdateCommand);
            LoadProductById(data.ProductId);
        }

        //Execute UpdateProductCommand
        private void ExecuteUpdateCommand()
        {
            var result = MessageBox.Show("Do you want to proceed with the Update?","Proceed to update",MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result != MessageBoxResult.Yes)
            {
                return;
            }

            var updatedProduct = new ProductModel
            {
                ProductId = ProductID,
                ProductName = EditName,
                ModelNumber = EditModel,
                Brand = EditBrand,
                Category = EditCategory,
                Description = EditDescription,
                SellingPrice = EditSellingPrice,
                MinStockLevel = EditMinStockLevel,
                StockQuantity = EditStockQuantity,
                UnitCost = EditUnitCost,
                
            };

            _dbCon.UpdateProductData(updatedProduct);

            CurrentAddView = new CatalogViewModel();

        }

        //Execute ReturnBtnCommand
        private void ExecuteReturnBtn()
        {
            CurrentAddView = new CatalogViewModel();
        }

        // Method to load product data by ID
        public void LoadProductById(int productId)
        {
            //Messagebox for testing purposes
            //MessageBox.Show($"Loading product with ID: {productId}");
            var products = _dbCon.GetProductById(productId);

            if (products != null && products.Count > 0)
            {
                var prod = products[0];
                EditName = prod.ProductName;
                EditModel = prod.ModelNumber;
                EditBrand = prod.Brand;
                EditCategory = prod.Category;
                EditDescription = prod.Description;
                EditSellingPrice = prod.SellingPrice;
                EditMinStockLevel = prod.MinStockLevel;
                EditStockQuantity = prod.StockQuantity;
                EditUnitCost = prod.UnitCost;
            }
            else
                MessageBox.Show("Product not found.");

        }

    }
}
