using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF.TextMiner
{
    public class TextCorpus
    {
        static string punctuation = @",.:;!?&^-%$/()=¿\";
        private List<NGram> _ngrams;

        public string  Identifier { get; set; }
        public List<NGram> NGrams { get { return _ngrams; } }

        public double TotalWords { get { return NGrams.Sum(x => (double) (x.Count ?? 0)); } }

        public TextCorpus(string identifier)
        {
            Identifier = identifier;
            _ngrams = new List<NGram>();
        }

        public static TextCorpus GenerateNewCorpus(string corpusIdentifier, string textToProcess)
        {
            TextCorpus corpus = new TextCorpus(corpusIdentifier);
            //List<NGram> ngrams = new List<NGram>();
            for(byte i = 2; i <= 4; i++)
            {
                foreach (string gram in GetWords(textToProcess, i))
                {
                    var tmpNgram = corpus.NGrams.Find(x => x.GramSize == i &&  x.Text == gram);
                    if(tmpNgram != null)
                    {
                        tmpNgram.Count++;
                    }
                    else
                    {
                        corpus.NGrams.Add(new NGram() { Count = 1, GramSize = i, Text = gram });
                    }
                }
            }
            return corpus;
        }

        private static List<string> GetWords(string text, byte gramSize)
        {
            List<NGram> ngrams = new List<NGram>();
            List<string> words = new List<string>();
            text = text.Trim();
            string tmpContent = string.Empty;
            byte spaceCount = 0;
            int lastSpaceFound = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char current = text[i];
                if (punctuation.Contains(current)) // puntuation
                {
                    if (++spaceCount == gramSize)
                    {
                        words.Add(tmpContent);

                        spaceCount = 0;

                        //i = lastSpaceFound != 0 ? lastSpaceFound : i;
                        lastSpaceFound = 0;
                        tmpContent = string.Empty;
                    }
                }
                else if (current == ' ')   // blank space
                {
                    if (!string.IsNullOrWhiteSpace(tmpContent))
                    {
                        if (++spaceCount == gramSize)
                        {
                            spaceCount = 0;
                            words.Add(tmpContent);
                            tmpContent = string.Empty;
                            i = lastSpaceFound != 0 ? lastSpaceFound : i;
                            lastSpaceFound = 0;
                        }
                        else
                        {
                            tmpContent += ' ';
                            lastSpaceFound = lastSpaceFound <= 0 ? i : lastSpaceFound;
                        }
                    }
                }
                else if( i == text.Length - 1) // end of text
                {
                    words.Add(tmpContent + current);
                }
                else
                    tmpContent += current;
            }
            return words;
        }

        public void CalculatePercentagesAndSort()
        {
            CalculatePercentages();
            Sort();
        }

        private void CalculatePercentages()
        {
            foreach(var element in _ngrams)
            {
                element.Percentaje = (double) element.Count / _ngrams.Where(x => x.GramSize == element.GramSize).Sum(x => x.Count);
            }
        }

        private void Sort()
        {
            this._ngrams = this._ngrams.OrderBy(x => x.GramSize).ThenByDescending(x => x.Percentaje).ToList();
        }
    }
}
