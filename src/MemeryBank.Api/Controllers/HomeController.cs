using Microsoft.AspNetCore.Mvc;
using MemeryBank.Api.Models;
using Services;
using ServiceContracts;

namespace MemeryBank.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService;

        //constructor
        public HomeController(ICitiesService citiesService) 
        {
            ////Should never do this ...instead use dependancy injection
            //create object of CityService class
            //_citiesService = new CitiesService();


            //_citiesService = citiesService;
        }
        
        [Route("home")]
        [Route("/")]
        //public IActionResult Index([FromServices] ICitiesService _citiesService) //if you just want the service to be availabe to a particular method you can pass it like this... in this can you should remove _citiesService = citiesService from the constructor
        public IActionResult Index(ICitiesService _citiesService)
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Home Page";
            List<string> cities = _citiesService.GetCities();
            return View(cities); 
        }

        [Route("memes-area")]
        public IActionResult MemesArea()
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Meme Page";
            return View();
        }

        [Route("merch")]
        public IActionResult Merch()
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Merch Page";
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "About Page";
            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Contact Page";
            return View();
        }

        [Route("notifications")]
        public IActionResult Notifications()
        {
            return PartialView("_NotificationsPartial");
        }

        [Route("subscribe")]
        public IActionResult Subscribe()
        {
            return PartialView("_SubscriptionPartial");
        }

        [Route("donate")]
        public IActionResult Donate()
        {
            return PartialView("_DonatePartial");
        }

        [Route("login")]
        public IActionResult Login()
        {
            return PartialView("_LoginPartial");
        }

        [Route("programming-languages")]
        public IActionResult ProgrammingLanguages()
        {
            ItemList itemList = new() { ListTitle = "Programming Languages", ListItems = ["C++", "C#", "Python"], IsOrderedList = true };
            //return new PartialViewResult(ViewName = "_ListPartialView", Model= itemList);
            return PartialView("_ListPartialView", itemList);
        }

        [Route("family-list")]
        public IActionResult LoadFamilyList()
        {
           List<Person> people =
            [
                new Person() { FirstName = "aoife", LastName = "mullally pimentel" },
                new Person() { FirstName = "david", LastName = "mullally" },
                new Person() { FirstName = "maria eduarda", LastName = "de brito pimentel" }
            ];

            return ViewComponent("Grid", people);
        }
    }
}
