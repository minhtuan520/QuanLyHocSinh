using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyHocSinh.DAL.Models.Account;

namespace QuanLyHocSinh.DAL.Context
{
    public class HocSinhSqlContext :IdentityDbContext<Account>
    {
        private IConfiguration _configuration;        
        private readonly string connectionString = "Server=.;Database=QuanLyHocSinh;Trusted_Connection=True;MultipleActiveResultSets=true";
        public HocSinhSqlContext(DbContextOptions<HocSinhSqlContext> options)
            : base(options)
        {
        }       
        protected HocSinhSqlContext()
        {
        }
        #region Set DBSet model
        public DbSet<Account> Accounts { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            OnBeforeSaving();
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>()
                .HasQueryFilter(model => EF.Property<bool>(model, "IsDisabled") == false).ToTable("Account");                        
            //builder.Entity<CenterLocation>().HasKey(key => new { key.latitude, key.longitude });
            //builder.Entity<CenterLocation>().HasOne(cl => cl.MapLocation).WithOne(ml => ml.Center);
            //builder.Entity<MapLocation>().HasOne(ml => ml.Center).WithOne(cl => cl.MapLocation);            
        }
        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDisabled"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDisabled"] = true;
                        break;
                }
            }
        }
    }
}
