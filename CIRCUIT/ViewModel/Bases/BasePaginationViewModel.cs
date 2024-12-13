using CIRCUIT.Utilities;

namespace CIRCUIT.ViewModel.Bases
{
    public class BasePaginationViewModel : PropertyChange
    {
        private int _currentPage = 1;
        private int _itemsPerPage = 5; // Default items per page
        private int _totalItems;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value && value > 0 && value <= TotalPages)
                {
                    _currentPage = value;
                    OnPropertyChange();
                    UpdatePagedItems();
                }
            }
        }

        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set
            {
                if (_itemsPerPage != value)
                {
                    _itemsPerPage = value;
                    OnPropertyChange();
                    UpdatePagedItems();
                }
            }
        }

        public int TotalItems
        {
            get => _totalItems;
            set
            {
                if (_totalItems != value)
                {
                    _totalItems = value;
                    OnPropertyChange();
                }
            }
        }

        //Calculates the total pages dynamically
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);

        //Override this method for implementation
        protected virtual void UpdatePagedItems()
        {
            // Basic pagination logic (can be overridden)
        }

    }
}
