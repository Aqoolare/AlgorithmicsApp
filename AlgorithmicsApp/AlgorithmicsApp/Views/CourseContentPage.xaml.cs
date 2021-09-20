using Xamarin.Forms;
using AlgorithmicsApp.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseContentPage : ContentPage
    {
        CourseContentViewModel _viewModel;
        public CourseContentPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CourseContentViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
