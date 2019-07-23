using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConfigurationDemo.Models;
using Microsoft.Extensions.Configuration;
using ConfigurationDemo.Domain;

namespace ConfigurationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration = null;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var foo = _configuration["Foo"]; // provided from FileProvider
            var dbValue = _configuration["One"]; // provided by custom DbProvider

            ViewData["Foo"] = foo;
            ViewData["DbValue"] = dbValue;

            return View();
        }

        public IActionResult ChangeConfig()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeConfig(string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                throw new ArgumentException("You must specify a value", nameof(newValue));
            }

            var item = _dbContext.ApplicationConfigurationItem.Single(e => e.Title == "One");
            item.Value = newValue;
            await _dbContext.SaveChangesAsync();

            (_configuration as IConfigurationRoot).Reload();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
