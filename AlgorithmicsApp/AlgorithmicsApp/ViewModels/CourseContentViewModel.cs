using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using AlgorithmicsApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlgorithmicsApp.ViewModels
{
    [QueryProperty(nameof(CourseId), nameof(CourseId))]
    [QueryProperty(nameof(CourseName), nameof(CourseName))]
    public class CourseContentViewModel : BaseViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public ObservableRangeCollection<Theory> TheoryList { get; set; }
        public ObservableRangeCollection<Question> QuestionsList { get; set; }
        public AsyncCommand LoadTheoryCommand { get; }
        public AsyncCommand LoadQuestionsCommand { get; }


        public CourseContentViewModel()
        {
            TheoryList = new ObservableRangeCollection<Theory>();
            QuestionsList = new ObservableRangeCollection<Question>();

            LoadTheoryCommand = new AsyncCommand(LoadTheory);
            LoadQuestionsCommand = new AsyncCommand(LoadQuestions);

            LoadTheoryCommand.ExecuteAsync();
        }

        private Task LoadQuestions()
        {
            throw new NotImplementedException();
        }

        async Task LoadTheory()
        {
            IsBusy = true;
            var theory = await CourseContentDbService.GetTheory(CourseId);
            TheoryList.Clear();
            TheoryList.AddRange(theory);
            IsBusy = false;
        }
    }
}
