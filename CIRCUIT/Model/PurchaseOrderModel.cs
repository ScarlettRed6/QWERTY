namespace CIRCUIT.Model
{
    public class PurchaseOrderModel
    {
        public int OrderID { get; set; }
        public int SupplierID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending"; // Default value
        public decimal TotalAmount { get; set; }
        public decimal ShippingFee { get; set; }
        public string SupplierName { get; set; }

        // Navigation property for related Supplier
        public virtual SuppliersModel Supplier { get; set; }

        // Navigation property for related PurchaseOrderDetails
        public virtual ICollection<PurchaseOrderDetailModel> PurchaseOrderDetails { get; set; }

    }

    public class PurchaseOrderDetailModel
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }

        // Navigation properties
        public virtual PurchaseOrderModel PurchaseOrder { get; set; }
        public virtual Product Product { get; set; }
    }

}
