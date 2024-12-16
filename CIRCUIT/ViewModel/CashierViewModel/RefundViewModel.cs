using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CIRCUIT.ViewModel.CashierViewModel
{
    public class RefundViewModel : PropertyChange
    {
        private List<SaleHistoryModel> _salesHistory;

        public List<SaleHistoryModel> SalesHistory
        {
            get { return _salesHistory; }
            set
            {
                _salesHistory = value;
                OnPropertyChange(nameof(SalesHistory));
            }
        }

        private string _selectedRefundReason;
        public string SelectedRefundReason
        {
            get => _selectedRefundReason;
            set
            {
                _selectedRefundReason = value;
                OnPropertyChange(nameof(SelectedRefundReason));
            }
        }







        SalesRepository saleConn = new SalesRepository();

        public ICommand ProcessRefundCommand { get; set; }

        private SaleHistoryModel _selectedSale;

        public SaleHistoryModel SelectedSale
        {
            get { return _selectedSale; }
            set
            {
                _selectedSale = value;
                OnPropertyChange(nameof(SelectedSale));

                PurchasedItems = _selectedSale?.SaleItems ?? new List<SaleItemModel>();
            }
        }

        private List<SaleItemModel> _purchasedItems;
        public List<SaleItemModel> PurchasedItems
        {
            get { return _purchasedItems; }
            set
            {
                _purchasedItems = value;
                OnPropertyChange(nameof(PurchasedItems));
            }
        }

        public RefundViewModel()
        {
            SalesHistory = saleConn.GetSalesHistoryWithItems()
                              .Where(sale => sale.DateTime.Date == DateTime.Today && !sale.IsVoid)
                              .ToList();


            ProcessRefundCommand = new RelayCommand(ProcessRefund);
        }


        private void ProcessRefund()
        {
            if (SelectedSale == null)
            {
                MessageBox.Show("Please select a sale to refund.");
                return;
            }

            if (SelectedSale.IsVoid)
            {
                MessageBox.Show("This sale is voided and cannot be refunded.");
                return;
            }

            var itemsToRefund = SelectedSale.SaleItems
                .Where(item => item.RefundSelected && item.RefundQuantity > 0)
                .ToList();

            if (!itemsToRefund.Any())
            {
                MessageBox.Show("Please select at least one item and specify a valid refund quantity.");
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedRefundReason))
            {
                MessageBox.Show("Please select a reason for the refund.");
                return;
            }

            try
            {
                decimal totalRefundAmount = 0;

                foreach (var saleItem in itemsToRefund)
                {
                    if (saleItem.RefundQuantity > saleItem.Quantity)
                    {
                        MessageBox.Show($"Refund quantity for {saleItem.ProductName} exceeds the purchased quantity.");
                        return;
                    }

                    //saleConn.MarkItemAsRefunded(saleItem.SaleItemId, saleItem.RefundQuantity);

                    if (SelectedRefundReason == "Customer Change Item")
                    {
                        //saleConn.RestockItem(saleItem.ProductId, saleItem.RefundQuantity);
                    }

                    totalRefundAmount += saleItem.RefundQuantity * saleItem.UnitPrice;

                    saleItem.Quantity -= saleItem.RefundQuantity;
                }


                SelectedSale.SaleItems = SelectedSale.SaleItems
                    .Where(item => item.Quantity > 0)
                    .ToList();

                //saleConn.MarkSaleAsRefunded(SelectedSale.SaleId, SelectedRefundReason);

                PurchasedItems = new List<SaleItemModel>(SelectedSale.SaleItems);

                MessageBox.Show($"Refund processed successfully for Sale ID: {SelectedSale.SaleId}.\nReason: {SelectedRefundReason}\nTotal Refund: {totalRefundAmount:C}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing refund: {ex.Message}");
            }
        }





    }
}
