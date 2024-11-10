using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DistributedDBSolution.DAL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LabDbContext>
{
    public LabDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LabDbContext>();
        optionsBuilder.UseSqlServer("YourConnectionStringHere");

        return new LabDbContext(optionsBuilder.Options);
    }
}

}
