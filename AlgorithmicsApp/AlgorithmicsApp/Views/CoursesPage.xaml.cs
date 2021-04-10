using Xamarin.Forms;
using AlgorithmicsApp.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursesPage : ContentPage
    {
        CourseViewModel _viewModel;
        public CoursesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CourseViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}