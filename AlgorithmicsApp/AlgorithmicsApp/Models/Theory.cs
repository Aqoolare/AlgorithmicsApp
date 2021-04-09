using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class Theory
    {
        public string Content { get; set; }
        public int CourseId { get; set; }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Order { get; set; }
    }
}
