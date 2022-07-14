using System.Globalization;
using System.Text.RegularExpressions;

namespace FinancialRecord.API.Helpers
{
    public class DateTimeHelper
    {
        /// <summary>
        /// Evaluate the DateTime with Valid Format (dd/MM/yyyy)
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static bool IsValidDate(string dateOfBirth)
        {
            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
            //Verify whether date entered in dd/MM/yyyy format.
            bool isValid = regex.IsMatch(dateOfBirth.Trim());

            string[] dateFormats = { "dd/MM/yyyy" };
            DateTime dt;
            isValid = DateTime.TryParseExact(dateOfBirth, dateFormats,
                                                new CultureInfo("en-GB"),
                                                DateTimeStyles.None, out dt);
            if (isValid)
                return true;
            else
                return false;
        }
    }
}
