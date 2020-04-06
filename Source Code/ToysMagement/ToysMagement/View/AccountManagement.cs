using BusinessObject.DataAccess;
using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToysMagement.View
{
    public partial class AccountManagement : Form
    {
        public AccountManagement()
        {
            InitializeComponent();
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtName.Enabled = false;
        }
        AccountData accountData = new AccountData();
        DataTable dtAccount = new DataTable();

        private void getAllAccounts()
        {
            try
            {
                dtAccount = accountData.GetAccountsByDataSet().Tables[0];

                txtUsername.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtName.DataBindings.Clear();

                txtUsername.DataBindings.Add("Text", dtAccount, "Username", true, DataSourceUpdateMode.Never);
                txtPassword.DataBindings.Add("Text", dtAccount, "Password", true, DataSourceUpdateMode.Never);
                txtName.DataBindings.Add("Text", dtAccount, "FullName", true, DataSourceUpdateMode.Never);

                dgvAccounts.DataSource = dtAccount;
                btnDelete.Enabled = true;
                btnRestore.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void AccountManagement_Load(object sender, EventArgs e)
        {
            getAllAccounts();

        }
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUsername.Text;
                string password = txtPassword.Text;
                string fullName = txtName.Text;
                bool checkedUsername = accountData.checkedAvailable1(userName);
                bool checkedPassword = accountData.checkedAvailable1(password);
                bool checkedFullName = accountData.checkedAvailable2(fullName);

                if (!userName.Equals("") && !password.Equals("") && !fullName.Equals(""))
                {
                    if (checkedUsername == true && checkedPassword == true && checkedFullName == true)
                    {
                        Account addNew = new Account(userName, password, fullName);

                        if (accountData.AddNewAccount(addNew))
                        {
                            getAllAccounts();
                            MessageBox.Show("Added successfully");
                        }
                        else
                        {
                            MessageBox.Show("Add fail");
                        }
                        txtName.Enabled = false;
                        txtPassword.Enabled = false;
                        txtUsername.Enabled = false;
                        btnCreateAccount.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Username is about chars and number, and Password is about chars and number, and Full name is only chars!!");
                    }
                }
                else
                {
                    MessageBox.Show("Username or Password or Full name is blank");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY"))
                {
                    MessageBox.Show("This account is existed");
                }
                else {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Int32.Parse(dgvAccounts.Rows[dgvAccounts.CurrentCell.RowIndex].Cells[0].Value.ToString());
                string password = txtPassword.Text;
                string fullName = txtName.Text;
                bool checkedPassword = accountData.checkedAvailable1(password);
                bool checkedFullName = accountData.checkedAvailable2(fullName);
                if (accountData.checkStatus(id) == false)
                {
                    if (!password.Equals("") && !fullName.Equals(""))
                    {
                        if (checkedPassword == true && checkedFullName == true)
                        {
                            Account updateAccount = new Account(id, password, fullName);
                            if (accountData.UpdateAccount(updateAccount))
                            {
                                getAllAccounts();
                                MessageBox.Show("Updated successfully");
                                txtPassword.Enabled = false;
                                txtName.Enabled = false;
                                btnUpdate.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show("Updated fail");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password is about chars and number, and Full name is ONLY chars");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Username or Password is blank");
                    }
                }
                else
                {
                    MessageBox.Show("You must restore this account before updating");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


       

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBoxManager.OK = "Sure";
                MessageBoxManager.Cancel = "No";
                MessageBoxManager.Register();
                DialogResult dialogResult = MessageBox.Show("Are you sure to do this?", "", MessageBoxButtons.OKCancel);
                int id = Int32.Parse(dgvAccounts.Rows[dgvAccounts.CurrentCell.RowIndex].Cells[0].Value.ToString());
                if (accountData.checkStatus(id) == false)
                {
                    if (!accountData.checkAdminAccount(id) && (dialogResult == DialogResult.OK))
                    {
                        if (accountData.DeleteAccount(id))
                        {
                            getAllAccounts();
                            MessageBox.Show("Deleted successfully");
                        }
                        else 
                        {
                            MessageBox.Show("Deleted fail");
                        }
                        MessageBoxManager.Unregister();
                    }
                    else if (accountData.checkAdminAccount(id))
                    {
                        MessageBoxManager.Unregister();
                        MessageBox.Show("Warning: You can not delete an admin account!!!");
                    }
                    MessageBoxManager.Unregister();
                }
                else if ((accountData.checkStatus(id) == true) && dialogResult == DialogResult.OK)
                {
                    MessageBoxManager.Unregister();
                    MessageBox.Show("This account has already deleted");
                }
                MessageBoxManager.Unregister();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Int32.Parse(dgvAccounts.Rows[dgvAccounts.CurrentCell.RowIndex].Cells[0].Value.ToString());
                if (accountData.checkStatus(id) == true)
                {
                    if (accountData.RestoreAccount(id))
                    {

                        getAllAccounts();
                        MessageBox.Show("Restored successfully");
                    }
                    else
                    {
                        MessageBox.Show("Restored fail");
                    }

                }
                else
                {
                    MessageBox.Show("This account has not deleted yet");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (txtUsername.Enabled == true && txtPassword.Enabled == true && txtName.Enabled == true && btnUpdate.Enabled == false)
            {
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtName.Text = string.Empty;
                txtName.Enabled = false;
                txtPassword.Enabled = false;
                txtUsername.Enabled = false;
                btnCreateAccount.Enabled = false;
            }
            else if (txtUsername.Enabled == true || txtPassword.Enabled == true || txtName.Enabled == true)
            {
                txtPassword.Text = string.Empty;
                txtName.Text = string.Empty;
                txtName.Enabled = false;
                txtPassword.Enabled = false;
                txtUsername.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else
            {
                MessageBox.Show("Nothing to do");
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBoxManager.OK = "ADD";
                MessageBoxManager.Cancel = "UPDATE";
                MessageBoxManager.Register();
                DialogResult dialogResult = MessageBox.Show("Do you wanna ADD or UPDATE", "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    if (btnCreateAccount.Enabled == false)
                    {
                        btnCreateAccount.Enabled = true;
                    }
                    txtUsername.Enabled = true;
                    txtUsername.Text = String.Empty;

                    txtPassword.Enabled = true;
                    txtPassword.Text = String.Empty;

                    txtName.Enabled = true;
                    txtName.Text = String.Empty;
                    btnUpdate.Enabled = false;
                    MessageBoxManager.Unregister();

                }
                else
                {
                    if (txtUsername.Enabled == true || btnUpdate.Enabled == false)
                    {
                        txtUsername.Enabled = false;
                        btnUpdate.Enabled = true;
                    }
                    txtPassword.Enabled = true;
                    txtPassword.Text = String.Empty;

                    txtName.Enabled = true;
                    txtName.Text = String.Empty;
                    btnCreateAccount.Enabled = false;
                    btnUpdate.Enabled = true;
                    MessageBoxManager.Unregister();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
