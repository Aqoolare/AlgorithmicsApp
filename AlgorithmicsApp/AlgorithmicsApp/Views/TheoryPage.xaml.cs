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
    public partial class TheoryPage : ContentPage
    {
        TheoryViewModel _viewModel;

        public TheoryPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TheoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}