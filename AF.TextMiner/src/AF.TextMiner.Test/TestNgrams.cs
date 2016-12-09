using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF.TextMiner.Test
{
    
    public class TestNgrams
    {
        string textToProcess = @"The probability and statistics cookbook is a succinct representation of various topics in probability theory and statistics. It provides a comprehensive mathematical reference reduced to its essence, rather than aiming for elaborate explanations.";
        string text2 = "probability theory and statistics. It provides a comprehensive mathematical. It provides a comprehensive";
        string text3 = "probability theory and statistics";
        string textWith2And = "The probability and statistics cookbook is a succinct representation of various topics in probability theory and statistics.";
        string only3words = "words words words";
        string tweet_neg1 = "Para   qu no soy tan duro  Hay algo peor que #messi  Y es a propaganda de #movistar !!";
        string tweet_pos1 = "Felicidades a @Telcel xq despues de hacerme prdr mi tiempo durante 1 mes y hacerme pagar sin recib el serv, me dieron soluc, bravo! hurra!";
        string tweet_account = "Felicidades a @Telcel";
        string textAfterPeriod = "y es a propaganda de #movistar !!. si @telcel me manda otro";
        string textLinks = @"http://stackoverflow.com/questions/26519893/how-do-i-remove-url-from-text";
        string tweetsConcatenados = "para qu no soy tan duro hay algo peor que #messi y es a propaganda de #movistar !!. si @telcel me manda otro estupido mensaje de promocion al celular ire a meterselos por el cu%&amp;. aaaaaa quiero meter una tarjeta de mensajes desdayer y nada q agarra movistar!!. tontamente cambie @telcel por sus promesas de mejor servicio @nextelmx, y ya contratado imposible cancelar solo son ganchos";
        AF.TextMiner.TextCleaning.TextCleaningConfiguration config = new AF.TextMiner.TextCleaning.TextCleaningConfiguration()
        {
            RemoveTwitterAccounts = true,
            RemoveLinks = true
        };


        [Fact]
        public void TesMethod()
        {
            var corpus = AF.TextMiner.TextCorpus.GenerateNewCorpus("test", only3words);

            Assert.Equal(corpus.NGrams_.Where(x => x.GramSize == 2 && x.Text == "words words").FirstOrDefault().Count, 2);
            Assert.Equal(corpus.NGrams_.Where(x => x.GramSize == 3 && x.Text == "words words words").FirstOrDefault().Count, 1);
            Assert.Equal(corpus.NGrams_.Where(x => x.GramSize == 4 && x.Text == "words words words words").FirstOrDefault(), null);
        }

        [Fact]
        public void TextCorpus_Test5grams()
        {
            var corpus = AF.TextMiner.TextCorpus.GenerateNewCorpus("test", textToProcess, new byte[] { 5 });

            Assert.True(corpus.NGrams_.Any(x => x.GramSize == 5));

        }

        [Fact]
        public void TextCleaning_TwitterAccount()
        {
            string text = AF.TextMiner.TextCleaning.CleaningActions.Clean(tweet_account, config);

            Assert.DoesNotContain("@Telcel", text);
        }

        [Fact]
        public void TextCorpus_TestSortAndPercentages()
        {
            var corpus = AF.TextMiner.TextCorpus.GenerateNewCorpus("test", text2);

            corpus.CalculatePercentagesAndSort();

            var dos = corpus.NGrams_.Where(x => x.GramSize == 2).Sum(x => x.Percentaje);
            var tres = corpus.NGrams_.Where(x => x.GramSize == 3).Sum(x => x.Percentaje);
            var cuatro = corpus.NGrams_.Where(x => x.GramSize == 4).Sum(x => x.Percentaje);

            Assert.InRange(dos.Value, 0.99999999, 1);
            Assert.InRange(tres.Value, 0.99999999, 1);
            Assert.InRange(cuatro.Value, 0.99999999, 1);
        }

        [Fact]
        public void TextCorpus_TestAvoidEmptyStringsAsNGrans()
        {
            tweetsConcatenados = AF.TextMiner.TextCleaning.CleaningActions.Clean(tweetsConcatenados, config);
            var corpus = AF.TextMiner.TextCorpus.GenerateNewCorpus("test", tweetsConcatenados);

            var element = corpus.NGrams_.Any(x => string.IsNullOrWhiteSpace(x.Text));

            Assert.Equal(false, element);
        }

        [Fact]
        public void TextCorpus_TestTextAfterPerios()
        {
            //tweetsConcatenados = AF.TextMiner.TextCleaning.CleaningActions.Clean(tweetsConcatenados, config);
            var corpus = AF.TextMiner.TextCorpus.GenerateNewCorpus("test", textAfterPeriod);

            var element = corpus.NGrams_.Any(x => x.Text == "si");

            Assert.Equal(false, element);
        }

        [Fact]
        public void TextCleaning_RemoveLinks()
        {
            string text = AF.TextMiner.TextCleaning.CleaningActions.Clean(textLinks, config);

            Assert.Equal(true, string.IsNullOrWhiteSpace(text));
        }
    }
}
