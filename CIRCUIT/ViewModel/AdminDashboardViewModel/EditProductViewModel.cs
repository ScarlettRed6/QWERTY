using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
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
        private decimal _editSellingPrice;

        [ObservableProperty]
        private int _editMinStockLevel;

        [ObservableProperty]
        private int _editStockQuantity;

        [ObservableProperty]
        private decimal _editUnitCost;

        [ObservableProperty]
        private object _currentAddView;

        [ObservableProperty]
        private string _imagePath;

        private Db _dbCon;

        //Commands
        public RelayCommand UpdateProductCommand { get; }
        public RelayCommand ReturnBtnCommand { get; }
        public RelayCommand UploadImageCommand { get; }

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
            UploadImageCommand = new RelayCommand(UploadImage);
            LoadProductById(data.ProductId);
        }

        //Method to upload image
        private void UploadImage()
        {
            
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select an Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff|All Files|*.*" // Filter for image files
            };

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == true)
            {
                
                string originalImagePath = openFileDialog.FileName;

                //Generates a unique file name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(originalImagePath);

                string imagesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Images", "ProductImages");

                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                string newImagePath = Path.Combine(imagesFolderPath, fileName);

                File.Copy(originalImagePath, newImagePath);

                ImagePath = Path.Combine("Assets", "Images", "ProductImages", fileName);

                MessageBox.Show("Image uploaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
                ImagePath = ImagePath,
                
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
        private void LoadProductById(int productId)
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
                ImagePath = prod.ImagePath;
            }
            else
                MessageBox.Show("Product not found.");

        }

    }
}
