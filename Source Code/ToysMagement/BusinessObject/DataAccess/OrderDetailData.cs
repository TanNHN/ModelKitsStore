using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DataAccess
{
    public class OrderDetailData
    {
       public bool InsertProductToOrder(int OrderID, string ProID, int Quantity)
        
        
        {
            string SQL =
              "INSERT INTO OrderDetail values(@OrderID,@ProID,@Quantity)";
            SqlParameter id = new SqlParameter("@OrderID", OrderID);
            SqlParameter proID = new SqlParameter("@ProID", ProID);
            SqlParameter quantity = new SqlParameter("@Quantity", Quantity);
            try
            {
                 DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id,proID, quantity);
                string SQL2 = "Update Product set Quantity = Quantity - @Quantity where ID = @ProID ";
                SqlParameter id2 = new SqlParameter("@OrderID", OrderID);
                SqlParameter proID2 = new SqlParameter("@ProID", ProID);
                SqlParameter quantity2 = new SqlParameter("@Quantity", Quantity);
                return DataProvider.DataProvider.ExecuteNonQuery(SQL2, CommandType.Text, id2, proID2, quantity2);
               
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }
        public DataSet GetOrderDetail(int orderID)
        {

            string SQL = "select [ProductID], o.[Quantity], p.Price from [dbo].[OrderDetail] o join Product p on o.ProductID = p.ID WHERE [OrderID] = @ID";
            DataSet dsBook = new DataSet();
            SqlParameter id = new SqlParameter("@ID", orderID);
            try
            {
                dsBook = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text, id);

            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return dsBook;
        }
    }

    
}
