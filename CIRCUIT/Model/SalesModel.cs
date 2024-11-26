using CIRCUIT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIRCUIT.Model
{
    public class SaleModel : PropertyChange
    {
        private decimal _totalAmount;
        private decimal _changeGiven;

        public int SaleId { get; set; }
        public DateTime DateTime { get; set; }
        public int CashierId { get; set; }

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

        public string PaymentMethod { get; set; }
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


    }
}