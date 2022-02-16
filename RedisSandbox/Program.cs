var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDistributedRedisCache(option =>
{
    option.Configuration = "127.0.0.1:6379";
    // option.InstanceName = "master";
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
