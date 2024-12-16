using CIRCUIT.Utilities;

namespace CIRCUIT.Model
{
    public class SaleModel : PropertyChange
    {
        private decimal _totalAmount;
        private decimal _changeGiven;
        private decimal _paymentMethod;

        public int SaleId { get; set; }
        public DateTime DateTime { get; set; }
        public int CashierId { get; set; }
        public string CashierName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal CustomerPayment {  get; set; }

        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    OnPropertyChange(nameof(TotalAmount));
                }
            }
        }


        public decimal CustomerPaid { get; set; }

        public decimal ChangeGiven
        {
            get => _changeGiven;
            set
            {
                if (_changeGiven != value)
                {
                    _changeGiven = value;
                    OnPropertyChange(nameof(ChangeGiven));
                }
            }
        }

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