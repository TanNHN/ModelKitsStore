using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    public class Order
    {
        public Order(int orderID, int accountID, DateTime orderDate, double total)
        {
            OrderID = orderID;
            AccountID = accountID;
            OrderDate = orderDate;
            Total = total;
        }

        public int OrderID { get; set; }
        public int AccountID { set; get; }
        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
    }
}
