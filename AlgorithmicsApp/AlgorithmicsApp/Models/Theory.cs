using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class Theory : CourseItem
    {
        public int CourseId { get; set; }
        public int Id { get; set; }

        public Theory()
        {
            ItemType = Type.Theory;
            Icon = IconFont.BookAlt;
        }
    }
}
