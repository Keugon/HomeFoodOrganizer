using System.Globalization;

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
    /// <summary>
    /// Returns the Filename from a Fullpath
    /// </summary>
    public class FilePathToFileNameConverter : IValueConverter
    {
        /// <summary>
        /// Path to File
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string filePath)
            {
                return Path.GetFileName(filePath);
            }
            return value!; // Return original value if it's not a string
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// checks if in a multibinding with 2 Binds the ToString equal is if so gives a truthfull return
    /// </summary>
    public class TwoValueMustEqualMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// If 1 and 2 same true
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return false;

            string value1 = values[0]?.ToString()!;
            string value2 = values[1]?.ToString()!;

            return value1 == value2;
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
