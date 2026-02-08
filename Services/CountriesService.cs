using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countriesList = [];

        public CountriesService (bool initialize = true)
        {
            _countriesList = [];
            if (initialize)
            {
                _countriesList.AddRange
                 (
                    new Country() { CountryId = Guid.Parse("D855B9CC-87DC-47BD-836B-6C1FF3F26B09"), CountryName = "Ireland" },
                    new Country() { CountryId = Guid.Parse("5FC4199D-31AB-432B-9AE4-CA89DF93445A"), CountryName = "Germany" },
                    new Country() { CountryId = Guid.Parse("DD15BE81-3EBA-4609-A5D9-FE471ABECAF0"), CountryName = "Brazil" },
                    new Country() { CountryId = Guid.Parse("C3643444-642D-41E3-964E-E5889B0E9CCE"), CountryName = "UK" },
                    new Country() { CountryId = Guid.Parse("CFBDC2B9-3B89-4CC4-9BF1-6408239933D1"), CountryName = "Spain" }
                 );
            }
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            //validate all parameters of CountryAddRequest
            //Country cannot be null
            ArgumentNullException.ThrowIfNull(countryAddRequest);
            //CountryName cannot be null
            if (countryAddRequest.CountryName == null) throw new ArgumentException(nameof(countryAddRequest.CountryName));
            //Duplicate countries are not allowed 
            if (_countriesList.Any((temp) => temp.CountryName == countryAddRequest.CountryName)) throw new ArgumentException($"Duplicate Country: {countryAddRequest.CountryName}");

            //convert countryAddRequest from type CountryAddRequest to type Country
            Country? country = countryAddRequest.ToCountry();

            //Generate a new CountryId
            country.CountryId = Guid.NewGuid();

            //then add into List<Country> NB. Duplicate countries are not allowed
            _countriesList.Add(country);

            //return Country object with generated CountryId
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            //Convert all countries from 'Country' type to 'CountryResponse' type
            //Return all country response objects
            return [.. _countriesList.Select((country) => country.ToCountryResponse())];
        }

        public CountryResponse? GetCountryByID(Guid? countryID)
        {
            //Validate all parameters
            //Guid cannot be null
            if (countryID == null) return null;

            //get matching country from list basewd on given guid
            Country? country =  _countriesList.FirstOrDefault(temp => temp.CountryId.Equals(countryID));

            //convert matching country from Country to CountryResponse type
            CountryResponse? country_response_frm_get_method;
            if (country != null)
            {
                country_response_frm_get_method = country.ToCountryResponse();

                //Return CountryResponse object
                return country_response_frm_get_method;
            }

            return null;
        }
    }
}
