using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;
using Xunit.Abstractions;
using Entities;

namespace Tests
{
    public class PersonsServiceTest
    {
        //Private fields
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _outputHelper;

        //Constructor
        public PersonsServiceTest(ITestOutputHelper testOutputHelper) 
        {
            _personService = new PersonsService();
            _countriesService = new CountriesService();
            _outputHelper = testOutputHelper;
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

        #region GetAllPersons
        /// <summary>
        /// Should return an empty list by default
        /// </summary>
        [Fact]
        public void GetAllPersons_Empty()
        {
            Assert.Empty(_personService.GetPersonList());
        }

        /// <summary>
        /// When a null value is supplied instead of an id, it should return null
        /// </summary>
        [Fact]
        public void GetAllPersons_AllPersons()
        {
            //Arrange
            List<PersonResponse>? personResponses = _personService.AddSomeMockData();

            _outputHelper.WriteLine("Expected:");
            //print persons_response_from_add

            if(personResponses != null && personResponses.Count > 0)
            {
                foreach (PersonResponse person_response_from_add in personResponses)
                {
                    _outputHelper.WriteLine(person_response_from_add.ToString());
                }
                ;
            }
            
            _outputHelper.WriteLine("Actual:");
            //Act
            List<PersonResponse> persons_response_from_get = _personService.GetPersonList();
            //print persons_response_from_get
            foreach (PersonResponse person_response_from_get in persons_response_from_get)
            {
                _outputHelper.WriteLine(person_response_from_get.ToString());
            };

            //Assert
            Assert.Equal(persons_response_from_get, personResponses);
        }
        #endregion

        #region GetFilteredPersons
        /// <summary>
        /// When field is Name and search str is empty, it should return all persons
        /// </summary>
        /// 
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            //Arrange
            List<PersonResponse>? persons_response_from_add = _personService.AddSomeMockData();

            _outputHelper.WriteLine("Expected:");
            //print persons_response_from_add
            if(persons_response_from_add != null && persons_response_from_add.Count > 0)
            {
                foreach (PersonResponse person_response_from_add in persons_response_from_add)
                {
                    _outputHelper.WriteLine(person_response_from_add.ToString());
                }
            }  

            _outputHelper.WriteLine("Actual:");
            //Act
            List<PersonResponse>? filtered_persons_response_from_search = _personService.GetFilteredPersons(nameof(Person.Name), "");
            //print persons_response_from_get
            if(filtered_persons_response_from_search != null)
            {
                foreach (PersonResponse filtered_person_response_from_search in filtered_persons_response_from_search)
                {
                    _outputHelper.WriteLine(filtered_person_response_from_search.ToString());
                }

                //Assert
                if(persons_response_from_add != null && persons_response_from_add.Count > 0)
                {
                    foreach (PersonResponse person_response_from_add in persons_response_from_add)
                    {
                        Assert.Contains(person_response_from_add, filtered_persons_response_from_search);
                    }
                }
               
            } 
        }

        /// <summary>
        /// When field is Name and search str has value it should return all persons who match
        /// </summary>
        /// 
        [Fact]
        public void GetFilteredPersons_TextSearchByName()
        {
            string searchText = "Ao";
            //Arrange
            List<PersonResponse>? personResponses = _personService.AddSomeMockData();

            List<PersonResponse> filtered_persons_response_from_add = [];

            _outputHelper.WriteLine("Expected:");
            //print persons_response_from_add
            if(personResponses != null && personResponses.Count > 0)
            {
                foreach (PersonResponse person_response_from_add in personResponses)
                {
                    if (person_response_from_add.Name != null && person_response_from_add.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        filtered_persons_response_from_add.Add(person_response_from_add);
                        _outputHelper.WriteLine(person_response_from_add.ToString());
                    }
                }
                ;
            }
           
            _outputHelper.WriteLine("Actual:");

            //Act
            List<PersonResponse>? filtered_persons_response_from_search = _personService.GetFilteredPersons(nameof(Person.Name), searchText);
            //print persons_response_from_get
            if(filtered_persons_response_from_search != null)
            {
                foreach (PersonResponse filtered_person_response_from_search in filtered_persons_response_from_search)
                {
                    _outputHelper.WriteLine(filtered_person_response_from_search.ToString());
                }
            }
            
            //Assert
            foreach (PersonResponse person_response_from_add in filtered_persons_response_from_add)
            {
                if(person_response_from_add.Name != null)
                {
                    if (person_response_from_add.Name.Contains("da", StringComparison.OrdinalIgnoreCase))
                    {
                        if (filtered_persons_response_from_search != null)
                        {
                            Assert.Contains(person_response_from_add, filtered_persons_response_from_search);
                        }
                    }
                } 
            }
        }
        #endregion

