using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class Answer
    {
        public string Content { get; set; }
        public bool IsTrue { get; set; }
        public int QuestionId { get; set; }
        public string AnswerColor { get;set; }
    }
}
