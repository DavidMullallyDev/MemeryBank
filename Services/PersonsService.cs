using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;
using System.Net.Http.Headers;

namespace Services
{
    public class PersonsService : IPersonService
    {
        //Private field
        private readonly List<Person> _persons;
        private readonly CountriesService _countriesService;

        public PersonsService(bool initialize = true)
        {
            _persons = [];
            _countriesService = new CountriesService();

            List <CountryResponse> allCountries = _countriesService.GetAllCountries();
            if (initialize)
            {
                AddPerson(new PersonAddRequest() { Name = "Rhoda", Address = "address1", CountryId = allCountries[0].CountryId, Dob = new DateTime(1985, 7, 1), Email = "email1@email.com", Gender = GenderOptions.Diverse, RecieveNewsLetters = true });
                AddPerson(new PersonAddRequest() { Name = "Anjela", Address = "address2", CountryId = allCountries[1].CountryId, Dob = new DateTime(1965, 9, 10), Email = "email2@email.com", Gender = GenderOptions.Female, RecieveNewsLetters = false });
                AddPerson(new PersonAddRequest() { Name = "P3", Address = "address1", CountryId = allCountries[0].CountryId, Dob = new DateTime(1985, 7, 1), Email = "email1@email.com", Gender = GenderOptions.Diverse, RecieveNewsLetters = true });
                AddPerson(new PersonAddRequest() { Name = "P4", Address = "address2", CountryId = allCountries[1].CountryId, Dob = new DateTime(1965, 9, 10), Email = "email2@email.com", Gender = GenderOptions.Female, RecieveNewsLetters = false });
                AddPerson(new PersonAddRequest() { Name = "P1", Address = "address1", CountryId = allCountries[0].CountryId, Dob = new DateTime(1985, 7, 1), Email = "email1@email.com", Gender = GenderOptions.Diverse, RecieveNewsLetters = true });
                AddPerson(new PersonAddRequest() { Name = "P2", Address = "address2", CountryId = allCountries[1].CountryId, Dob = new DateTime(1965, 9, 10), Email = "email2@email.com", Gender = GenderOptions.Female, RecieveNewsLetters = false });
                AddPerson(new PersonAddRequest() { Name = "P3", Address = "address1", CountryId = allCountries[0].CountryId, Dob = new DateTime(1985, 7, 1), Email = "email1@email.com", Gender = GenderOptions.Diverse, RecieveNewsLetters = true });
                AddPerson(new PersonAddRequest() { Name = "P4", Address = "address2", CountryId = allCountries[1].CountryId, Dob = new DateTime(1965, 9, 10), Email = "email2@email.com", Gender = GenderOptions.Female, RecieveNewsLetters = false });
                AddPerson(new PersonAddRequest() { Name = "P1", Address = "address1", CountryId = allCountries[0].CountryId, Dob = new DateTime(1985, 7, 1), Email = "email1@email.com", Gender = GenderOptions.Diverse, RecieveNewsLetters = true });
                AddPerson(new PersonAddRequest() { Name = "P2", Address = "address2", CountryId = allCountries[1].CountryId, Dob = new DateTime(1965, 9, 10), Email = "email2@email.com", Gender = GenderOptions.Female, RecieveNewsLetters = false });
            }
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

            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByID(personResponse.CountryId)?.CountryName;
            PersonResponse covertedPersonRwsponse = ConvertPersonToPersonResponse(person);
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetPersonList()
        {
            List<PersonResponse> list = [.. _persons.Select(p => ConvertPersonToPersonResponse(p))];
            return [.. _persons.Select(p => ConvertPersonToPersonResponse(p))];
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

            if (searchBy.Equals("Country"))
            {
                return allPersons.Where(p => p.Country.Contains(searchStr))?.ToList();
            }
            return searchBy switch
            {
                nameof(PersonResponse.Name) =>
                    [.. allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Name) &&
                        p.Name.Contains(searchStr, StringComparison.OrdinalIgnoreCase))],

                nameof(PersonResponse.Email) =>
                    [.. allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Email) &&
                        p.Email.Contains(searchStr, StringComparison.OrdinalIgnoreCase))],

