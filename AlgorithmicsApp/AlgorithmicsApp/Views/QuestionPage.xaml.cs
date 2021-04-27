using AlgorithmicsApp.Models;
using AlgorithmicsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        QuestionViewModel _viewModel;
        public QuestionPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new QuestionViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.UpdateAnswersInterface = () => {
                IsBusy = true;
                if (_viewModel.SelectedAnswers.Count > 0)
                {
                    var answersAreRight = true;
                    foreach (var answer in _viewModel.SelectedAnswers)
                    {
                        Answer curAnswer = (Answer)answer;
                        if (!curAnswer.IsTrue)
                        {
                            App.Current.Resources["MainColor"] = App.Current.Resources["WrongAnswerColor"];
                            conditionLabel.Text = "Не верно, попробуйте ещё раз.";
                            conditionLabel.TextColor = (Color)App.Current.Resources["WrongAnswerColor"];
                            answersAreRight = false;
                            break;
                        }
                    }
                    if (answersAreRight)
                    {
                        var temp = _viewModel.Answers.Where(a => a.IsTrue == true);
                        if (_viewModel.SelectedAnswers.Count() == temp.Count())
                        {
                            App.Current.Resources["MainColor"] = App.Current.Resources["RightAnswerColor"];
                            conditionLabel.Text = "Абсолютно верно!";
                            conditionLabel.TextColor = (Color)App.Current.Resources["RightAnswerColor"];
                        }
                        else
                        {
                            App.Current.Resources["MainColor"] = App.Current.Resources["WrongAnswerColor"];
                            conditionLabel.Text = "Не верно, попробуйте ещё раз.";
                            conditionLabel.TextColor = (Color)App.Current.Resources["WrongAnswerColor"];
                            answersAreRight = false;
                        }
                    }
                    foreach (var answer in _viewModel.SelectedAnswers)
                    {
                        Answer curAnswer = (Answer)answer;
                        if (answersAreRight)
                        {
                            if (_viewModel.Answers.Where(a => a.))
                        }
                    }
                    collectionView.SelectionMode = SelectionMode.None;
                }

                IsBusy = false;
            };
            _viewModel.OnAppearing();
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