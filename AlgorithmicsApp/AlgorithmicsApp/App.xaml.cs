using AlgorithmicsApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute($"{nameof(CourseContentPage)}", typeof(CourseContentPage));
            Routing.RegisterRoute($"{nameof(TheoryPage)}", typeof(TheoryPage));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