                nameof(PersonResponse.Dob) =>
                    [.. allPersons.Where(p =>
                        p.Dob != null &&
                        p.Dob.Value.ToString("dd MMMM yyyy")
                            .Contains(searchStr, StringComparison.OrdinalIgnoreCase))],

                nameof(PersonResponse.Gender) =>
                    [.. allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Gender) &&
                        p.Gender.StartsWith
                        
                        
                        (searchStr, StringComparison.OrdinalIgnoreCase))],

                nameof(PersonResponse.CountryId) =>
                    [.. allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Country) &&
                        p.Country.Contains(searchStr, StringComparison.OrdinalIgnoreCase))],

                nameof(PersonResponse.Address) =>
                    [.. allPersons.Where(p =>
                        !string.IsNullOrEmpty(p.Address) &&
                        p.Address.Contains(searchStr, StringComparison.OrdinalIgnoreCase))],

                

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
                nameof(Person.RecieveNewsLetters) => p => p.RecieveNewsletters,
                _ => p => p.Name // default sort
            };

            return sortBy == null ? personsToSort : sortOrder == SortOrderOptions.ASC
                ? [.. personsToSort.OrderBy(keySelector)]
                : [.. personsToSort.OrderByDescending(keySelector)];
        }

        public PersonResponse? UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            ArgumentNullException.ThrowIfNull(personUpdateRequest);

            ValidationHelper.ModelValidation(personUpdateRequest);

            Person? personToUpdate = _persons.Where(p => p.Id == personUpdateRequest?.Id).FirstOrDefault() ?? throw new ArgumentException($"No person found with this Id: {personUpdateRequest.Id}");

            personToUpdate.Address = personUpdateRequest?.Address;
            personToUpdate.Gender = personUpdateRequest?.Gender.ToString();
            personToUpdate.RecieveNewsLetters = personUpdateRequest.RecieveNewsLetters;
            personToUpdate.Email = personUpdateRequest.Email;
            personToUpdate.CountryId = personUpdateRequest.CountryId;
            personToUpdate.Dob = personUpdateRequest.Dob;
            personToUpdate.Name = personUpdateRequest.Name;

            return personToUpdate.ToPersonResponse();     
        }

        public PersonResponse? DeletePerson(PersonDeleteRequest? personDeleteRequest)
        {
            if (personDeleteRequest == null) throw new ArgumentNullException();

            Person? personToDelete = _persons.Where(p => p.Id == personDeleteRequest.Id).FirstOrDefault();

            if ( personToDelete == null)
            {
                throw new ArgumentException("Person not found");
            }
            else
            {
                if(_persons != null)
                {
                    personToDelete = _persons.Where(p => p.Id == personDeleteRequest.Id).FirstOrDefault();
                    if (personToDelete != null)
                    {
                        _persons.Remove(personToDelete);

                        return personToDelete.ToPersonResponse();
                    }
                }

                return null;
            }    
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
                Guid? countryId = countryResponse != null ? _countriesService.GetCountryByID(countryResponse.CountryId)?.CountryId : null;
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
                    RecieveNewsLetters = true
                },
                new PersonAddRequest()
                {
                    Address = "TestStr2",
                    CountryId = countryId,
                    Dob = new DateTime(2000,1,1),
                    Email = "123@456.com",
                    Gender = GenderOptions.Female,
                    Name = "Duda",
                    RecieveNewsLetters = true
                },
                new PersonAddRequest()
                {
                    Address = "TestStr",
                    CountryId = countryId,
                    Dob = new DateTime(2000,1,1),
                    Email = "123@456.com",
                    Gender = GenderOptions.Female,
                    Name = "Aoife",
                    RecieveNewsLetters = true
                },
                 new PersonAddRequest()
                {
                    Address = "TestStr",
                    CountryId = countryId,
                    Dob = new DateTime(2000,1,1),
                    Email = "123@456.com",
                    Gender = GenderOptions.Female,
                    Name = "Darragh",
                    RecieveNewsLetters = true
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
