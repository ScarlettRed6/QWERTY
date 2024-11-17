using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public RelayCommand AddNewProduct { get; }
        public RelayCommand<ProductModel> EditCommand { get; }

        private EditProductViewModel _editProductViewModel;

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
            string query = "SELECT product_id, product_name, category, brand, model_number, stock_quantity, unit_cost, selling_price FROM Products";
            Product = new ObservableCollection<ProductModel>(dbCon.FetchData(query));
            AddNewProduct = new RelayCommand(ExecuteAddProduct);
            EditCommand = new RelayCommand<ProductModel>(ExecuteEditCommand);
            UpdateCatalog(query);

        }

        private void ExecuteEditCommand(ProductModel data)
        {
            _editProductViewModel = new EditProductViewModel(data);
            AddProductView = _editProductViewModel; 
        }

        public void UpdateCatalog(string query)
        {
            Product.Clear();
            foreach (var prod in dbCon.FetchData(query))
            {
                Product.Add(prod);
            }
        }

        private void ExecuteAddProduct()
        {
            AddProductView = new AddProductViewModel();
        }
    }
}
