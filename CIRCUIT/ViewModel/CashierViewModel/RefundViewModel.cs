using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Db _db;

        public ICommand ProcessRefundCommand { get; set; }

        private SaleHistoryModel _selectedSale;
        public SaleHistoryModel SelectedSale
        {
            get { return _selectedSale; }
            set
            {
                _selectedSale = value;
                OnPropertyChange(nameof(SelectedSale));
            }
        }
        public RefundViewModel()
        {
            _db = new Db();
            SalesHistory = _db.GetSalesHistory();
            ProcessRefundCommand = new RelayCommand(ProcessRefund);
        }

        private void ProcessRefund()
        {
            
        }

    }
}
