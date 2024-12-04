using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CIRCUIT.Model
{
    public class ProductModel : ObservableObject
    {
        public int ProductId {  get; set; }   
        public string ProductName { get; set; }
        public string Category {  get; set; }
        public string Brand { get; set; }
        public string ModelNumber { get; set; }
        public int SKU { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal UnitCost { get; set; }
        public string ImagePath { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set 
            { 
                _isSelected = value;
                OnPropertyChanged();
            }

        }

        private int _orderQuantity;
        public int OrderQuantity
        {
            get => _orderQuantity;
            set
            {
                SetProperty(ref _orderQuantity, value);
                // Update TotalCost whenever OrderQuantity changes
                TotalCost = UnitCost * _orderQuantity;
            }
        }

        private decimal _totalCost;
        public decimal TotalCost
        {
            get => _totalCost;
            private set => SetProperty(ref _totalCost, value);
        }


        //price
        public decimal SellingPrice { get; set; }
        public int MinStockLevel { get; set; }
        public bool IsArchived { get; set; }


        // Renamed to follow C# naming conventions
        public string ImageSource { get; set; }
        public string ProductDescription { get; set; }


    }

    //For charts
    public class DailyRevenue
    {
        public DateTime SaleDate { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
