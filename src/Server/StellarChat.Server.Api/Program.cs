var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfiguration();
builder.AddInfrastructure();

var app = builder.Build();

app.UseInfrastructure();

app.Run();
