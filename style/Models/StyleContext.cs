using Microsoft.EntityFrameworkCore;

namespace Style.Models
{
  public class SpookyParkContext : DbContext
  {
    public virtual DbSet<Stylist> Stylists { get; set; }
    public DbSet<Client> Clients { get; set; }
    
    public StyleContext(DbContextOptions options) : base(options) { }
  }
}