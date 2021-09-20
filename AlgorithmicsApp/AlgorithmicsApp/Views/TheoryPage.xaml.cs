using AlgorithmicsApp.Services;
using AlgorithmicsApp.ViewModels;
using CSharpMath.Forms;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp.Views
{
    public class MyTextView : TextView
    {
        public MyTextView()
        {
            InvalidateSurface();
        }
    }

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