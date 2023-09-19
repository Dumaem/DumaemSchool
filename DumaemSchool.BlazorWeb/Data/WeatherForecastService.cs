using DumaemSchool.Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace DumaemSchool.BlazorWeb.Data
{
    public class WeatherForecastService
    {
        private readonly SignInManager<SchoolUser> _signInManager;
        private readonly UserManager<SchoolUser> _userManager;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastService(SignInManager<SchoolUser> signInManager, UserManager<SchoolUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
        {
            var user = new SchoolUser()
            {
                Email = "wingimobile@gmail.com",
                UserName = "WingiM"
            };
            await _userManager.CreateAsync(user);
            await _userManager.AddPasswordAsync(user, "kredit200");
            await _signInManager.SignInAsync(user, true);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();
        }
    }
}