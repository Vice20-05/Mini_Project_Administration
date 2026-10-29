using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_Project_Administration
{
    // New in Project 2: product category managed by EF (Id, Name)
    [Table("ProductCategories")]
    public class ProductCategory
    {
        // primary key (auto-increment)
        public int Id { get; set; }

        // category name, max 50 characters
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        // all products in this category (navigation property, one category -> many products)
        public virtual ICollection<Product> Products { get; set; }

        // the ComboBoxes show this text
        public override string ToString()
        {
            return Name;
        }
    }

    // Product from Lab 5 + the new CategoryId field
    [Table("Products")]
    public class Product
    {
        // unique identifier (primary key, auto-increment)
        public int Id { get; set; }

        // name, max 30 characters
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        // description, max 500 characters
        [MaxLength(500)]
        public string Description { get; set; }

        // date the product entered the shop
        public DateTime EntryDate { get; set; }

        // expiration date
        public DateTime ExpirationDate { get; set; }

        // available quantity
        public int Quantity { get; set; }

        // New in Project 2: foreign key to ProductCategory
        public int CategoryId { get; set; }

        // navigation property, EF fills it when we use Include(p => p.Category)
        [ForeignKey("CategoryId")]
        public virtual ProductCategory Category { get; set; }

        // only for display in the grid, not stored in the database
        [NotMapped]
        public string CategoryName
        {
            get { return Category == null ? "" : Category.Name; }
        }
    }

    // New in Project 2: one row for every sale (Id, ProductId, Quantity)
    [Table("SalesHistory")]
    public class SalesHistory
    {
        // primary key (auto-increment)
        public int Id { get; set; }

        // foreign key to the sold product
        public int ProductId { get; set; }

        // how many pieces were sold
        public int Quantity { get; set; }

        // navigation property to the sold product (and through it to the category)
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }

    // one row in the "Sales history" grid (product and category names instead of only Ids)
    public class SaleRow
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }

    // one row in the "Categories" grid (category + how many products it has)
    public class CategoryRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
}
