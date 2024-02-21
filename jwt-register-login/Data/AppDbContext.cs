using jwt_register_login.Models;
using Microsoft.EntityFrameworkCore;

namespace jwt_register_login.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
}
