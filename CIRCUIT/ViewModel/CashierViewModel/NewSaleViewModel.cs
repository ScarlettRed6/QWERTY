


using CIRCUIT.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CIRCUIT.ViewModel
{
    public class NewSaleViewModel : PropertyChange
    {
        private string _staffName;
        private string _productName;
        private int _quantity;
        private decimal _price;
        private decimal _totalAmount;
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChange(nameof(Products));
            }
        }

        //method
        public NewSaleViewModel()
        {
            StaffName = "Rhenz pogi"; // Default staff name
            Products = new ObservableCollection<Product>
            {
                //tezsting
            new Product { ProductName = "Rhenz", Price = 100m, ImageSource = "Assets/Images/Rhenzpic.jpg" },
            new Product { ProductName = "Jamero", Price = 200m, ImageSource = "path_to_image2.jpg" },
            new Product { ProductName = "Magno", Price = 300m, ImageSource = "path_to_image3.jpg" },
            new Product { ProductName = "Clarence", Price = 300m, ImageSource = "path_to_image3.jpg" },
            new Product { ProductName = "Rhenz", Price = 100m, ImageSource = "Assets/Images/Rhenzpic.jpg" },
            new Product { ProductName = "Rhenz", Price = 100m, ImageSource = "Assets/Images/Rhenzpic.jpg" },
            new Product { ProductName = "Jamero", Price = 200m, ImageSource = "path_to_image2.jpg" },
            new Product { ProductName = "Magno", Price = 300m, ImageSource = "path_to_image3.jpg" },
            new Product { ProductName = "Clarence", Price = 300m, ImageSource = "path_to_image3.jpg" },
            new Product { ProductName = "Rhenz", Price = 100m, ImageSource = "Assets/Images/Rhenzpic.jpg" },
            new Product { ProductName = "Clarence", Price = 300m, ImageSource = "path_to_image3.jpg" }
            };
        }


        public string StaffName
        {
            get => _staffName;
            set
            {
                _staffName = value;
                OnPropertyChange();
            }
        }

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChange();
                CalculateTotal();
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChange();
                CalculateTotal();
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChange();
                CalculateTotal();
            }
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChange();
            }
        }


        private void CalculateTotal()
        {
            TotalAmount = Price * Quantity;
        }
    }
}
