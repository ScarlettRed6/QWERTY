using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIRCUIT.Model
{
    public class SaleItemModel
    {
        public int SaleItemId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
        public decimal ItemTotalPrice { get; set; }
        public int RefundQuantity { get; set; }
        public bool RefundSelected { get; set; }
        public int totalRefundAmount { get; set; }

    }

}
