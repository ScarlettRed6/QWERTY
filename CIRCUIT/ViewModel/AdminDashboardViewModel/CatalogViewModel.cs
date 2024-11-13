using CIRCUIT.Model;
using CIRCUIT.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class CatalogViewModel : PropertyChange
    {
        private Db dbCon = new Db();
        public ObservableCollection<ProductModel> Product { get; set; }

        public CatalogViewModel()
        {
            string query = "SELECT product_id, product_name, category, brand, model_number, stock_quantity, unit_cost, selling_price FROM products";

            Product = new ObservableCollection<ProductModel>(dbCon.FetchData(query));

        }

        

    }
}
