using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class ArchivedProductsViewModel : PropertyChange
    {
        //Fields
        private int _currentPage = 1;
        private int _itemsPerPage = 10; // Adjust as needed
        private int _totalItems;
        private string _searchTerm;
        private object _addProductView;

        //database connection instance of an object
        private Db dbCon = new Db();

        //Calculates the total pages dynamically
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);

        //Collections
        public ObservableCollection<ProductModel> PagedProducts { get; set; }
        public ObservableCollection<ProductModel> Product { get; set; }
        private List<ProductModel> _items;

        //Commands
        public RelayCommand<ProductModel> RecoverCommand { get; }
        public RelayCommand<ProductModel> ViewCommand { get; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }

        //Class objects
        private EditProductViewModel _editProductViewModel;
        private ViewProductViewModel viewProductViewModel;

        //Properties
        //For pagination
        public object AddProductView
        {
            get { return _addProductView; }
            set
            {
                _addProductView = value;
                OnPropertyChange();
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value && value > 0 && value <= TotalPages)
                {
                    _currentPage = value;
                    OnPropertyChange();
                    UpdatePagedProducts();
                }
            }
        }
        //For pagination number of items per page
        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set
            {
                _itemsPerPage = value;
                OnPropertyChange();
                UpdatePagedProducts();
            }
        }
        //For total items in the dataset Product collection
        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = value;
                OnPropertyChange();
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    OnPropertyChange();
                    UpdatePagedProducts();
                }
            }
        }

        public List<ProductModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChange();
            }
        }

        //Constructor
        public ArchivedProductsViewModel()
        {
            Product = new ObservableCollection<ProductModel>();
            PagedProducts = new ObservableCollection<ProductModel>();
            RecoverCommand = new RelayCommand<ProductModel>(ExecuteRecoverCommand);
            ViewCommand = new RelayCommand<ProductModel>(ExecuteViewCommand);
            FirstPageCommand = new RelayCommand(() => CurrentPage = 1);
            LastPageCommand = new RelayCommand(() => CurrentPage = TotalPages);
            NextPageCommand = new RelayCommand(() =>
            {
                if (CurrentPage < TotalPages)
                    CurrentPage++;
            });
            PreviousPageCommand = new RelayCommand(() =>
            {
                if (CurrentPage > 1)
                    CurrentPage--;
            });

            UpdateCatalog();

        }

        private void ExecuteRecoverCommand(ProductModel? model)
        {
            var result = MessageBox.Show("Are you sure you want to recover this archived product?", "Recover product", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            var updatedProduct = new ProductModel
            {
                ProductId = model.ProductId,
                IsArchived = true
            };

            dbCon.RecoverArchived(updatedProduct);
            UpdateCatalog();

        }

        //Execute ArchiveCommand
        private void ExecuteViewCommand(ProductModel data)
        {
            viewProductViewModel = new ViewProductViewModel(data, 2);
            AddProductView = viewProductViewModel;
        }

        //Refreshes the products in the datagrid
        public void UpdateCatalog()
        {
            string query = "SELECT product_id, product_name, category, description, brand, model_number, stock_quantity, unit_cost, selling_price, image_path FROM tbl_products WHERE is_archived = 1";
            var products = dbCon.FetchData(query);

            Product.Clear();
            Items = new List<ProductModel>(); // Clear and repopulate Items as well
            foreach (var prod in products)
            {
                Product.Add(prod);
                Items.Add(prod); // Populate Items for "Select All" logic
            }

            TotalItems = Product.Count;
            UpdatePagedProducts();
        }

        //Updates data per page
        private void UpdatePagedProducts()
        {
            IEnumerable<ProductModel> filteredProducts = Product;

            // Apply filtering if a search term is provided
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filteredProducts = filteredProducts.Where(p =>
                    p.ProductName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Update TotalItems dynamically
            TotalItems = filteredProducts.Count();

            // Paginate the results
            PagedProducts = new ObservableCollection<ProductModel>(
                filteredProducts.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            OnPropertyChange(nameof(PagedProducts));
        }

        //Search and filters data by Product name, filter can be modified later
        public void SearchProducts(string searchTerm)
        {
            // Filter the product list based on the search term
            var filteredProducts = Product
                .Where(p => p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Update TotalItems to reflect filtered results
            TotalItems = filteredProducts.Count;

            // Paginate the filtered results
            PagedProducts = new ObservableCollection<ProductModel>(
                filteredProducts.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            // Notify UI about changes
            OnPropertyChange(nameof(PagedProducts));
        }

    }
}
