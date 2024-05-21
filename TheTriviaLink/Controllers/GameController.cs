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
        // GET: Game/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var results = await _gamesDao.GetGameByIdAsync(id);

            if (results == null)
            {
                return NotFound();
            }

            return View(results);
        }

        [HttpGet("{id}/Edit")]
        // GET: Game/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var game = await _gamesDao.GetGameByIdAsync(id.Value);

                return View(game);
            }
        }

        [HttpGet("Save")]
        public async Task Save(Game game)
        {
            await _gamesDao.UpdateGame(game);
            //return View(game);
        }

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
