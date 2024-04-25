using DataAccess.SqlMaps;
using DataTransfer;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess
{
    public interface IGamesDao
    {
        public Task<List<Game>> GetAllGamesAsync();
        public Task<Game> GetGameByIdAsync(int id);
        //public Task<Game> UpdateGame(BaseDao _baseDao, int id);
        //public Task<Game> CreateGameAsync(Game game);
    }

    public class GamesDao : IGamesDao
    {
        private readonly IBaseDao _baseDao;
        private readonly IMemoryCache _memoryCache;

        public GamesDao(IBaseDao baseDao, IMemoryCache memoryCache) 
        {
            _baseDao = baseDao;
            _memoryCache = memoryCache;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            try
            {
                var results = _memoryCache.Get<List<Game>>("games");
                if (results == null || results.Count == 0)
                {
                    results = await _baseDao.QueryAsync<Game, dynamic>(GamesSql.SelectAllGames(), new { });
                    _memoryCache.Set("games", results, TimeSpan.FromMinutes(45));
                }
                return results?.OrderByDescending(x => x.GameDay).ToList() ?? [];
            }
            catch (Exception ex) 
            {
                var errorMessage = ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            try
            {
                var result = await _baseDao.QueryAsync<Game, dynamic>(GamesSql.SelectGameById(), new { id });
                return result?.FirstOrDefault() ?? new Game();
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                throw new Exception(errorMessage);
            }
        }

        //public async Task<int> CreateGameAsync(Game game)
        //{
        //    try
        //    {
        //        string insertSql = "INSERT INTO Games (GameCode, GameDay, GameFormat, GameTheme, GameLocation, MasterFirstName, MasterLastName) VALUES (@GameCode, @GameDay, @GameFormat, @GameTheme, @GameLocation, @MasterFirstName, @MasterLastName); SELECT SCOPE_IDENTITY();";

        //        int newGameId = await _baseDao.QueryFirstOrDefaultAsync<int>(insertSql, game);

        //        return await GetGameByIdAsync(newGameId);
        //    }
        //    catch (Exception ex) 
        //    {
        //        var errorMessage = ex.Message;
        //        throw new Exception(errorMessage);
        //    }
        //}

        //public async Task<Game> UpdateGame(BaseDao _baseDao, int id)
        //{
        //    try
        //    {
        //        var sql = "UPDATE Game SET GameTheme = @GameTheme WHERE GameID = @Id";
        //        var parameters = new { GameTheme = "Updated Game Theme", Id = id };

        //        await _baseDao.ExecuteAsync(sql, parameters);

        //        var updatedGame = await _baseDao.QueryFirstOrDefaultAsync<Game>("SELECT * FROM Game WHERE GameID = @Id", new { Id = id });
        //        return updatedGame;
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorMessage = ex.Message;
        //        throw new Exception(errorMessage);
        //    }
        //}
    }
}
