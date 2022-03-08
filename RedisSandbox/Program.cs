var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDistributedRedisCache(option =>
{
    option.Configuration = "127.0.0.1:6379";
    // option.InstanceName = "master";
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
