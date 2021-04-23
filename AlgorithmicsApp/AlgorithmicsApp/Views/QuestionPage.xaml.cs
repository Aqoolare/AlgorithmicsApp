using AlgorithmicsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        QuestionViewModel _viewModel;
        public QuestionPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new QuestionViewModel();
        }
    }
}