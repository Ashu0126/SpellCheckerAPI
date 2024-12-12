using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellCheckerAPI.Models
{
    public class SpellError
    {
        public string Word { get; set; }
        public int LineNumber { get; set; } // Added
        public int Position { get; set; }
        public List<string> Suggestions { get; set; }
    }
}