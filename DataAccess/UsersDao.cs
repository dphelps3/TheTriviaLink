using DataAccess.SqlMaps;
using DataTransfer;

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

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
