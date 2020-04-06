using BusinessObject.DataAccess;
using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToysMagement.View
{
    public partial class CashierForm : Form
    {
        int selectedRow;
        ProductData productDT = new ProductData();
        List<CartItem> Cart = new List<CartItem>();
        DataTable dt = new DataTable();
        List<CartItem> itemRemoval = new List<CartItem>();
        OrderData orderData = new OrderData();
        OrderDetailData orderDetailData = new OrderDetailData();
        AccountData accountData = new AccountData();
        public static int UserID = LoginForm.UserID;

        Account account;
        private void CashierForm_Load(object sender, EventArgs e)
        {
            account = accountData.GetAccountDetailFromID(LoginForm.UserID);
            lbStaffName.Text = account.FullName;
            UserID = account.ID;
        }
        public void LoadProduct()
        {
            try
            {
                List<Product> list = productDT.GetAvailableProduct();

                foreach (Product p in list)
                {
                    txtSearch.AutoCompleteCustomSource.Add(p.ProductID);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void GetTotal()
        {
            double sum = 0;

            for (int i = 0; i < dgvCart.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(dgvCart.Rows[i].Cells[4].Value);
            }
            lbTotal.Text = sum.ToString();

        }
        public CashierForm()
        {
            InitializeComponent();
            
            LoadProduct();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                string id = txtSearch.Text;
                lbQuantity.Text = productDT.getProductQuantity(id).ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtSearch.Text.Length == 0)
                {
                    MessageBox.Show("Please insert Product ID");
                }
                else
                {
                    Product p = productDT.getProductByID(txtSearch.Text);
                    if (p != null)
                    {
                        int chkNum;
                        bool isNumeric = int.TryParse(txtQuantity.Text, out chkNum);
                        if (txtQuantity.Text.Length == 0 || !isNumeric)
                        {
                            MessageBox.Show("Invalid Quantity input");
                        }
                        else
                        {
                            bool chkDuplicate = true;
                            int quantity = Int32.Parse(txtQuantity.Text);
                            if (quantity > 0 && quantity <= productDT.getProductQuantity(p.ProductID))
                            {
                                for (int i = 0; i < Cart.Count; i++)
                                {       
                                    if (Cart[i].product.ProductID.Equals(txtSearch.Text))
                                    {
                                        Cart[i].Quantity = quantity;
                                        chkDuplicate = false;
                                        dgvCart.Rows[i].Cells[0].Value = Cart[i].product.ProductID;
                                        dgvCart.Rows[i].Cells[1].Value = Cart[i].product.ProductName;
                                        dgvCart.Rows[i].Cells[2].Value = Cart[i].product.Price;
                                        dgvCart.Rows[i].Cells[3].Value = quantity;
                                        dgvCart.Rows[i].Cells[4].Value = Cart[i].product.Price * quantity;
                                        MessageBox.Show("Product's " + p.ProductName + " has been updated");
                                    }
                                }
                                if (chkDuplicate)
                                {
                                    dgvCart.Rows.Add();
                                    Cart.Add(new CartItem(p, quantity));
                                    for (int i = 0; i < Cart.Count; i++)
                                    { 
                                        if (Cart[i].product.ProductID.Equals(txtSearch.Text))
                                        {
                                            dgvCart.Rows[i].Cells[0].Value = Cart[i].product.ProductID;
                                            dgvCart.Rows[i].Cells[1].Value = Cart[i].product.ProductName;
                                            dgvCart.Rows[i].Cells[2].Value = Cart[i].product.Price;
                                            dgvCart.Rows[i].Cells[3].Value = Cart[i].Quantity;
                                            dgvCart.Rows[i].Cells[4].Value = Cart[i].product.Price * quantity;
                                        }
                                    }
                                }
                                GetTotal();
                            }
                            else
                            {
                                MessageBox.Show("Product quantity must be greater than 0 and lower than product available");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Product is not exist");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                btnRemove.Enabled = false;
                selectedRow = -1;
            }
            else
            {
                btnRemove.Enabled = true;
                selectedRow = dgvCart.Rows[e.RowIndex].Index;
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try 
            {
                if (selectedRow > -1)
                {
                    string proID = dgvCart.Rows[selectedRow].Cells[0].Value.ToString();

                    foreach (CartItem cartItem in Cart)
                    {
                        if (cartItem.product.ProductID.Equals(proID))
                        {
                            itemRemoval.Add(cartItem);
                            dgvCart.Rows.RemoveAt(dgvCart.Rows[selectedRow].Index);
                        }
                    }foreach(CartItem cartItem1 in itemRemoval)
                    {
                        Cart.Remove(cartItem1);
                    }
                    itemRemoval.Clear();
                    GetTotal();

                }
                else
                {
                    MessageBox.Show("Fail to remove item");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            
        }

        public void reloadForm()
        {
            Cart.Clear();
            dgvCart.Rows.Clear();
            dgvCart.Refresh();
            txtSearch.Text = "";
            txtQuantity.Text = "";
            lbQuantity.Text = "";
            selectedRow = -1;
            lbTotal.Text = "0";
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            reloadForm();
          
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cart.Count != 0)
                {
                    double total = double.Parse(lbTotal.Text);
                    int orderID = orderData.CreateOrder(account.ID, total);
                    if (orderID > 0)
                    {
                        bool chk = false;
                        foreach (CartItem cartItem in Cart)
                        {
                            if (orderDetailData.InsertProductToOrder(orderID, cartItem.product.ProductID, cartItem.Quantity))
                            {
                                chk = true;
                            }
                        }
                        if (chk)
                        {
                            MessageBox.Show("Order created");
                            reloadForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed when create order");

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Empty cart");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            ViewOrder vo = new ViewOrder();
            vo.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewProduct vp = new ViewProduct();
            vp.Show();
        }
        public void ShowForm()
        {
            LoginForm f = new LoginForm();
            Application.Run(f);

        }
        private void btnLogOut_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Thread t = new Thread(new ThreadStart(ShowForm));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            this.Close();

        }

        
    }
}
