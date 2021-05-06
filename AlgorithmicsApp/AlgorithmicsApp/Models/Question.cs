using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class Question : CourseItem
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Formulation { get; set; }
        public int CourseId { get; set; }
        public bool IsAnswered { get; set; }
        
        public Question()
        {
            ItemType = Type.Question;
            Icon = IconFont.QuestionSquare;
        }
    }
}
