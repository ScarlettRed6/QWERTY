namespace CIRCUIT.Model
{
    public class SuppliersModel
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ContactName { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Navigation property
        public virtual ICollection<PurchaseOrderModel> PurchaseOrders { get; set; }
    }
}
