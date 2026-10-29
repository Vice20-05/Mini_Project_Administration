using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Mini_Project_Administration
{
    // Main window: product list (DataGridView) + menu (Add / Sell / Search / Categories / Sales history)
    public partial class Form1 : Form
    {
        // categories from the last database load (used by the filter ComboBox and by AddForm)
        private List<ProductCategory> categories = new List<ProductCategory>();

        // true while we refill the category ComboBox, so its SelectedIndexChanged doesn't reload the grid
        private bool fillingCategoryFilter = false;

        public Form1()
        {
            InitializeComponent();
        }

        // create the tables (first start only) and load everything when the window opens
        private void Form1_Load(object sender, EventArgs e)
        {
            RunDbTask(db =>
            {
                db.CreateTables();
                return "Products loaded";
            });
        }

        // the result of one database operation: message + the fresh data for the grid and the ComboBox
        private class LoadResult
        {
            public string Message;
            public List<Product> Products;
            public List<ProductCategory> Categories;
        }

        // Runs dbWork on the Thread Pool (through DbTask), then reloads products + categories,
        // and finally shows everything on the GUI thread.
        private void RunDbTask(Func<ShopDbContext, string> dbWork)
        {
            // read the UI here, before leaving the GUI thread
            string filter = txtSearch.Text.Trim();
            int categoryId = SelectedFilterCategoryId();
            lblStatus.Text = "Working...";

            DbTask.Run(this, db =>
            {
                LoadResult result = new LoadResult();
                result.Message = dbWork(db);                        // the actual operation (add / sell / nothing)
                result.Products = LoadProducts(db, filter, categoryId); // then reload the grid content
                result.Categories = db.Categories.OrderBy(c => c.Name).ToList();
                return result;
            },
            (result, error) =>
            {
                if (error != null)
                    lblStatus.Text = "Error: " + error;
                else
                    ShowResult(result);
            });
        }

        // Search = filtering what the grid shows (by name, description or category name)
        // + optional filter by the category chosen in the ComboBox (0 = all categories)
        // (everything is compared in lower case, because Contains() is case-sensitive in SQLite)
        private List<Product> LoadProducts(ShopDbContext db, string filter, int categoryId)
        {
            filter = filter.ToLower();
            var query = from p in db.Products.Include(p => p.Category)
                        where (filter == "" || p.Name.ToLower().Contains(filter)
                               || p.Description.ToLower().Contains(filter)
                               || p.Category.Name.ToLower().Contains(filter))
                           && (categoryId == 0 || p.CategoryId == categoryId)
                        orderby p.Id
                        select p;
            return query.ToList();
        }

        // runs on the GUI thread
        private void ShowResult(LoadResult result)
        {
            categories = result.Categories;
            FillCategoryFilter();

            dataGridView1.DataSource = result.Products;

            // hide the technical columns, the grid shows CategoryName instead
            if (dataGridView1.Columns["Category"] != null)
                dataGridView1.Columns["Category"].Visible = false;
            if (dataGridView1.Columns["CategoryId"] != null)
                dataGridView1.Columns["CategoryId"].Visible = false;
            if (dataGridView1.Columns["CategoryName"] != null)
                dataGridView1.Columns["CategoryName"].HeaderText = "Category";

            lblStatus.Text = result.Message + "  (" + result.Products.Count + " products shown)";
        }

        // refills the filter ComboBox: "(All categories)" + every category, keeping the current choice
        private void FillCategoryFilter()
        {
            int selectedId = SelectedFilterCategoryId();
            fillingCategoryFilter = true;

            cboCategoryFilter.Items.Clear();
            cboCategoryFilter.Items.Add(new ProductCategory { Id = 0, Name = "(All categories)" });
            int indexToSelect = 0;
            foreach (ProductCategory c in categories)
            {
                int index = cboCategoryFilter.Items.Add(c);
                if (c.Id == selectedId)
                    indexToSelect = index;
            }
            cboCategoryFilter.SelectedIndex = indexToSelect;

            fillingCategoryFilter = false;
        }

        // Id of the category chosen in the filter ComboBox (0 = all)
        private int SelectedFilterCategoryId()
        {
            ProductCategory selected = cboCategoryFilter.SelectedItem as ProductCategory;
            return selected == null ? 0 : selected.Id;
        }

        // choosing another category in the ComboBox filters the grid right away
        private void cboCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fillingCategoryFilter)
                return;
            // read the text on the GUI thread, the lambda below runs on a worker thread
            string categoryName = cboCategoryFilter.Text;
            RunDbTask(db => "Filtered by category: " + categoryName);
        }

        // Refresh button, clears the search + category filter and shows everything
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            fillingCategoryFilter = true;
            if (cboCategoryFilter.Items.Count > 0)
                cboCategoryFilter.SelectedIndex = 0;
            fillingCategoryFilter = false;
            RunDbTask(db => "Refreshed");
        }

        // Menu: Search products -> applies the text from the search box as a filter
        private void searchProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text; // read on the GUI thread
            RunDbTask(db => "Search: \"" + searchText + "\"");
        }

        // ENTER in the search box does the same as the menu item
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                searchProductsToolStripMenuItem_Click(sender, e);
            }
        }

        // Add products -> New product (now with a category)
        private void newProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // a product must belong to a category, so at least one category is needed
            if (categories.Count == 0)
            {
                MessageBox.Show("There are no categories yet. Create one first (menu Categories).");
                return;
            }

            AddForm form = new AddForm(false, categories, 0);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            Product newProduct = form.NewProduct;
            RunDbTask(db =>
            {
                db.Products.Add(newProduct);
                db.SaveChanges();
                return "Added: " + newProduct.Name;
            });
        }

        // Add products -> Quantity to existing product (Id is pre-filled with the selected row)
        private void existingProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedId = 0;
            if (dataGridView1.CurrentRow != null)
                selectedId = ((Product)dataGridView1.CurrentRow.DataBoundItem).Id;

            AddForm form = new AddForm(true, categories, selectedId);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            int id = form.ProductId;
            int quantity = form.Quantity;
            RunDbTask(db =>
            {
                Product product = db.Products.Find(id);
                if (product == null)
                    return "No product with Id " + id;

                product.Quantity += quantity;
                db.SaveChanges();
                return product.Name + " quantity is now " + product.Quantity;
            });
        }

        // double click on a row opens the sell form for that product
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return; // header was clicked

            Product selected = (Product)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            SellProduct(selected);
        }

        // Menu: Sell products -> sells the currently selected row
        private void sellProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select a product in the table first (or double click it).");
                return;
            }
            SellProduct((Product)dataGridView1.CurrentRow.DataBoundItem);
        }

        // Sells a product: lowers its quantity and writes a row into SalesHistory
        private void SellProduct(Product selected)
        {
            if (selected.Quantity == 0)
            {
                MessageBox.Show(selected.Name + " is sold out. Add quantity first (Add products -> Quantity to existing product).");
                return;
            }

            SellForm form = new SellForm(selected);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            int id = selected.Id;
            int sold = form.SoldQuantity;
            RunDbTask(db =>
            {
                Product product = db.Products.Find(id);
                if (product == null)
                    return "Product no longer exists";
                if (sold > product.Quantity)
                    return "Only " + product.Quantity + " left";

                product.Quantity -= sold;

                // New in Project 2: remember the sale in the history
                db.Sales.Add(new SalesHistory { ProductId = id, Quantity = sold });
                db.SaveChanges();

                // The product is NOT deleted at 0 (like in Lab 5), because the sales history
                // points to it and has to show its name and category.
                string message = "Sold " + sold + " x " + product.Name;
                if (product.Quantity == 0)
                    message += " (sold out)";
                return message;
            });
        }

        // Menu: Categories -> add / rename / delete categories, then reload (names may have changed)
        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryForm form = new CategoryForm();
            form.ShowDialog();
            RunDbTask(db => "Categories updated");
        }

        // Menu: Sales history -> list of every sale with product and category
        private void salesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesHistoryForm form = new SalesHistoryForm();
            form.ShowDialog();
        }
    }
}
