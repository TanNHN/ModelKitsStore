using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    class OrderDetail
    {
        public OrderDetail(int orderID, string productID, int quantity)
        {
            OrderID = orderID;
            ProductID = productID;
            Quantity = quantity;
        }

        public int OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
