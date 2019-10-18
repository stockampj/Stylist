using Microsoft.EntityFrameworkCore;

namespace Style.Models
{
  public class StyleContext : DbContext
  {
    public virtual DbSet<Stylist> Stylists { get; set; }
    public DbSet<Client> Clients { get; set; }
    
    public StyleContext(DbContextOptions options) : base(options) { }
  }
}