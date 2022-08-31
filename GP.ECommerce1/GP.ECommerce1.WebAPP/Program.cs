using System.Globalization;
using GP.ECommerce1.BlazorWebApp.AppConfiguration;

CultureInfo.DefaultThreadCurrentCulture =
    CultureInfo.DefaultThreadCurrentUICulture =
        CultureInfo.CreateSpecificCulture("de-DE");
var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAppServices(appSettings);
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

app.MapControllers();

app.Run();