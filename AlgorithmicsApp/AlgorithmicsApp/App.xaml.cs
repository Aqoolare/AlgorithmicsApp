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

            MainPage = new NavigationPage(new BottomTabbedPage());
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
