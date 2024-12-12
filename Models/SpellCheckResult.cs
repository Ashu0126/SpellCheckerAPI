using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellCheckerAPI.Models
{
    public class SpellCheckResult
    {
        public string FileName { get; set; }
        public List<SpellError> Errors { get; set; }
    }
}