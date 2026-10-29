using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_Project_Administration
{
    // New in Project 2: product category managed by EF (Id, Name)
    [Table("ProductCategories")]
    public class CategorieProdus
    {
        // primary key (auto-increment)
        public int id { get; set; }

        // category name, max 50 characters
        [Required]
        [MaxLength(50)]
        public string denumire { get; set; }

        // all products in this category (navigation property, one category -> many products)
        public virtual ICollection<Produse> Products { get; set; }

        // the ComboBoxes show this text
        public override string ToString()
        {
            return denumire;
        }
    }

    // Produse from Lab 5 + the new idCategorie field
    [Table("Products")]
    public class Produse
    {
        // unique identifier (primary key, auto-increment)
        public int id { get; set; }

        // name, max 30 characters
        [Required]
        [MaxLength(30)]
        public string denumire { get; set; }

        // description, max 500 characters
        [MaxLength(500)]
        public string Description { get; set; }

        // date the product entered the shop
        public DateTime EntryDate { get; set; }

        // expiration date
        public DateTime ExpirationDate { get; set; }

        // available quantity
        public int Quantity { get; set; }

        // New in Project 2: foreign key to CategorieProdus
        public int idCategorie { get; set; }

        // navigation property, EF fills it when we use Include(p => p.Category)
        [ForeignKey("idCategorie")]
        public virtual CategorieProdus Category { get; set; }

        // only for display in the grid, not stored in the database
        [NotMapped]
        public string CategoryName
        {
            get { return Category == null ? "" : Category.denumire; }
        }
    }

    // New in Project 2: one row for every sale (id, idProdus, cantitate)
    [Table("IstoricVanzari")]
    public class IstoricVanzari
    {
        // primary key (auto-increment)
        public int id { get; set; }

        // foreign key to the sold product
        public int idProdus { get; set; }

        // how many pieces were sold
        public int cantitate { get; set; }

        // navigation property to the sold product (and through it to the category)
        [ForeignKey("idProdus")]
        public virtual Produse Product { get; set; }
    }

    // one row in the "Sales history" grid (product and category names instead of only Ids)
    public class RandVanzare
    {
        public int idVanzare { get; set; }
        public int idProdus { get; set; }
        public string denumireProdus { get; set; }
        public string categorie { get; set; }
        public int cantitate { get; set; }
    }

    // one row in the "Categories" grid (category + how many products it has)
    public class RandCategorie
    {
        public int id { get; set; }
        public string denumire { get; set; }
        public int numarProduse { get; set; }
    }
}
