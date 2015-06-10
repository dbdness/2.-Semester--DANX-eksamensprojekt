namespace ApiDanx
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            base.Configuration.AutoDetectChangesEnabled = false;
        }

        public virtual DbSet<LoggedInEmployee> LoggedInEmployees { get; set; }
        public virtual DbSet<AdminEmployee> AdminEmployees { get; set; }
        public virtual DbSet<StandardEmployee> StandardEmployees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoggedInEmployee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AdminEmployee>()
                .Property(e => e.Manager)
                .IsUnicode(false);

            modelBuilder.Entity<AdminEmployee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<StandardEmployee>()
                .Property(e => e.Manager)
                .IsUnicode(false);

            modelBuilder.Entity<StandardEmployee>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
