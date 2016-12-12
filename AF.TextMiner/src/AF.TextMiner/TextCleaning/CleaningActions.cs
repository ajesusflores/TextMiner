using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AF.TextMiner.TextCleaning
{
    public class CleaningActions
    {
        public static string Clean(string textToClean, TextCleaningConfiguration config)
        {
            if(config.ToLowerCase)
                textToClean = textToClean.ToLower();
            
            if (config.RemoveTwitterAccounts)
                textToClean = RemoveTwitterAccounts(textToClean);
            if (config.RemoveLinks)
                textToClean = RemoveLinks(textToClean);

            if (config.ReplaceDoubleSpace)
                while (textToClean.Contains("  ")) textToClean = textToClean.Replace("  ", " ");
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

        public static string RemoveLinks(string textToClean)
        {
            return Regex.Replace(textToClean, @"http[^\s]+", "");
        }


        static string RemoveLink(string tweet)
        {
            string[] words = tweet.Split(' ');
            for (int i = 0; i < words.Length; i++)
                words[i] = words[i].StartsWith("http://") ? string.Empty : words[i];
            return string.Join(" ", words);
        }
    }
}
