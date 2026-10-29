using System;
using System.Windows.Forms;

namespace Mini_Project_Administration
{
    // Separate form for selling, opened by double click on a grid row
    public partial class SellForm : Form
    {
        // how many pieces the user wants to sell (read by Form1 after OK)
        public int SoldQuantity { get { return (int)numSell.Value; } }

        // the constructor receives the details of the selected record
        public SellForm(Product product)
        {
            InitializeComponent();

            lblDetails.Text =
                "Id: " + product.Id + "\n" +
                "Name: " + product.Name + "\n" +
                "Category: " + product.CategoryName + "\n" +
                "Description: " + product.Description + "\n" +
                "Entry date: " + product.EntryDate.ToShortDateString() + "\n" +
                "Expiration date: " + product.ExpirationDate.ToShortDateString() + "\n" +
                "Available: " + product.Quantity;

            // can't sell more than what is available
            numSell.Minimum = 1;
            numSell.Maximum = Math.Max(1, product.Quantity);
            numSell.Value = 1;
        }

        // Sell button sets the dialog result to OK, so Form1 can read the SoldQuantity property
        private void btnSell_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
