using Microsoft.AspNetCore.Mvc;

namespace MoviesWebApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(string errorMessage="")
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }
    }
}
