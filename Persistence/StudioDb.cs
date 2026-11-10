using Cifrofoto.Coursework.Models;
using Microsoft.EntityFrameworkCore;

namespace Cifrofoto.Coursework.Persistence;

public class StudioDb(DbContextOptions<StudioDb> options) : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasIndex(client => client.Phone).IsUnique();
    }
}
