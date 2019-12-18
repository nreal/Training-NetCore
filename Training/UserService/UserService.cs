using System.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Data.Models;

namespace Training.UserService
{
    public class UserService : IUserService
    {
        private readonly MachineContext _context;

        public UserService(MachineContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }
    }
}
