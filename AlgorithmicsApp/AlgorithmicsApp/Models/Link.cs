using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class Link
    {
        public int Id { get; set; }
        public int TheoryId { get; set; }
        public string TheoryTitle { get; set; }
        public int ElementIndex { get; set; }
        public string Text { get; set; }
    }
}
