
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating person entity
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Finds a person with the given Id
        /// </summary>
        /// <param name="Id">Id to find</param>
        /// <returns>Returns person with that Id</returns>
        PersonResponse? GetPersonByID(Guid? Id);
        /// <summary>
        /// Adds a new Person into the existing list of persons
        /// </summary>
        /// <param name="personAddRequest">Person to add</param>
        /// <returns>Returns newly added person along with newly generated ID</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Returns all persons
        /// </summary>
        /// <returns>Returns a list of objects of PersonResponse type</returns>
        List<PersonResponse> GetPersonList();

        /// <summary>
        /// Returns all persons whose name contain the search string provided
        /// </summary>
        /// <param name="searchBy">Which field to search</param>
        /// <param name="searchStr">the value to search for</param>
        /// <returns>Returns a list of objects of PersonResponse type matching the serach str</returns>
        List<PersonResponse>? GetFilteredPersons(string searchBy, string? searchStr);

        /// <summary>
        /// Returns all persons sorted in either asc or desc order
        /// </summary>
        /// <param name="personsToSort">Represents the list of persons to sort</param>
        /// <param name="sortBy">Name of the property (key) by which the perosns should be sorted</param>
        /// <param name="sortOrder">Order in which the persons should be sorted</param>
        /// <returns>Returns sorted persons as List of PersonResponse</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> personsToSort, string sortBy, SortOrderOptions sortOrder);

        /// <summary>
        /// Update the Person with the given ID and values and return an object of PersObjectResponse
        /// </summary>
        /// <param name="id">Id of person to be updated</param>
        /// <param name="personUpdateRequest">contains updated info</param>
        /// <returns>Returns the PersonUpdateResponse object</returns>
        PersonResponse? UpdatePerson(PersonUpdateRequest? personUpdateRequest);

        // <summary>
        /// Delete the Person with the given ID and values and return an object of PersonResponse
        /// </summary>
        /// <param name="id">Id of person to be deleted</param>
        /// <returns>Returns the PersonResponse object</returns>
        PersonResponse? DeletePerson(PersonDeleteRequest? personDeleteRequest);

        List<PersonResponse> AddSomeMockData();
    }
}
