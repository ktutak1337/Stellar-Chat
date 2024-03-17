using StellarChat.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddSharedInfrastructure();

var app = builder.Build();

app.UseSharedInfrastructure();

app.Run();
