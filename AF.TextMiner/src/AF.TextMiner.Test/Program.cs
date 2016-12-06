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

            AF.TextMiner.TextCleaning.TextCleaningConfiguration config = new TextCleaning.TextCleaningConfiguration()
                                                                            { ToLowerCase = true,
                                                                                ReplaceDoubleSpace = true
                                                                            };
            

            // test library
            string textToProcess = @"The probability and statistics cookbook is a succinct representation of various topics in probability theory and statistics. It provides a comprehensive mathematical reference reduced to its essence, rather than aiming for elaborate explanations.";
            string text2 = "probability theory and statistics. It provides a comprehensive";
            string text3 = "probability theory and statistics";
            string textWith2And = "The probability and statistics cookbook is a succinct representation of various topics in probability theory and statistics.";
            string only3words = "words words words.";
            string tweet_neg1 = "Para   qu no soy tan duro  Hay algo peor que #messi  Y es a propaganda de #movistar !!";
            string tweet_pos1 = "Felicidades a @Telcel xq despues de hacerme prdr mi tiempo durante 1 mes y hacerme pagar sin recib el serv, me dieron soluc, bravo! hurra!";
            string cleanedTweet_neg1 = AF.TextMiner.TextCleaning.CleaningActions.Clean(tweet_neg1, config);


            var corpus = AF.TextMiner.TextCorpus.GenerateNewCorpus("test", text3);

            corpus.CalculatePercentagesAndSort();
            System.Console.ReadLine();
        }
    }
}
