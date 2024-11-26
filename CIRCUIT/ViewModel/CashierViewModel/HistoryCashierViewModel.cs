using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CIRCUIT.Model.NewSaleModel;
using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.Input;

namespace CIRCUIT.ViewModel.CashierViewModel
{
    public class HistoryCashierViewModel : PropertyChange
    {
        private ObservableCollection<SaleHistoryModel> _salesHistory;
        private readonly Db _database;
        private string _staffName;

        public ObservableCollection<SaleHistoryModel> SalesHistory
        {
            get => _salesHistory;
            set
            {
                _salesHistory = value;
                OnPropertyChange(nameof(SalesHistory));
            }
        }

        public string StaffName
        {
            get => _staffName;
            set
            {
                _staffName = value;
                OnPropertyChange(nameof(StaffName));
            }
        }

        // Calculated total sales amount
        public decimal TotalSalesAmount => SalesHistory.Sum(s => s.TotalAmount);

        // Command to refresh the sales history
        public ICommand RefreshHistoryCommand { get; private set; }

        // Constructor
        public HistoryCashierViewModel()
        {
            _database = new Db(); 
            SalesHistory = new ObservableCollection<SaleHistoryModel>();
            RefreshHistoryCommand = new RelayCommand(LoadSalesHistory);

            StaffName = "Rhenz Pogi"; 
            LoadSalesHistory(); 
        }

        // Load sales history data
        private void LoadSalesHistory()
        {
            try
            {
                var allSales = _database.GetSalesHistory();

                if (allSales == null || !allSales.Any())
                {
                    Console.WriteLine("No sales data available.");
                    return;
                }

                SalesHistory.Clear();
                foreach (var sale in allSales)
                {
                    SalesHistory.Add(sale);
                }

                OnPropertyChange(nameof(TotalSalesAmount));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sales history: {ex.Message}");
            }
        }
    }
}
