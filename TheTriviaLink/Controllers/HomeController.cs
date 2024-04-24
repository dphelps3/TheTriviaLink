using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheTriviaLink.Models;
using DataAccess;
using DataTransfer;

namespace TheTriviaLink.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesDao _gamesDao;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IGamesDao gamesDao)
        {
            _logger = logger;
            _gamesDao = gamesDao;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _gamesDao.GetGamesAsync();

            return View(games);
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
