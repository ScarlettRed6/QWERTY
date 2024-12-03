using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CIRCUIT.Model.DataRepositories;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace CIRCUIT.ViewModel.CashierViewModel
{
    public class VoidOrderViewModel : ObservableObject
    {
        private readonly Db _database;
        private readonly AccountRepository accountRepository;
        private ObservableCollection<SaleHistoryModel> _voidOrders;
        private SaleHistoryModel _selectedOrder;
        private string _orderIdToVoid;
        private string _voidReason;

        public VoidOrderViewModel()
        {
            _database = new Db();
            accountRepository = new AccountRepository();
            LoadVoidOrders();

            // Initialize command
            VoidOrderCommand = new RelayCommand(VoidOrder);
        }

        public ObservableCollection<SaleHistoryModel> VoidOrders
        {
            get => _voidOrders;
            set
            {
                _voidOrders = value;
                OnPropertyChanged(nameof(VoidOrders));
            }
        }

        public SaleHistoryModel SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));

                // Update OrderIdToVoid when a new order is selected
                OrderIdToVoid = _selectedOrder?.SaleId.ToString();
            }
        }

        public string OrderIdToVoid
        {
            get => _orderIdToVoid;
            set
            {
                _orderIdToVoid = value;
                OnPropertyChanged(nameof(OrderIdToVoid));
            }
        }

        public string VoidReason
        {
            get => _voidReason;
            set
            {
                _voidReason = value;
                OnPropertyChanged(nameof(VoidReason));
            }
        }

        public ICommand VoidOrderCommand { get; }

        private void LoadVoidOrders()
        {
            try
            {
                var users = accountRepository.FetchUser();
                var today = DateTime.Today;

                var voidOrders = _database.GetSalesHistory()
                                          .Where(s => !s.IsVoid && s.DateTime.Date == today) // Filter only today's orders
                                          .OrderByDescending(s => s.DateTime) // Sort by DateTime descending
                                          .ToList();

                foreach (var order in voidOrders)
                {
                    var matchingUser = users.FirstOrDefault(u => u.UserId == order.UserId);
                    if (matchingUser != null)
                    {
                        order.CashierName = matchingUser.Username;
                        order.CashierRole = matchingUser.Role;
                    }
                }

                // Bind filtered and sorted data to VoidOrders
                VoidOrders = new ObservableCollection<SaleHistoryModel>(voidOrders);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading today's non-voided orders: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void VoidOrder()
        {
            if (SelectedOrder == null || string.IsNullOrWhiteSpace(VoidReason))
            {
                MessageBox.Show("Please select an order and provide a void reason.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Update the database with the VoidReason and set IsVoid to true
                var success = _database.UpdateVoidOrder(SelectedOrder.SaleId, VoidReason);

                if (success)
                {
                    // Mark the order as void locally and reset the UI
                    SelectedOrder.IsVoid = true;
                    SelectedOrder.VoidReason = VoidReason;
                    LoadVoidOrders();  // Reload orders to reflect the changes
                    MessageBox.Show("Order has been voided successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to void the order.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error voiding order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
