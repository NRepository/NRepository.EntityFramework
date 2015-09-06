namespace NRepository.EntityFramework.Tests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class FamilyDbContext : DbContext
    {
        private const string DbConnectionName = "NRepositoryTestDb";

        public FamilyDbContext()
            : base(DbConnectionName)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FamilyDbContext, DbMigrationConfig>(DbConnectionName));
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = false;
        }

        static FamilyDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FamilyDbContext, DbMigrationConfig>(DbConnectionName));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Person> Persons { get; set; }

        public DbSet<Animal> Animal { get; set; }

        public DbSet<Rabitt> Rabbits { get; set; }

        public DbSet<Cat> Cats { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Item> Items { get; set; }

    }

    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsLargeItem { get; set; }
    }
}
