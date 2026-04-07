using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Models;

namespace SmartKiwiApp.Data;

public class SmartKiwiContextInMemory : DbContext
{
    public SmartKiwiContextInMemory(DbContextOptions<SmartKiwiContextInMemory> options) : base(options)
    {
    }

    public DbSet<Call> Calls { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Workstation> Workstations { get; set; }
}