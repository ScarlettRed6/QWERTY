//ALL TRANSACTION HERE ANG KUKUHA

using CIRCUIT.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace CIRCUIT.ViewModel
{
    public class NewSaleViewModel : PropertyChange
    {
        //VARIABLES HERE
        //++++
        //GET INFORMATION
        private string _staffName;
        private string _Description;
        private string _productName;
        private int _quantity;
        private decimal _price;
        private decimal _totalAmount;


        //GET TIME DATE 
        private string _currentDate;
        private string _currentTime;


        //GET SEARCH TO PRODUCT MODEL
        private string _searchQuery;
        private ObservableCollection<Product> _filteredProducts;


        //COLLECTIONS HERE/ GETTTING DATABASE HERE
        private ObservableCollection<Product> _products;




        //MAIN EME EME METHOD CALL THIS SHIT HAHAHA
        public NewSaleViewModel()
        {
            StaffName = "Rhenz pogi"; // Default staff name MUNA WALA PANG DB
            Products = new ObservableCollection<Product>
            {
                new Product { ProductName = "Rhenz", Price = 100m, ImageSource = "Assets/Images/Rhenzpic.jpg", Description = "Pinakamasarap sa apat na miyembro." },
                new Product { ProductName = "Jamero", Price = 200m, ImageSource = "path_to_image2.jpg", Description = "Exclusive Jamero item." },
                new Product { ProductName = "Magno", Price = 300m, ImageSource = "path_to_image3.jpg", Description = "Premium Magno gear." },
                new Product { ProductName = "Rhenz", Price = 100m, ImageSource = "Assets/Images/Rhenzpic.jpg", Description = "Pinakamasarap sa apat na miyembro." },
                new Product { ProductName = "Jamero", Price = 200m, ImageSource = "path_to_image2.jpg", Description = "Exclusive Jamero item." },
                new Product { ProductName = "Magno", Price = 300m, ImageSource = "path_to_image3.jpg", Description = "Premium Magno gear." },
                new Product { ProductName = "Clarence", Price = 300m, ImageSource = "path_to_image3.jpg", Description = "Top-tier Clarence merchandise." },
                new Product { ProductName = "Clarence", Price = 300m, ImageSource = "path_to_image3.jpg", Description = "Top-tier Clarence merchandise." },
            };



            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (sender, args) =>
            {
                CurrentDate = DateTime.Now.ToString("MM/dd/yyyy");
                CurrentTime = DateTime.Now.ToString("hh:mm tt");
            };
            timer.Start();

            CurrentDate = DateTime.Now.ToString("MM/dd/yyyy");
            CurrentTime = DateTime.Now.ToString("hh:mm tt");


            FilteredProducts = new ObservableCollection<Product>(Products);
        }



        // date

        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChange(nameof(CurrentDate));
            }
        }

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChange(nameof(CurrentTime));
            }
        }
        //ends here

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChange(nameof(Products));
            }
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

        public string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                OnPropertyChange(nameof(Description));
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

        // Property for search partial Matchmaking
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChange(nameof(SearchQuery));
                FilterProducts();
            }
        }

        // Filtered collections
        public ObservableCollection<Product> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChange(nameof(FilteredProducts));
            }
        }


        // Filter logic
        private void FilterProducts()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                // Show all products if search query is empty
                FilteredProducts = new ObservableCollection<Product>(Products);
            }
            else
            {
                var lowerCaseQuery = SearchQuery.ToLower();
                var filtered = Products
                    .Where(p => p.ProductName.ToLower().Contains(lowerCaseQuery))
                    .ToList();

                FilteredProducts = new ObservableCollection<Product>(filtered);
            }
        }
    }
}
