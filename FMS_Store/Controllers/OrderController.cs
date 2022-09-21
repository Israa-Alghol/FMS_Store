using Microsoft.AspNetCore.Mvc;

namespace FMS_Store.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
