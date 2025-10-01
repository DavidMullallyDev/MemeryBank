using ServiceContracts;
namespace Services
{
    public class CitiesService : ICitiesService
    {
        private List<string> _citiesList = [];

        public CitiesService()
        {
            _citiesList = ["dublin", "recife", "rome", "duesseldorf"];
        }

        public List<string> GetCities()
        {
           return _citiesList;
        }
    }
}
