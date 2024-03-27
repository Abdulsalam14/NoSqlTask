using AzureStorageLibrary.Models;
using AzureStorageLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;
using System.Diagnostics;

namespace MVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoSqlStorage<Player> _noSqlStorage;

        public HomeController(INoSqlStorage<Player> noSqlStorage)
        {
            _noSqlStorage = noSqlStorage;
        }

        public async Task<IActionResult> Index()
        {
            var player = new Player
            {
                RowKey = Guid.NewGuid().ToString(),
                PartitionKey = "Players",
                Name="Player1",
                Surname="Player1 Surname",
                BirthDate=new DateOnly(1994,04,04).ToString(),
                Score=11.4,
                Salary=4000
            };
            await _noSqlStorage.Add(player);
            ViewBag.players = (await _noSqlStorage.All()).ToList();
            return View();
        }
    }
}
