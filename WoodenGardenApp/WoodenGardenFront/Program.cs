using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WoodenGardenFront;
using WoodenGardenFront.Services;
using WoodenGardenFront.Services.IServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<IGardenHouseService, GardenHouseService>();
builder.Services.AddScoped<IGardenHouseImageService, GardenHouseImageService>();

await builder.Build().RunAsync();