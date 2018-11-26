using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConfigurationDemo.Models;
using Microsoft.Extensions.Configuration;

namespace ConfigurationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration = null;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var foo = _configuration["Foo"]; // provided from FileProvider
            var dbValue = _configuration["One"]; // provided by custom DbProvider

            ViewData["Foo"] = foo;
            ViewData["DbValue"] = dbValue;

            return View();
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
