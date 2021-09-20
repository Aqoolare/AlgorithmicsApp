using AlgorithmicsApp.Models;
using AlgorithmicsApp.Themes;
using AlgorithmicsApp.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlgorithmicsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatPage : ContentPage
    {
        SKPaintSurfaceEventArgs args;
        StatViewModel _viewModel;
        ProgressUtils progressUtils = new ProgressUtils();



        public StatPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new StatViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.UpdateInterface = initiateProgressUpdate;
            await _viewModel.LoadQuestionCounts();
        }

        private void canvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            args = e;
            drawGaugeAsync();
        }


        void sliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (canvas != null)
            {
                
                canvas.InvalidateSurface();
            }
        }

        async void animateProgress(int progress)
        {
            
            sweepAngleSlider.Value = 1;

            
            for (int i = 0; i < progress; i = i + 5)
            {
                sweepAngleSlider.Value = i;
                await Task.Delay(3);
            }

            sweepAngleSlider.Value = progress;
            

        }

        void initiateProgressUpdate()
        {
            animateProgress(progressUtils.getSweepAngle(_viewModel.TotalQuestionsCount, _viewModel.AnsweredQuestionsCount));
        }

        public void drawGaugeAsync()
        {
            
            int uPadding = 150;
            int side = 500;
            int radialGaugeWidth = 25;

            
            int lineSize1 = 220;
            int lineSize2 = 70;
            int lineSize3 = 80;

            
            int lineHeight1 = 100;
            int lineHeight2 = 200;
            int lineHeight3 = 300;

            
            float startAngle = -220;
            float sweepAngle = 260;

            try
            {

                
                SKImageInfo info = args.Info;
                SKSurface surface = args.Surface;
                SKCanvas canvas = surface.Canvas;
                progressUtils.setDevice(info.Height, info.Width);
                canvas.Clear();

                
                float upperPading = progressUtils.getFactoredHeight(uPadding);

                
                int Xc = info.Width / 2;
                float Yc = progressUtils.getFactoredHeight(side);

                
                int X1 = (int)(Xc - Yc);
                int Y1 = (int)(Yc - Yc + upperPading);

                
                int X2 = (int)(Xc + Yc);
                int Y2 = (int)(Yc + Yc + upperPading);


                
                SKPaint paint1 = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = Color.FromHex("#e0dfdf").ToSKColor(),                   
                    StrokeWidth = progressUtils.getFactoredWidth(radialGaugeWidth), 
                    StrokeCap = SKStrokeCap.Round                                   
                };

                
                SKPaint paint2 = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = Color.FromHex("#30D158").ToSKColor(),                   
                    StrokeWidth = progressUtils.getFactoredWidth(radialGaugeWidth), 
                    StrokeCap = SKStrokeCap.Round                                   
                };

                
                SKRect rect = new SKRect(X1, Y1, X2, Y2);


                
                SKPath path1 = new SKPath();
                path1.AddArc(rect, startAngle, sweepAngle);
                canvas.DrawPath(path1, paint1);

                
                SKPath path2 = new SKPath();
                path2.AddArc(rect, startAngle, (float)sweepAngleSlider.Value);
                canvas.DrawPath(path2, paint2);

                

                
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Style = SKPaintStyle.Fill;
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColor.Parse("#3A3A3C");
                    skPaint.TextAlign = SKTextAlign.Center;
                    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize1);
                    skPaint.Typeface = SKTypeface.FromFamilyName(
                                        "Arial",
                                        SKFontStyleWeight.Bold,
                                        SKFontStyleWidth.Normal,
                                        SKFontStyleSlant.Upright);

                    
                    canvas.DrawText(_viewModel.AnsweredQuestionsCount + "", Xc, Yc + progressUtils.getFactoredHeight(lineHeight1), skPaint);
                }

                
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Style = SKPaintStyle.Fill;
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColor.Parse("#484848");
                    skPaint.TextAlign = SKTextAlign.Center;
                    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize2);
                    canvas.DrawText("Решено правильно", Xc, Yc + progressUtils.getFactoredHeight(lineHeight2), skPaint);
                }

                
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Style = SKPaintStyle.Fill;
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColor.Parse("#FF375F");
                    skPaint.TextAlign = SKTextAlign.Center;
                    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize3);

                    
                    canvas.DrawText("Всего: " + _viewModel.TotalQuestionsCount, Xc, Yc + progressUtils.getFactoredHeight(lineHeight3), skPaint);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
        }
    }
}