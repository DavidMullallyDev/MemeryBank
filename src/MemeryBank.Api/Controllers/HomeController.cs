using Microsoft.AspNetCore.Mvc;
using MemeryBank.Api.Models;
using MemeryBank.Api.ValidatableObjects;
using MemeryBank.Api.ModelBinders;
using MemeryBank.Api.Middleware;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace MemeryBank.Api.Controllers
{
    // works with either controller as attribute or by ending the class name with Controller
    // making it a public class is optional 
    //[Controller]
    public class HomeController : Controller
    {
        //[Route("home")]
        //[Route("/")]
        //Return type should be IActionResult ... this is just for learning the concepts
        //public ContentResult Index()
        //{
        //    return Content("Hello World! Index (Method 1)", "text/plain");
        //    //with controller base class we would need the following instead
        //    //return new ContentResult() { Content = "Hello World! Index (Method 1)", ContentType = "text/plain" };
        //}

        [Route("home")]
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Home Page";
            List<Person> people = [
                new Person { FirstName = "doodles", LastName = "de brito pimentel", DateOfBirth = new DateTime(1991, 5, 16)},
                new Person { FirstName = "ifazihna", LastName = "mullally pimentel",DateOfBirth = null}, //DateOfBirth = new DateTime(2023, 3, 09)},
                new Person { FirstName = "dave", LastName = "mullally", DateOfBirth = new DateTime(1991, 5, 16)},
                new Person { FirstName = "aoife", LastName = "mullally pimentel", DateOfBirth = null},//DateOfBirth = new DateTime(2023, 3, 09)},
                new Person { FirstName = "duda", LastName = "de brito pimentel",DateOfBirth = null} //DateOfBirth = null}
            ];
            //ViewData["people"] = people;
            //ViewBag.people = people;
            return View("Index", people); //if you dont specify a name it will be Index.cshtml
            //if Index.cshtml is not found in the Views/Home folder Views/Shared/Index.cshtml will be used if it exists. 
            //return View("abc"); //here it will be abc.cshtml

            //return new ViewResult() { ViewName ="abc"};
            //return View();
            //return Content("Hello World! Index (Method 1)", "text/plain");
            //with controller base class we would need the following instead
            //return new ContentResult() { Content = "Hello World! Index (Method 1)", ContentType = "text/plain" };
        }

        [Route("person-details/{name}")]
        public IActionResult PersonDeatils(string? name)
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Person Details";
            if (name == null)
            {
                return Content("You must supply the persons name");
            } else
            {
                List<Person> people = [
                new Person { FirstName = "doodles", LastName = "de brito pimentel", DateOfBirth = new DateTime(1991, 5, 16)},
                new Person { FirstName = "ifazihna", LastName = "mullally pimentel",DateOfBirth = null}, //DateOfBirth = new DateTime(2023, 3, 09)},
                new Person { FirstName = "dave", LastName = "mullally", DateOfBirth = new DateTime(1991, 5, 16)},
                new Person { FirstName = "aoife", LastName = "mullally pimentel", DateOfBirth = null},//DateOfBirth = new DateTime(2023, 3, 09)},
                new Person { FirstName = "duda", LastName = "de brito pimentel",DateOfBirth = null}, //DateOfBirth = null}
                new Person { FirstName = "dengo", LastName = "mullally", DateOfBirth = new DateTime(1985, 1, 7)},
            ];
                Person? matchingPerson = people.Where(temp => temp.FirstName == name).FirstOrDefault();
                if(matchingPerson == null)
                {
                    return Content($"Could  not find {name}");
                }
                //adding the view name is only necessary if you want its name to be different from that of the coresponding action method 
                return View("PersonDetails", matchingPerson);
            }
            
        }

        [Route("person-with-product")]
        public IActionResult PersonWithProduct()
        {
            PersonAndProductWrapper productAndPerson = new() { PersonData = new Person { FirstName = "dengo", LastName = "mullally" }, ProductData = new Product { Id = 10, Name = "Laptop" } };
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Person With Product";
            return View(productAndPerson);
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
            Person person = new() {Id = Guid.NewGuid() , FirstName = "Dengo", LastName = "Mullally", Email="mullallydavid99@outlook.com", DateOfBirth= new DateTime(7/1/1985)};
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


        //// Without model binding
        ////[Route("meme")]
        ////public IActionResult Meme()
        ////{
        ////    IQueryCollection query = ControllerContext.HttpContext.Request.Query;
        ////    HttpResponse response = ControllerContext.HttpContext.Response;
        ////    if (Convert.ToBoolean(query["isloggedin"]) == false)
        ////    {
        ////        return Unauthorized("You must login to proceed");
        ////        //response.StatusCode = 401;
        ////        //return Content("You must login to proceed");
        ////    }

        ////    if (!query.ContainsKey("memeid"))
        ////    {
        ////        return BadRequest("An Id must be entered");
        ////        //response.StatusCode = 400;
        ////        //return Content("An Id must be entered");
        ////    }
        ////    else
        ////    {
        ////        if (String.IsNullOrEmpty(query["memeid"])) 
        ////        {
        ////            return BadRequest("the id cannot be empty");
        ////            //response.StatusCode = 400;
        ////            //return Content("the id cannot be empty");
        ////        };

        ////        int? memeId = Convert.ToInt32(query["memeid"]);
        ////        if (memeId <= 0 || memeId > 1000)
        ////        {
        ////            ////NotFound() = not found result
        ////            ////NotFound("....") = not found object result
        ////            return NotFound("The id is outside the range of 1-1000");
        ////            //response.StatusCode = 400;
        ////            //return Content("The id is outside the range of 1-1000");
        ////        }
        ////        else
        ////        {
        ////            return File("sample-1.png", "image/png");
        ////        }
        ////    }
        ////}

        //// using model binding (model binding occurs automaically in aspnet.core)
        /// making it much eiser to work with requests 
        /// as a defualt the data will be looked for in the form Fields, then the request body, then the Route data, and finally the query string paramenters
        /// form fields should be used when a form has 10 or more fields to submit ... if you want to submit one or more files - form fields is the only option
        /// this can be limited to a single source by adding [FromRoute], [fromQuery] --- this can also be added a model for a specific parameter
        [Route("meme/{memeid?}/{isloggedin?}")]
        public IActionResult Meme(int? memeid, string? author, bool? isloggedin, Meme meme)
        {
            HttpResponse response = ControllerContext.HttpContext.Response;
            if (!isloggedin.HasValue || isloggedin == false)
            {
                return Unauthorized("You must login to proceed");
                //response.StatusCode = 401;
                //return Content("You must login to proceed");
            } else
            {
                if (memeid.HasValue == false)
                {
                    return BadRequest("An Id must be entered");
                    //response.StatusCode = 400;
                    //return Content("An Id must be entered");
                }
                else
                {
                    if (memeid <= 0)
                    {
                        return BadRequest("the id cannot be empty");
                        //response.StatusCode = 400;
                        //return Content("the id cannot be empty");
                    }
                    ;

                    if (memeid <= 0 || memeid > 1000)
                    {
                        ////NotFound() = not found result
                        ////NotFound("....") = not found object result
                        return NotFound("The id is outside the range of 1-1000");
                        //response.StatusCode = 400;
                        //return Content("The id is outside the range of 1-1000");
                    }
                    else
                    {
                        return Content($"Meme ID: {memeid}-----{meme}");
                        //return File("sample-1.png", "image/png");
                    }
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


        //[Route("register")]
        //public IActionResult Register(Person person)
        //{
        //    List<string> errors = [];
        //    if (!ModelState.IsValid) {
        //        foreach (var val in ModelState.Values)
        //        {
        //            foreach (var err in val.Errors)
        //            {
        //                errors.Add(err.ErrorMessage);
        //            }
        //        }
        //        string errorsStr = string.Join("\n", errors);
        //        return BadRequest(errorsStr);
        //    }
        //    return Content($"$Person: {person}");
        //}

        // above code shortened uing LINQ
        [Route("register")]
        public IActionResult Register(Person person, [FromHeader( Name = "User-Agent")] string UserAgent)
        {
            string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage));
            if (!ModelState.IsValid) return BadRequest(errors);
            return Content($"$Person: {person}, {UserAgent}");
        }

        [Route("register/validatableobjectexample")]
        public IActionResult RegisterValidatablePerson(ValidatablePerson validatablePerson)
        {
            string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage));
            if (!ModelState.IsValid) return BadRequest(errors);
            return Content($"$Person: {validatablePerson}");
        }

        [Route("register/custommodelbinderexample")]
        // if you use the CustomPersonModelBinderProvider you can remove [ModelBinder(BinderType = typeof(CustomModelBinderPerson))]
        //public IActionResult RegisterCustomModelBinderPerson([ModelBinder(BinderType = typeof(CustomPersonModelBinder))] Person person)
        public IActionResult RegisterCustomModelBinderPerson(Person person)
        {
            string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage));
            if (!ModelState.IsValid) return BadRequest(errors);
            return Content($"$Person: {person}");
        }

        [Route("register/frombodyexample")]
        //[FromBody] enables the input fromatters to read data from the request body (json, xml or custom only)
        public IActionResult ReadFromBody([FromBody] Person person)
        {
            string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage));
            if (!ModelState.IsValid) return BadRequest(errors);
            return Content($"$Person: {person}");
        }



    }
}
