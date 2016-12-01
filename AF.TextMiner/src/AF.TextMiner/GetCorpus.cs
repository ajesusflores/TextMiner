using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF.TextMiner
{
    public class GetCorpus
    {
        List<string> textToProcess;
        string punctuation = ",.:;!";
            
        public TextCorpus GetNewCorpus()
        {
            return GetNewCorpus(string.Empty);
        }

        public TextCorpus GetNewCorpus(string corpusIdentifier)
        {
            return AppendToCorpus(new TextCorpus(corpusIdentifier));
        }

        public TextCorpus AppendToCorpus(TextCorpus corpusToAppenNGrams)
        {





            List<string> allWords = new List<string>();
            foreach(string text in textToProcess)
            {
                allWords.AddRange(GetWords(text, 1));
                allWords.AddRange(GetWords(text, 2));
                allWords.AddRange(GetWords(text, 3));
                allWords.AddRange(GetWords(text, 4));
            }
            return corpusToAppenNGrams;
        }

        /// <summary>
        /// Generates ngrams from text based on gramSize
        /// </summary>
        /// <param name="text">Original text to process</param>
        /// <param name="gramSize">Words quantity</param>
        /// <returns>A list of processed ngrams</returns>
        private List<string> GetWords(string text, byte gramSize)
        {
            List<NGram> ngrams = new List<NGram>();
            List<string> words = new List<string>();
            text = text.Trim();
            string tmpContent = string.Empty;
            byte spaceCount = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char current = text[i];
                if (punctuation.Contains(current))
                {
                    spaceCount = 0;
                    words.Add(tmpContent);
                    if (ngrams.Exists(x => x.Text == tmpContent))
                        ngrams.Find(x => x.Text == tmpContent).Count++;
                    else
                        ngrams.Add(new NGram() { Count = 1, GramSize = gramSize, Text = text });
                    tmpContent = string.Empty;
                }
                else if (current == ' ')
                {
                    if (spaceCount++ == gramSize)
                    {
                        spaceCount = 0;
                        words.Add(tmpContent);
                        tmpContent = string.Empty;
                    }
                }
                else
                    tmpContent += current;
                tmpContent += text[i];
            }
            return words;
        }

        private List<NGram> ProcessWords(List<string> words)
        {
            List<NGram> corpus = new List<NGram>();
            return corpus;
        }
    }
}
