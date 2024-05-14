﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
    public class FilePathToFileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string filePath)
            {
                return Path.GetFileName(filePath);
            }
            return value; // Return original value if it's not a string
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
