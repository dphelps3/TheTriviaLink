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

        // GET: /Index
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var results = await _gamesDao.GetAllGamesAsync();

            return View(results);
        }

        // GET: 3/Details
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

        // GET: Edit
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

        // POST: Edit
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

        // GET: Create
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        
        // POST: Create
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

        // Controller Action to Generate Unique Code
        [HttpGet("/Game/GenerateUniqueCode")]
        public async Task<IActionResult> GenerateUniqueCode()
        {
            var uniqueCode = await _codeGeneratorService.GenerateUniqueCode();
            return Json(new { uniqueCode });
        }
    }
}
