using AlgorithmicsApp.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmicsApp.ViewModels
{
    public class StatViewModel : BaseViewModel
    {
        public int TotalQuestionsCount { get; set; }
        public int AnsweredQuestionsCount { get; set; }
        public Action UpdateInterface { get; set; }

        public async Task LoadQuestionCounts()
        {
            IsBusy = true;
            TotalQuestionsCount = await CourseContentDbService.GetTotalQuestionsCount();
            AnsweredQuestionsCount = await CourseContentDbService.GetAnsweredQuestionsCount();
            UpdateInterface();
            IsBusy = false;
        }
    }
}
