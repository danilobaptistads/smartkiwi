using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Models;

namespace SmartKiwiApp.Data;

public class SmartKiwiContext : DbContext
{
    public SmartKiwiContext(DbContextOptions<SmartKiwiContext> options) : base(options)
    {
    }

    public DbSet<Call> Calls { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Workstation> Workstations { get; set; }
}