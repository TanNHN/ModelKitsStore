using BusinessObject.DataAccess;
using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToysMagement.View;

namespace ToysMagement
{
    public partial class AdminForm : Form
    {
       
        AccountData accountData = new AccountData();
        Account account;
        public AdminForm()
        {
            InitializeComponent();
        }


        private void btnAccountManagement_Click(object sender, EventArgs e)
        {
            AccountManagement am = new AccountManagement();
            am.Show();
        }

        private void btnOrderManagement_Click(object sender, EventArgs e)
        {
            OrderManagement om = new OrderManagement();
            om.Show();
        }

        private void btnProductManagement_Click(object sender, EventArgs e)
        {
            ProductManagement pm = new ProductManagement();
            pm.Show();
        }
        public void ShowForm()
        {
            LoginForm f = new LoginForm();
            Application.Run(f);

        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Thread t = new Thread(new ThreadStart(ShowForm));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            this.Close();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            account = accountData.GetAccountDetailFromID(LoginForm.UserID);
            lbAdminName.Text = account.FullName;
        }
    }
}
