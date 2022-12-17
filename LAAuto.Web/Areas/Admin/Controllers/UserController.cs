using SERVICES = LAAuto.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LAAuto.Web.Models.Users;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly SERVICES.IUserService _userService;

        public UserController(SERVICES.IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var userServices = await _userService.ListUsersAsync();

            var users = userServices
                .Select(Conversion.ConvertUser)
                .ToList();

            return View(users);
        }
    }
}
