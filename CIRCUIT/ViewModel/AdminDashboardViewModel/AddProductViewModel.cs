using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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
        private string _imagePath = "no image";

        [ObservableProperty]
        private string _newBrand;

        [ObservableProperty]
        private string _newCategory;

        //For category and brand comboxes and textboxes, disables if either has value or selection
        public bool IsBrandBoxEnabled => string.IsNullOrWhiteSpace(NewBrand);
        public bool IsNewBrandEnabled => string.IsNullOrWhiteSpace(BrandBox);
        public bool IsCategoryBoxEnabled => string.IsNullOrWhiteSpace(NewCategory);
        public bool IsNewCategoryEnabled => string.IsNullOrWhiteSpace(CategoryBox);

        //Collections
        public ObservableCollection<string> Brands { get; set; }
        public ObservableCollection<string> Categories { get; set; }

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
            Brands = new ObservableCollection<string>();
            Categories = new ObservableCollection<string>();

            LoadBrandsAndCategories();

            ReturnBtnCommand = new RelayCommand(ExecuteReturnBtn);
            SaveProductCommand = new RelayCommand(ExecuteSaveProductCommand);
            UploadImageCommand = new RelayCommand(UploadImage);

        }

        private void LoadBrandsAndCategories()
        {
            Brands.Clear();
            Brands.Add("None"); // Placeholder option
            foreach (var brand in _dbCon.GetBrands())
            {
                Brands.Add(brand);
            }

            Categories.Clear();
            Categories.Add("None"); // Placeholder option
            foreach (var category in _dbCon.GetCategories())
            {
                Categories.Add(category);
            }
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
            // Check if required fields are empty
            if (string.IsNullOrWhiteSpace(ProductNameBox) ||
                string.IsNullOrWhiteSpace(ProductModelBox) ||
                string.IsNullOrWhiteSpace(ProductDescriptionBox) ||
                SellingPriceBox <= 0 ||
                MinStockLevel <= 0 ||
                StockQuantityBox <= 0 ||
                UnitCost <= 0)
            {
                MessageBox.Show("Mandatory product details are missing or invalid!", "Missing Product Details", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if both Brand and NewBrand are empty
            if (string.IsNullOrWhiteSpace(BrandBox) && string.IsNullOrWhiteSpace(NewBrand))
            {
                MessageBox.Show("Please specify a brand (existing or new)!", "Missing Brand", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if both Category and NewCategory are empty
            if (string.IsNullOrWhiteSpace(CategoryBox) && string.IsNullOrWhiteSpace(NewCategory))
            {
                MessageBox.Show("Please specify a category (existing or new)!", "Missing Category", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Proceed with adding the product
            _random = new Random();
            var _products = AddProduct(_random);

            if (_products != null)
            {
                _dbCon = new Db();
                _dbCon.InsertProduct(_products);
                CurrentAddView = new CatalogViewModel();
                MessageBox.Show("Product added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private ProductModel AddProduct(Random _random)
        {
            var _products = new ProductModel();

            // Handle Brand
            if (!string.IsNullOrWhiteSpace(NewBrand))
            {
                if (Brands.Contains(NewBrand, StringComparer.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"The brand '{NewBrand}' already exists!", "Duplicate Brand", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null; // Exit early if there's a conflict
                }
                else
                {
                    _products.Brand = NewBrand;
                }
            }
            else if (!string.IsNullOrWhiteSpace(BrandBox))
            {
                _products.Brand = BrandBox;
            }
            else
            {
                MessageBox.Show("Please specify a brand.", "Brand Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            // Handle Category
            if (!string.IsNullOrWhiteSpace(NewCategory))
            {
                if (Categories.Contains(NewCategory, StringComparer.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"The category '{NewCategory}' already exists!", "Duplicate Category", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null; // Exit early if there's a conflict
                }
                else
                {
                    _products.Category = NewCategory;
                }
            }
            else if (!string.IsNullOrWhiteSpace(CategoryBox))
            {
                _products.Category = CategoryBox;
            }
            else
            {
                MessageBox.Show("Please specify a category.", "Category Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            // Populate remaining fields
            _products.ProductName = ProductNameBox;
            _products.ModelNumber = ProductModelBox;
            _products.Description = ProductDescriptionBox;
            _products.SellingPrice = SellingPriceBox;
            _products.MinStockLevel = MinStockLevel;
            _products.StockQuantity = StockQuantityBox;
            _products.UnitCost = UnitCost;
            _products.SKU = _random.Next(1, 9);
            _products.IsArchived = false;
            _products.ImagePath = ImagePath;

            return _products;

        }

        private void ExecuteReturnBtn()
        {
            CurrentAddView = new CatalogViewModel();
        }


        partial void OnNewBrandChanged(string value)
        {
            OnPropertyChanged(nameof(IsBrandBoxEnabled));
        }

        partial void OnBrandBoxChanged(string value)
        {
            if (value == "None")
            {
                BrandBox = null;
            }

            // Enable or clear NewBrand based on selection
            if (!string.IsNullOrEmpty(value) && value != "None")
            {
                NewBrand = null; // Clear new brand if an existing brand is selected
            }
            OnPropertyChanged(nameof(IsNewBrandEnabled));
        }

        partial void OnNewCategoryChanged(string value)
        {
            OnPropertyChanged(nameof(IsCategoryBoxEnabled));
        }

        partial void OnCategoryBoxChanged(string value)
        {
            if (value == "None")
            {
                CategoryBox = null;
            }

            // Enable or clear NewCategory based on selection
            if (!string.IsNullOrEmpty(value) && value != "None")
            {
                NewCategory = null; // Clear new category if an existing category is selected
            }
            OnPropertyChanged(nameof(IsNewCategoryEnabled));
        }
    }
}
