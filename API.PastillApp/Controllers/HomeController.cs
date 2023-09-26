using Microsoft.AspNetCore.Mvc;

namespace API.PastillApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
