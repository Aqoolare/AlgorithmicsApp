using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlgorithmicsApp.ViewModels
{
    [QueryProperty(nameof(QuestionId), nameof(QuestionId))]
    [QueryProperty(nameof(QuestionTitle), nameof(QuestionTitle))]
    [QueryProperty(nameof(Formulation), nameof(Formulation))]
    public class QuestionViewModel : BaseViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }

        string formulation = string.Empty;
        public string Formulation
        {
            get { return formulation; }
            set { SetProperty(ref formulation, value); }
        }

        public ObservableRangeCollection<Answer> Answers { get; set; }

        public AsyncCommand LoadCommand { get; }

        public QuestionViewModel()
        {
            Answers = new ObservableRangeCollection<Answer>();
            LoadCommand = new AsyncCommand(LoadAnswers);
        }

        async Task LoadAnswers()
        {
            IsBusy = true;
            var answers = await QuestionContentDbService.GetQuestionContent(QuestionId);
            Answers.Clear();
            Answers.AddRange(answers);
            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }


}
