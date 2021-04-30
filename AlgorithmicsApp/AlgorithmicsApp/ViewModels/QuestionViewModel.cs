using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
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
    public class QuestionViewModel : BaseViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public bool IsAnswered { get; set; }

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
        public Action UpdateAnswersInterface;

        public QuestionViewModel()
        {
            Answers = new ObservableRangeCollection<Answer>();
            SelectedAnswers = new ObservableCollection<object>();
            LoadCommand = new AsyncCommand(LoadAnswers);
            CheckAnswersCommand = new AsyncCommand(CheckAnswers);
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

        async Task LoadAnswers()
        {
            IsBusy = true;
            var answers = await QuestionContentDbService.GetQuestionContent(QuestionId);
            Answers.Clear();
            Answers.AddRange(answers);
            CollectionViewHeightRequest = 105 * Answers.Count;
            IsBusy = false;
        }



        public void OnAppearing()
        {
            IsBusy = true;
        }
    }


}
