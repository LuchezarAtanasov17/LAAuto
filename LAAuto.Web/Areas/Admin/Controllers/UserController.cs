using LAAuto.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using SERVICES = LAAuto.Services.Users;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents service controller.
    /// </summary>
    public class UserController : BaseController
    {
        private readonly SERVICES.IUserService _userService;

        /// <summary>
        /// Initialize new instance of <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserController(SERVICES.IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Lists the users
        /// </summary>
        /// <returns>the list view</returns>
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
