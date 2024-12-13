using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CIRCUIT.ViewModel.Bases;
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
    public class CatalogViewModel : BasePaginationViewModel
    {
        //Fields
        private object _addProductView;
        private string _searchTerm;
        private bool? isAllSelected;
        private string _filterPriceRange;
        private string _filterBrandBox;
        private string _filterCategoryBox;

        //database connection instance of an object
        private Db dbCon = new Db();

        //Collections
        public ObservableCollection<ProductModel> PagedProducts { get; set; }
        public ObservableCollection<ProductModel> Product { get; set; }
        public ObservableCollection<string> Brands { get; set; }
        public ObservableCollection<string> Categories { get; set; }
        private List<ProductModel> _items;

        //Commands
        public RelayCommand AddNewProduct { get; }
        public RelayCommand<ProductModel> EditCommand { get; }
        public RelayCommand<ProductModel> ViewCommand { get; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }
        public RelayCommand CheckSelectAll { get; private set; }
        public RelayCommand CheckSelectCell { get; private set; }
        public RelayCommand<ProductModel> TestCommand { get; }
        public RelayCommand ClearFilterFCommand { get; set; }

        //Class objects
        private EditProductViewModel _editProductViewModel;
        private ViewProductViewModel viewProductViewModel;

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

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    OnPropertyChange();
                    UpdatePagedItems();
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

        public bool? IsAllSelected
        {
            get { return isAllSelected; }
            set
            {
                isAllSelected = value;
                OnPropertyChange();
            }
        }

        public string FilterPriceRange
        {
            get => _filterPriceRange;
            set
            {
                if (_filterPriceRange != value)
                {
                    _filterPriceRange = value;
                    OnPropertyChange();
                    ClearFilterFCommand.NotifyCanExecuteChanged();
                    FilterProductsByPrice();
                }
            }
        }

        public string FilterBrandBox
        {
            get => _filterBrandBox;
            set
            {
                if (_filterBrandBox != value)
                {
                    _filterBrandBox = value;
                    OnPropertyChange();
                    ClearFilterFCommand.NotifyCanExecuteChanged();
                    UpdatePagedItems();
                }
            }
        }


        public string FilterCategoryBox // Added Category Filter property
        {
            get => _filterCategoryBox;
            set
            {
                if (_filterCategoryBox != value)
                {
                    _filterCategoryBox = value;
                    OnPropertyChange();
                    ClearFilterFCommand.NotifyCanExecuteChanged();
                    UpdatePagedItems();
                }
            }
        }

        //Constructor
        public CatalogViewModel()
        {
            Product = new ObservableCollection<ProductModel>();
            PagedProducts = new ObservableCollection<ProductModel>();
            Brands = new ObservableCollection<string>();
            Categories = new ObservableCollection<string>();
            AddNewProduct = new RelayCommand(ExecuteAddProduct);
            EditCommand = new RelayCommand<ProductModel>(ExecuteEditCommand);
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

            CheckSelectAll = new RelayCommand(ChkSelectAllCommand);
            CheckSelectCell = new RelayCommand(ChkSelectCellCommand);
            TestCommand = new RelayCommand<ProductModel>(ExecuteTestCommand, CanArchive);
            ClearFilterFCommand = new RelayCommand(ClearFilters, CanClearFilter);

            IsAllSelected = false;
            LoadBrandsAndCategories();
            UpdateCatalog();

        }

        private bool CanClearFilter()
        {
            return !(string.IsNullOrWhiteSpace(FilterCategoryBox) || FilterCategoryBox == "Category")
                    || string.IsNullOrWhiteSpace(FilterBrandBox) || FilterBrandBox == "Brand"
                    || string.IsNullOrWhiteSpace(FilterPriceRange) || FilterPriceRange == "Price Range";
        }

        private void ClearFilters()
        {
            FilterCategoryBox = "Category";
            FilterBrandBox = "Brand";
            FilterPriceRange = "Price Range";
            UpdatePagedItems();
        }

        private void LoadBrandsAndCategories()
        {
            Brands.Clear();
            Brands.Add("Brand"); // Placeholder option
            foreach (var brand in dbCon.GetBrands())
            {
                Brands.Add(brand);
            }
            FilterBrandBox = "Brand";

            Categories.Clear();
            Categories.Add("Category"); // Placeholder option
            foreach (var category in dbCon.GetCategories())
            {
                Categories.Add(category);
            }
            FilterCategoryBox = "Category";
        }

        //Used for archiving button , enables the button if there is a row selected.
        private bool CanArchive(ProductModel? obj)
        {
            return Items.Any(x => x.IsSelected);
        }
        //Testing purposes, might change to an officiall button for archiving products later
        private void ExecuteTestCommand(ProductModel? model)
        {
            var result = MessageBox.Show("Do you want to proceed with the archive?", "Proceed to archive", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            var selectedItems = Items.Where(x => x.IsSelected).ToList();
            var productId = selectedItems.Select(p => p.ProductId).ToList();
            dbCon.ArchiveProduct(productId);

            UpdateCatalog();

        }

        //Checks if all items are selected
        private void ChkSelectAllCommand()
        {
            if (IsAllSelected == true)
            {
                Items.ForEach(x => x.IsSelected = true);
            }
            else if (IsAllSelected == false)
            {
                Items.ForEach(x => x.IsSelected = false);
            }

            //NotifyCanExecuteChanged to notify property changes for test button to enable or disable
            TestCommand.NotifyCanExecuteChanged();
        }

        //Checks the selected cells
        private void ChkSelectCellCommand()
        {
            if (Items.All(x => x.IsSelected))
            {
                IsAllSelected = true;
            }
            else if (Items.All(x => !x.IsSelected))
            {
                IsAllSelected = false;
            }
            else
            {
                IsAllSelected = null; // Indicates an indeterminate state, meaning not all are selected
            }

            //NotifyCanExecuteChanged to notify property changes for test button to enable or disable
            TestCommand.NotifyCanExecuteChanged();

        }

        //Execute ArchiveCommand
        private void ExecuteViewCommand(ProductModel data)
        {
            viewProductViewModel = new ViewProductViewModel(data, 1);
            AddProductView = viewProductViewModel;
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
            string query = "SELECT product_id, product_name, description, category, brand, model_number, stock_quantity, unit_cost, selling_price, image_path FROM tbl_Products WHERE is_archived = 0";
            var products = dbCon.FetchData(query);

            Product.Clear();
            Items = new List<ProductModel>(); // Clear and repopulate Items as well
            foreach (var prod in products)
            {
                Product.Add(prod);
                Items.Add(prod); // Populate Items for "Select All" logic
            }

            TotalItems = Product.Count;
            OnPropertyChange(nameof(TotalPages));
            UpdatePagedItems();
        }

        //Updates data per page, overrides the UpdatePagedProducts from BasePaginationViewModel
        protected override void UpdatePagedItems()
        {
            IEnumerable<ProductModel> filteredProducts = Product;

            // Apply filtering if a search term is provided
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filteredProducts = filteredProducts.Where(p =>
                    p.ProductName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(FilterBrandBox) && FilterBrandBox != "Brand")
            {
                filteredProducts = filteredProducts.Where(p => p.Brand.Equals(FilterBrandBox, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(FilterCategoryBox) && FilterCategoryBox != "Category")
            {
                filteredProducts = filteredProducts.Where(p => p.Category.Equals(FilterCategoryBox, StringComparison.OrdinalIgnoreCase));
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

        // method to filter products by price range,, StringFormat=₱{0:N2}
        private void FilterProductsByPrice()
        {
            if (string.IsNullOrEmpty(FilterPriceRange) || FilterPriceRange == "Price Range")
            {
                UpdatePagedItems();
                return;
            }

            var ranges = new Dictionary<string, (decimal Min, decimal Max)>
            {
                { "Below ₱1000", (0, 1000) },
                { "₱1000 - ₱5000", (1000, 5000) },
                { "₱5001 - ₱10000", (5001, 10000) },
                { "Above ₱10000", (10000, decimal.MaxValue) }
            };

            if (ranges.TryGetValue(FilterPriceRange, out var range))
            {
                var filteredProducts = Product
                    .Where(p => p.SellingPrice >= range.Min && p.SellingPrice < range.Max)
                    .ToList();

                // Update the paginated products
                TotalItems = filteredProducts.Count;
                PagedProducts = new ObservableCollection<ProductModel>(
                    filteredProducts.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
                );

                OnPropertyChange(nameof(PagedProducts));
            }
        }

        //Execute AddNewProduct command
        private void ExecuteAddProduct()
        {
            AddProductView = new AddProductViewModel();
        }

        //Implement later memory management features
        public void Dispose()
        {

        }
    }
}