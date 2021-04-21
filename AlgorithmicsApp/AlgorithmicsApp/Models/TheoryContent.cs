using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class TheoryContent
    {
        public int TheoryId { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string BoldText { get; set; }
        public int LinkId { get; set; }
        public string Formula { get; set; }
    }
}
