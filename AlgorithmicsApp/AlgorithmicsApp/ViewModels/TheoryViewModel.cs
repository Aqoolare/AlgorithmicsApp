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
        public AsyncCommand LoadTheoryContentCommand { get; }


        public TheoryViewModel()
        {
            TheoryContentList = new ObservableRangeCollection<TheoryContent>();

            LoadTheoryContentCommand = new AsyncCommand(LoadTheoryContent);
        }

        async Task LoadTheoryContent()
        {
            IsBusy = true;
            var theoryContent = await CourseContentDbService.GetTheoryContent(TheoryId);
            TheoryContentList.Clear();
            TheoryContentList.AddRange(theoryContent);
            IsBusy = false;
        }
    }
}
