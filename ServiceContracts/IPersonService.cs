using ServiceContracts.DTO;

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
    }
}
