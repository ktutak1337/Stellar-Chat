using StellarChat.Server.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructure();

var app = builder.Build();

app.UseInfrastructure();

app.Run();
