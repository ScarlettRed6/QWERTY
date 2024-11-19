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
    /// <summary>
    /// Interaction logic for ProductDetailsModal.xaml
    /// </summary>
    public partial class ProductDetailsModal : Window
    {
        public ProductDetailsModal()
        {
            InitializeComponent();
            this.DataContext = new NewSaleViewModel();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




    }
}
