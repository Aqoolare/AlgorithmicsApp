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
            //collectionView.SelectedItems.Clear();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            collectionView.SelectedItems.Clear();
            _viewModel.UpdateAnswersInterface = UpdateIntrface;
            _viewModel.UpdateAnswersInterfaceAfterLoad = UpdateInterfaceAfterLoad;
            _viewModel.OnAppearing();
            //if (_viewModel.IsAnswered)
            //{
            //    if (_viewModel.Answers.Any(answer => answer.AnswerColor == "Red"))
            //    {
            //        conditionLabel.Text = resultMessages[1];
            //        App.Current.Resources["AnswerColor"] = Color.Red;
            //    }
            //    else
            //    {
            //        conditionLabel.Text = resultMessages[0];
            //        App.Current.Resources["AnswerColor"] = Color.Green;
            //    }
            //    collectionView.SelectionMode = SelectionMode.None;
            //    checkButton.Text = "Решить заново";
            //}
            //else
            //{
            //    checkButton.IsEnabled = false;
            //    checkButton.Text = "Проверить ответы";
            //}
            if (_viewModel.CurrentPosition == CourseContentDbService.CourseItems.Count())
            {
                nextButton.IsEnabled = false;
            }
        }

        void UpdateInterfaceAfterLoad()
        {
            if (_viewModel.IsAnswered)
            {
                if (_viewModel.Answers.Any(answer => answer.AnswerColor == "#ff748e"))
                {
                    conditionLabel.Text = resultMessages[1];
                    App.Current.Resources["AnsColor"] = Color.FromHex("#FF375F");
                }
                else
                {
                    conditionLabel.Text = resultMessages[0];
                    App.Current.Resources["AnsColor"] = Color.FromHex("#30D158");
                }
                collectionView.SelectionMode = SelectionMode.None;
                checkButton.Text = "Решить заново";
                checkButton.IsEnabled = true;
            }
            else
            {
                checkButton.IsEnabled = false;
                checkButton.Text = "Проверить ответы";
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
            _viewModel.IsTrue = false;
            //_viewModel.IsAnswered = false;
            collectionView.SelectionMode = SelectionMode.Multiple;
            checkButton.Text = "Проверить ответы";
        }

        void UpdateIsNotAnsweredInteface()
        {
            //if (_viewModel.SelectedAnswers.Count > 0)
            {
                IEnumerable<Answer> selectedAnswers = from selectedAnswer in _viewModel.SelectedAnswers select (Answer)selectedAnswer;
                IEnumerable<int> selectedAnswersIds = from selectedAnswer in selectedAnswers select selectedAnswer.Id;
                string selectedColor = String.Empty;
                if (selectedAnswers.Count() == _viewModel.Answers.Where(answer => answer.IsTrue == true).Count() &&
                    !selectedAnswers.Any(answer => answer.IsTrue == false))
                {
                    selectedColor = "#6edf8a";
                    _viewModel.IsTrue = true;
                }
                else
                {
                    selectedColor = "#ff748e";
                    _viewModel.IsTrue = false;
                }
                foreach (var answer in _viewModel.Answers)
                {
                    if (selectedAnswersIds.Contains(answer.Id))
                    {
                        answer.AnswerColor = selectedColor;
                    }
                }
                collectionView.SelectionMode = SelectionMode.None;
                if (selectedColor == "#6edf8a")
                {
                    conditionLabel.Text = resultMessages[0];
                    App.Current.Resources["AnsColor"] = Color.FromHex("#30D158");
                }
                else
                {
                    conditionLabel.Text = resultMessages[1];
                    App.Current.Resources["AnsColor"] = Color.FromHex("#FF375F");
                }
                //_viewModel.IsAnswered = true;
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