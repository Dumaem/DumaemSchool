using DumaemSchool.Auth;
using DumaemSchool.BlazorWeb.Data;
using DumaemSchool.BlazorWeb.Middleware;
using DumaemSchool.Core;
using DumaemSchool.Database;
using DumaemSchool.Migrator;
using DumaemSchool.SMTP;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<WeatherForecastService>();

builder.Services.AddMudServices();
builder.Services.AddRadzenComponents();
builder.Services
    .AddCore()
    .AddDatabase(builder.Configuration)
    .AddMigrations(builder.Configuration)
    .AddAuth(builder.Configuration)
    .AddSMTP(builder.Configuration)
    .AddScoped<AuthenticationStateProvider, DumaemSchool.BlazorWeb.AuthenticationStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<AuthenticationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();