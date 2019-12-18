using System.Threading.Tasks;
using Infrastructure.Data.Models;

namespace Training.UserService
{
    public interface IUserService
    {
        Task AddUser(User user);
    }
}
