using System.Text.RegularExpressions;

namespace MemeryBank.Api.Constraints
{
    public class MonthsCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if(!values.ContainsKey(routeKey)) return false;

            Regex regex = new Regex($"^(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)$");
            string? month = Convert.ToString(values[routeKey]);

            if (regex.IsMatch(month!)) return true;

            return false;
        }
    }
}
