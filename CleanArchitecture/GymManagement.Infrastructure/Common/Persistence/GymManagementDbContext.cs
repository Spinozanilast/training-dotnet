using System.Reflection;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymManagementDbContext : DbContext, IUnitWork
{
    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public GymManagementDbContext(DbContextOptions options) : base(options)
    {
    }

    public async Task CommitChangesAsync()
    {

        await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}