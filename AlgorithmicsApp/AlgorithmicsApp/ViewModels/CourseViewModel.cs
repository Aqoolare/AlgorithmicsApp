using AlgorithmicsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmicsApp.ViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            CoursesList = new List<Course>
            {
                new Course
                {
                    Name = "Курс по сосанию членов",
                },
                new Course
                {
                    Name = "Курс по депрессии",
                },
                new Course
                {
                    Name = "Курс по трамваям без кондукторов",
                }
            };
        }

        public List<Course> CoursesList { get; set; }
    }
}
