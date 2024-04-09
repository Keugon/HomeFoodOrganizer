using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.Tools
{
    /// <summary>
    /// Class that Provides general use to the app
    /// </summary>
    public static class General : System.Object
    {
        /// <summary>
        /// Get the current Date 
        /// </summary>
        /// <returns>string returns in Format eg.
        /// Day_Month_year/ 26_10_2024</returns>
        public static string GetCurrentDate()
        {
            string currentDay = DateTime.Today.Day.ToString();
            string currentMonth = DateTime.Today.Month.ToString();
            string currentYear = DateTime.Today.Year.ToString();

            return $"{currentDay}_{currentMonth}_{currentYear}"; ;
        }

    }
}
