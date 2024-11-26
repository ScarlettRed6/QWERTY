using CIRCUIT.Utilities;

namespace CIRCUIT.Model
{
    public class SalesModel : PropertyChange
    {
        //Sales details
        public int SaleId { get; set; }
        public DateTime DateTime { get; set; }
        public int CashierId { get; set; }
        public string CashierName { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public decimal CustomerPayment {  get; set; }
        public decimal ChangeGiven { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChange();
            }

        }

    }

    public class SalesItemModel
    {
        //Sales items details
        public int SaleItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotalPrice { get; set; }
    }
}
