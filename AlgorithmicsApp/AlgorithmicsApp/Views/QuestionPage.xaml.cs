using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using AlgorithmicsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        QuestionViewModel _viewModel;
        string[] resultMessages = { "Абсолютно верно!", "Пока не верно, попробуйте ещё раз." };
        public QuestionPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new QuestionViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.UpdateAnswersInterface = UpdateIntrface;
            _viewModel.OnAppearing();
            if (_viewModel.IsAnswered)
            {
                Thread.Sleep(1000);
                if (_viewModel.Answers.Any(answer => answer.AnswerColor == "Red"))
                {
                    conditionLabel.Text = resultMessages[1];
                    App.Current.Resources["AnswerColor"] = Color.Red;
                }
                else
                {
                    conditionLabel.Text = resultMessages[0];
                    App.Current.Resources["AnswerColor"] = Color.Green;
                }
                collectionView.SelectionMode = SelectionMode.None;
                checkButton.Text = "Решить заново";
            }
            else
            {
                checkButton.IsEnabled = false;
                checkButton.Text = "Проверить ответы";
            }
            if (_viewModel.CurrentPosition == CourseContentDbService.CourseItems.Count())
            {
                nextButton.IsEnabled = false;
            }
        }

        void UpdateIntrface()
        {
            switch (_viewModel.IsAnswered)
            {
                case true:
                    UpdateIsAnsweredInteface();
                    break;
                case false:
                    UpdateIsNotAnsweredInteface();
                    break;
            }
        }

        void UpdateIsAnsweredInteface()
        {
            _viewModel.SelectedAnswers.Clear();
            conditionLabel.Text = String.Empty;
            foreach (var answer in _viewModel.Answers)
            {
                answer.AnswerColor = "White";
            }
            _viewModel.IsAnswered = false;
            collectionView.SelectionMode = SelectionMode.Multiple;
            checkButton.Text = "Проверить ответы";
        }

        void UpdateIsNotAnsweredInteface()
        {
            if (_viewModel.SelectedAnswers.Count > 0)
            {
                IEnumerable<Answer> selectedAnswers = from selectedAnswer in _viewModel.SelectedAnswers select (Answer)selectedAnswer;
                IEnumerable<int> selectedAnswersIds = from selectedAnswer in selectedAnswers select selectedAnswer.Id;
                string selectedColor = String.Empty;
                if (selectedAnswers.Count() == _viewModel.Answers.Where(answer => answer.IsTrue == true).Count() &&
                    !selectedAnswers.Any(answer => answer.IsTrue == false))
                {
                    selectedColor = "Green";
                }
                else
                {
                    selectedColor = "Red";
                }
                foreach (var answer in _viewModel.Answers)
                {
                    if (selectedAnswersIds.Contains(answer.Id))
                    {
                        answer.AnswerColor = selectedColor;
                    }
                }
                collectionView.SelectionMode = SelectionMode.None;
                if (selectedColor == "Green")
                {
                    conditionLabel.Text = resultMessages[0];
                    App.Current.Resources["AnswerColor"] = Color.Green;
                }
                else
                {
                    conditionLabel.Text = resultMessages[1];
                    App.Current.Resources["AnswerColor"] = Color.Red;
                }
                _viewModel.IsAnswered = true;
                checkButton.Text = "Решить заново";
            }
        }

        private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (collectionView.SelectedItems.Count > 0)
            {
                checkButton.IsEnabled = true;
            }
            else
            {
                checkButton.IsEnabled = false;
            }
        }
    }
}