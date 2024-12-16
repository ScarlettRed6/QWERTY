using System.ComponentModel;
using CIRCUIT.Utilities;
namespace CIRCUIT.Model
{
    public class CartItem : PropertyChange
    {

        // Product information
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * SellingPrice;

        public string Description { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string ImageSource { get; set; }

        private decimal _sellingPrice;
        public decimal SellingPrice
        {
            get => _sellingPrice;
            set
            {
                if (_sellingPrice != value)
                {
                    _sellingPrice = value;
                    OnPropertyChange(nameof(SellingPrice));
                    OnPropertyChange(nameof(TotalPrice)); // Update TotalPrice when SellingPrice changes
                }
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value > 0 ? value : 1; // Ensure quantity is at least 1
                    OnPropertyChange(nameof(Quantity));
                    OnPropertyChange(nameof(TotalPrice)); // Update TotalPrice when Quantity changes
                }
            }
        }

        public class OrderItem
        {
            public int OrderItemId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal DiscountPercentage { get; set; }
            public decimal TotalPrice => Quantity * Price;
        }

    }
}
