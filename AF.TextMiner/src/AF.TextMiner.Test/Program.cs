using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF.TextMiner.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string textToProcess = @"The probability and statistics cookbook is a succinct representation of various topics in probability theory and statistics. It provides a comprehensive mathematical reference reduced to its essence, rather than aiming for elaborate explanations.";
            string text2 = "probability theory and statistics. It provides a comprehensive";
            string text3 = "probability theory and statistics";
            string textWith2And = "The probability and statistics cookbook is a succinct representation of various topics in probability theory and statistics.";
            string only3words = "words words words.";
            var corpus = AF.TextMiner.TextCorpus.GenerateNewCorpus("test", text2);
        }
    }
}
