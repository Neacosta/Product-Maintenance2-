using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Books_Add_and_Modify_ADO_NET
{
    public partial class FrmMaintenance : Form
    {
        public FrmMaintenance()
        {
            InitializeComponent();
        }

        //create a p object.
        private product p;

        private void btnGet_Click(object sender, EventArgs e)
        {
            //Get value from textbox
            string strCode = txtProductCode.Text;

            //pass value to GetProduct Class
            GetProduct(strCode);

            //if no product is available
            if (p == null)
            {
                //display error message to user.
                MessageBox.Show("No Product found with this code. " +
                    "Please try again.", "Product Not Found");
                //clear out all controls.
                this.ClearControls();
            }
            else
            {
                //display product
                this.DisplayProduct();
            }

        }
        private void GetProduct(string code)
        {
            //set p object from the product class to be populated.
            try
            {
                p = ProductDB.GetProduct(code);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void ClearControls()
        {
            txtDescription.Text = "";
            txtProductCode.Text = "";
            txtPrice.Text = "";
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            txtProductCode.Focus();

        }
        private void DisplayProduct()
        {
            txtDescription.Text = p.Description;
            txtPrice.Text = p.Price.ToString("c2");
            btnModify.Enabled = true;
            btnDelete.Enabled = true;


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //create new form object
            FRMAddProduct addProductForm = new FRMAddProduct();
            addProductForm.addProduct = true;
            DialogResult result = addProductForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                p = addProductForm.product;
                txtProductCode.Text = p.Code;
                this.DisplayProduct();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            FRMAddProduct modifyProductForm = new FRMAddProduct();
            modifyProductForm.addProduct = false;
            modifyProductForm.product = p;

            DialogResult result = modifyProductForm.ShowDialog();

            if(result == DialogResult.OK)
            {
                p = modifyProductForm.product;
                this.DisplayProduct();
            }

            else if (result == DialogResult.Retry)
            {
                this.GetProduct(p.Code);
                if (p != null)
                {
                    this.DisplayProduct();
                }
                else
                    this.ClearControls();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //message requesting approval to delete fromt he user
            DialogResult result = MessageBox.Show("Delete " + p.Code.Trim() + " ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //if the user still wants to delete the row go ahead.
            if (result == DialogResult.Yes)
            {
                try
                {
                    //the row disapeared between the user selecting delete and this point in time.
                    if (!ProductDB.DeleteProduct(p))
                    {
                        //Display a message that alerts when a row was already altered and cant be deleted.
                        MessageBox.Show("Another user has updated or deleted " + "that product.", "Database Error");
                        if (p != null)
                        {
                            //Run the Display Product Method
                            this.DisplayProduct();
                        }
                        else
                            //empty controls
                            this.ClearControls();
                    }
                    else
                        //empty controls
                        this.ClearControls();
                }
                catch (Exception ex)
                {
                    //show exception
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //close the application
            this.Close();
        }
    }
}
