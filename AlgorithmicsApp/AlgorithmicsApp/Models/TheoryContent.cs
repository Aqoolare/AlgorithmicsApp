using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class TheoryContent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TheoryId { get; set; }
        public string Text { get; set; }
        public string Formula { get; set; }
    }
}
