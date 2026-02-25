using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace MemeryBank.Api.Controllers
{
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;

        public PersonController(IPersonService personService, ICountriesService countriesService)
        {
            _personService = personService;
            _countriesService = countriesService;
        }

        [Route("[action]")]
        public IActionResult Index(string searchBy, string searchStr, string sortBy = nameof(PersonResponse.Name), SortOrderOptions sortOrder = SortOrderOptions.DESC)
        {
            List<PersonResponse>? persons = _personService.GetPersonList();
            if(persons != null) persons = _personService.GetFilteredPersons(searchBy, searchStr);
            if(persons != null) persons = _personService.GetSortedPersons(persons, sortBy, sortOrder);
            ViewBag.SearchFields = new Dictionary<string, string>() 
            { 
                {nameof(PersonResponse.Name), "Name" },
                {nameof(PersonResponse.Dob), "Date Of Birth"},
                {nameof(PersonResponse.Age), "Age" },
                {nameof(PersonResponse.Email), "E-Mail"},
                {nameof(PersonResponse.Gender), "Gender" },
                {nameof(PersonResponse.CountryId), "Country Id"},
                {nameof(PersonResponse.Country), "Country"},
                {nameof(PersonResponse.RecieveNewsletters) , "Recieve News Letters"},  
            };
            ViewBag.CurrentSearchStr = searchStr;
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder == SortOrderOptions.ASC ? SortOrderOptions.DESC : SortOrderOptions.ASC;
            ViewData["PersonService"] = _personService;
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Persons List";
            return View(persons);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            List<CountryResponse> countries = _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(c=> new SelectListItem() { Value = c.CountryId.ToString(), Text = c.CountryName });
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                List<CountryResponse> countries = _countriesService.GetAllCountries();
                return View(countries);
            }
            _personService.AddPerson(personAddRequest);
            return RedirectToAction("Index", "Person");
        }
    }
}
