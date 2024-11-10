using DistributedDBSolution.DAL;
using DistributedDBSolution.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using DistributedDBSolution.DAL.Settings;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LabDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});


builder.Services.AddSingleton<DataSeedService>();
builder.Services.AddSingleton<StudentService>();
builder.Services.AddHostedService<UpdateService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataSeedService = scope.ServiceProvider.GetRequiredService<DataSeedService>();
    await dataSeedService.SeedDataAsync();
}

app.Run();
