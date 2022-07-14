using FinancialRecord.API.ApplicationEnum;

namespace FinancialRecord.API.Helpers
{
    public static class UploadFileHelper
    {
        /// <summary>
        /// Get the AccountType enumerated value
        /// </summary>
        /// <param name="ItemName">ItemName</param>
        /// <returns></returns>
        public static FinanceAccountType GetFinanceAccountTypeEnumValue(string ItemName)
        {
            return (FinanceAccountType)Enum.Parse(typeof(FinanceAccountType), ItemName);
        }
        /// <summary>
        /// Get the Status enumerated value
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public static FinanceStatus GetFinanceStatusEnumValue(string ItemName)
        {
            return (FinanceStatus)Enum.Parse(typeof(FinanceStatus), ItemName);
        }
        /// <summary>
        /// Convert the string value to double generic method
        /// </summary>
        /// <param name="number">number</param>
        /// <returns></returns>
        public static double ConvertToDouble(string number)
        {
            if (String.IsNullOrWhiteSpace(number))
                return 0; // or throw exception, or whatever

            // Instead of all those "IndexOf" and "Substrings"
            var temp = number.Replace("x 10^", "E");

            double returnDouble;
            if (double.TryParse(temp, out returnDouble))
                return returnDouble;

            // Return whatever or throw an exception, etc.
            return 0;
        }
        /// <summary>
        /// Convert the string value to decimal generic method
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(string price)
        {
            decimal result = string.IsNullOrEmpty(price) ? 0 : decimal.Parse(price);
            return result;
        }
    }
}
