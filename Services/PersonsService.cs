using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services
{
    public class PersonsService : IPersonService
    {
        //Private field
        private readonly List<Person> _persons;
        private readonly CountriesService _countriesService;

        public PersonsService()
        {
            _persons = [];
            _countriesService = new CountriesService();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByID(personResponse.CountryId)?.CountryName;

            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //check if PersonAddRequest is null
            if(personAddRequest == null)
            {
                throw new ArgumentNullException();
            }

            //Validate model
            ValidationHelper.ModelValidation(personAddRequest);

            //Convert PersonAddRequest into Person type
            Person person = personAddRequest.ToPerson();

            //Generate new Id for person
            person.Id = Guid.NewGuid();

            //Add person to list/Datastore
            _persons.Add(person);

            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetPersonList()
        {
            return [.. _persons.Select(p => p.ToPersonResponse())];
        }

        public PersonResponse? GetPersonByID(Guid? Id)
        {
            PersonResponse? personResponse = _persons.FirstOrDefault(p => p.Id.Equals(Id))?.ToPersonResponse();
            return personResponse ?? null;
        }

        public List<PersonResponse>? GetFilteredPersons(string searchBy, string? searchStr)
        {
            if (searchBy == null || string.IsNullOrEmpty(searchStr))
                return GetPersonList();

            List<PersonResponse> allPersons = GetPersonList() ?? [];

            return searchBy switch
            {
                nameof(Person.Name) =>
                    allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Name) &&
                        p.Name.Contains(searchStr, StringComparison.OrdinalIgnoreCase)).ToList(),

                nameof(Person.Email) =>
                    allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Email) &&
                        p.Email.Contains(searchStr, StringComparison.OrdinalIgnoreCase)).ToList(),

                nameof(Person.Dob) =>
                    allPersons.Where(p =>
                        p.Dob != null &&
                        p.Dob.Value.ToString("dd MMMM yyyy")
                            .Contains(searchStr, StringComparison.OrdinalIgnoreCase)).ToList(),

                nameof(Person.Gender) =>
                    allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Gender) &&
                        p.Gender.Contains(searchStr, StringComparison.OrdinalIgnoreCase)).ToList(),

                nameof(Person.CountryId) =>
                    allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Country) &&
                        p.Country.Contains(searchStr, StringComparison.OrdinalIgnoreCase)).ToList(),

                nameof(Person.Address) =>
                    allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Address) &&
                        p.Address.Contains(searchStr, StringComparison.OrdinalIgnoreCase)).ToList(),

                _ => allPersons
            };
        }


        public List<PersonResponse> GetSortedPersons(List<PersonResponse> personsToSort, string sortBy, SortOrderOptions sortOrder)
        {
            if (personsToSort == null || personsToSort.Count == 0)
                return [];

            Func<PersonResponse, object?> keySelector = sortBy switch
            {
                nameof(Person.Name) => p => p.Name,
                nameof(Person.Email) => p => p.Email,
                nameof(Person.Dob) => p => p.Dob,
                nameof(Person.Gender) => p => p.Gender,
                nameof(Person.CountryId) => p => p.CountryId,
                nameof(Person.Address) => p => p.Address,
                nameof(Person.RecieveNewsletters) => p => p.RecieveNewsletters,
                _ => p => p.Name // default sort
            };

            return sortBy == null ? personsToSort : sortOrder == SortOrderOptions.ASC
                ? [.. personsToSort.OrderBy(keySelector)]
                : [.. personsToSort.OrderByDescending(keySelector)];
        }

        public PersonResponse? UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            PersonResponse? personResponse;
            if (personUpdateRequest == null || personUpdateRequest.Name == null) throw new ArgumentException();

            PersonResponse? personToUpdate = AddSomeMockData().Where(p => p.Id == personUpdateRequest?.Id).FirstOrDefault();

            if (personToUpdate != null)
            {
                personToUpdate.Address = personUpdateRequest?.Address;
                personToUpdate.Gender = personUpdateRequest?.Gender.ToString();
                personToUpdate.RecieveNewsletters = personUpdateRequest.RecieveNewsletters;
                personToUpdate.Email = personUpdateRequest.Email;
                personToUpdate.CountryId = personUpdateRequest.CountryId;
                personToUpdate.Dob = personUpdateRequest.Dob;
                personToUpdate.Name = personUpdateRequest.Name;
            }

            personResponse = personUpdateRequest?.ToPerson().ToPersonResponse();

            return personResponse;
        }

        public List<PersonResponse> AddSomeMockData()
        {
            List<PersonResponse> personResponses = [];
            CountryResponse? countryResponse = new CountryResponse();
            if(_countriesService.GetAllCountries().Any(c => c.CountryName == "Ireland"))
            {
                countryResponse = _countriesService.GetAllCountries().Where(c => c.CountryName == "Ireland").FirstOrDefault();
            }
            else
            {
                countryResponse = _countriesService.AddCountry(new CountryAddRequest() { CountryName = "Ireland" });
            }
                Guid? countryId = _countriesService.GetCountryByID(countryResponse.CountryId)?.CountryId;
            List<PersonAddRequest> personAddRequests =
            [
                new PersonAddRequest()
                {
                    Address = "TestStr",
                    CountryId = countryId,
                    Dob = new DateTime(2000,1,1),
                    Email = "123@456.com",
                    Gender = GenderOptions.Female,
                    Name = "Dave",
                    RecieveNewsletters = true
                },
                new PersonAddRequest()
                {
                    Address = "TestStr2",
                    CountryId = countryId,
                    Dob = new DateTime(2000,1,1),
                    Email = "123@456.com",
                    Gender = GenderOptions.Female,
                    Name = "Duda",
                    RecieveNewsletters = true
                },
                new PersonAddRequest()
                {
                    Address = "TestStr",
                    CountryId = countryId,
                    Dob = new DateTime(2000,1,1),
                    Email = "123@456.com",
                    Gender = GenderOptions.Female,
                    Name = "Aoife",
                    RecieveNewsletters = true
                },
                 new PersonAddRequest()
                {
                    Address = "TestStr",
                    CountryId = countryId,
                    Dob = new DateTime(2000,1,1),
                    Email = "123@456.com",
                    Gender = GenderOptions.Female,
                    Name = "Darragh",
                    RecieveNewsletters = true
                }
            ];
            
            foreach(PersonAddRequest personAddRequest in personAddRequests)
            {
                PersonResponse personResponse = AddPerson(personAddRequest);
                personResponses.Add(personResponse);

            }
           
            return personResponses;
        }
    }
}
