using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyze
{
    /// <summary>
    /// The TextData class contains all the facts for a given word within a TextDictionary
    /// </summary>
    public class TextData
    {
        public string Word { get; set; }
        public int Frequency { get; set; }
        public bool IsPrime { get; set; }
    }
}
