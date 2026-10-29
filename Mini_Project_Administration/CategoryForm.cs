using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mini_Project_Administration
{
    // New in Project 2: administration of product categories (add / rename / delete)
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        // load the categories when the form opens
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            RunCategoryTask(db => "Categories loaded");
        }

        // the result of one operation: message + the fresh category list
        private class CategoryResult
        {
            public string Message;
            public List<CategoryRow> Rows;
        }

        // Runs dbWork on the Thread Pool (DbTask), then reloads the category grid on the GUI thread
        private void RunCategoryTask(Func<ShopDbContext, string> dbWork)
        {
            lblStatus.Text = "Working...";
            SetButtonsEnabled(false); // no double clicks while the database is busy

            DbTask.Run(this, db =>
            {
                CategoryResult result = new CategoryResult();
                result.Message = dbWork(db);

                // every category + how many products it has
                result.Rows = (from c in db.Categories
                               orderby c.Name
                               select new CategoryRow
                               {
                                   Id = c.Id,
                                   Name = c.Name,
                                   ProductCount = c.Products.Count()
                               }).ToList();
                return result;
            },
            (result, error) =>
            {
                SetButtonsEnabled(true);
                if (error != null)
                {
                    lblStatus.Text = "Error: " + error;
                    return;
                }
                dataGridView1.DataSource = result.Rows;
                dataGridView1.Columns["ProductCount"].HeaderText = "Products";
                lblStatus.Text = result.Message;
            });
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnAdd.Enabled = btnRename.Enabled = btnDelete.Enabled = enabled;
        }

        // the category selected in the grid (null if nothing is selected)
        private CategoryRow SelectedCategory()
        {
            if (dataGridView1.CurrentRow == null)
                return null;
            return (CategoryRow)dataGridView1.CurrentRow.DataBoundItem;
        }

        // clicking a row copies its name into the text box (easier renaming)
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            CategoryRow selected = SelectedCategory();
            if (selected != null)
                txtName.Text = selected.Name;
        }

        // checks the name typed by the user, returns null if it is not OK
        private string ReadName()
        {
            string name = txtName.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("Type a category name.");
                return null;
            }
            return name;
        }

        // Add a new category
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = ReadName();
            if (name == null)
                return;

            RunCategoryTask(db =>
            {
                // no two categories with the same name
                string lowerName = name.ToLower();
                if (db.Categories.Any(c => c.Name.ToLower() == lowerName))
                    return "Category \"" + name + "\" already exists";

                db.Categories.Add(new ProductCategory { Name = name });
                db.SaveChanges();
                return "Added category: " + name;
            });
        }

        // Rename the selected category
        private void btnRename_Click(object sender, EventArgs e)
        {
            CategoryRow selected = SelectedCategory();
            if (selected == null)
            {
                MessageBox.Show("Select a category first.");
                return;
            }
            string newName = ReadName();
            if (newName == null)
                return;

            int id = selected.Id;
            RunCategoryTask(db =>
            {
                string lowerName = newName.ToLower();
                if (db.Categories.Any(c => c.Id != id && c.Name.ToLower() == lowerName))
                    return "Category \"" + newName + "\" already exists";

                ProductCategory category = db.Categories.Find(id);
                if (category == null)
                    return "Category no longer exists";

                category.Name = newName;
                db.SaveChanges();
                return "Renamed to: " + newName;
            });
        }

        // Delete the selected category (only if it has no products)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            CategoryRow selected = SelectedCategory();
            if (selected == null)
            {
                MessageBox.Show("Select a category first.");
                return;
            }
            if (MessageBox.Show("Delete category \"" + selected.Name + "\"?", "Delete",
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            int id = selected.Id;
            RunCategoryTask(db =>
            {
                // blocked: products (and through them the sales history) still use this category
                int productCount = db.Products.Count(p => p.CategoryId == id);
                if (productCount > 0)
                    return "Can't delete: the category still has " + productCount + " product(s)";

                ProductCategory category = db.Categories.Find(id);
                if (category == null)
                    return "Category no longer exists";

                db.Categories.Remove(category);
                db.SaveChanges();
                return "Deleted category: " + category.Name;
            });
        }
    }
}
