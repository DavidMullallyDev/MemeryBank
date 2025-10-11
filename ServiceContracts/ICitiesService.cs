namespace ServiceContracts
{
    public interface ICitiesService
    {
        public Guid ServiceInstanceId { get; }

        List<string> GetCities();
    }
}
