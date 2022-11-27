using SERVICES = LAAuto.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LAAuto.Web.Models.Users;

namespace LAAuto.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
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

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery]
            Guid id)
        {
            var userService = await _userService.GetUserAsync(id);

            var user = Conversion.ConvertUser(userService);

            return View(user);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);

            return Redirect(nameof(List));
        }
    }
}
