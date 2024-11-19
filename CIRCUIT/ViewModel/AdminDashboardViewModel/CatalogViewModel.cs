using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class CatalogViewModel : PropertyChange
    {
        //Fields
        private int _currentPage = 1;
        private int _itemsPerPage = 10; // Adjust as needed
        private int _totalItems;
        private object _addProductView;
        private string _searchTerm;

        //database connection instance of an object
        private Db dbCon = new Db();

        //Calculates the total pages dynamically
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);

        //Collections
        public ObservableCollection<ProductModel> PagedProducts { get; set; }
        public ObservableCollection<ProductModel> Product { get; set; }

        //Commands
        public RelayCommand AddNewProduct { get; }
        public RelayCommand<ProductModel> EditCommand { get; }
        public RelayCommand<ProductModel> ArchiveCommand { get; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }

        //Class objects
        private EditProductViewModel _editProductViewModel;
        private ArchiveProductViewModel _archiveProductViewModel;

        //Properties
        //For switching to addproduct view
        public object AddProductView
        {
            get { return _addProductView; }
            set
            {
                _addProductView = value;
                OnPropertyChange();
            }
        }
        //For pagination
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

        private string _searchTerm;
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
        //For pagination
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

        //For the search box
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

        //Constructor
        public CatalogViewModel()
        {
            Product = new ObservableCollection<ProductModel>();
            PagedProducts = new ObservableCollection<ProductModel>();
            AddNewProduct = new RelayCommand(ExecuteAddProduct);
            EditCommand = new RelayCommand<ProductModel>(ExecuteEditCommand);
            ArchiveCommand = new RelayCommand<ProductModel>(ExecuteArchiveCommand);
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

        //Execute ArchiveCommand
        private void ExecuteArchiveCommand(ProductModel data)
        {
            _archiveProductViewModel = new ArchiveProductViewModel(data);
            AddProductView = _archiveProductViewModel;
        }

        //Execute EditCommand
        private void ExecuteEditCommand(ProductModel data)
        {
            _editProductViewModel = new EditProductViewModel(data);
            AddProductView = _editProductViewModel;
        }

        //Refreshes the products in the datagrid
        public void UpdateCatalog()
        {
            string query = "SELECT product_id, product_name, category, brand, model_number, stock_quantity, unit_cost, selling_price FROM Products WHERE is_archived = 0";
            var products = dbCon.FetchData(query);

            Product.Clear();
            foreach (var prod in products)
            {
                Product.Add(prod);
            }

            TotalItems = Product.Count;
            UpdatePagedProducts();
        }

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

        //Execute AddNewProduct command
        private void ExecuteAddProduct()
        {
            AddProductView = new AddProductViewModel();
        }
    }
}