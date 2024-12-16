using CIRCUIT.Model;
using CIRCUIT.ViewModel.AdminDashboardViewModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

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


        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void ControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void ControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
