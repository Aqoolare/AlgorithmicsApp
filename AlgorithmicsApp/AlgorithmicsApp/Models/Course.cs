using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class Course
    {
        public string Name { get; set; }
        public List<string> Theory { get; set; }
        public List<Question> Questions { get; set; }
    }
}
