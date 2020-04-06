using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObject.DataAccess
{
    //public class StoreData
    //{
    //    public DataSet GetStoreByDataSet()
    //    {
    //        string SQL = "SELECT StoreID, StoreName, Address FROM Store WHERE Status = 1";
    //        DataSet dsStore = new DataSet();
    //        try
    //        {
    //            dsStore = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text);

    //        }
    //        catch (SqlException se)
    //        {
    //            throw new Exception(se.Message);
    //        }
    //        return dsStore;
    //    }
    //    public List<Store> GetAllStore()
    //    {
    //        List<Store> list = new List<Store>();
    //        try
    //        {
    //            string sql = "SELECT StoreID, StoreName, Address FROM Store WHERE Status = 1";
    //            SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(sql, CommandType.Text);
    //            if (rd.HasRows)
    //            {
    //                string name,id , address;
    //                while (rd.Read())
    //                {
    //                    id = rd.GetString(0);
    //                    name = rd.GetString(1);
    //                    address = rd.GetString(2);
    //                    list.Add(new Store(id, name, address));
    //                }
    //            }
    //        } catch (SqlException ex)
    //        {
    //            throw new Exception(ex.Message) ;
    //        }
    //        return list;
    //    }

    //    public bool AddNewStore(Store store)
    //    {
    //        string SQL =
    //          "INSERT INTO Store values(@ID,@Name,@Address,@Status)";
    //        SqlParameter id = new SqlParameter("@ID", store.StoreID);
    //        SqlParameter name = new SqlParameter("@Name", store.Name);
    //        SqlParameter address = new SqlParameter("@Address", store.Address);
    //        SqlParameter status = new SqlParameter("@Status", 1);

    //        try
    //        {
    //            return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id,name,address,status);
    //        }
    //        catch (SqlException se)
    //        {
    //            throw new Exception(se.Message);
    //        }
    //    }

    //    public bool UpdateStore(Store store)
    //    {
    //        string SQL =
    //          "UPDATE Store SET StoreName = @Name, Address = @Address WHERE StoreID = @ID AND Status = 1";
    //        SqlParameter id = new SqlParameter("@ID", store.StoreID);
    //        SqlParameter name = new SqlParameter("@Name", store.Name);
    //        SqlParameter address = new SqlParameter("@Address", store.Address);
    //        try
    //        {
    //            return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id, name, address);
    //        }
    //        catch (SqlException se)
    //        {
    //            throw new Exception(se.Message);
    //        }
    //    }

    //    public bool DeleteStore(string StoreID)
    //    {
    //        string SQL =
    //          "UPDATE Store SET Status = 0 WHERE StoreID = @ID";
    //        SqlParameter id = new SqlParameter("@ID", StoreID);
    //        try
    //        {
    //            return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id);
    //        }
    //        catch (SqlException se)
    //        {
    //            throw new Exception(se.Message);
    //        }
    //    }
    //}
}
