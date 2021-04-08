using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using MvvmHelpers;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using AlgorithmicsApp.Views;
using Xamarin.Forms;

namespace AlgorithmicsApp.ViewModels
{
    public class CourseViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Course> CoursesList { get; set; }
        public AsyncCommand LoadCommand { get; }
        public AsyncCommand<Course> TappedCommand { get; }


        public CourseViewModel()
        {
            CoursesList = new ObservableRangeCollection<Course>();

            LoadCommand = new AsyncCommand(LoadCourses);
            TappedCommand = new AsyncCommand<Course>(Tapped);

            Task.Run(async () => await LoadCourses());
        }

        async Task LoadCourses()
        {
            IsBusy = true;
            await Task.Delay(500);
            CoursesList.Clear();
            var courses = await CoursesDbService.GetCourses();
            CoursesList.AddRange(courses);
            IsBusy = false;
        }

        async Task Tapped(Course course)
        {
            var route = $"{nameof(CourseContentPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
