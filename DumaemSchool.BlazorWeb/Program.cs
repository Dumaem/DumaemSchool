using DumaemSchool.Auth;
using DumaemSchool.BlazorWeb.Localization;
using DumaemSchool.Core;
using DumaemSchool.Database;
using DumaemSchool.Migrator;
using DumaemSchool.SMTP;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;
using Radzen;
using System.Globalization;

//Add localisation
var cultureInfo = new CultureInfo("ru-RU");
var dateTimeInfo = cultureInfo.DateTimeFormat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<WeatherForecastService>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});
builder.Services.AddRadzenComponents();
builder.Services
    .AddCore()
    .AddDatabase(builder.Configuration)
    .AddMigrations(builder.Configuration)
    .AddAuth(builder.Configuration)
    .AddSMTP(builder.Configuration)
    .AddSingleton<MudLocalizer, RussianMudblazorLocalizer>()
    .AddScoped<AuthenticationStateProvider, DumaemSchool.BlazorWeb.AuthenticationStateProvider>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization("ru");
app.UseMiddleware<AuthenticationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();