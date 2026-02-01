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
            return _persons.Select(p => p.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByID(Guid? Id)
        {
            PersonResponse? personResponse = _persons.FirstOrDefault(p => p.Id.Equals(Id))?.ToPersonResponse();
            return personResponse ?? null;
        }

        public List<PersonResponse>? GetFilteredPersons(string searchBy, string? searchStr)
        {
            if (searchBy == null || string.IsNullOrEmpty(searchStr)) return GetPersonList();

            if (searchBy.Equals("Name")) return [.. GetPersonList().Where(p => p.Name.Contains(searchStr, StringComparison.OrdinalIgnoreCase))];

            throw new NotImplementedException();
        }

        public List<PersonAddRequest> AddSomeMockData()
        {
            CountryResponse? countryResponse = _countriesService.AddCountry(new CountryAddRequest() { CountryName = "Ireland" });
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

            return personAddRequests;
        }
    }
}
