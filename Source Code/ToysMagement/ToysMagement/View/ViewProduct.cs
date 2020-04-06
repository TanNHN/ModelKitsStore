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
    public partial class ViewProduct : Form
    {
        public ViewProduct()
        {
            InitializeComponent();
            LoadProduct();

        }
        public void LoadProduct()
        {
            try
            {
                List<Product> list = productData.GetAvailableProduct();

                foreach (Product p in list)
                {
                    txtSearch.AutoCompleteCustomSource.Add(p.ProductID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        ProductData productData = new ProductData();
        CategoryData CategoryDT = new CategoryData();
        DataTable dtCategory = new DataTable();
        DataTable dtProduct = new DataTable();
        public void LoadCategory()
        {
            try
            {
                dtCategory = CategoryDT.GetAvailableCategoryByDataSet().Tables[0];
                dtCategory.PrimaryKey = new DataColumn[] { dtCategory.Columns[0] };

                txtCategoryID.Enabled = false;
                txtCategory.Enabled = false;
                txtProductID.Enabled = false;

                dgvCategory.DataSource = dtCategory;
                dgvCategory.Columns[0].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void ViewProduct_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void dgvCategory_SelectionChanged(object sender, EventArgs e)
        {
            string cateID = dgvCategory.Rows[dgvCategory.CurrentRow.Index].Cells[0].Value.ToString();

            txtCategory.DataBindings.Clear();
            txtCategoryID.DataBindings.Clear();
            txtCategoryID.DataBindings.Add("Text", dtCategory, "CategoryID");
            txtCategory.DataBindings.Add("Text", dtCategory, "CategoryName");
            txtProductID.DataBindings.Clear();
            txtName.DataBindings.Clear();
            txtPrice.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtName.Text = "";
            txtPrice.Text = "";
            txtProductID.Text = "";
            txtQuantity.Text = "";
            txtSearch.Text = "";
            getProductFromCategory(cateID);

        }

        public void getProductFromCategory(string id)
        {
            try
            {
                dtProduct = productData.GetAvailableProductByCategryID(id).Tables[0];
                dtProduct.PrimaryKey = new DataColumn[] { dtProduct.Columns[0] };
                txtProductID.DataBindings.Clear();
                txtName.DataBindings.Clear();
                txtPrice.DataBindings.Clear();
                txtQuantity.DataBindings.Clear();
                txtPrice.DataBindings.Clear();
                txtProductID.DataBindings.Add("Text", dtProduct, "ID");
                txtName.DataBindings.Add("Text", dtProduct, "Name");
                txtQuantity.DataBindings.Add("Text", dtProduct, "Quantity");
                txtPrice.DataBindings.Add("Text", dtProduct, "Price");

                dgvProduct.DataSource = dtProduct;
                dgvProduct.Columns[4].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductID.DataBindings.Clear();
            txtName.DataBindings.Clear();
            txtPrice.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtPrice.DataBindings.Clear();


            txtProductID.DataBindings.Add("Text", dtProduct, "ID");
            txtName.DataBindings.Add("Text", dtProduct, "Name");
            txtQuantity.DataBindings.Add("Text", dtProduct, "Quantity");
            txtPrice.DataBindings.Add("Text", dtProduct, "Price");

            dgvProduct.DataSource = dtProduct;
            dgvProduct.Columns[4].Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = productData.getProductByID(txtSearch.Text);
                if (p != null)
                {
                    txtCategory.DataBindings.Clear();
                    txtCategoryID.DataBindings.Clear();
                    foreach (DataGridViewRow row in dgvCategory.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(p.CategoryID))
                        {
                            dgvCategory.ClearSelection();
                            dgvCategory.Rows[row.Index].Selected = true;
                            txtCategory.Text = dgvCategory.Rows[row.Index].Cells[0].Value.ToString();
                            txtCategoryID.Text = dgvCategory.Rows[row.Index].Cells[1].Value.ToString();
                            getProductFromCategory(dgvCategory.Rows[row.Index].Cells[0].Value.ToString());
                            foreach (DataGridViewRow row2 in dgvProduct.Rows)
                            {
                                if (row2.Cells[0].Value.ToString().Equals(p.ProductID))
                                {
                                    dgvProduct.ClearSelection();

                                    dgvProduct.Rows[row2.Index].Selected = true;

                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This Product is not exist");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
    }
    } 

