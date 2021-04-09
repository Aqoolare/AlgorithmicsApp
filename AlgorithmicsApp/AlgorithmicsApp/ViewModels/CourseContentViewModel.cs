using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using AlgorithmicsApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
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
        public AsyncCommand<Theory> TheoryTappedCommand { get; }


        public CourseContentViewModel()
        {
            TheoryList = new ObservableRangeCollection<Theory>();
            QuestionsList = new ObservableRangeCollection<Question>();

            LoadTheoryCommand = new AsyncCommand(LoadTheory);
            LoadQuestionsCommand = new AsyncCommand(LoadQuestions);
            TheoryTappedCommand = new AsyncCommand<Theory>(TheoryTapped);
        }

        async Task TheoryTapped(Theory theory)
        {
            if (theory == null)
                return;

            var route = $"{nameof(TheoryPage)}?TheoryId={theory.Id}&TheoryTitle={theory.Title}";
            await Shell.Current.GoToAsync(route);
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
