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
    public class CategoryData
    {
        public DataSet GetCategoryByDataSet()
        {
            string SQL = "SELECT [CategoryID],[CategoryName],[Status] FROM Category";
            DataSet dsCategory = new DataSet();
            try
            {
                dsCategory = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text);

            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return dsCategory;
        }

        public List<Category> GetCategoryList()
        {
            List<Category> list = new List<Category>();
            try
            {
                string SQL = "SELECT [CategoryID],[CategoryName],[Status] FROM Category";
                SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(SQL, CommandType.Text);
                if (rd.HasRows)
                {
                    string name, categoryID; bool status;
                    while (rd.Read())
                    {
                        categoryID = rd.GetString(0);
                        name = rd.GetString(1);
                        status = rd.GetBoolean(2);
                        list.Add(new Category(categoryID, name, status));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
            
        public DataSet GetAvailableCategoryByDataSet()
        {
            string SQL = "SELECT [CategoryID],[CategoryName] FROM Category WHERE [Status] = 1";
            DataSet dsCategory = new DataSet();
            try
            {
                dsCategory = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text);

            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return dsCategory;
        }

        public bool CheckCategoryStatus(string cateID)
        {
            string sql = "SELECT Status FROM Category WHERE CategoryID = @ID";
            SqlParameter id = new SqlParameter("@ID", cateID);
            try
            {
                SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(sql, CommandType.Text, id);
                if (rd.HasRows)
                {
                    if (rd.Read())
                    {
                        return rd.GetBoolean(0);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public bool AddCategory(Category category)
        {
            string SQL = "INSERT INTO [Category] values (@ID,@Name,1) ";
            SqlParameter id = new SqlParameter("@ID", category.CategoryID);
            SqlParameter name = new SqlParameter("@Name", category.CategoryName);

            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id, name);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }

        public bool updateCategory(Category category)
        {
            string SQL = "Update Category set CategoryName=@Name where CategoryID=@ID";
            SqlParameter id = new SqlParameter("@ID", category.CategoryID);
            SqlParameter name = new SqlParameter("@Name", category.CategoryName);

            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id, name);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }

        public bool deleteCategory(string CateID)
        {
            string SQL = "UPDATE Product SET Status = 0 WHERE CategoryID=@ID;" +
                "Update Category SET Status = 0 WHERE CategoryID=@ID";
            SqlParameter id = new SqlParameter("@ID", CateID);
            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }

        public bool restoreCategory(string CateID)
        {
            string sql = "Update Category SET Status = 1 WHERE CategoryID=@ID";
            SqlParameter id = new SqlParameter("@ID", CateID);
            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(sql, CommandType.Text, id);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }
    }
}
