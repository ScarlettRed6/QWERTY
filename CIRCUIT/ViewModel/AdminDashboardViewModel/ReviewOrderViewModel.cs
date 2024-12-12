using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CIRCUIT.View.AdminDashboardViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class ReviewOrderViewModel : ObservableObject
    {
        //Fields and Properties
        private StockControlRepository _sControlRepo;

        [ObservableProperty]
        private string _supplierSelected;

        [ObservableProperty]
        private int _supplierID;

        [ObservableProperty]
        private decimal _subTotal;

        [ObservableProperty]
        private decimal _shippingFee;

        [ObservableProperty]
        private decimal _totalAmount;

        private ObservableCollection<SuppliersModel> _suppliersDetails;

        private ObservableCollection<ProductModel> _filteredProducts;

        public ObservableCollection<ProductModel> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
            }
        }

        //Commands
        public ICommand ConfirmOrderCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        //Constructor
        public ReviewOrderViewModel()
        {
            _sControlRepo = new StockControlRepository();
            _suppliersDetails = new ObservableCollection<SuppliersModel>();
            FilteredProducts = new ObservableCollection<ProductModel>();
            
        }

        public void ConfirmOrder(Window window)
        {
            LoadSupplierDetails(); // Ensure supplier details are loaded

            if (_suppliersDetails.Count > 0)
            {
                var supplierId = _suppliersDetails[0].SupplierID; // Get the SupplierID

                // Insert the main order and retrieve the OrderID
                int orderId = _sControlRepo.InsertOrder(supplierId, TotalAmount, ShippingFee);

                GenerateSupplierReceipt(orderId);

                // Insert the product details
                _sControlRepo.InsertOrderDetails(orderId, FilteredProducts);

                MessageBox.Show("Order has been successfully placed along with its details!",
                                "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                window?.Close();
            }
            else
            {
                MessageBox.Show("Supplier details not found. Please check your selection.",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateSupplierReceipt(int orderId)
        {
            // Prompt user to save the receipt
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"Supplier_Receipt_{orderId}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string exportPath = saveFileDialog.FileName;

                // Generate the receipt PDF
                PdfGenerator.PrintSupplierReceipt(
                    _suppliersDetails[0], // Assuming the first supplier is selected
                    orderId,
                    FilteredProducts.ToList(),
                    SubTotal,
                    ShippingFee,
                    TotalAmount,
                    exportPath
                );

                MessageBox.Show("Receipt generated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Receipt generation canceled by user.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Load supplier data
        private void LoadSupplierDetails()
        {
            string query = "SELECT * FROM tbl_suppliers WHERE SupplierName = @SupplierName";
            var supplierDetails = _sControlRepo.FetchSuppliers(query, SupplierSelected);

            _suppliersDetails.Clear();
            foreach (var supplier in supplierDetails)
            {
                _suppliersDetails.Add(supplier);
            }
            SupplierID = _suppliersDetails[0].SupplierID;

        }

        public void CancelOrder(Window window)
        {
            window?.Close();
        }

        public void LoadTotalAmount()
        {
            int totalItems = 0;
            foreach (var product in FilteredProducts)
            {
                SubTotal += product.TotalCost;
                totalItems += product.OrderQuantity;
            }

            ShippingFee = totalItems * 120;
            TotalAmount = SubTotal + ShippingFee;
        }

    }
}
