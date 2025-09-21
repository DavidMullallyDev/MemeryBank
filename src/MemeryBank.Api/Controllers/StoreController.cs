using Microsoft.AspNetCore.Mvc;

namespace MemeryBank.Api.Controllers
{
    public class StoreController : Controller
    {
        [Route("store/books")]
        public IActionResult Book()
        {

            return Content("redirect from bookstore/" + HttpContext.Request.Query["id"]);
        }
    }
}
