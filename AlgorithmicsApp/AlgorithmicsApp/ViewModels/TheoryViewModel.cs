﻿using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using MvvmHelpers;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using System;
using AlgorithmicsApp.Views;
using System.Linq;
using Type = AlgorithmicsApp.Models.Type;

namespace AlgorithmicsApp.ViewModels
{
    [QueryProperty(nameof(TheoryId), nameof(TheoryId))]
    [QueryProperty(nameof(TheoryTitle), nameof(TheoryTitle))]
    [QueryProperty(nameof(CurrentPosition), nameof(CurrentPosition))]
    public class TheoryViewModel : BaseViewModel
    {
        public int TheoryId { get; set; }
        public string TheoryTitle { get; set; }
        public int CurrentPosition { get; set; }

        public ObservableRangeCollection<Wrapper> TheoryContentList { get; set; }
        public AsyncCommand<Link> LinkTappedCommand { get; }
        public AsyncCommand GoToCourseContentListCommand { get; }
        public AsyncCommand GoToPreviousPageCommand { get; }
        public AsyncCommand GoToNextPageCommand { get; }
        public AsyncCommand LoadCommand { get; }
        public Action RefreshScrollDown;


        public TheoryViewModel()
        {
            TheoryContentList = new ObservableRangeCollection<Wrapper>();
            LoadCommand = new AsyncCommand(LoadTheoryContent);
            LinkTappedCommand = new AsyncCommand<Link>(LinkTapped);
            GoToCourseContentListCommand = new AsyncCommand(GoToCourseContentList);
            GoToPreviousPageCommand = new AsyncCommand(GoToPreviousPage);
            GoToNextPageCommand = new AsyncCommand(GoToNextPage);
        }

        async Task LoadTheoryContent()
        {
            IsBusy = true;
            TheoryContentList.Clear();
            var theoryContent = await TheoryContentDbService.GetTheoryContent(TheoryId);
            foreach (var theoryItem in theoryContent)
            {
                var link = await TheoryContentDbService.GetLinkById(theoryItem.LinkId);
                TheoryContentList.Add(new Wrapper(theoryItem, link));
            }
            RefreshScrollDown();
            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        

        async Task GoToCourseContentList()
        {
            var a = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"///{nameof(CoursesPage)}/{nameof(CourseContentPage)}");
        }

        async Task GoToNextPage()
        {
            CourseContentDbService.CourseItemsHistory.Add(CourseContentDbService.CourseItems[CurrentPosition]);

            var route = String.Empty;
            var item = CourseContentDbService.CourseItems[CurrentPosition];
            if (item.ItemType == Models.Type.Theory)
            {
                Theory theory = (Theory)item;
                route = $"{nameof(TheoryPage)}?TheoryId={theory.Id}&TheoryTitle={theory.Title}&CurrentPosition={CurrentPosition+1}";
            }
            else
            {
                Question question = (Question)item;
                route = $"{nameof(QuestionPage)}?QuestionId={question.Id}&QuestionTitle={question.Title}" +
                    $"&Formulation={Uri.EscapeDataString(question.Formulation)}&IsAnswered={question.IsAnswered}&CurrentPosition={CurrentPosition+1}";
            }
            await Shell.Current.GoToAsync(route);
        }

        async Task GoToPreviousPage()
        {
            var a = Shell.Current.CurrentState;
            CourseContentDbService.CourseItemsHistory.RemoveAt(CourseContentDbService.CourseItemsHistory.Count - 1);
            await Shell.Current.GoToAsync($"..");
        }

        async Task LinkTapped(Link link)
        {
            if (link == null)
                return;
            // получить позицию некст элемента
            var theoryPages = from page in CourseContentDbService.CourseItems where page.ItemType == Type.Theory select (Theory)page;
            var theoryId = from page in theoryPages where page.Id == link.TheoryId select page.Id;

            var route = $"{nameof(TheoryPage)}?TheoryId={link.TheoryId}&TheoryTitle={link.TheoryTitle}&ItemIndexToScroll={link.ElementIndex}&CurrentPosition={theoryId.FirstOrDefault()}";
            IsBusy = true;
            var nextPage = from page in theoryPages where page.Id == link.TheoryId select page;
            CourseContentDbService.CourseItemsHistory.Add(nextPage.FirstOrDefault());
            await Shell.Current.GoToAsync(route);
            IsBusy = false;
        }
    }
}
