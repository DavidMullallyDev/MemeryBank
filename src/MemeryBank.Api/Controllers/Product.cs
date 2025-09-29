using Microsoft.AspNetCore.Mvc;

namespace MemeryBank.Api.Controllers
{
    public class Product : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
