using DataAccess;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using TriviaLink.Services;

namespace TriviaApp.Controllers
{
    [Route("Game")]
    public class GameController : Controller
    {
        private readonly IGamesDao _gamesDao;
        private readonly ICodeGeneratorService _codeGeneratorService;

        public GameController(IGamesDao gamesDao, ICodeGeneratorService codeGeneratorService)
        {
            _gamesDao = gamesDao;
            _codeGeneratorService = codeGeneratorService;
        }

        [HttpGet("Index")]
        // GET: Games
        public async Task<IActionResult> Index()
        {
            var results = await _gamesDao.GetAllGamesAsync();

            return View(results);
        }

        [HttpGet("{id}/Details")]
        public async Task<IActionResult> Details(int id)
        {
            var game = await _gamesDao.GetGameByIdAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpGet("{id}/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gamesDao.GetGameByIdAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpPost("{id}/Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("GameID,GameDay,GameFormat,GameTheme,GameLocation,MasterFirstName,MasterLastName,GameCode")] Game game)
        {
            if (id != game.GameID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _gamesDao.UpdateGame(game);
                return RedirectToAction(nameof(Index));
            }

            return View(game);
        }

        //[HttpGet("Save")]
        //public async Task Save(Game game)
        //{
        //    await _gamesDao.UpdateGame(game);
        //    return View(game);
        //}

        // GET: Game/Create
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var uniqueCode = await _codeGeneratorService.GenerateUniqueCode();
            ViewBag.UniqueCode = uniqueCode;

            return View();
        }

        // POST: Game/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameDay,GameFormat,GameTheme,GameLocation,MasterFirstName,MasterLastName,GameCode")] Game game)
        {
            if (ModelState.IsValid)
            {
                await _gamesDao.CreateGame(game);

                return RedirectToAction(nameof(Index));
            }

            return View(game);
        }
    }
}
