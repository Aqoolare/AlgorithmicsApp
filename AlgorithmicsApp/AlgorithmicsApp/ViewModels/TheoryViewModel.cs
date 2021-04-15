using AlgorithmicsApp.Models;
using AlgorithmicsApp.Services;
using MvvmHelpers;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace AlgorithmicsApp.ViewModels
{
    [QueryProperty(nameof(TheoryId), nameof(TheoryId))]
    [QueryProperty(nameof(TheoryTitle), nameof(TheoryTitle))]
    public class TheoryViewModel : BaseViewModel
    {
        public int TheoryId { get; set; }
        public string TheoryTitle { get; set; }

        public ObservableRangeCollection<TheoryContent> TheoryContentList { get; set; }
        public AsyncCommand<TheoryContent> LinkTappedCommand { get; }


        public TheoryViewModel()
        {
            TheoryContentList = new ObservableRangeCollection<TheoryContent>();
            LinkTappedCommand = new AsyncCommand<TheoryContent>(LinkTapped);
            LoadTheoryContent();
        }

        void LoadTheoryContent()
        {
            IsBusy = true;
            var theoryContent = TheoryContentDbService.GetTheoryContent(TheoryId);
            TheoryContentList.Clear();
            TheoryContentList.AddRange(theoryContent);
            IsBusy = false;
        }

        async Task LinkTapped(TheoryContent theoryContent)
        {
            if (theoryContent == null)
                return;

            var route = $"{theoryContent.LinkPage}";
            IsBusy = true;
            await Shell.Current.GoToAsync(route);
            IsBusy = false;
        }
    }
}
