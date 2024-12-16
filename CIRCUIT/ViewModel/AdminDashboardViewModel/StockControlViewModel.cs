using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class StockControlViewModel : ObservableObject
    {
        //Fields
        [ObservableProperty]
        private object _currentView;

        //Commands
        public RelayCommand ShowSuppliersCommand { get; set; }
        public RelayCommand ShowOrdersCommand { get; set; }

        public StockControlViewModel()
        {
            ShowSuppliersCommand = new RelayCommand(ExecuteShowSuppliers);
            ShowOrdersCommand = new RelayCommand(ExecuteShowOrders);
            ExecuteShowOrders();
        }

        private void ExecuteShowOrders()
        {
            CurrentView = new StockOrdersViewModel();
        }

        private void ExecuteShowSuppliers()
        {
            CurrentView = new StockSuppliersViewModel();
        }
    }
}
