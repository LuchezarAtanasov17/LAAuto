using LAAuto.Web.Infrastructure;
using LAAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LAAuto.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            var statusCode = exception is not null
                ? Helper.ProcessException(exception)
                : StatusCodes.Status500InternalServerError;

            var viewModel = new ErrorViewModel
            {
                StatusCode = statusCode,
                Message = exception?.Message
            };

            return View(viewModel);
        }
    }
}