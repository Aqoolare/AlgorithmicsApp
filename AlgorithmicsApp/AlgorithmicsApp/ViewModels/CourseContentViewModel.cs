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
    [QueryProperty(nameof(CurrentPosition), nameof(CurrentPosition))]
    public class CourseContentViewModel : BaseViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CurrentPosition { get; set; }

        public ObservableRangeCollection<CourseItem> CourseItems { get; set; }
        public AsyncCommand LoadCourseItemsCommand { get; }
        public AsyncCommand<CourseItem> ItemTappedCommand { get; }


        public CourseContentViewModel()
        {
            CourseItems = new ObservableRangeCollection<CourseItem>();

            LoadCourseItemsCommand = new AsyncCommand(LoadCourseItems);
            ItemTappedCommand = new AsyncCommand<CourseItem>(ItemTapped);
        }

        async Task ItemTapped(CourseItem item)
        {
            if (item == null)
                return;

            var route = String.Empty;
            CurrentPosition = item.Order;
            if (item.ItemType == Models.Type.Theory)
            {
                Theory theory = (Theory)item;
                route = $"{nameof(TheoryPage)}?TheoryId={theory.Id}&TheoryTitle={theory.Title}&CurrentPosition={CurrentPosition}";
            }
            else
            {
                Question question = (Question)item;
                route = $"{nameof(QuestionPage)}?QuestionId={question.Id}&QuestionTitle={question.Title}" +
                    $"&Formulation={Uri.EscapeDataString(question.Formulation)}&IsAnswered={question.IsAnswered}&CurrentPosition={CurrentPosition}";
            }
            CourseContentDbService.CourseItemsHistory = new List<CourseItem>();
            CourseContentDbService.CourseItemsHistory.Add(item);

            await Shell.Current.GoToAsync(route);
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
            CourseContentDbService.CourseItems = new CourseItem[CourseItems.Count()];
            for (var i = 0; i < CourseItems.Count(); i++)
            {
                CourseContentDbService.CourseItems[i] = CourseItems[i];
            }

            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
