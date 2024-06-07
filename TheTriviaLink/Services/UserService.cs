using DataTransfer;
using DataAccess;

namespace TriviaApp.Services
{
    public interface IUserService
    {
        void RegisterUser(User user, string password);
        User LoginUser(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly IUsersDao _usersDao;

        public void RegisterUser(User user, string password)
        {
            user.PasswordHash = HashPassword(password);
            _usersDao.AddUser(user);
        }

        public User LoginUser(string username, string password)
        {
            var user = _usersDao.GetUserByUsername(username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var builder = new System.Text.StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }
}
