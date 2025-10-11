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
    public class ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country Object to add</param>
        /// <returns>Returns the country object after adding it(including newly generated country id</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {

        }
    }
}
