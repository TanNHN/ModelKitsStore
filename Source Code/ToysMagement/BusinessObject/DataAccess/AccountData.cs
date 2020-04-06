using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessObject.DataAccess
{
    public class AccountData
    {

        public DataSet GetAccountsByDataSet()
        {
            string SQL = "Select ID, Username, Password, FullName, RoleName,Status "
                       + "From Account , RoleDetail "
                       + "Where Account.RoleID = RoleDetail.RoleID";
            DataSet dsAccount = new DataSet();
            try
            {
                dsAccount = DataProvider.DataProvider.ExecuteQueryWithDataSet(SQL, CommandType.Text);

            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return dsAccount;
        }
        public Account GetAccountDetailFromID(int id)
        {
            try
            {
                string sql = "SELECT [Username],[Fullname],[RoleID] FROM Account WHERE ID = @ID";
                SqlParameter aID = new SqlParameter("@ID", id);
                SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(sql, CommandType.Text, aID);
                if (rd.HasRows)
                {
                    string username, fullname; int role;
                    while (rd.Read())
                    {
                        username = rd.GetString(0);
                        fullname = rd.GetString(1);
                        role = rd.GetInt32(2);
                        return new Account(id,username,fullname,role);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return null;

        }
        public Account GetAccountDetailFromUsername(string uname)
        {
            try
            {
                string sql = "SELECT [ID],[Fullname] FROM Account WHERE Username = @Username";
                SqlParameter username = new SqlParameter("@Username", uname);
                SqlDataReader rd = DataProvider.DataProvider.ExecuteQueryWithDataReader(sql, CommandType.Text, username);
                if (rd.HasRows)
                {
                    int accountID; string fullname; 
                    while (rd.Read())
                    {
                        accountID = rd.GetInt32(0);
                        fullname = rd.GetString(1);
                        return new Account(accountID, fullname);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return null;

        }

        public Account CheckLogin(string username, string password)
        {
           try
           {
                Account a = null;
                string SQL = "Select [ID],FullName, dbo.RoleDetail.RoleName "
                           + "From dbo.Account , dbo.RoleDetail "
                           + "Where dbo.Account.RoleID = dbo.RoleDetail.RoleID "
                           + "And Username = @Username "
                           + "And Password = @Password AND Status = 1";
                SqlParameter userName = new SqlParameter("@Username", username);
                SqlParameter passWord = new SqlParameter("@Password", password);
                SqlDataReader dataReader = DataProvider.DataProvider.ExecuteQueryWithDataReader(SQL, CommandType.Text, userName, passWord);
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        int id = dataReader.GetInt32(0);
                        string fullname = dataReader.GetString(1);
                        string roleName = dataReader.GetString(2);
                        int role = 2;
                        if (roleName.Equals("Staff"))
                        {
                            role = 1;
                        }
                        else if (roleName.Equals("Admin"))
                        {
                            role = 0;

                        }
                        a = new Account(id, username, fullname, role);
                    }
                }
                return a;
           }
           catch (SqlException ex)
           {
                throw new Exception(ex.Message);
           } 
        }

        public bool checkStatus(int checkedIDStatus)
        {
            try
            {
                string SQL = "Select Status From Account Where ID = @ID";
                SqlParameter accountID = new SqlParameter("@ID", checkedIDStatus);
                SqlDataReader dataReader = DataProvider.DataProvider.ExecuteQueryWithDataReader(SQL, CommandType.Text, accountID);
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        bool status = dataReader.GetBoolean(0);
                        if (status == false)
                        {
                            return true;
                        }
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool checkedAvailable1(string input)
        {
            Regex rx = new Regex("^[a-zA-Z0-9]*$");
            if (rx.IsMatch(input))
            {
                return true;
            }
            return false;
        }
        public bool checkedAvailable2(string input)
        {
            Regex rx = new Regex("^[a-zA-Z_ ]*$");
            if (rx.IsMatch(input))
            {
                return true;
            }
            return false;
        }
        public bool AddNewAccount(Account a)
        {
            try {
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            string SQL =
                "Insert into Account (Username, Password, FullName, RoleID,Status) values(@Username, @Password, @FullName, 1,1)";
            SqlParameter userName = new SqlParameter("@Username", a.Username);
            SqlParameter password = new SqlParameter("@Password", a.Password);
            SqlParameter fullName = new SqlParameter("@FullName", a.FullName);
            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, userName, password, fullName);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }

        public bool DeleteAccount(int id)
        {
            string SQL = "UPDATE Account SET Status =  0  WHERE ID = @ID";
            SqlParameter accountID = new SqlParameter("@ID", id);
            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, accountID);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }
        public bool RestoreAccount(int id)
        {
            string SQL = "UPDATE Account SET Status =  1  WHERE ID = @ID";
            SqlParameter accountID = new SqlParameter("@ID", id);
            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, accountID);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }
        public bool UpdateAccount(Account a)
        {
            string SQL = "Update Account set Password=@Password, FullName=@FullName where ID= @ID";
            SqlParameter id = new SqlParameter("@ID", a.ID);
            SqlParameter password = new SqlParameter("@Password", a.Password);
            SqlParameter fullName = new SqlParameter("@FullName", a.FullName);
            try
            {
                return DataProvider.DataProvider.ExecuteNonQuery(SQL, CommandType.Text, id, password, fullName);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
        }
        public List<Account> GetAllDataAccounts()
        {
            List<Account> list = new List<Account>();
            try
            {
                string SQL = "Select ID, Username, Password, FullName, RoleID "
                           + "From Account";
                SqlDataReader dataReader = DataProvider.DataProvider.ExecuteQueryWithDataReader(SQL, CommandType.Text);
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        int id = dataReader.GetInt32(0);
                        string username = dataReader.GetString(1);
                        string password = dataReader.GetString(2);
                        string fullName = dataReader.GetString(3);
                        int roleID = dataReader.GetInt32(4);
                        list.Add(new Account(id, username, password, fullName, roleID));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public bool checkAdminAccount(int id)
        {
            try
            {
                string SQL = "Select RoleID FROM Account WHERE ID = @ID";
                SqlParameter accountID = new SqlParameter("@ID", id);
                SqlDataReader dataReader = DataProvider.DataProvider.ExecuteQueryWithDataReader(SQL, CommandType.Text, accountID);
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        int roleID = dataReader.GetInt32(0);
                        if(roleID == 0)
                        {
                            return true;
                        }
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    }




