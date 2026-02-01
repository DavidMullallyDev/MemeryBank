using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;

namespace Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        //constructor
        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry
        //Example Test requiremnets
        //When CountryAddRequest == null a NullArgumentException should be thrown
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;
  
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }

        //When CountryName == null a ArgumentException should be thrown
        [Fact]
        public void AddCountry_CountryNameIsNull() 
        {
            //Arrange
            CountryAddRequest? request =new() { CountryName = null};

            //Assert
            Assert.Throws<ArgumentException>(() => 
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }

        //When the CounryName is a duplicate a ArgumentException should be thrown
        [Fact]
        public void AddCountry_CountryNameIsDuplicate()
        {
            //Arrange
            CountryAddRequest? request = new() { CountryName = "Brazil" };
            CountryAddRequest? request2 = new() { CountryName = "Brazil" };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
                _countriesService.AddCountry(request2);
            });
        }

        //Valid entry should be inserted into the list of countries with a newly gereated Guid
        [Fact]
        public void AddCountry_ValidCountryEntry()
        {
            //Arrange
            CountryAddRequest? request = new() { CountryName = "Brazil" };

            //Act
            CountryResponse countryResponse = _countriesService.AddCountry(request);
            List<CountryResponse>  countries_frm_getAllCountries = _countriesService.GetAllCountries();

            //Assert

            //Guid has been generated and assigned to new country 
            Assert.True(countryResponse.CountryId != Guid.Empty);
            //new counrry has been added to list
            Assert.Contains(countryResponse, countries_frm_getAllCountries); 
            // internally calls the equals method 
            // .Equals by default only checks if the object refernces match not the values .. so (objA).Equals(objB) is always False for example
            // can override the Equals method in the CountryResponse class
        }
        
        #endregion

        #region GetAllCountries
        [Fact]
        //The list of countries should be empty by default (before any countries have been added)
        public void GetAllCountries_EmptyList()
        {
            //Arrange
            
            //Act
            List<CountryResponse> actual_country_CountryResponse_list = _countriesService.GetAllCountries();
            //Assert
            Assert.Empty(actual_country_CountryResponse_list);
        }

        //The List<CountryResponse> should match the actual list of countries
        [Fact]
        public void GetAllCountries_ListsMatch()
        {
            //Arrange
            List<CountryAddRequest> countryRequestList = [
                new CountryAddRequest() {CountryName = "Ireland"},
                new CountryAddRequest() {CountryName = "Germany"},
                new CountryAddRequest() {CountryName = "Brazil"}
            ];
            List<CountryResponse> expectedCountryResponseList = [];
            foreach (CountryAddRequest country_add_request in countryRequestList)
            {
                expectedCountryResponseList.Add(_countriesService.AddCountry(country_add_request));
            }

            //Act
            List<CountryResponse> actualCountryResponsLlist = _countriesService.GetAllCountries();

            ////read each element from countryResponseList
            foreach (CountryResponse expectedCountry in expectedCountryResponseList)
            {
                Assert.Contains(expectedCountry, actualCountryResponsLlist);
            }
        }
        #endregion

        #region GetCountryByID
        [Fact]
        public void GetCountryByID_EmptyId()
        {
            //Arrange 
            Guid? countryId = null;

            //Act
            CountryResponse? country_response_frm_get_method = _countriesService.GetCountryByID(countryId);

            //Assert
            Assert.Null(country_response_frm_get_method);
        }

        [Fact]
        //If we supply a valid country id, it should return the country as a CountryResponse
        public void GetCountryByID_MatchingCountry()
        {
            //Arrange 
            CountryResponse country_response_frm_add= _countriesService.AddCountry(new CountryAddRequest() { CountryName = "Ireland"});
            Guid countryId = country_response_frm_add.CountryId;

            //Act
            CountryResponse? counrry_response_frm_get_method = _countriesService.GetCountryByID(countryId);

            //Assert
            Assert.Equal(country_response_frm_add, counrry_response_frm_get_method);
        }
        #endregion
    }
}
