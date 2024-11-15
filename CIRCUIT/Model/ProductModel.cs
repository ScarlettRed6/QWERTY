﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIRCUIT.Model
{
    public class ProductModel
    {
        public int ProductId {  get; set; }
        public string ProductName { get; set; }
        public string Category {  get; set; }
        public string Brand { get; set; }
        public string ModelNumber { get; set; }
        public int StockQuantity { get; set; }
        public double UnitCost { get; set; }

        //price
        public double SellingPrice { get; set; }
        public int MinStockLevel { get; set; }
        public bool IsArchived { get; set; }


        // Renamed to follow C# naming conventions
        public string _imageSource { get; set; }
        public string ProductDescription { get; set; }

    }
}
