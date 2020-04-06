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
    public partial class OrderManagement : Form
    {
        public OrderManagement()
        {
            InitializeComponent();
        }

        Order order;
        int orderID = -1;
        DataTable dtOrderDetail = new DataTable();

        OrderDetailData orderDetailData = new OrderDetailData();

        DataTable dtOrder = new DataTable();
        AccountData accountData = new AccountData();
        OrderData orderData = new OrderData();

        public void LoadProduct()
        {
            dtOrder = orderData.GetAllOrder().Tables[0];
            dgvOrder.DataSource = dtOrder;
        }
        private void OrderManagement_Load(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void ViewDetail_Click(object sender, EventArgs e)
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
                    od.dgvOrderDetail.DataSource = dtOrderDetail;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            orderID = Int32.Parse(dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString());
            btnViewDetail.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int result = DateTime.Compare(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
            if (dateTimePicker1.Value.Date <= dateTimePicker2.Value.Date && dateTimePicker1.Value.Date < DateTime.Now)
            {
                dtOrder = orderData.GetOrderBaseOnDate(dateTimePicker1.Value.Date.ToString(), dateTimePicker2.Value.Date.ToString()).Tables[0];
                dgvOrder.DataSource = dtOrder;
            }
            else
            {
                MessageBox.Show("Invalid date input ");

            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadProduct();

        }
    }
}
