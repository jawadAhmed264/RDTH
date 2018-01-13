using System;
using System.Text.RegularExpressions;

namespace RDTH.Areas.Admin.CustomClasses
{
    public class CardNumberGenerator
    {
        public static string CardNumber() {
            Random rn = new Random();

            string charsToUse = "1234567890";


            MatchEvaluator RandomChar = delegate (Match m)
            {
                return charsToUse[rn.Next(charsToUse.Length)].ToString();
            };

            return Regex.Replace("XXXX-XXXX-XXXX", "X", RandomChar).ToString();
        }
    }
}
