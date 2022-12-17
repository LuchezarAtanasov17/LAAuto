using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LAAuto.Web.Areas.Admin.Constants;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents base controller.
    /// </summary>
    [Area(AreaName)]
    [Route("/Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = AdminRoleName)]
    public class BaseController : Controller
    {
    }
}
