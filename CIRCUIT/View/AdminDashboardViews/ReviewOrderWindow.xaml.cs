using CIRCUIT.Model;
using CIRCUIT.ViewModel.AdminDashboardViewModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace CIRCUIT.View.AdminDashboardViews
{
  
    public partial class ReviewOrderWindow : Window
    {
        public ReviewOrderWindow(ObservableCollection<ProductModel> filteredProducts, string supplier)
        {
            InitializeComponent();

            var viewModel = new ReviewOrderViewModel
            {
                FilteredProducts = filteredProducts,
                SupplierSelected = supplier
            };

            viewModel.LoadTotalAmount();
            viewModel.ConfirmOrderCommand = new RelayCommand(() => viewModel.ConfirmOrder(this));
            viewModel.CancelCommand = new RelayCommand(() => viewModel.CancelOrder(this));
            this.DataContext = viewModel;
        }
    }
}
