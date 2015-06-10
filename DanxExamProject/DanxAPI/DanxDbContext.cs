namespace DanxAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DanxDbContext : DbContext
    {
        public DanxDbContext()
            : base("name=EmployeeDbContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<AdminEmployee> AdminEmployee { get; set; }
        public virtual DbSet<LoggedInEmployee> LoggedInEmployee { get; set; }
        public virtual DbSet<StandardEmployee> StandardEmployee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminEmployee>()
                .Property(e => e.Manager)
                .IsUnicode(false);

            modelBuilder.Entity<AdminEmployee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AdminEmployee>()
                .Property(e => e.StdHours)
                .HasPrecision(2);

            modelBuilder.Entity<AdminEmployee>()
                .Property(e => e.WatchHours)
                .HasPrecision(2);

            modelBuilder.Entity<AdminEmployee>()
                .Property(e => e.TotalHours)
                .HasPrecision(2);

            modelBuilder.Entity<LoggedInEmployee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<LoggedInEmployee>()
                .Property(e => e.TotalHours)
                .HasPrecision(2);

            modelBuilder.Entity<StandardEmployee>()
                .Property(e => e.Manager)
                .IsUnicode(false);

            modelBuilder.Entity<StandardEmployee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<StandardEmployee>()
                .Property(e => e.StdHours)
                .HasPrecision(2);

            modelBuilder.Entity<StandardEmployee>()
                .Property(e => e.WatchHours)
                .HasPrecision(2);

            modelBuilder.Entity<StandardEmployee>()
                .Property(e => e.TotalHours)
                .HasPrecision(2);
        }
    }
}
