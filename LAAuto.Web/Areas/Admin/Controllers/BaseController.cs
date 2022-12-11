using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LAAuto.Web.Areas.Admin.Constants;

namespace LAAuto.Web.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Route("/Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = AdminRoleName)]
    public class BaseController : Controller
    {
    }
}
