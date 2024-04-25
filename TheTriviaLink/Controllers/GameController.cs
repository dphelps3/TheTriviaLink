using DataAccess;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using TriviaLink.Services;

namespace TriviaApp.Controllers
{

    public class GameController : Controller
    {
        private readonly IGamesDao _gamesDao;
        private readonly ICodeGeneratorService _codeGeneratorService;

        public GameController(IGamesDao gamesDao, ICodeGeneratorService codeGeneratorService)
        {
            _gamesDao = gamesDao;
            _codeGeneratorService = codeGeneratorService;
        }

        // GET: Game
        public async Task<IActionResult> Index()
        {
            var results = await _gamesDao.GetAllGamesAsync();

            return View(results);
        }

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

        // GET: Game/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var game = await _gamesDao.GetGameByIdAsync(id.Value);
            //await _gamesDao.UpdateGame(game);


            //var game = await _gamesDao.UpdateGame.FindAsync(id);
            //if (game == null)
            //{
            //    return NotFound();
            //}
            return View(game);
        }

        public async Task<IActionResult> Save(Game game)
        {
            await _gamesDao.UpdateGame(game);
            return View(game);
        }

        public async Task<IActionResult> Create()
        {
            var uniqueCode = await _codeGeneratorService.GenerateUniqueCode();
            ViewBag.UniqueCode = uniqueCode;

            return View();
        }

        // POST: Game/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Game game)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _gamesDao.CreateGameAsync(game);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(game);
        //}

        // POST: Game/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("GameID,GameCode,GameDay,GameFormat,GameTheme,GameLocation,MasterFirstName,MasterLastName")] Game game)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(game);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(game);
        //}

        // POST: Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("GameID,GameDay,GameFormat,GameTheme,GameLocation,MasterFirstName,MasterLastName")] Game game)
        //{
        //    if (id != game.GameID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(game);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GameExists(game.GameID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(game);
        //}

        // GET: Game/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Game == null)
        //    {
        //        return NotFound();
        //    }

        //    var game = await _context.Game
        //        .FirstOrDefaultAsync(m => m.GameID == id);
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(game);
        //}

        // POST: Game/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Game == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Game'  is null.");
        //    }
        //    var game = await _context.Game.FindAsync(id);
        //    if (game != null)
        //    {
        //        _context.Game.Remove(game);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool GameExists(int id)
        //{
        //    return (_context.Game?.Any(e => e.GameID == id)).GetValueOrDefault();
        //}
    }
}
