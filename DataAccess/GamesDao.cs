using DataAccess.SqlMaps;
using DataTransfer;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess
{
    public interface IGamesDao
    {
        public Task<List<Game>> GetAllGamesAsync();
        public Task<Game> GetGameByIdAsync(int id);
        public Task<Game> UpdateGame(Game game);
        public Task<Game> CreateGame(Game game);
    }

    public class GamesDao : IGamesDao
    {
        private readonly IBaseDao _baseDao;
        //private readonly IMemoryCache _memoryCache;

        //public GamesDao(IBaseDao baseDao, IMemoryCache memoryCache)
        public GamesDao(IBaseDao baseDao)
        {
            _baseDao = baseDao;
            //_memoryCache = memoryCache;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            try
            {
                //var results = _memoryCache.Get<List<Game>>("games");
                var results = await _baseDao.QueryAsync<Game, dynamic>(GamesSql.SelectAllGames(), new { });

                //if (results == null || results.Count == 0)
                //{
                //    results = await _baseDao.QueryAsync<Game, dynamic>(GamesSql.SelectAllGames(), new { });
                //    _memoryCache.Set("games", results, TimeSpan.FromMinutes(45));
                //}

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

        public async Task<Game> CreateGame(Game game)
        {
            try
            {
                int newGameId = await _baseDao.ExecuteScalarAsync(GamesSql.InsertGame(), game);

                game.GameID = newGameId;

                return game;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public async Task<Game> UpdateGame(Game game)
        {
            try
            {
                await _baseDao.ExecuteAsync(GamesSql.UpdateGame(), game);

                var updatedGame = await _baseDao.QueryFirstOrDefaultAsync<Game, object>("SELECT * FROM Game WHERE GameID = @Id", new { Id = game.GameID });

                return updatedGame;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public async Task Delete(Game game)
        {
            try
            {
                var deleteGameSql = @"DELETE FROM Game WHERE GameID = @Id";

                await _baseDao.ExecuteAsync(deleteGameSql, new { Id = game.GameID });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                throw new Exception(errorMessage);
            }
        }
    }
}
