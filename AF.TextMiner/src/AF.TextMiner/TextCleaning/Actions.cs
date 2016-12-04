using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AF.TextMiner.TextCleaning
{
    public class Actions
    {
        public static string Clean(string textToClean, TextCleaningConfiguration config)
        {
            if (config.ToLowerCase)
                textToClean = textToClean.ToLower();
            if (config.ReplaceDoubleSpace)
                while (textToClean.Contains("  ")) textToClean = textToClean.Replace("  ", " ");



            return textToClean;
        }

        public static string RemoveSpecificString(string textToClean, string stringToRemove)
        {
            return textToClean.Replace(stringToRemove, "");
        }
    }
}
