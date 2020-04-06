using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    public class Category
    {
        public Category(string categoryID, string categoryName)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
        }

        public Category(string categoryID, string categoryName, bool status)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
            this.Status = status;
        }

        public string CategoryID { get; set; }
        public string CategoryName { get; set; }

        public bool Status { get; set; }
    }
}
