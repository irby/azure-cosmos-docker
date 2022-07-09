using Microsoft.EntityFrameworkCore;

namespace TestCosmosDocker;

public class CosmosContext : DbContext
{
    public static readonly string MyContainerName = "MyContainer";
    
    public CosmosContext(DbContextOptions<CosmosContext> options) : base(options)
    {
    }
    
    public virtual DbSet<MyModel> MyModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer(MyContainerName);
        modelBuilder.Entity<MyModel>().ToContainer(MyContainerName).HasPartitionKey(p => p.Id);
    }
}

public class MyModel
{
    public Guid Id { get; set; }
    public string Value { get; set; }
}