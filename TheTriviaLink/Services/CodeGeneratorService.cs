using System.Data;
using DataAccess;
using DataAccess.SqlMaps;
using DataTransfer;

namespace TriviaLink.Services
{
    public interface ICodeGeneratorService
    {
        Task<string> GenerateUniqueCode();
    }

    public class CodeGeneratorService : ICodeGeneratorService
    {
        private readonly IBaseDao _baseDao;

        public CodeGeneratorService(IBaseDao context)
        {
            _baseDao = context;
        }

        public static string GenerateCode()
        {
            Random rand = new Random();

            int stringlength = 4;
            int randValue;
            string str = String.Empty;
            char letter;

            for (int i = 0; i < stringlength; i++)
            {
                randValue = rand.Next(0, 26);

                letter = Convert.ToChar(randValue + 65);

                str = str + letter;
            }

            return str;
        }

        public async Task<string> GenerateUniqueCode()
        {
            string code = string.Empty;
            bool isMatch = true;

            while (isMatch == true)
            {
                code = GenerateCode();
                isMatch = await GameCodeMatchExisting(code);
            }
            return code;
        }

        public async Task<bool> GameCodeMatchExisting(string newGameCode)
        {
            var results = await _baseDao.QueryAsync<Game, dynamic>(GamesSql.SelectGames(), new { });
            var existingGameCodes = results.Select(x => x.GameCode).ToList();

            if (existingGameCodes.Any(x => x == newGameCode))
            {
                return true;
            }
            return false;
        }
    }
}
