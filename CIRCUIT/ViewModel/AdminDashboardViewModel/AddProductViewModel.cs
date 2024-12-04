using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
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
        private decimal _sellingPriceBox;

        [ObservableProperty]
        private int _minStockLevel;

        [ObservableProperty]
        private int _stockQuantityBox;

        [ObservableProperty]
        private decimal _unitCost;

        [ObservableProperty]
        private object _currentAddView;

        [ObservableProperty]
        private string _imagePath;

        //Random number integer for sku placeholder
        private Random _random;

        //Db object
        private Db _dbCon;

        //Commands
        public RelayCommand ReturnBtnCommand { get; }
        public RelayCommand SaveProductCommand { get; }
        public RelayCommand UploadImageCommand { get; }

        //Constructor
        public AddProductViewModel()
        {
            _dbCon = new Db();
            ReturnBtnCommand = new RelayCommand(ExecuteReturnBtn);
            SaveProductCommand = new RelayCommand(ExecuteSaveProductCommand);
            UploadImageCommand = new RelayCommand(UploadImage);

        }

        //Method to upload image
        private void UploadImage()
        {
            // Create and configure the OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select an Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff|All Files|*.*" // Filter for image files
            };

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the file path of the selected image
                string originalImagePath = openFileDialog.FileName;

                // Generate a unique file name (you could use a GUID or timestamp to ensure uniqueness)
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(originalImagePath);

                // Define the folder path where you want to store the image
                string imagesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Images", "ProductImages");

                // Ensure the directory exists
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                // Define the new image path
                string newImagePath = Path.Combine(imagesFolderPath, fileName);

                // Copy the selected image to the new location
                File.Copy(originalImagePath, newImagePath);

                // Store the image path (relative or absolute depending on how you will use it)
                ImagePath = Path.Combine("Assets", "Images", "ProductImages", fileName); // Store relative path or full path

                MessageBox.Show("Image uploaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Execute to save product
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
                IsArchived = false,
                ImagePath = ImagePath  // Include the image path here
            };
            return _products;

        }

        private void ExecuteReturnBtn()
        {
            CurrentAddView = new CatalogViewModel();
        }
    }
}
