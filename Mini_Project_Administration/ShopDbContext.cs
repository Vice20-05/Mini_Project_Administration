using System.Data.Entity;

namespace Mini_Project_Administration
{
    // The EF database context. The connection string "ShopDbContext" is in App.config
    // and points to the SQLite file shop.db.
    public class ShopDbContext : DbContext
    {
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesHistory> Sales { get; set; }

        static ShopDbContext()
        {
            // The SQLite EF6 provider can't create a database by itself (no Code First creation),
            // so we switch off the EF initializer and create the tables ourselves in CreateTables().
            Database.SetInitializer<ShopDbContext>(null);
        }

        public ShopDbContext() : base("name=ShopDbContext")
        {
            // no lazy loading: the context is closed after every operation,
            // so related data is loaded on purpose with Include()
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        // Creates the three tables if they don't exist yet (called once when the app starts).
        // The columns match the classes in Models.cs.
        public void CreateTables()
        {
            Database.ExecuteSqlCommand(
                @"CREATE TABLE IF NOT EXISTS ProductCategories (
                    Id   INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                  )");

            Database.ExecuteSqlCommand(
                @"CREATE TABLE IF NOT EXISTS Products (
                    Id             INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name           TEXT NOT NULL,
                    Description    TEXT,
                    EntryDate      DATETIME NOT NULL,
                    ExpirationDate DATETIME NOT NULL,
                    Quantity       INT NOT NULL,
                    CategoryId     INT NOT NULL REFERENCES ProductCategories(Id)
                  )");

            Database.ExecuteSqlCommand(
                @"CREATE TABLE IF NOT EXISTS SalesHistory (
                    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProductId INT NOT NULL REFERENCES Products(Id),
                    Quantity  INT NOT NULL
                  )");
        }
    }
}
