using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    public class Product
    {
        public Product(string categoryID, string productID, string productName, double price)
        {
            CategoryID = categoryID;
            ProductID = productID;
            ProductName = productName;
            Price = price;
        }

        public Product(string productID, string productName, double price, int quantity)
        {
            ProductID = productID;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public Product(string categoryID, string productID, string productName, double price,  int quantity)
        {
            CategoryID = categoryID;
            ProductID = productID;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public string CategoryID { get; set; }

        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }

        public int Quantity { get; set; }
        public bool status { get; set; }

        
    }
}
