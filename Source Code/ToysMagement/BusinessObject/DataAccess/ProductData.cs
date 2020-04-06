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
    public class ProductData
    {

        public DataSet GetProductByDataSet(string proID)
        {
            string SQL = "SELECT [ID],[CategoryID],[Name],[Price] FROM Product WHERE ID = @ID AND Status = 1";
            SqlParameter id = new SqlParameter("@ID", proID);
            DataSet dsProduct = new DataSet();
            try
            {
                dsProduct = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text, id);

            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return dsProduct;
        }

        public DataSet GetProductByCategryID(string CateID)
        {
            string SQL = "SELECT [ID],[Name],[Quantity],[Price],[Status] FROM Product WHERE [CategoryID] = @ID";
            SqlParameter id = new SqlParameter("@ID", CateID);
            DataSet dsProduct = new DataSet();
            try
            {
                dsProduct = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text, id);

            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return dsProduct;
        }

        public DataSet GetAvailableProductByCategryID(string CateID)
        {
            string SQL = "SELECT [ID],[Name],[Quantity],[Price],[Status] FROM Product WHERE [CategoryID] = @ID AND Status = 1";
            SqlParameter id = new SqlParameter("@ID", CateID);
            DataSet dsProduct = new DataSet();
            try
            {
                dsProduct = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text, id);

            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return dsProduct;
        }

        public List<Product> GetAvailableProduct()
        {
            List<Product> list = new List<Product>();
            try
            {
                string sql = "SELECT [ID],[CategoryID],[Name],[Price],[Status],[Quantity] FROM Product WHERE STATUS = 1 ";
                SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(sql, CommandType.Text);
                if (rd.HasRows)
                {
                    string name, id, categoryID; int Quantity; double price; bool status;
                    while (rd.Read())
                    {
                        id = rd.GetString(0);
                        categoryID = rd.GetString(1);
                        name = rd.GetString(2);
                        price = rd.GetDouble(3);
                        status = rd.GetBoolean(4);
                        Quantity = rd.GetInt32(5);
                        list.Add(new Product(categoryID, id, name, price));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public List<Product> GetAllProduct()
            {
                List<Product> list = new List<Product>();
                try
                {
                    string sql = "SELECT [ID],[CategoryID],[Name],[Price],[Status],[Quantity]   FROM Product";
                    SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(sql, CommandType.Text);
                    if (rd.HasRows)
                    {
                        string name, id, categoryID; int Quantity; double price; bool status;
                        while (rd.Read())
                        {
                            id = rd.GetString(0);
                        categoryID = rd.GetString(1);
                        name = rd.GetString(2);
                            price = rd.GetDouble(3);
                            status = rd.GetBoolean(4);
                            Quantity = rd.GetInt32(5);
                            list.Add(new Product(categoryID,id,name,price));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return list;
            }

        public int getProductQuantity(string proID)
        {
            int quan = 0;
            string sql = "SELECT Quantity FROM Product WHERE ID = @ID AND Status = 1";
            SqlParameter id = new SqlParameter("@ID", proID);
            try
            {
                SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(sql, CommandType.Text, id);
                if (rd.HasRows)
                {
                    if (rd.Read())
                    {
                        quan = rd.GetInt32(0);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return quan;
        }


        public Product getProductByID(string proID)
        {
            Product p = null;
            string sql = "SELECT [ID],[CategoryID],[Name],[Price],[Quantity],[Status] FROM Product WHERE ID = @ID";
            SqlParameter id = new SqlParameter("@ID", proID);
            try
            {
                SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(sql, CommandType.Text, id);
                if (rd.HasRows)
                {
                    string getProID,name, categoryID; double price;int quantity; 

                    if (rd.Read())
                    {
                        getProID = rd.GetString(0);
                        categoryID = rd.GetString(1);
                        name = rd.GetString(2);
                        price = rd.GetDouble(3);
                        quantity = rd.GetInt32(4);
                        p = new Product(categoryID,getProID,name,price,quantity);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public bool AddNewProduct(Product product)
        {
            string SQL =
              "INSERT INTO Product values(@ID,@CateID,@Name,@Price,@Quantity,1)";
            SqlParameter id = new SqlParameter("@ID", product.ProductID);
            SqlParameter cateID = new SqlParameter("@CateID", product.CategoryID);
            SqlParameter name = new SqlParameter("@Name", product.ProductName);
            SqlParameter price = new SqlParameter("@Price", product.Price);
            SqlParameter quantity = new SqlParameter("@Quantity", product.Quantity);

            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id,cateID, quantity,price, name);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }

        public bool UpdateProduct(Product product)
        {
            string SQL =
              "UPDATE Product SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE ID = @ID AND Status = 1";
            SqlParameter id = new SqlParameter("@ID", product.ProductID);
            SqlParameter name = new SqlParameter("@Name", product.ProductName);
            SqlParameter quantity = new SqlParameter("@Quantity", product.Quantity);
            SqlParameter price = new SqlParameter("@Price", product.Price);
            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id, name, quantity,price);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }

        public bool DeleteProduct(string proID,string categoryID)
        {
            string SQL =
              "UPDATE Product SET Status = 0 WHERE ID = @ID AND CategoryID = @CategoryID";
            SqlParameter id = new SqlParameter("@ID", proID);
            SqlParameter cateID = new SqlParameter("@CategoryID", categoryID);
            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id, cateID);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }

        public bool RestoreProduct(string proID)
        {
            string SQL =
              "UPDATE Product SET Status = 1 WHERE ID = @ID";
            SqlParameter id = new SqlParameter("@ID", proID);
            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }
    }
}
        
    
