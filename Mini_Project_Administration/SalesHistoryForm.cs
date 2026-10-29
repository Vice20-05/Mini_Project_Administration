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
            public List<RandVanzare> Rows;
            public List<CategorieProdus> Categories;
        }

        // Loads the sales on the Thread Pool (DbTask), optionally filtered by category
        private void LoadSales(bool reloadCategories)
        {
            // read the UI here, before leaving the GUI thread
            CategorieProdus selected = cboCategory.SelectedItem as CategorieProdus;
            int categoryId = selected == null ? 0 : selected.id;
            lblStatus.Text = "Loading...";

            DbTask.Run(this, db =>
            {
                SalesResult result = new SalesResult();

                // IstoricVanzari -> Product -> CategorieProdus, EF makes the JOINs from the navigation properties
                result.Rows = (from s in db.Sales
                               where categoryId == 0 || s.Product.idCategorie == categoryId
                               orderby s.id descending
                               select new RandVanzare
                               {
                                   idVanzare = s.id,
                                   idProdus = s.idProdus,
                                   denumireProdus = s.Product.denumire,
                                   categorie = s.Product.Category.denumire,
                                   cantitate = s.cantitate
                               }).ToList();

                if (reloadCategories)
                    result.Categories = db.Categories.OrderBy(c => c.denumire).ToList();
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
                dataGridView1.Columns["idVanzare"].HeaderText = "Sale Id";
                dataGridView1.Columns["idProdus"].HeaderText = "Product Id";
                dataGridView1.Columns["denumireProdus"].HeaderText = "Product";
                dataGridView1.Columns["categorie"].HeaderText = "Category";
                dataGridView1.Columns["cantitate"].HeaderText = "Quantity";

                lblStatus.Text = result.Rows.Count + " sales, " + result.Rows.Sum(r => r.cantitate) + " pieces sold";
            });
        }

        // "(All categories)" + every category
        private void FillCategoryFilter(List<CategorieProdus> categories)
        {
            fillingCategoryFilter = true;
            cboCategory.Items.Clear();
            cboCategory.Items.Add(new CategorieProdus { id = 0, denumire = "(All categories)" });
            foreach (CategorieProdus c in categories)
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
