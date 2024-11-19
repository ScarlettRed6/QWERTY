using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class ArchiveProductViewModel : ObservableObject
    {
        //Fields and Properties
        [ObservableProperty]
        private object _currentArchiveView;

        [ObservableProperty]
        private string _archiveProductName;

        [ObservableProperty]
        private string _archiveCategory;

        [ObservableProperty]
        private int _archiveStockQuantity;

        [ObservableProperty]
        private string _archiveDescription;

        [ObservableProperty]
        private int _archiveSKU;

        [ObservableProperty]
        private string _archiveModel;

        [ObservableProperty]
        private string _archiveBrand;

        [ObservableProperty]
        private double _archivePrice;

        [ObservableProperty]
        private double _archiveUnitCost;

        [ObservableProperty]
        private int _archiveMinStockLevel;

        [ObservableProperty]
        private int _archiveProductId;

        //Class objects
        Db _dbConn;

        //Commands
        public RelayCommand ArchiveCancelCommand { get; }
        public RelayCommand ArchiveProceedCommand { get; }

        //Default constructor
        public ArchiveProductViewModel() => _dbConn = new Db();

        //Override constructor with parameters
        public ArchiveProductViewModel(ProductModel data)
        {
            _dbConn = new Db();
            ArchiveProductId = data.ProductId;
            ArchiveProceedCommand = new RelayCommand(ExecuteArchiveCommand);
            ArchiveCancelCommand = new RelayCommand(ExecuteCancelCommand);
            LoadProductById(ArchiveProductId);

        }

        private void ExecuteCancelCommand()
        {
            CurrentArchiveView = new CatalogViewModel();
        }

        private void ExecuteArchiveCommand()
        {
            var result = MessageBox.Show("Do you want to proceed with the archive?", "Proceed to archive", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            var updatedProduct = new ProductModel
            {
                ProductId = ArchiveProductId,
                IsArchived = true
            };

            _dbConn.ArchiveProduct(updatedProduct);

            CurrentArchiveView = new CatalogViewModel();
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
                ArchiveProductName = prod.ProductName;
                ArchiveModel = prod.ModelNumber;
                ArchiveBrand = prod.Brand;
                ArchiveCategory = prod.Category;
                ArchiveDescription = prod.Description;
                ArchivePrice = prod.SellingPrice;
                ArchiveMinStockLevel = prod.MinStockLevel;
                ArchiveStockQuantity = prod.StockQuantity;
                ArchiveUnitCost = prod.UnitCost;
                ArchiveSKU = prod.SKU;
            }
            else
                MessageBox.Show("Product not found.");

        }


    }
}
