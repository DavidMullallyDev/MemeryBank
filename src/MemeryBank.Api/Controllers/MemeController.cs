using MemeryBank.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemeryBank.Api.Controllers
{
    public class MemeController : Controller
    {
        [Route("/memes")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Memes";
            return View();
        }

        [Route("meme/{memeid?}/{isloggedin?}")]
        public IActionResult Meme(int? memeid, string? author, bool? isloggedin, Meme meme)
        {
            HttpResponse response = ControllerContext.HttpContext.Response;
            if (!isloggedin.HasValue || isloggedin == false)
            {
                return Unauthorized("You must login to proceed");
            }
            else
            {
                if (memeid.HasValue == false)
                {
                    return BadRequest("An Id must be entered");
                }
                else
                {
                    if (memeid <= 0)
                    {
                        return BadRequest("the id cannot be empty");
                    }
                    ;

                    if (memeid <= 0 || memeid > 1000)
                    {
                        return NotFound("The id is outside the range of 1-1000");
                    }
                    else
                    {
                        return Content($"Meme ID: {memeid}-----{meme}");
                    }
                }
            }
        }
    }
}