        #region GetSortedPersons
        /// <summary>
        /// When sort by person name in ascending order, it shoul return the list sorted in ascendin godr of name 
        /// </summary>
        /// 
        [Fact]
        public void GetSortedPersons()
        {
            //Arrange

            //Act

            List<PersonResponse> personsResponsesFromAdd = [.. _personService.AddSomeMockData().OrderByDescending(p => p.Name)];

            _outputHelper.WriteLine("Expected:");
            foreach (PersonResponse sorted_persons_from_add in personsResponsesFromAdd)
            {
                _outputHelper.WriteLine(sorted_persons_from_add.ToString());
            }

            List<PersonResponse> sorted_persons_from_sort = _personService.GetSortedPersons(personsResponsesFromAdd, nameof(Person.Name), SortOrderOptions.DESC);

            _outputHelper.WriteLine("Actual:");
            foreach (PersonResponse sortedPerson in sorted_persons_from_sort)
            {
                _outputHelper.WriteLine(sortedPerson.ToString());
            }

            //Assert 
            for(int i = 0; i < personsResponsesFromAdd.Count; i++)
            {
                Assert.Equal(personsResponsesFromAdd[i], sorted_persons_from_sort[i]);
            }
        }
        #endregion

        #region UpdatePerson
        [Fact]
        public void UpdatePerson_NullPersonUpdateRequest()
        {
            PersonUpdateRequest? personUpdateRequest = null;

            Assert.Throws<ArgumentException>(() =>
            {
                PersonResponse? personResponse = _personService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public void UpdatePerson_InvalidId()
        {
            PersonUpdateRequest? personUpdateRequest = new() { Id = Guid.NewGuid()};

            Assert.Throws<ArgumentException>(() =>
            {
                PersonResponse? personResponse = _personService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public void UpdatePerson_NameIsNull()
        {
            List<PersonResponse> allPersons = _personService.AddSomeMockData();

            PersonUpdateRequest personUpdateRequest = new()
            {
                Id = allPersons[2].Id,
                Name = null,
                Address = "TestAddress",
                Gender = GenderOptions.Diverse,
                Dob = new DateTime(2002, 1, 1),
                RecieveNewsletters = false,
                Email = "test@email.com"
            };

            Assert.Throws<ArgumentException>(()=>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public void UpdatePerson_ValidPersonUpdateRequest()
        {
            List<PersonResponse> allPersons = _personService.AddSomeMockData();

            PersonResponse personToUpdate = allPersons[1];
            PersonUpdateRequest personUpdateRequest = new()
            {
                Id = personToUpdate.Id,
                Name = "TestUpdate",
                Address = "TestAddress",
                Gender = Enum.Parse<GenderOptions>(personToUpdate.Gender, true),
                Dob = personToUpdate.Dob,
                RecieveNewsletters = personToUpdate.RecieveNewsletters,
                CountryId = personToUpdate.CountryId,
                Email = "updated@email.com"
            };

            personToUpdate.Name = personUpdateRequest.Name;
            personToUpdate.Address = personUpdateRequest.Address;
            personToUpdate.Email = personUpdateRequest.Email;
            
            PersonResponse? personResponseFromUpdate = _personService.UpdatePerson(personUpdateRequest);


            _outputHelper.WriteLine("Expected:");
            _outputHelper.WriteLine(personToUpdate.ToString());

           

            _outputHelper.WriteLine("Actual:");
            _outputHelper.WriteLine(personResponseFromUpdate.ToString());

            Assert.Equal(personToUpdate,personResponseFromUpdate);
        }
        #endregion
    }
}
