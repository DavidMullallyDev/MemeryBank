using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using static MemeryBank.Api.Models.Person;

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
                ViewBag.Method = "Create";
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                List<CountryResponse> countries = _countriesService.GetAllCountries();
                return View(countries);
            }
            _personService.AddPerson(personAddRequest);
            return RedirectToAction("Index", "Person");
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public IActionResult Update([FromRoute] Guid? id)
        {
            PersonResponse? personToUpdate;
            if (id != null)
            {
                personToUpdate = _personService.GetPersonByID(id);
                PersonUpdateRequest personUpdateRequest = new()
                {
                    Id = personToUpdate.Id,
                    Address = personToUpdate.Address,
                    CountryId = personToUpdate.CountryId,
                    Gender = !string.IsNullOrWhiteSpace(personToUpdate.Gender)
                    && Enum.TryParse<GenderOptions>(personToUpdate.Gender, ignoreCase: true, out var parsedGender)
                    ? parsedGender
                    : (GenderOptions?)null,
                    Dob = personToUpdate.Dob,
                    Email = personToUpdate.Email,
                    Name = personToUpdate.Name,
                    RecieveNewsLetters = personToUpdate.RecieveNewsletters
                };

                List<CountryResponse> countries = _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(c => new SelectListItem() { Value = c.CountryId.ToString(), Text = c.CountryName });
                return View(personUpdateRequest);
            }
            return View();
        }

        [Route("[action]/{Id}")]
        [HttpPost]
        public IActionResult Update(PersonUpdateRequest personResponse)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Method = "Update";
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                List<CountryResponse> countries = _countriesService.GetAllCountries();
                return View(countries);
            }

            PersonUpdateRequest personUpdateRequest = new()
            {
                Id = (Guid)personResponse.Id,
                Name = personResponse.Name,
                Email = personResponse.Email,
                Dob = personResponse.Dob,
                Gender = personResponse.Gender,
                CountryId = personResponse.CountryId,
                Address = personResponse.Address,
                RecieveNewsLetters = personResponse.RecieveNewsLetters
            };
                _personService.UpdatePerson(personUpdateRequest);
            return RedirectToAction("Index", "Person");
        }
    }
}
