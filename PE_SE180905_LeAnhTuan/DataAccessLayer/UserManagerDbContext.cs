using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class UserManagerDbContext : DbContext
    {
        public UserManagerDbContext() { }
        public UserManagerDbContext(DbContextOptions<UserManagerDbContext> options) : base(options) { }

        public virtual DbSet<UserEntity> UserEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=UserManager;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        // THÊM ĐOẠN ĐỔI TÊN MAPPING NÀY VÀO:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("UserEntity");
        }
    }
}