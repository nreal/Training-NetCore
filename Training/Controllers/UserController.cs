using Microsoft.AspNetCore.Mvc;
using Training.UserService;

namespace Training.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
