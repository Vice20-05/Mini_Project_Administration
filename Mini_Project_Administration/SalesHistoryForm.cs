using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mini_Project_Administration
{
    // New in Project 2: shows the sales history, every sale with its product AND category
    public partial class SalesHistoryForm : Form
    {
        // true while we fill the ComboBox, so SelectedIndexChanged doesn't reload the grid
        private bool fillingCategoryFilter = false;

        public SalesHistoryForm()
        {
            InitializeComponent();
        }

        private void SalesHistoryForm_Load(object sender, EventArgs e)
        {
            LoadSales(true);
        }

        // the result of one load: sales rows + the categories for the ComboBox
        private class SalesResult
        {
            public List<SaleRow> Rows;
            public List<ProductCategory> Categories;
        }

        // Loads the sales on the Thread Pool (DbTask), optionally filtered by category
        private void LoadSales(bool reloadCategories)
        {
            // read the UI here, before leaving the GUI thread
            ProductCategory selected = cboCategory.SelectedItem as ProductCategory;
            int categoryId = selected == null ? 0 : selected.Id;
            lblStatus.Text = "Loading...";

            DbTask.Run(this, db =>
            {
                SalesResult result = new SalesResult();

                // SalesHistory -> Product -> ProductCategory, EF makes the JOINs from the navigation properties
                result.Rows = (from s in db.Sales
                               where categoryId == 0 || s.Product.CategoryId == categoryId
                               orderby s.Id descending
                               select new SaleRow
                               {
                                   SaleId = s.Id,
                                   ProductId = s.ProductId,
                                   ProductName = s.Product.Name,
                                   Category = s.Product.Category.Name,
                                   Quantity = s.Quantity
                               }).ToList();

                if (reloadCategories)
                    result.Categories = db.Categories.OrderBy(c => c.Name).ToList();
                return result;
            },
            (result, error) =>
            {
                if (error != null)
                {
                    lblStatus.Text = "Error: " + error;
                    return;
                }
                if (result.Categories != null)
                    FillCategoryFilter(result.Categories);

                dataGridView1.DataSource = result.Rows;
                dataGridView1.Columns["SaleId"].HeaderText = "Sale Id";
                dataGridView1.Columns["ProductId"].HeaderText = "Product Id";
                dataGridView1.Columns["ProductName"].HeaderText = "Product";

                lblStatus.Text = result.Rows.Count + " sales, " + result.Rows.Sum(r => r.Quantity) + " pieces sold";
            });
        }

        // "(All categories)" + every category
        private void FillCategoryFilter(List<ProductCategory> categories)
        {
            fillingCategoryFilter = true;
            cboCategory.Items.Clear();
            cboCategory.Items.Add(new ProductCategory { Id = 0, Name = "(All categories)" });
            foreach (ProductCategory c in categories)
                cboCategory.Items.Add(c);
            cboCategory.SelectedIndex = 0;
            fillingCategoryFilter = false;
        }

        // choosing a category shows only the sales of the products in that category
        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!fillingCategoryFilter)
                LoadSales(false);
        }
    }
}
