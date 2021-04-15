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
            LoadDataToStackLayout();
        }

        void LoadDataToStackLayout()
        {
            theoryStack.Children.Clear();
            var formattedString = new FormattedString();
            foreach (var item in _viewModel.TheoryContentList)
            {
                if (item.Type == false)
                {
                    var span = new Span { Text = item.Text };
                    if (item.LinkPage != null)
                    {
                        span.GestureRecognizers.Add(new TapGestureRecognizer { Command = _viewModel.LinkTappedCommand, CommandParameter = item });
                        span.TextDecorations = TextDecorations.Underline;
                        span.TextColor = Color.Blue;
                        span.FontSize = 100;
                    }
                    formattedString.Spans.Add(span);
                }
                else
                {
                    theoryStack.Children.Add(new Label { FormattedText = formattedString });
                    formattedString = new FormattedString();
                    var mathView = new CSharpMath.Forms.MathView();
                    var span = new Span();
                    mathView.LaTeX = item.Text;
                    theoryStack.Children.Add(mathView);
                }
            }
        }
    }
}