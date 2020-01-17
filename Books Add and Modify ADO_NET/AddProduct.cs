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
    public partial class FRMAddProduct : Form
    {
        public FRMAddProduct()
        {
            InitializeComponent();
        }

        //create an addproduct boolean to identify if user selected to add a product
        public bool addProduct;

        //use the product object
        public product product;

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if(IsValidData())
            {
                if(addProduct)
                {
                    product = new product();
                    this.PutProductData(product);
                    try
                    {
                        if (!ProductDB.AddProduct(product))
                        {
                            MessageBox.Show("A Product with that code already exists.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else {
                            this.DialogResult = DialogResult.OK;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    product newProduct = new product();
                    this.PutProductData(newProduct);
                    try
                    {
                        if(!ProductDB.UpdateProduct(product,newProduct))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that product.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            product = newProduct;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
        }


        private void FRMAddProduct_Load(object sender, EventArgs e)
        {
            if (addProduct)
            {
                //change text and controls to an add product process
                this.Text = "Add Product";
                txtProductCode.ReadOnly = false;
                txtProductCode.TabStop = true;
                txtProductCode.Focus();

            }
            else
            {
                //change text and settings for a modify process.
                this.Text = "Modify Product";
                txtProductCode.ReadOnly = true;
                txtProductCode.TabStop = false;
                txtDescription.Focus();
                this.DisplayProduct();
            }
        }

        private void DisplayProduct()
        {
            //put values of the objects properties into the text boxes.
            txtProductCode.Text = product.Code;
            txtDescription.Text = product.Description;
            txtPrice.Text = product.Price.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //close this form.
            this.Close();
        }

        private bool IsValidData()
        {
            return
                Validator.IsPresent(txtProductCode) &&
                Validator.IsPresent(txtDescription) &&
                Validator.IsPresent(txtPrice) &&
                Validator.IsDecimal(txtPrice);
        }

        private void PutProductData(product product)
        {
            product.Code = txtProductCode.Text;
            product.Description = txtDescription.Text;
            product.Price = Convert.ToDecimal(txtPrice.Text);

        }
    }
}
