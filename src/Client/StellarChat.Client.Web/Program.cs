using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StellarChat.Client.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["api:url"];

builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri(apiUrl!);
});

builder.AddServices();

await builder.Build().RunAsync();
