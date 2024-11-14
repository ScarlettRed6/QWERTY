using CIRCUIT.Model;
using CIRCUIT.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class CatalogViewModel : PropertyChange
    {
        //Fields

        //database connection instance of an object
        private Db dbCon = new Db();
        public ObservableCollection<ProductModel> Product { get; set; }

        //Commands
        public ICommand AddNewProduct { get; }

        private object _addProductView;

        public object AddProductView
        {
            get { return _addProductView; }
            set 
            { 
                _addProductView = value;
                OnPropertyChange();
            }
        }

        public CatalogViewModel()
        {
            string query = "SELECT product_id, product_name, category, brand, model_number, stock_quantity, unit_cost, selling_price FROM products";
            Product = new ObservableCollection<ProductModel>(dbCon.FetchData(query));

            AddNewProduct = new CommandRelay(ExecuteAddProduct);

        }

        private void ExecuteAddProduct(object obj)
        {
            AddProductView = new AddProductViewModel();
        }
    }
}
