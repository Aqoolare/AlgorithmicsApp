using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using AlgorithmicsApp.ViewModels;
using CSharpMath.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp.Views
{
    //class RussianBreakingEngine : Typography.TextBreak.EngBreakingEngine
    //{
    //    public override bool CanHandle(char c) => c is >= '\u0400' and <= '\u052f'; // Unicode Cyrillic and Cyrillic Supplement
    //}
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemIndexToScroll), nameof(ItemIndexToScroll))]
    public partial class TheoryPage : ContentPage
    {
        TheoryViewModel _viewModel;
        public int ItemIndexToScroll { get; set; }

        public TheoryPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TheoryViewModel();
            //CSharpMath.Rendering.Text.TextLaTeXParser.AdditionalBreakingEngines.Add(new RussianBreakingEngine());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.RefreshScrollDown = () => {
                if (_viewModel.TheoryContentList.Count > 0)
                {
                    Device.BeginInvokeOnMainThread(() => {
                        listView.ScrollTo(_viewModel.TheoryContentList[ItemIndexToScroll], ScrollToPosition.Start, true);
                    });
                }
            };
            _viewModel.OnAppearing();
            if (_viewModel.CurrentPosition == CourseContentDbService.CourseItems.Count())
            {
                nextButton.IsEnabled = false;
            }
            if (_viewModel.ByLink)
            {
                linkLabel.Text = "Переход выполнен по ссылке";
                nextButton.IsEnabled = false;
            }
            else
            {
                linkLabel.HeightRequest = 0;
            }
        }
    }
}