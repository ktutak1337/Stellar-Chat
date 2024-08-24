var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfiguration();
builder.AddInfrastructure();

var app = builder.Build();

await app.Services.SeedMongoIdentityAsync();

app.UseInfrastructure();

app.Run();
