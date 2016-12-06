using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AF.TextMiner.TextCleaning
{
    public class CleaningActions
    {
        public static string Clean(string textToClean, TextCleaningConfiguration config)
        {
            if(config.ToLowerCase)
                textToClean = textToClean.ToLower();
            if(config.ReplaceDoubleSpace)
                while (textToClean.Contains("  ")) textToClean = textToClean.Replace("  ", " ");
            if (config.RemoveTwitterAccounts)
                textToClean = RemoveTwitterAccounts(textToClean);
                    

            return textToClean;
        }

        public static string RemoveSpecificString(string textToClean, string stringToRemove)
        {
            return textToClean.Replace(stringToRemove, "");
        }

        public static string RemoveTwitterAccounts(string textToClean)
        {
            string[] words = textToClean.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].StartsWith("@") ? string.Empty : words[i];
            }
            return string.Join(" ", words);
        }
    }
}
