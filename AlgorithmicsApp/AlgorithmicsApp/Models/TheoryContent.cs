using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class TheoryContent
    {
        public int TheoryId { get; set; }
        public bool Type { get; set; }
        public string Text { get; set; }
        public string LinkPage { get; set; }
    }
}
