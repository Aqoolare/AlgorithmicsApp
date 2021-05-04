using AlgorithmicsApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp
{
    class RussianBreakingEngine : Typography.TextBreak.EngBreakingEngine
    {
        public override bool CanHandle(char c) => c is >= '\u0400' and <= '\u052f'; // Unicode Cyrillic and Cyrillic Supplement
    }
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute($"{nameof(CourseContentPage)}", typeof(CourseContentPage));
            Routing.RegisterRoute($"{nameof(TheoryPage)}", typeof(TheoryPage));
            Routing.RegisterRoute($"{nameof(QuestionPage)}", typeof(QuestionPage));

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
