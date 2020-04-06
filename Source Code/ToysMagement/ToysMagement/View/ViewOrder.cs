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
    public partial class ViewOrder : Form
    {
        OrderData orderData = new OrderData();
        DataTable dtOrder = new DataTable();
        OrderDetailData orderDetailData = new OrderDetailData();
        DataTable dtOrderDetail = new DataTable();
        Order order;
        AccountData accountData = new AccountData();
        public ViewOrder()
        {
            InitializeComponent();
        }

        int orderID = -1;
        private void ViewOrder_Load(object sender, EventArgs e)
        {
            try
            {
                dtOrder = orderData.ViewStaffOrder(CashierForm.UserID).Tables[0];
                dgvOrder.DataSource = dtOrder;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            try 
            {
                if (orderID != -1)
                {
                    OrderDetail od = new OrderDetail();
                    od.Show();
                    order = orderData.GetOrderFromOrderID(orderID);
                    Account account = accountData.GetAccountDetailFromID(order.AccountID);
                    od.txtCashier.Text = account.Username;
                    od.txtDate.Text = order.OrderDate.ToString();
                    od.txtOrderID.Text = order.OrderID.ToString();
                    od.txtTotal.Text = order.Total.ToString();
                    dtOrderDetail = orderDetailData.GetOrderDetail(orderID).Tables[0];
                    dtOrderDetail.Columns.Add("SubTotal", typeof(double), "Price*Quantity");
                    od.dgvOrderDetail.DataSource = dtOrderDetail;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            orderID = Int32.Parse(dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString());
            btnViewDetail.Enabled = true;
        }
    }
}
