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
    public class OrderData
    {
        public int CreateOrder(int accountID, double total)
        {
            DateTime dateTimeVariable = DateTime.Now;
            string SQL =
              "INSERT INTO [dbo].[Order](AccountID, OrderDate, Total) values (@AccountID,@OrderDate,@Total)";
            SqlParameter UserID = new SqlParameter("@AccountID", accountID);
            SqlParameter Date = new SqlParameter("@OrderDate", dateTimeVariable);
            SqlParameter Total = new SqlParameter("@Total", total);

            try
            {
                DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, UserID, Date, Total);


                string SQL2 = "SELECT TOP 1 ID from [dbo].[Order] order by ID desc";
                SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(SQL2, CommandType.Text);
                if (rd.HasRows)
                {
                    if (rd.Read())
                    {
                        return rd.GetInt32(0);
                    }
                }
                return -1;
            }

            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }

        public Order GetOrderFromOrderID(int OrderID)
        {
            string sqlSelect = "select [AccountID], [OrderDate],[Total] from [dbo].[Order] WHERE [ID] = @ID";
            SqlParameter id = new SqlParameter("@ID", OrderID);

            SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(sqlSelect, CommandType.Text, id);
            if (rd.HasRows)
            {
                if (rd.Read())
                {
                     
                    int accountID = rd.GetInt32(0);
                    DateTime date = rd.GetDateTime(1);
                    double total = rd.GetDouble(2);

                    return new Order(OrderID, accountID, date, total);
                }
            }
            return null;
        }
        public DataSet ViewStaffOrder(int accountID)
        {
            DataSet dt = new DataSet();
            try
            {
                string SQL = "SELECT [ID],[OrderDate], [Total] FROM [dbo].[Order] WHERE [AccountID] = @AccountID";
                SqlParameter id = new SqlParameter("@AccountID", accountID);
                try
                {
                    dt = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text, id);

                }
                catch (SqlException se)
                {
                    throw new Exception(se.Message);
                }
                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetAllOrder()
        {
            DataSet dt = new DataSet();
            try
            {
                string SQL = "SELECT o.[ID], o.[AccountID], a.FullName, [OrderDate], [Total] FROM [dbo].[Order] o join [dbo].[Account] a on o.AccountID = a.ID";
                try
                {
                    dt = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text);

                }
                catch (SqlException se)
                {
                    throw new Exception(se.Message);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetOrderBaseOnDate(string date1, string date2)
        {
            DataSet dt = new DataSet();
            try
            {
                string SQL = "SELECT o.[ID], o.[AccountID], a.FullName, [OrderDate], [Total] FROM [dbo].[Order] o join [dbo].[Account] a " +
                    "on o.AccountID = a.ID where CAST(o.OrderDate as DATE) between @Date1 and @Date2";
                SqlParameter d1 = new SqlParameter("@Date1", date1);
                SqlParameter d2 = new SqlParameter("@Date2", date2);
                    
                try
                {
                    dt = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text, d1,d2);

                }
                catch (SqlException se)
                {
                    throw new Exception(se.Message);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
