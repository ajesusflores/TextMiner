using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF.TextMiner.TextCleaning
{
    public class TextCleaningConfiguration
    {
        public bool ToLowerCase { get; set; }
        public bool ReplaceDoubleSpace { get; set; }
        public bool RemoveLinks { get; set; }
    }
}
