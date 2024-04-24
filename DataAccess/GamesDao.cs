using DataAccess.SqlMaps;
using DataTransfer;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess
{
    public interface IGamesDao
    {
        public Task<List<Game>> GetGamesAsync();
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

        public async Task<List<Game>> GetGamesAsync()
        {
            try
            {
                var results = _memoryCache.Get<List<Game>>("games");
                if (results == null || results.Count == 0)
                {
                    results = await _baseDao.QueryAsync<Game, dynamic>(GamesSql.SelectGames(), new { });
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
    }
}
