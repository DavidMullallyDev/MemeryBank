using Microsoft.AspNetCore.Mvc;
using MemeryBank.Api.Models;

namespace MemeryBank.Api.Controllers
{
    // works with either controller as attribute or by ending the class name with Controller
    // making it a public class is optional 
    //[Controller]
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        //Return type should be IActionResult ... this is just for learning the concepts
        public ContentResult Index()
        {
            return Content("Hello World! Index (Method 1)", "text/plain");
            //with controller base class we would need the following instead
            //return new ContentResult() { Content = "Hello World! Index (Method 1)", ContentType = "text/plain" };
        }

        [Route("about")]
        public ContentResult About()
        {
            return Content("<h1>About!</h1> <h2>(Method 1)</h2>", "text/html");
        }

        [Route("contact")]
        public ContentResult Contact()
        {
            return Content("<h1>Contact!<h1> <h3>(Method 1)</h3>", "text/plain");
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person person = new() {Id = Guid.NewGuid() , FirstName = "Dengo", LastName = "Mullally", Email="mullallydavid99@outlook.com", Age=40};
            return Json(person);
            //return new JsonResult(person);
        }

        [Route("download-from-static-files-folder")]

        //use virtaul file result if the file is within the static files folder
        public VirtualFileResult FileDownload()
        {
            return File("sample-1.png", "image/png");
            //return new VirtualFileResult("sample-1.png", "image/png");
        }

        [Route("download-from-outside-static-files-folder")]
        public PhysicalFileResult PhsysicalFileDownload()
        {
            return PhysicalFile(@"C:\Users\mulla\source\repos\MemeryBankRepo\src\MemeryBank.Api\sample-1.txt", "text/plain");
            //return PhysicalFileResult(@"C:\Users\mulla\source\repos\MemeryBankRepo\src\MemeryBank.Api\sample-1.txt", "text/plain");
        }

        [Route("file-byte-array")]
        public FileContentResult FileByteArray()
        {
             byte[] fileByteArr = System.IO.File.ReadAllBytes(@"C:\Users\mulla\source\repos\MemeryBankRepo\src\MemeryBank.Api\sample-1.png");
             return File(fileByteArr, "image/png");
            //return new FileContentResult(fileByteArr, "image/png");
        }

        [Route("meme")]
        public IActionResult Meme()
        {
            IQueryCollection query = ControllerContext.HttpContext.Request.Query;
            HttpResponse response = ControllerContext.HttpContext.Response;
            if (Convert.ToBoolean(query["isloggedin"]) == false)
            {
                return Unauthorized("You must login to proceed");
                //response.StatusCode = 401;
                //return Content("You must login to proceed");
            }

            if (!query.ContainsKey("memeid"))
            {
                return BadRequest("An Id must be entered");
                //response.StatusCode = 400;
                //return Content("An Id must be entered");
            }
            else
            {
                if (String.IsNullOrEmpty(query["memeid"])) 
                {
                    return BadRequest("the id cannot be empty");
                    //response.StatusCode = 400;
                    //return Content("the id cannot be empty");
                };

                int? memeId = Convert.ToInt32(query["memeid"]);
                if (memeId <= 0 || memeId > 1000)
                {
                    ////NotFound() = not found result
                    ////NotFound("....") = not found object result
                    return NotFound("The id is outside the range of 1-1000");
                    //response.StatusCode = 400;
                    //return Content("The id is outside the range of 1-1000");
                }
                else
                {
                    return File("sample-1.png", "image/png");
                }
            }
        }

        // endpoint was bookstore but is now store/books so all querys bookstore should be redirected to store/books
        [Route("bookstore")]
        public IActionResult Book()
        {
            int? bookId = Convert.ToInt32(HttpContext.Request.Query["id"]!);
            return RedirectToAction("Book", "Store", new { id = bookId});
            //return LocalRedirect("store/books/{id}");
            //return LocalRedirectPermanent("store/books/{bookid}");
            ////return new RedirectToActionResult("Book", "Store", new {id=1});
            //// return new RedirectToActionResult("Book", "Store", new { }, true);
            /////adding true will signify that the zrl has changed premanently and the old one can be ignored by search engines going forward
        }
    }
}
