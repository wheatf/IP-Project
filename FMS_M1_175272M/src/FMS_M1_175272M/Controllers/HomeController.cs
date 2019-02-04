using Microsoft.AspNetCore.Mvc;

namespace FMS_M1_175272M.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
    }
}
