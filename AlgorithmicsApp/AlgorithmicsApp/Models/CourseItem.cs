using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class CourseItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public Type ItemType { get; set; }
    }
}
