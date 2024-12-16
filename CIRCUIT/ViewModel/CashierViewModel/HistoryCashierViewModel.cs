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


        public ObservableCollection<SaleHistoryModel> SalesHistory
        {
            get => _salesHistory;
            set
            {
                _salesHistory = value;
                OnPropertyChange(nameof(SalesHistory));
            }
        }




        public decimal TotalSalesAmount => SalesHistory.Sum(s => s.TotalAmount);

        // Command to refresh the sales history
        public ICommand RefreshHistoryCommand { get; private set; }

        // Constructor
        public HistoryCashierViewModel()
        {
            _database = new Db();
            SalesHistory = new ObservableCollection<SaleHistoryModel>();
            RefreshHistoryCommand = new RelayCommand(LoadSalesHistory);

            LoadSalesHistory();
        }


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

                var today = DateTime.Today;
                var todaysSales = allSales
                    .Where(s => s.DateTime.Date == today)
                    .OrderByDescending(s => s.DateTime)
                    .ToList();

                SalesHistory.Clear();
                foreach (var sale in todaysSales)
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
