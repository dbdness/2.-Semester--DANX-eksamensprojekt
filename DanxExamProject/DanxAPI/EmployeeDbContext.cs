namespace DanxAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
            : base("name=EmployeeDbContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<LoggedInEmployee> LoggedInEmployees { get; set; }
        public virtual DbSet<MainEmployee> MainEmployees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoggedInEmployee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MainEmployee>()
                .Property(e => e.Manager)
                .IsUnicode(false);

            modelBuilder.Entity<MainEmployee>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
