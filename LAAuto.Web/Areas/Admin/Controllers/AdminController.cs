using Microsoft.AspNetCore.Mvc;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents admin controller.
    /// </summary>
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
