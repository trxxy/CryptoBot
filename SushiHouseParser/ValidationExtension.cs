using System.Linq;

namespace SushiHouseParser
{
    static class ValidationExtension
    {
        const string SPACE_CHAR = " ";
        const string TWO_SPACE_CHAR = "  ";
        const string NEW_LINE = "\n";
        const string NUMERIC_CHAR = "01234567890.,";

        public static string ValidateDescription(this string input)
        {
            input = input.Replace(TWO_SPACE_CHAR, string.Empty);
            input = input.Replace(NEW_LINE, string.Empty);
            return input;
        }

        public static decimal ValidatePrice(this string input)
        {
            var allowedChars = NUMERIC_CHAR;
            var str = new string(input.Where(c => allowedChars.Contains(c)).ToArray()).Replace(".", ",");
            return decimal.Parse(str);
        }

        public static decimal ValidateWeight(this string input)
        {
            input = input.Replace(SPACE_CHAR, string.Empty);
            return decimal.Parse(new string(input.TakeWhile(char.IsDigit).ToArray()));
        }
    }
}