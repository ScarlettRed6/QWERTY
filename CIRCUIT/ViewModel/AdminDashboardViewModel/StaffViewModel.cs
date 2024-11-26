using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class StaffViewModel : ObservableObject, IDisposable 
    {
        //Fields
        private int _currentPage = 1;
        private int _itemsPerPage = 10; // Adjust as needed
        private string _searchTerm;

        //database connection instance of an object
        Db dbCon = new Db();
        AccountRepository acRCon = new AccountRepository();

        //Calculates the total pages dynamically
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);

        //Collections
        public ObservableCollection<UsersModel> PagedUsers { get; set; }
        public ObservableCollection<UsersModel> Users { get; set; }

        //Commands
        public RelayCommand AddNewUser { get; }
        public RelayCommand<UsersModel> EditCommand { get; }
        public RelayCommand<UsersModel> ViewCommand { get; }
        public RelayCommand<UsersModel> DeleteCommand { get; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }
        public RelayCommand CheckSelectAll { get; private set; }
        public RelayCommand CheckSelectCell { get; private set; }

        //Class objects
        EditAccountViewModel _editAccountViewModel;
        ViewAccountViewModel _viewAccountViewModel;

        //Properties
        //For switching to addproduct view
        [ObservableProperty]
        private object _currentView;

        //For total items in the dataset Product collection
        [ObservableProperty]
        private int _totalItems;

        //For listing selectedall
        [ObservableProperty]
        private List<UsersModel> _items;

        //Checks if checkbox for select all feature is true
        [ObservableProperty]
        private bool? _isAllSelected;

        [ObservableProperty]
        private int _totalUsers;

        //For pagination
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value && value > 0 && value <= TotalPages)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                    UpdatePagedUsers();
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
                OnPropertyChanged();
                UpdatePagedUsers();
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
                    OnPropertyChanged();
                    UpdatePagedUsers();
                }
            }
        }

        //Constructor
        public StaffViewModel()
        {
            Users = new ObservableCollection<UsersModel>();
            PagedUsers = new ObservableCollection<UsersModel>();
            AddNewUser = new RelayCommand(ExecuteAddUser);
            EditCommand = new RelayCommand<UsersModel>(ExecuteEditCommand);
            ViewCommand = new RelayCommand<UsersModel>(ExecuteViewCommand);
            DeleteCommand = new RelayCommand<UsersModel>(ExecuteDeleteCommand);
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

            IsAllSelected = false;

            UpdateList();

        }

        private void ExecuteDeleteCommand(UsersModel? model)
        {
            var result = MessageBox.Show($"Are you sure you want to delete user {model.Username}?","Delete account",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes)
                return;

            acRCon.DeleteUserAccount(model.UserId);
            TotalUsers = 0;
            UpdateList();

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

        }

        //Execute Viewcommand
        private void ExecuteViewCommand(UsersModel data)
        {
            _viewAccountViewModel = new ViewAccountViewModel(data, this);
            CurrentView = _viewAccountViewModel;
        }

        //Execute EditCommand
        private void ExecuteEditCommand(UsersModel data)
        {
            _editAccountViewModel = new EditAccountViewModel(data);
            CurrentView = _editAccountViewModel;
        }

        //Refreshes the products in the datagrid
        public void UpdateList()
        {
            //string query = "SELECT product_id, product_name, category, brand, model_number, stock_quantity, unit_cost, selling_price FROM Products WHERE is_archived = 0";
            var users = acRCon.FetchUser();

            Users.Clear();
            Items = new List<UsersModel>(); // Clear and repopulate Items as well
            foreach (var user in users)
            {
                Users.Add(user);
                Items.Add(user); // Populate Items for "Select All" logic
                TotalUsers++;
            }

            TotalItems = Users.Count;
            UpdatePagedUsers();
        }

        //Updates data per page
        private void UpdatePagedUsers()
        {
            IEnumerable<UsersModel> filteredProducts = Users;

            // Apply filtering if a search term is provided
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filteredProducts = filteredProducts.Where(p =>
                    p.Username.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Update TotalItems dynamically
            TotalItems = filteredProducts.Count();

            // Paginate the results
            PagedUsers = new ObservableCollection<UsersModel>(
                filteredProducts.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            OnPropertyChanged(nameof(PagedUsers));
        }

        //Search and filters data by Product name, filter can be modified later
        public void SearchProducts(string searchTerm)
        {
            // Filter the product list based on the search term
            var filteredUsers = Users
                .Where(p => p.Username.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Update TotalItems to reflect filtered results
            TotalItems = filteredUsers.Count;

            // Paginate the filtered results
            PagedUsers = new ObservableCollection<UsersModel>(
                filteredUsers.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            // Notify UI about changes
            OnPropertyChanged(nameof(PagedUsers));
        }

        //Execute Add new user view command
        private void ExecuteAddUser()
        {
            CurrentView = new AddUserViewModel();
        }

        public void Dispose()
        {
            
        }
    }
}
