using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS_M1_175272M.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /<controller>/{id?}
        [Route("/Error")]
        [Route("/Error/HttpError/")]
        public IActionResult RedirectToHome()
        {
            return NotFound();
        }

        public IActionResult HttpError(int id)
        {
            ViewBag.Title = "Http Error";
            return View("HttpError", id);
        }
    }
}
