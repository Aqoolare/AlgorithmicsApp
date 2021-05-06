using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using MvvmHelpers;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using System;
using AlgorithmicsApp.Views;
using System.Linq;
using Type = AlgorithmicsApp.Models.Type;
using Xamarin.Essentials;

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

        int fSize = 0;
        public int FSize
        {
            get { return fSize; }
            set { SetProperty(ref fSize, value); }
        }


        public TheoryViewModel()
        {
            TheoryContentList = new ObservableRangeCollection<Wrapper>();
            LoadCommand = new AsyncCommand(LoadTheoryContent);
            LinkTappedCommand = new AsyncCommand<Link>(LinkTapped);
            GoToCourseContentListCommand = new AsyncCommand(GoToCourseContentList);
            GoToPreviousPageCommand = new AsyncCommand(GoToPreviousPage);
            GoToNextPageCommand = new AsyncCommand(GoToNextPage);
            var p = DeviceDisplay.MainDisplayInfo.Density;
            FSize = Convert.ToInt32(43 * (p / 2.625));
        }

        async Task LoadTheoryContent()
        {
            IsBusy = true;
            TheoryContentList.Clear();
            var theoryContent = await TheoryContentDbService.GetTheoryContent(TheoryId);
            ObservableRangeCollection<Wrapper> items = new ObservableRangeCollection<Wrapper>(); 
            foreach (var theoryItem in theoryContent)
            {
                var link = await TheoryContentDbService.GetLinkById(theoryItem.LinkId);
                items.Add(new Wrapper(theoryItem, link));
            }
            TheoryContentList.AddRange(items);
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
            var route = String.Empty;
            var a = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"..");
        }

        async Task LinkTapped(Link link)
        {
            if (link == null)
                return;
            IsBusy = true;
            // получить позицию некст элемента
            var theoryItem = await CourseContentDbService.GetTheoryById(link.TheoryId);

            var route = $"{nameof(TheoryPage)}?TheoryId={link.TheoryId}&TheoryTitle={theoryItem.Title}&ItemIndexToScroll={link.ElementIndex}&CurrentPosition={link.TheoryId}";
            //IsBusy = true;
            //var nextPage = from page in theoryPages where page.Id == link.TheoryId select page;
            await Shell.Current.GoToAsync(route);
            IsBusy = false;
        }
    }
}
