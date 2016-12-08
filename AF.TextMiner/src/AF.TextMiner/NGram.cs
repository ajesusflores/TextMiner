using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF.TextMiner
{
    public class NGram
    {
        private int _hash;
        string _text;
        public int? Hash {
            get { return _hash; }
        }
        public string Text { get { return _text; } set { _text = value; _hash = _text.GetHashCode(); } }
        public int? Count { get; set; }
        public double? Percentaje { get; set; }
        public byte GramSize { get; set; }
    }
}
