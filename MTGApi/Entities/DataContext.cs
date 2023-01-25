using Microsoft.EntityFrameworkCore;

namespace MTGApi.Entities
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Deck>  Decks { get; set; } 

        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("DataBaseConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasMany(acc => acc.Decks)
                .WithOne(d => d.Account)
                .HasForeignKey(d => d.AccountId);
        }
    }
}
