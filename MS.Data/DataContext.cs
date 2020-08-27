namespace MS.Data
{
    using MS.Model.Entity;
    using System.Data.Entity;
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
            Database.SetInitializer<DataContext>(null);
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Trader> Traders { get; set; }
    }
}