using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

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
    }
}
