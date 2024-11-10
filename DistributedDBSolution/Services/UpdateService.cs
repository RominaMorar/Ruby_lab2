using System.Transactions;
using DistributedDBSolution.DAL;
using DistributedDBSolution.Services;

public class UpdateService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UpdateService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Create a scope for service dependencies
        await using (var scope = _serviceScopeFactory.CreateAsyncScope())
        {
            // Get required services from the scope
            var mongoService = scope.ServiceProvider.GetRequiredService<StudentService>();
            var dbContext = scope.ServiceProvider.GetRequiredService<LabDbContext>();

            // Use a transaction for the database operations
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Retrieve students from the DbContext
                    var students = dbContext.Students.ToList();

                    // Iterate over the students and insert them into MongoDB
                    foreach (var student in students)
                    {
                        await mongoService.CreateAsync(student);
                    }

                    // Complete the transaction
                    transaction.Complete();
                }
                catch
                {
                    // Dispose of the transaction if an error occurs
                    transaction.Dispose();
                    throw;
                }
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
