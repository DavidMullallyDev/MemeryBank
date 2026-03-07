using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace MemeryBank.Api.Controllers
{
    [Route("[controller]")]
    public class CalenderController : Controller
    {
        private readonly ICalenderService _calenderService;

        public CalenderController(ICalenderService calenderService)
        {
            _calenderService = calenderService;
        }

        [Route("[action]")]
        public IActionResult Index(int? year, int? month)
        {
            int y = year ?? DateTime.Now.Year;
            int m = month ?? DateTime.Now.Month;

            ViewBag.CurrentSelectedYear = y;
            ViewBag.CurrentSelectedMonth = m;

            ViewData["appTitle"] = "Memery Bank";
            ViewData["pageName"] = "Calender";

            var firstDay = new DateTime(y, m, 1);
            int daysInMonth = DateTime.DaysInMonth(y, m);

            // Calendar structure: 6 weeks × 7 days
            var calendar = new List<List<DateTime?>>();

            int currentDay = 1;

            // Start on Monday (ISO)
            int startOffset = ((int)firstDay.DayOfWeek + 6) % 7;

            for (int week = 0; week < 6; week++)
            {
                var weekRow = new List<DateTime?>();

                for (int day = 0; day < 7; day++)
                {
                    if (week == 0 && day < startOffset)
                    {
                        weekRow.Add(null); // empty cell before month starts
                    }
                    else if (currentDay > daysInMonth)
                    {
                        weekRow.Add(null); // empty cell after month ends
                    }
                    else
                    {
                        weekRow.Add(new DateTime(y, m, currentDay));
                        currentDay++;
                    }
                }

                calendar.Add(weekRow);
            }

            ViewBag.Calendar = calendar;
            return View();
        }
    }
}
