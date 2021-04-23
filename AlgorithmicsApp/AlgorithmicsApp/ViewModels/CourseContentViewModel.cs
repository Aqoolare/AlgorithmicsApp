using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using AlgorithmicsApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace AlgorithmicsApp.ViewModels
{
    [QueryProperty(nameof(CourseId), nameof(CourseId))]
    [QueryProperty(nameof(CourseName), nameof(CourseName))]
    public class CourseContentViewModel : BaseViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public ObservableRangeCollection<CourseItem> CourseItems { get; set; }
        public AsyncCommand LoadCourseItemsCommand { get; }
        public AsyncCommand<Theory> TheoryTappedCommand { get; }


        public CourseContentViewModel()
        {
            CourseItems = new ObservableRangeCollection<CourseItem>();

            LoadCourseItemsCommand = new AsyncCommand(LoadCourseItems);
            TheoryTappedCommand = new AsyncCommand<Theory>(TheoryTapped);
        }

        async Task TheoryTapped(Theory theory)
        {
            if (theory == null)
                return;

            var route = $"{nameof(TheoryPage)}?TheoryId={theory.Id}&TheoryTitle={theory.Title}";
            await Shell.Current.GoToAsync(route);
        }

        private Task LoadQuestions()
        {
            throw new NotImplementedException();
        }

        async Task LoadCourseItems()
        {
            IsBusy = true;
            var theory = await CourseContentDbService.GetTheory(CourseId);
            var questions = await CourseContentDbService.GetQuestions(CourseId);

            List<CourseItem> temp = new List<CourseItem>();
            temp.AddRange(theory);
            temp.AddRange(questions);
            IEnumerable<CourseItem> courseItems = from item in temp orderby item.Order ascending select item;
            
            CourseItems.Clear();
            CourseItems.AddRange(courseItems);
            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
