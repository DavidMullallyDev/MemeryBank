using ServiceContracts;
using Xunit;
using Entities;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;

namespace Tests
{
    public class PersonsServiceTest
    {
        //Private fields
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;

        //Constructor
        public PersonsServiceTest() 
        {
            _personService = new PersonsService();
            _countriesService = new CountriesService();
        }

        #region GetPersonById
        /// <summary>
        /// When a null value is supplied instead of an id, it should return null
        /// </summary>
        [Fact]
        public void GetPersonById_NullID()
        {
            //Arrange
            Guid? id = null;

            //Act
            PersonResponse? person_response_from_get = _personService.GetPersonByID(id);
            
            //Assert
            Assert.Null(person_response_from_get);
        }

        /// <summary>
        /// When a valid Id is supllied, person with that Id should be returned
        /// </summary>
        [Fact]
        public void GetPersonByName_ValidId() 
        {
            //Arrange
            CountryResponse? countryResponse = _countriesService.AddCountry(new CountryAddRequest() { CountryName = "Ireland" });
            Guid? countryId = _countriesService.GetCountryByID(countryResponse.CountryId)?.CountryId;

            PersonAddRequest personAddRequest = new()
            {
                Name ="Test",
                Email = "abc@def.com",
                CountryId = countryId,
                Address = "TestAdress St.",
                Dob = new DateTime(1985, 1, 7),
                Gender = GenderOptions.Male,
                RecieveNewsletters = false
            };

            PersonResponse? person_response_from_add = _personService.AddPerson(personAddRequest);

            //Act
            PersonResponse? person_response_from_get = _personService.GetPersonByID(person_response_from_add.Id);

            //Assert
            Assert.Equal(person_response_from_add, person_response_from_get);
        }
        #endregion

        #region AddPerson
        /// <summary>
        /// When a null value is supplied instead of object, it should throw an ArgumentNullException
        /// </summary>
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange 
            PersonAddRequest? personAddRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    _personService.AddPerson(personAddRequest);
                }); 
        }

        /// <summary>
        /// When a null value  as PersonName is supllied, it should throw an ArgumentException (Object is not null but name value is missing)
        /// </summary>
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange 
            PersonAddRequest? personAddRequest = new() { Name = null};

            //Act
            Assert.Throws<ArgumentException>(
                () =>
                {
                    _personService.AddPerson(personAddRequest);
                });
        }

        //Can repeat this for all properties


        /// <summary>
        /// When all rewuired person details are supplied it should add person to list
        /// and it should return an object of PersonResponse type  with newly generated Guid
        /// </summary>
        [Fact]
        public void AddPerson_ValidPersonDetails()
        {
            //Arrange 
            PersonAddRequest? personAddRequest = new() 
            {
                Name = "Test",
                Address = "Test Address",
                CountryId= Guid.NewGuid(), 
                Dob = new DateTime(1985, 1, 7), 
                Email="abc@def.com",
                Gender = GenderOptions.Male, 
                RecieveNewsletters = false
            };

            //Act
            PersonResponse person_response_from_add= _personService.AddPerson(personAddRequest);

            List<PersonResponse> allPersons = _personService.GetPersonList();

            //Assert
            Assert.True(person_response_from_add.Id != Guid.Empty);

            Assert.Contains(person_response_from_add, allPersons);
        }
        #endregion
    }
}
