using DataAccess.SqlMaps;
using DataTransfer;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess
{
    public interface IUsersDao
    {
        void AddUser(User user);
        User GetUserByUsername(string username);
    }

    public class UsersDao : IUsersDao
    {
        private readonly IBaseDao _baseDao;

        public UsersDao(IBaseDao baseDao)
        {
            _baseDao = baseDao;
        }
    }

    public async void AddUser(User user)
    {
        try
        {
            
        }
    }

    public User GetUserByUsername(string username)
    {

    }
}
