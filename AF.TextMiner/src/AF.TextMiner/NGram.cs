using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF.TextMiner
{
    public class NGram
    {
        public int? Hash {
            get { return Text.GetHashCode(); }
        }
        public string Text { get; set; }
        public int? Count { get; set; }
        public double? Percentaje { get; set; }
        public byte GramSize { get; set; }
    }
}
