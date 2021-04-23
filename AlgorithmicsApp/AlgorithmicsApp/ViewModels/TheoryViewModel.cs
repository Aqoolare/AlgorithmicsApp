using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using MvvmHelpers;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using System;

namespace AlgorithmicsApp.ViewModels
{
    [QueryProperty(nameof(TheoryId), nameof(TheoryId))]
    [QueryProperty(nameof(TheoryTitle), nameof(TheoryTitle))]
    public class TheoryViewModel : BaseViewModel
    {
        public int TheoryId { get; set; }
        public string TheoryTitle { get; set; }

        public ObservableRangeCollection<Wrapper> TheoryContentList { get; set; }
        public AsyncCommand<Link> LinkTappedCommand { get; }
        public AsyncCommand LoadCommand { get; }
        public Action RefreshScrollDown;


        public TheoryViewModel()
        {
            TheoryContentList = new ObservableRangeCollection<Wrapper>();
            LoadCommand = new AsyncCommand(LoadTheoryContent);
            LinkTappedCommand = new AsyncCommand<Link>(LinkTapped);
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

        async Task LinkTapped(Link link)
        {
            if (link == null)
                return;

            var route = $"{link.Page}?ItemIndexToScroll={link.ElementIndex}";
            IsBusy = true;
            await Shell.Current.GoToAsync(route);
            IsBusy = false;
        }
    }
}
