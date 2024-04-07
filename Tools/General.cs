using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.Tools
{
    public static class General : System.Object
    {

        public static string GetCurrentDate()
        {
            string currentDay = DateTime.Today.Day.ToString();
            string currentMonth = DateTime.Today.Month.ToString();
            string currentYear = DateTime.Today.Year.ToString();

            return $"{currentDay}_{currentMonth}_{currentYear}"; ;
        }

    }
}
