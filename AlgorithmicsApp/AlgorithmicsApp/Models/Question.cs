using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class Question : CourseItem
    {
        public string Formulation { get; set; }
        public int Id { get; set; }
        public int CourseId { get; set; }
        
        public Question()
        {
            ItemType = Type.Question;
            Icon = IconFont.UniqueIdea;
        }
    }
}
