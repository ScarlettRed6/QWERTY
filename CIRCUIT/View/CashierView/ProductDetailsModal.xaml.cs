using CIRCUIT.Model;
using CIRCUIT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CIRCUIT.View.CashierView
{
    public partial class ProductDetailsModal : Window
    {
        public ProductModel SelectedProduct { get; set; }

        // Event for notifying the parent ViewModel
        public event Action<ProductModel> ProductAddedToCart;

        public ProductDetailsModal(ProductModel product)
        {
            InitializeComponent();
            SelectedProduct = product;
            this.DataContext = SelectedProduct;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            ProductAddedToCart?.Invoke(SelectedProduct);
            this.Close();
        }
    }
}
