using Entities;

namespace ServiceContracts
{
    public interface ICalenderService
    {
        /// <summary>
        /// Returns a list of public holidays as date-only values.
        /// </summary>
        public void GetHolidays();
    }
}
