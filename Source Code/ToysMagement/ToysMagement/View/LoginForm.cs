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
    public partial class LoginForm : Form
    {
        public static int UserID = -1;
        public LoginForm()
        {
            InitializeComponent();
            txtPassword.MaxLength = 15;
            txtPassword.Text = "";
            txtPassword.PasswordChar = '*';
        }

        AccountData accountData = new AccountData();

        public void ShowAdminForm()
        {
            AdminForm f = new AdminForm();
            Application.Run(f);

        }public void ShowCashierForm()
        {
            CashierForm f = new CashierForm();
            Application.Run(f);

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string uname = txtUsername.Text;
                string pwd = txtPassword.Text;
                Account account = accountData.CheckLogin(uname, pwd);

                if (account == null)
                {
                    MessageBox.Show("Check your username or password again");
                }
                else
                {
                    UserID = account.ID;
                    if (account.role == 0)
                    {
                        this.Hide();
                        Thread t = new Thread(new ThreadStart(ShowAdminForm));
                        t.SetApartmentState(ApartmentState.STA);
                        t.Start();
                        this.Close();

                    }
                    else if (account.role == 1)
                    {

                        this.Hide();
                        Thread t = new Thread(new ThreadStart(ShowCashierForm));
                        t.SetApartmentState(ApartmentState.STA);
                        t.Start();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Role");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
