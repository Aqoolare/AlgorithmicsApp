using Xamarin.Forms;
using AlgorithmicsApp.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseContentPage : ContentPage
    {
        public CourseContentPage()
        {
            InitializeComponent();
            BindingContext = new CourseContentViewModel();
        }
    }
}
