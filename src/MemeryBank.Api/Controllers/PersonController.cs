using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Runtime.CompilerServices;

namespace MemeryBank.Api.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;

        public PersonController(IPersonService personService, ICountriesService countriesService)
        {
            _personService = personService;
            _countriesService = countriesService;
        }

        [Route("persons/index")]
        public IActionResult Index(string searchBy, string searchStr)
        {
            List<PersonResponse>? persons = _personService.GetPersonList();
            if(searchBy != null && !string.IsNullOrEmpty(searchStr)) persons = _personService.GetFilteredPersons(searchBy, searchStr);
            ViewBag.SearchFields = new Dictionary<string, string>() 
            { 
                {nameof(PersonResponse.Name), "Name" },
                {nameof(PersonResponse.Dob), "Date Of Birth"},
                {nameof(PersonResponse.Gender), "Gender" },
                {nameof(PersonResponse.RecieveNewsletters) , "Recieve News Letters"},
                {nameof(PersonResponse.Country), "Country"},
                {nameof(PersonResponse.Email), "E-Mail"}
            };
            ViewData["PersonService"] = _personService;
            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Persons List";
            return View(persons);
        }
    }
}
