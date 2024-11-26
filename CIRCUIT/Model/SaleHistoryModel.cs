using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CIRCUIT.Model
{
    public class SaleHistoryModel
    {
        public int SaleId { get; set; }
        public DateTime DateTime { get; set; }
        public int CashierId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public decimal CustomerPaid { get; set; }
        public decimal ChangeGiven { get; set; }
        public decimal _staffName { get; set;}
    }

}