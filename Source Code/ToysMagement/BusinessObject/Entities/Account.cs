using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    public class Account
    {
        public Account(int iD, string fullName)
        {
            ID = iD;
            FullName = fullName;
        }

        public Account(string username, string password, string fullName)
        {
            Username = username;
            Password = password;
            FullName = fullName;
        }

        public Account(int iD, string password, string fullName)
        {
            ID = iD;
            Password = password;
            FullName = fullName;
        }

        public Account(int iD, string password, string username, string fullName)
        {
            ID = iD;
            Password = password;
            Username = username;
            FullName = fullName;
        }

        public Account(int iD, string username, string fullName, int role)
        {
            ID = iD;
            Username = username;
            FullName = fullName;
            this.role = role;
        }

     

        public Account(int iD, string username, string fullName, string password, int role)
        {
            ID = iD;
            Username = username;
            FullName = fullName;
            this.role = role;
            Password = password;
        }


        public int ID { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public int role { get; set; }

    }
}
