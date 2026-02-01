using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating country entry
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country Object to add</param>
        /// <returns>Returns the country object after adding it(including newly generated country id</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);


        /// <summary>
        /// Returns a list of all countries
        /// </summary>
        /// <returns>All countries from the list as list of CountryResponse</returns>
        List<CountryResponse> GetAllCountries();


        /// <summary>
        /// Returns a country object based on the guid provided
        /// </summary>
        /// <param name="countryID">Country Id (guid) to search for</param>
        /// <returns>Matching country object</returns>
        CountryResponse? GetCountryByID(Guid? countryID);
    }
}
