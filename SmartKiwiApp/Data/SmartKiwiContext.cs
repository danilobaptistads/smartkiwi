using SmartKiwiApp.Models;
using Microsoft.EntityFrameworkCore;
using  Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmartKiwiApp.Data;
public class SmartKiwiContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
    public DbSet<Call> Calls { get; set; }  
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientQueue> ClientQueues { get; set; }
    public DbSet<Workstation> Workstations { get; set; }
    public SmartKiwiContext(DbContextOptions<SmartKiwiContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }
}