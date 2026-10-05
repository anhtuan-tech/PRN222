using Microsoft.EntityFrameworkCore;

public class MyStoreContext(DbContextOptions<MyStoreContext> options) : DbContext(options)
{
    public DbSet<BusinessObjects.Product> Product { get; set; } = default!;
}
