using Microsoft.AspNetCore.Mvc;
using Redis.Models;
using StackExchange.Redis;
using System.Diagnostics;

namespace Redis.Controllers
{
    public class HomeController : Controller
    {
        public readonly IDatabase _redis;
        public HomeController(IConnectionMultiplexer connectionMultiplexer)
        {
            _redis = connectionMultiplexer.GetDatabase();
        }

        public IActionResult Index()
        {
            _redis.StringSet("1", "Иванов");
            return View();
        }

        public IActionResult Privacy()
        {
            string? str = _redis.StringGet("1");
            if (str == null)
            {
                return View("Не найдено");
            } else
            {
                Console.WriteLine(str);
                return View(new MessageClass { Message = str });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class MessageClass
    {
        public string Message { get; set; }
    }
}