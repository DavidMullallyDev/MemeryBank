using ServiceContracts;
namespace Services
{
    public class CitiesService : ICitiesService, IDisposable
    {
        private Guid _serviceInstanceId;
        public Guid ServiceInstanceId 
        {
            get
            {
                return _serviceInstanceId;
            }
         }
        private List<string> _citiesList = [];

        public CitiesService()
        {
            _serviceInstanceId = Guid.NewGuid();
            _citiesList = ["dublin", "recife", "rome", "duesseldorf"];
            //TODO: Add logic to open the database connection
        }

        public List<string> GetCities()
        {
           return _citiesList;
        }

        // this will be called at the end of the lifetime (transient, scoped, singleton)
        // howerver database connection shoild be called on the datatransfer is finished
        // this can be achieved using chained scopes 
        public void Dispose()
        {
            //TODO: Add logic to close database connection
        }
    }
}
