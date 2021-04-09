using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class TheoryContent
    {
        public int Id { get; set; }
        public int TheoryId { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
    }
}
