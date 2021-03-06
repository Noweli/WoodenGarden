using BlazorStrap;
using WoodenGardenFrontServer.Services;
using WoodenGardenFrontServer.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IGardenHouseService, GardenHouseService>();
builder.Services.AddScoped<IGardenHouseImageService, GardenHouseImageService>();
builder.Services.AddScoped<IImageHandlingService, ImageHandlingService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddBootstrapCss();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();