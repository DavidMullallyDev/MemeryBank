using Autofac;
using MemeryBank.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceContracts;
using Services;
using System.Text.Json;

namespace MemeryBank.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IConfiguration _configuration;
        //we can inject the config as a service instead
        private readonly OptionsClass _someApiOptions;
        private readonly ICitiesService _citiesService;
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ILifetimeScope _lifetimeScope; //for autofac DI
        private readonly StocksService _stocksService;
        //private readonly IServiceScopeFactory _serviceScopeFactory; for built in DI

        //constructor
        public HomeController(StocksService stocksService, IOptions<OptionsClass> someApiOptions, IWebHostEnvironment webHostEnvironment, ICitiesService citiesService, ICitiesService citiesService1, ICitiesService citiesService2, ILifetimeScope lifetimeScope)
        //public HomeController(IOptions<OptionsClass> options, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, ICitiesService citiesService, ICitiesService citiesService1, ICitiesService citiesService2, ILifetimeScope lifetimeScope)
        //public HomeController(ICitiesService citiesService, ICitiesService citiesService1, ICitiesService citiesService2, IServiceScopeFactory serviceScopeFactory) 
        {

            _webHostEnvironment = webHostEnvironment;
            //_configuration = configuration;
            _someApiOptions = someApiOptions.Value;
            this._stocksService = stocksService;
            ////Should never do this ...instead use dependancy injection
            //create object of CityService class
            //_citiesService = new CitiesService();

            _citiesService = citiesService;
            _citiesService1 = citiesService1;
            _citiesService2 = citiesService2;
            //_serviceScopeFactory = serviceScopeFactory;
            _lifetimeScope = lifetimeScope;
        }
        
        [Route("home")]
        [Route("/")]
        //public IActionResult Index([FromServices] ICitiesService _citiesService) //if you just want the service to be availabe to a particular method you can pass it like this... in this can you should remove _citiesService = citiesService from the constructor
        public IActionResult Index()
        {
            ViewBag.webHostEnvironment = _webHostEnvironment.EnvironmentName;
            ViewBag.currentRootPath = _webHostEnvironment.ContentRootPath;

            //ViewBag.ClientId = _configuration.GetValue<string>("SomeApi:ClientID", "12345");
            //ViewBag.ClientSecret = _configuration.GetValue<string>("SomeApi:ClientSecret", "abc123");
            //alternative below

            //the following code is no longer needed as the Config is being injected as a service
            //OptionsClass options = new OptionsClass();
            ////bind = loads config options into an existing intance of the options class
            ////_configuration.GetSection("SomeApi").Bind(options);

            ////get = loads config option into a new instance of the options class
            //OptionsClass someApi =_configuration.GetSection("SomeApi").Get<OptionsClass>();
            //ViewBag.ClientId = someApi.ClientId;
            //ViewBag.ClientSecret = someApi.ClientSecret;
            //ViewBag.X = _configuration.GetValue<int>("X", 9999);

            ViewBag.ClientId = _someApiOptions.ClientId;
            ViewBag.ClientSecret = _someApiOptions.ClientSecret;

            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Home Page";
            ViewBag.ServiceInstanceId = _citiesService.ServiceInstanceId;
            ViewBag.ServiceInstanceId1 = _citiesService1.ServiceInstanceId;
            ViewBag.ServiceInstanceId2 = _citiesService2.ServiceInstanceId;
            List<string> cities = _citiesService.GetCities();

            // using IServiceScope scope = _serviceScopeFactory.CreateScope();
            using ILifetimeScope scope = _lifetimeScope.BeginLifetimeScope();
            {
                //ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();
                ICitiesService citiesService = scope.Resolve<ICitiesService>();
                ViewBag.ServiceInstanceIdOfServiceInScope = citiesService.ServiceInstanceId;
                }
            ; // the scope ends here;it calls the dispose method of the injected services so this is how you can end the database connection asap
            //when using entityframework this step is unnessecary as entityfrwmwork handles this
            
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

        [Route("stocks")]
        public async Task<IActionResult> Stocks()
        {

            ViewBag.appTitle = "Memery Bank";
            ViewBag.pageName = "Stocks";
            if (_someApiOptions.DefaultStockSymbol == null) _someApiOptions.DefaultStockSymbol = "MSFT";
            Dictionary<string, object>? stockPriceQuote = await _stocksService.GetStockPriceQuote(_someApiOptions.DefaultStockSymbol);
            
            Stock stock = new Stock() { StockSymbol = _someApiOptions.DefaultStockSymbol, CurrentPrice = ((JsonElement)stockPriceQuote["c"]).GetDouble(), HighestPrice = ((JsonElement)stockPriceQuote["h"]).GetDouble(), LowestPrice = ((JsonElement)stockPriceQuote["l"]).GetDouble(), OpenPrice = ((JsonElement)stockPriceQuote["o"]).GetDouble() };
            return View(stock);
        }
    }
}
