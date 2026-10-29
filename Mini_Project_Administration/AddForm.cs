using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mini_Project_Administration
{
    // Separate form for adding products.
    // existingProduct = false -> new product (all fields + category)
    // existingProduct = true  -> only Id + quantity to add to an existing product
    public partial class AddForm : Form
    {
        private bool existingProduct;

        // results read by Form1 after ShowDialog() returns OK
        public Produse NewProduct { get; private set; }
        public int ProductId { get { return (int)numId.Value; } }
        public int Quantity { get { return (int)numQuantity.Value; } }

        // categories = list for the category ComboBox (loaded by Form1 from the database)
        // selectedProductId = Id pre-filled in "existing product" mode (0 = nothing selected)
        public AddForm(bool existingProduct, List<CategorieProdus> categories, int selectedProductId)
        {
            InitializeComponent();
            this.existingProduct = existingProduct;

            if (existingProduct)
            {
                Text = "Add quantity to existing product";
                // hide the fields of a new product, keep only Quantity + Id
                lblName.Visible = txtName.Visible = false;
                lblDescription.Visible = txtDescription.Visible = false;
                lblEntryDate.Visible = dtpEntryDate.Visible = false;
                lblExpiration.Visible = dtpExpiration.Visible = false;
                lblCategory.Visible = cboCategory.Visible = false;
                btnOK.Top = btnCancel.Top = 80;
                ClientSize = new System.Drawing.Size(ClientSize.Width, 115);

                if (selectedProductId > 0)
                    numId.Value = selectedProductId;
            }
            else
            {
                Text = "New product";
                lblId.Visible = numId.Visible = false;
                dtpExpiration.Value = DateTime.Today.AddMonths(1);

                // fill the category ComboBox (it shows CategorieProdus.ToString() = Name)
                foreach (CategorieProdus c in categories)
                    cboCategory.Items.Add(c);
                if (cboCategory.Items.Count > 0)
                    cboCategory.SelectedIndex = 0;
            }
        }

        // OK button: validates the input and creates the new Produse object if necessary
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!existingProduct)
            {
                // simple validation before creating the object
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("The name is required.");
                    return;
                }
                if (dtpExpiration.Value.Date < dtpEntryDate.Value.Date)
                {
                    MessageBox.Show("The expiration date can't be before the entry date.");
                    return;
                }
                CategorieProdus category = cboCategory.SelectedItem as CategorieProdus;
                if (category == null)
                {
                    MessageBox.Show("Choose a category.");
                    return;
                }

                NewProduct = new Produse
                {
                    denumire = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    EntryDate = dtpEntryDate.Value.Date,
                    ExpirationDate = dtpExpiration.Value.Date,
                    Quantity = Quantity,
                    idCategorie = category.id // only the Id is saved, not the category object
                };
            }

            DialogResult = DialogResult.OK; // closes the dialog
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }
    }
}
