using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using AlgorithmicsApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlgorithmicsApp.ViewModels
{
    [QueryProperty(nameof(QuestionId), nameof(QuestionId))]
    [QueryProperty(nameof(QuestionTitle), nameof(QuestionTitle))]
    [QueryProperty(nameof(Formulation), nameof(Formulation))]
    [QueryProperty(nameof(IsAnswered), nameof(IsAnswered))]
    [QueryProperty(nameof(CurrentPosition), nameof(CurrentPosition))]
    public class QuestionViewModel : BaseViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public bool IsAnswered { get; set; }
        public int CurrentPosition { get; set; }

        string formulation = string.Empty;
        public string Formulation
        {
            get { return formulation; }
            set { SetProperty(ref formulation, value); }
        }

        public ObservableRangeCollection<Answer> Answers { get; set; }

        int collectionViewHeightRequest = 0;
        public int CollectionViewHeightRequest
        {
            get { return collectionViewHeightRequest; }
            set { SetProperty(ref collectionViewHeightRequest, value); }
        }


        private ObservableCollection<object> _selectedAnswers;
        public ObservableCollection<object> SelectedAnswers
        {
            get { return _selectedAnswers; }
            set { SetProperty(ref _selectedAnswers, value); }
        }


        public AsyncCommand LoadCommand { get; }
        public AsyncCommand CheckAnswersCommand { get; }
        public AsyncCommand GoToCourseContentListCommand { get; }
        public AsyncCommand GoToPreviousPageCommand { get; }
        public AsyncCommand GoToNextPageCommand { get; }

        public Action UpdateAnswersInterface;
        public Action UpdateAnswersInterfaceAfterLoad;

        public QuestionViewModel()
        {
            Answers = new ObservableRangeCollection<Answer>();
            SelectedAnswers = new ObservableCollection<object>();
            LoadCommand = new AsyncCommand(LoadAnswers);
            CheckAnswersCommand = new AsyncCommand(CheckAnswers);
            GoToCourseContentListCommand = new AsyncCommand(GoToCourseContentList);
            GoToPreviousPageCommand = new AsyncCommand(GoToPreviousPage);
            GoToNextPageCommand = new AsyncCommand(GoToNextPage);
        }

        private async Task GoToNextPage()
        {
            var route = String.Empty;
            var item = CourseContentDbService.CourseItems[CurrentPosition];
            CourseContentDbService.CourseItemsHistory.Add(item);
            if (item.ItemType == Models.Type.Theory)
            {
                Theory theory = (Theory)item;
                route = $"{nameof(TheoryPage)}?TheoryId={theory.Id}&TheoryTitle={theory.Title}&CurrentPosition={CurrentPosition + 1}";
            }
            else
            {
                Question question = (Question)item;
                route = $"{nameof(QuestionPage)}?QuestionId={question.Id}&QuestionTitle={question.Title}" +
                    $"&Formulation={Uri.EscapeDataString(question.Formulation)}&IsAnswered={question.IsAnswered}&CurrentPosition={CurrentPosition + 1}";
            }
            await Shell.Current.GoToAsync(route);
        }

        private async Task GoToPreviousPage()
        {
            var route = String.Empty;
            var a = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"..");
        }

        private async Task GoToCourseContentList()
        {
            var a = Shell.Current.CurrentState;
            var courseItem = await CourseContentDbService.GetQuestionById(QuestionId);
            var course = await CoursesDbService.GetCourseById(courseItem.CourseId);
            await Shell.Current.GoToAsync($"///{nameof(CoursesPage)}/{nameof(CourseContentPage)}?CourseId={course.Id}&CourseName={course.Name}");
        }

        async Task CheckAnswers()
        {
            UpdateAnswersInterface();
            foreach (var answer in Answers)
            {
                await QuestionContentDbService.UpdateQuestionContent(answer);
            }
            var question = await CourseContentDbService.GetQuestion(QuestionId);
            question.IsAnswered = !question.IsAnswered;
            await CourseContentDbService.UpdateQuestion(question);
            
            await LoadAnswers();
        }

        public async Task LoadAnswers()
        {
            IsBusy = true;
            var answers = await QuestionContentDbService.GetQuestionContent(QuestionId);
            Answers.Clear();
            Answers.AddRange(answers);
            CollectionViewHeightRequest = 75 * Answers.Count;
            IsBusy = false;
            UpdateAnswersInterfaceAfterLoad();
        }



        public void OnAppearing()
        {
            IsBusy = true;
        }
    }


}
