using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using MvvmHelpers;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using AlgorithmicsApp.Views;
using Xamarin.Forms;
using Xamarin.Essentials;

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
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async Task LoadCourses()
        {
            IsBusy = true;
            var courses = await CoursesDbService.GetCourses();
            CoursesList.Clear();
            CoursesList.AddRange(courses);
            IsBusy = false;
        }

        async Task Tapped(Course course)
        {
            if (course == null)
                return;

            IsBusy = true;
            var theory = await CourseContentDbService.GetTheory(course.Id);
            IsBusy = false;

            var route = $"{nameof(CourseContentPage)}?CourseId={course.Id}&CourseName={course.Name}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
