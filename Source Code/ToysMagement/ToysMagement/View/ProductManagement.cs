using BusinessObject.DataAccess;
using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToysMagement
{
    public partial class ProductManagement : Form
    {
        public ProductManagement()
        {
            InitializeComponent();
            LoadProduct();
        }
        ProductData productData = new ProductData();
        CategoryData CategoryDT = new CategoryData();
        DataTable dtCategory = new DataTable();
        DataTable dtProduct = new DataTable();

        private void dgvCategory_SelectionChanged(object sender, EventArgs e)
        {
            
            lbStatusProduct.Text = "";
            btnNew.Enabled = true;
            btnAddCategory.Enabled = false;
            txtProductID.Enabled = false;
            
            string cateID = dgvCategory.Rows[dgvCategory.CurrentRow.Index].Cells[0].Value.ToString();
            txtCategory.DataBindings.Clear();
            txtCategoryID.DataBindings.Clear();
            txtCategoryStatus.DataBindings.Clear();
            txtCategoryID.DataBindings.Add("Text", dtCategory, "CategoryID");
            txtCategory.DataBindings.Add("Text", dtCategory, "CategoryName");
            txtCategoryStatus.DataBindings.Add("Text", dtCategory, "Status");
            string status = dgvCategory.Rows[dgvCategory.CurrentRow.Index].Cells[2].Value.ToString();
            dgvProduct.Enabled = true;
            btnSearch.Enabled = true;
            if (status.Equals("False"))
            {
                lbStatusCate.Text = "Deleted";
                btnRestoreCate.Enabled = true;
            }
            else if (status.Equals("True"))
            {
                lbStatusCate.Text = "";
            }
            else
            {
                lbStatusCate.Text = "???";

            }
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

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductID.Enabled = false;
            
            txtProductID.DataBindings.Clear();
            txtName.DataBindings.Clear();
            txtPrice.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtPrice.DataBindings.Clear();
            txtStatusProduct.DataBindings.Clear();
            txtCategory.DataBindings.Clear();
            txtCategoryID.DataBindings.Clear();

            txtCategoryID.Text = dgvCategory.Rows[dgvCategory.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtCategory.Text = dgvCategory.Rows[dgvCategory.CurrentCell.RowIndex].Cells[1].Value.ToString();
            txtCategoryID.DataBindings.Clear();
            txtProductID.DataBindings.Add("Text", dtProduct, "ID");
            txtName.DataBindings.Add("Text", dtProduct, "Name");
            txtQuantity.DataBindings.Add("Text", dtProduct, "Quantity");
            txtPrice.DataBindings.Add("Text", dtProduct, "Price");
            txtStatusProduct.DataBindings.Add("Text", dtProduct, "Status");

            if (txtStatusProduct.Text.Equals("False"))
            {
                btnRestoreProduct.Enabled = true;
                btnDelete.Enabled = false;
                lbStatusProduct.Text = "Deleted";
            }
            else if (txtStatusProduct.Text.Equals("True") || txtStatusProduct.Text.Equals(""))
            {
                btnRestoreProduct.Enabled = false;
                btnDelete.Enabled = true;

                lbStatusProduct.Text = "";
            }
            else
            {
                lbStatusProduct.Text = "???";

            }
            btnAdd.Enabled = false;

            txtCategory.Enabled = false;
            txtCategoryID.Enabled = false;
            btnAddCategory.Enabled = false;
            btnEdit.Enabled = false;

            txtName.Enabled = true;
            txtPrice.Enabled = true;
            txtQuantity.Enabled = true;
            txtPrice.Enabled = true;
            dgvProduct.DataSource = dtProduct;
            dgvProduct.Columns[4].Visible = false;
        }
        public void LoadProduct()
        {
            try
            {
                List<Product> list = productData.GetAllProduct();

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
        public void LoadCategory()
        {
            try
            {
                dtCategory = CategoryDT.GetCategoryByDataSet().Tables[0];
                dtCategory.PrimaryKey = new DataColumn[] { dtCategory.Columns[0] };

                txtCategoryID.Enabled = false;
                txtCategory.Enabled = false;
                txtProductID.Enabled = false;
               
                dgvCategory.DataSource = dtCategory;
                dgvCategory.Columns[0].Visible = false;
                dgvCategory.Columns[2].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void ProductManagement_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = productData.getProductByID(txtSearch.Text);
                if (p != null)
                {
                    foreach (DataGridViewRow row in dgvCategory.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(p.CategoryID))
                        {
                            dgvCategory.ClearSelection();
                            dgvCategory.Rows[row.Index].Selected = true;
                            txtCategory.DataBindings.Clear();
                            txtCategoryID.DataBindings.Clear();
                            txtCategoryStatus.DataBindings.Clear();
                            if (CategoryDT.CheckCategoryStatus(dgvCategory.Rows[row.Index].Cells[0].Value.ToString()).ToString().Equals("True"))
                            {
                                lbStatusCate.Text = "";
                            }
                            else
                            {
                                lbStatusCate.Text = "Deleted";
                            }
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void getProductFromCategory(string id)
        {
            try
            {
                dtProduct = productData.GetProductByCategryID(id).Tables[0];
                dtProduct.PrimaryKey = new DataColumn[] { dtProduct.Columns[0] };
                btnAdd.Enabled = false;
                txtProductID.DataBindings.Clear();
                txtName.DataBindings.Clear();
                txtPrice.DataBindings.Clear();
                txtQuantity.DataBindings.Clear();
                txtPrice.DataBindings.Clear();
                txtStatusProduct.DataBindings.Clear();
                txtCategoryStatus.DataBindings.Clear();
                txtProductID.DataBindings.Add("Text", dtProduct, "ID");
                txtName.DataBindings.Add("Text", dtProduct, "Name");
                txtQuantity.DataBindings.Add("Text", dtProduct, "Quantity");
                txtPrice.DataBindings.Add("Text", dtProduct, "Price");
                txtStatusProduct.DataBindings.Add("Text", dtProduct, "Status");
                txtCategoryStatus.DataBindings.Add("Text", dtCategory, "Status");
                if (txtStatusProduct.Text.Equals("False"))
                {
                    lbStatusProduct.Text = "Deleted";
                }
                else
                {

                    lbStatusProduct.Text = "";
                }

                dgvProduct.DataSource = dtProduct;
                dgvProduct.Columns[4].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private void btnNewCategory_Click(object sender, EventArgs e)
        {
            btnNew.Enabled = false;
            btnAddCategory.Enabled = true;
            btnEdit.Enabled = true;
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtCategoryID.DataBindings.Clear();
            txtCategory.DataBindings.Clear();
            txtProductID.DataBindings.Clear();
            txtName.DataBindings.Clear();
            txtPrice.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();

            lbStatusCate.Text = "";
            lbStatusProduct.Text = "";
            txtCategory.Text = "";
            txtCategory.Enabled = true;
            txtCategoryID.Enabled = true;
            txtCategoryID.Text = "";
            txtCategoryID.Select();
            txtName.Text = "";
            txtName.Enabled = false;
            txtPrice.Text = "";
            txtPrice.Enabled = false;
            txtProductID.Text = "";
            txtProductID.Enabled = false;
            txtQuantity.Text = "";
            txtQuantity.Enabled = false;
            txtSearch.Text = "";
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategory.TextLength != 0 && txtCategoryID.TextLength != 0)
                {
                    string cateID = txtCategoryID.Text;
                    string cateName = txtCategory.Text;
                    if (CategoryDT.AddCategory(new Category(cateID, cateName)))
                    {
                        dtCategory.RejectChanges();

                        dtCategory.Rows.Add(cateID, cateName);

                        dtCategory.AcceptChanges();
                        MessageBox.Show("New Category added!");

                    }
                    else
                    {
                        MessageBox.Show("Error when adding Category");
                    }
                }
                else
                {
                    MessageBox.Show("Please inset category name and category id");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {

                    MessageBox.Show("This category ID is already existed");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategory.TextLength != 0 && txtCategoryID.TextLength != 0)
                {

                    string cateID = txtCategoryID.Text;
                    string cateName = txtCategory.Text;
                    if (CategoryDT.updateCategory(new Category(cateID, cateName)))
                    {
                        DataRow r = dtCategory.Rows.Find(cateID);
                        r["CategoryID"] = cateID;
                        r["CategoryName"] = cateName;
                        dgvCategory.Refresh();
                        MessageBox.Show("Saved");

                    }
                    else
                    {
                        MessageBox.Show("Error when editing Category");
                    }
                }
                else
                {
                    MessageBox.Show("Please inset category name and category id");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategory.TextLength != 0 && txtCategoryID.TextLength != 0)
                {
                    string ID = txtCategoryID.Text;
                    if (MessageBox.Show("You sure want to delete this Category, all the products in this category will be deleted also?", "Confirm message",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (CategoryDT.deleteCategory(ID))
                        {
                            MessageBox.Show("Category Deleted");
                            lbStatusCate.Text = "Deleted";
                            LoadCategory();
                        }
                        else
                        {
                            MessageBox.Show("Delete fail.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please inset category id");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbStatusCate.Text.Equals("Deleted"))
                {
                    MessageBox.Show("This Category is unusable");
                }
                else
                {
                    if (txtCategoryID.TextLength != 0 && txtName.TextLength != 0 && txtPrice.TextLength != 0 &&
                    txtProductID.TextLength != 0 && txtQuantity.TextLength != 0)
                    {
                        double f;
                        int i;
                        if (double.TryParse(txtPrice.Text, out f) && int.TryParse(txtQuantity.Text, out i))
                        {

                            int quantity = Int32.Parse(txtQuantity.Text);
                            double price = Double.Parse(txtPrice.Text);
                            bool chkNum = true;
                            if (quantity < 0)
                            {
                                chkNum = false;
                                MessageBox.Show("Quantity must be positive");
                                return;
                            }
                            if (price <= 0)
                            {
                                chkNum = false;
                                MessageBox.Show("Price must be bigger than 0");
                                return;
                            }
                            if (chkNum)
                            {
                                string cateId = txtCategoryID.Text;
                                string proID = txtProductID.Text;
                                string proName = txtName.Text;
                                if (productData.AddNewProduct(new Product(cateId, proID, proName, price, quantity)))
                                {
                                    dtProduct.RejectChanges();

                                    dtProduct.Rows.Add(proID, proName, quantity, price);

                                    dtProduct.AcceptChanges();
                                    MessageBox.Show("New Product added!");
                                    lbStatusProduct.Text = "";
                                    LoadProduct();
                                }
                                else
                                {
                                    MessageBox.Show("Error when adding product !");

                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Quantity and Price must be number!!");

                        }

                    }
                    else
                    {
                        MessageBox.Show("You left some textfield blank!!! ");

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    MessageBox.Show("This productID is already existed");

                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (lbStatusCate.Text.Equals("Deleted"))
            {
                MessageBox.Show("This Category is unusable");
            }
            else
            {
                lbStatusProduct.Text = "";
                btnAdd.Enabled = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnAddCategory.Enabled = false;
                btnEdit.Enabled = false;

                txtProductID.DataBindings.Clear();
                txtName.DataBindings.Clear();
                txtPrice.DataBindings.Clear();
                txtQuantity.DataBindings.Clear();

                txtCategory.Enabled = false;
                txtCategoryID.Enabled = false;
                txtProductID.Enabled = true;
                txtProductID.Text = "";
                txtName.Text = "";
                txtName.Enabled = true; ;


                txtPrice.Text = "";
                txtPrice.Enabled = true;
                txtProductID.Text = "";
                txtQuantity.Text = "";
                txtQuantity.Enabled = true;
                txtSearch.Text = "";
            }
        }

        public void selectProductFromSearch()
        {
            try
            {
                txtProductID.DataBindings.Clear();
                txtName.DataBindings.Clear();
                txtPrice.DataBindings.Clear();
                txtQuantity.DataBindings.Clear();

                txtProductID.DataBindings.Add("Text", dtProduct, "ID");
                txtName.DataBindings.Add("Text", dtProduct, "Name");
                txtQuantity.DataBindings.Add("Text", dtProduct, "Quantity");
                txtPrice.DataBindings.Add("Text", dtProduct, "Price");


                dgvProduct.DataSource = dtProduct;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lbStatusCate.Text.Equals("Deleted") || lbStatusProduct.Text.Equals("Deleted"))
            {
                MessageBox.Show("User can't update Product when its category or status has been deleted");
            }
            else
            {
                if (txtCategoryID.TextLength != 0 && txtName.TextLength != 0 && txtPrice.TextLength != 0 &&
                txtProductID.TextLength != 0 && txtQuantity.TextLength != 0)
                {
                    double f;
                    int i;
                    if (double.TryParse(txtPrice.Text, out f) && int.TryParse(txtQuantity.Text, out i))
                    {
                        int quantity = Int32.Parse(txtQuantity.Text);
                        double price = double.Parse(txtPrice.Text);
                        bool chkNum = true;
                        if (quantity < 0)
                        {
                            chkNum = false;
                            MessageBox.Show("Quantity must be positive");
                            return;
                        }
                        if (price <= 0)
                        {
                            chkNum = false;
                            MessageBox.Show("Price must be bigger than 0");
                            return;
                        }
                        if (chkNum)
                        {
                            
                                string proID = txtProductID.Text;
                                string proName = txtName.Text;
                                if (productData.UpdateProduct(new Product(proID, proName, price, quantity)))
                                {
                                    DataRow r = dtProduct.Rows.Find(proID);
                                    r["ID"] = proID;
                                    r["Name"] = proName;
                                    r["Quantity"] = quantity;
                                    r["Price"] = price;
                                    dgvProduct.Refresh();
                                    MessageBox.Show("Product has been edited!");
                                }
                                else
                                {
                                    MessageBox.Show("Error when editing product !");

                                }
                            
                        }

                    }
                    else
                    {
                        MessageBox.Show("Quantity and Price must be number!!");

                    }
                }
                else
                {
                    MessageBox.Show("You leave some textfield blank!!! ");

                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCategoryID.TextLength != 0 && txtName.TextLength != 0 && txtPrice.TextLength != 0 &&
               txtProductID.TextLength != 0 && txtQuantity.TextLength != 0)
            {
                if (MessageBox.Show("You sure want to delete this product?", "Confirm message",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    string proID = txtProductID.Text;
                    string cateID = txtCategoryID.Text;
                    if (productData.DeleteProduct(proID, cateID))
                    {

                        MessageBox.Show("Product Deleted");
                        lbStatusProduct.Text = "Deleted";
                        getProductFromCategory(txtCategoryID.Text);
                    }
                    else
                    {
                        MessageBox.Show("Delete fail.");
                    }
                }
            }
            else
            {
                MessageBox.Show("You leave some textfield blank!!! ");

            }
        }



        private void btnRestoreCate_Click(object sender, EventArgs e)
        {
            if (txtCategoryID.Text.Length != 0)
            {
                string cateID = txtCategoryID.Text;
                if (CategoryDT.restoreCategory(cateID))
                {
                    MessageBox.Show("Restored successfully");
                    lbStatusCate.Text = "";
                    LoadCategory();
                }
                else
                {
                    MessageBox.Show("Failed to restore Category");

                }
            }
            else
            {
                MessageBox.Show("Please insert CategoryID you want to restore");
            }
        }

        private void btnRestoreProduct_Click(object sender, EventArgs e)
        {
            if (txtProductID.Text.Length != 0)
            {
                string proID = txtProductID.Text;
                if (!lbStatusCate.Text.Equals("Deleted"))
                {
                    if (productData.RestoreProduct(proID))
                    {

                        MessageBox.Show("Restored successfully");
                        getProductFromCategory(txtCategoryID.Text);

                    }
                    else
                    {
                        MessageBox.Show("Failed to restore Product");

                    }
                }
                else
                {
                    MessageBox.Show("You need to restore this Product's category first!!!");
                }

            }
            else
            {
                MessageBox.Show("Please insert ProductID you want to restore");

            }
        }
       
    }
}