using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlgorithmicsApp.ViewModels
{
    [QueryProperty(nameof(TheoryId), nameof(TheoryId))]
    [QueryProperty(nameof(QuestionTitle), nameof(QuestionTitle))]
    public class QuestionViewModel
    {
        public int TheoryId { get; set; }
        public string QuestionTitle { get; set; }

        public AsyncCommand LoadCommand { get; }
    }
}
