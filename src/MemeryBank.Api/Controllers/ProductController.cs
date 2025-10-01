using Microsoft.AspNetCore.Mvc;

namespace MemeryBank.Api.Controllers
{
    public class ProductController : Controller
    {
        [Route("/products")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Products";
            return View();
        }
        [Route("/search-product/{productid?}")]
        public IActionResult SearchProduct(int? ProductId)
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Search Products";
            ViewBag.ProductId = ProductId;
            return View();
        }
        [Route("/order-product")]
        public IActionResult OrderProduct()
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Order Products";
            return View();
        }

    }
}
