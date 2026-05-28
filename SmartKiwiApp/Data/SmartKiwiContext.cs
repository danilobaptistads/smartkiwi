using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Models;

namespace SmartKiwiApp.Data;
public class SmartKiwiContext : DbContext
{
    public DbSet<Call> Calls { get; set; }  
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientQueue> ClientQueues { get; set; }
    public DbSet<Workstation> Workstations { get; set; }
    public SmartKiwiContext(DbContextOptions<SmartKiwiContext> options) : base(options)
    {
    }
    
}