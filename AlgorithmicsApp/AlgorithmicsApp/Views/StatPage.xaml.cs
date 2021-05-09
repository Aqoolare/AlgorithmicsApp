using AlgorithmicsApp.Classes;
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
    class EnumPicker : Picker
    {
        public static readonly BindableProperty EnumTypeProperty =
            BindableProperty.Create(nameof(EnumType), typeof(System.Type), typeof(EnumPicker),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    EnumPicker picker = (EnumPicker)bindable;

                    if (oldValue != null)
                    {
                        picker.ItemsSource = null;
                    }
                    if (newValue != null)
                    {
                        if (!((System.Type)newValue).GetTypeInfo().IsEnum)
                            throw new ArgumentException("EnumPicker: EnumType property must be enumeration type");

                        picker.ItemsSource = Enum.GetValues((System.Type)newValue);
                    }
                });

        public System.Type EnumType
        {
            set => SetValue(EnumTypeProperty, value);
            get => (System.Type)GetValue(EnumTypeProperty);
        }
    }

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

        //void switchToggledAsync(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    initiateProgressUpdate();
        //}


        void sliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (canvas != null)
            {
                // Invalidating surface due to data change
                canvas.InvalidateSurface();
            }
        }

        async void animateProgress(int progress)
        {
            //sw_listToggle.IsEnabled = false;
            sweepAngleSlider.Value = 1;

            // Looping at data interval of 5
            for (int i = 0; i < progress; i = i + 5)
            {
                sweepAngleSlider.Value = i;
                await Task.Delay(3);
            }

            sweepAngleSlider.Value = progress;
            //sw_listToggle.IsEnabled = true;

        }

        void initiateProgressUpdate()
        {
            animateProgress(progressUtils.getSweepAngle(_viewModel.TotalQuestionsCount, _viewModel.AnsweredQuestionsCount));
        }

        public void drawGaugeAsync()
        {
            // Radial Gauge Constants
            int uPadding = 150;
            int side = 500;
            int radialGaugeWidth = 25;

            // Line TextSize inside Radial Gauge
            int lineSize1 = 220;
            int lineSize2 = 70;
            int lineSize3 = 80;

            // Line Y Coordinate inside Radial Gauge
            int lineHeight1 = 100;
            int lineHeight2 = 200;
            int lineHeight3 = 300;

            // Start & End Angle for Radial Gauge
            float startAngle = -220;
            float sweepAngle = 260;

            try
            {

                // Getting Canvas Info 
                SKImageInfo info = args.Info;
                SKSurface surface = args.Surface;
                SKCanvas canvas = surface.Canvas;
                progressUtils.setDevice(info.Height, info.Width);
                canvas.Clear();

                // Getting Device Specific Screen Values
                // -------------------------------------------------

                // Top Padding for Radial Gauge
                float upperPading = progressUtils.getFactoredHeight(uPadding);

                /* Coordinate Plotting for Radial Gauge
                *
                *    (X1,Y1) ------------
                *           |   (XC,YC)  |
                *           |      .     |
                *         Y |            |
                *           |            |
                *            ------------ (X2,Y2))
                *                  X
                *   
                *To fit a perfect Circle inside --> X==Y
                *       i.e It should be a Square
                */

                // Xc & Yc are center of the Circle
                int Xc = info.Width / 2;
                float Yc = progressUtils.getFactoredHeight(side);

                // X1 Y1 are lefttop cordiates of rectange
                int X1 = (int)(Xc - Yc);
                int Y1 = (int)(Yc - Yc + upperPading);

                // X2 Y2 are rightbottom cordiates of rectange
                int X2 = (int)(Xc + Yc);
                int Y2 = (int)(Yc + Yc + upperPading);

                //Loggig Screen Specific Calculated Values
                Debug.WriteLine("INFO " + info.Width + " - " + info.Height);
                Debug.WriteLine(" C : " + upperPading + "  " + info.Height);
                Debug.WriteLine(" C : " + Xc + "  " + Yc);
                Debug.WriteLine("XY : " + X1 + "  " + Y1);
                Debug.WriteLine("XY : " + X2 + "  " + Y2);

                //  Empty Gauge Styling
                SKPaint paint1 = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = Color.FromHex("#e0dfdf").ToSKColor(),                   // Colour of Radial Gauge
                    StrokeWidth = progressUtils.getFactoredWidth(radialGaugeWidth), // Width of Radial Gauge
                    StrokeCap = SKStrokeCap.Round                                   // Round Corners for Radial Gauge
                };

                // Filled Gauge Styling
                SKPaint paint2 = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = Color.FromHex("#30D158").ToSKColor(),                   // Overlay Colour of Radial Gauge
                    StrokeWidth = progressUtils.getFactoredWidth(radialGaugeWidth), // Overlay Width of Radial Gauge
                    StrokeCap = SKStrokeCap.Round                                   // Round Corners for Radial Gauge
                };

                // Defining boundaries for Gauge
                SKRect rect = new SKRect(X1, Y1, X2, Y2);


                //canvas.DrawRect(rect, paint1);
                //canvas.DrawOval(rect, paint1);

                // Rendering Empty Gauge
                SKPath path1 = new SKPath();
                path1.AddArc(rect, startAngle, sweepAngle);
                canvas.DrawPath(path1, paint1);

                // Rendering Filled Gauge
                SKPath path2 = new SKPath();
                path2.AddArc(rect, startAngle, (float)sweepAngleSlider.Value);
                canvas.DrawPath(path2, paint2);

                //---------------- Drawing Text Over Gauge ---------------------------

                // Achieved Minutes
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

                    // Drawing Achieved Minutes Over Radial Gauge
                    canvas.DrawText(_viewModel.AnsweredQuestionsCount + "", Xc, Yc + progressUtils.getFactoredHeight(lineHeight1), skPaint);
                }

                // Achieved Minutes Text Styling
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Style = SKPaintStyle.Fill;
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColor.Parse("#484848");
                    skPaint.TextAlign = SKTextAlign.Center;
                    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize2);
                    canvas.DrawText("Решено правильно", Xc, Yc + progressUtils.getFactoredHeight(lineHeight2), skPaint);
                }

                // Goal Minutes Text Styling
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Style = SKPaintStyle.Fill;
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColor.Parse("#FF375F");
                    skPaint.TextAlign = SKTextAlign.Center;
                    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize3);

                    // Drawing Text Over Radial Gauge
                    canvas.DrawText("Всего: " + _viewModel.TotalQuestionsCount, Xc, Yc + progressUtils.getFactoredHeight(lineHeight3), skPaint);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
        }

        private void EnumPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Theme theme = (Theme)picker.SelectedItem;

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Remove(mergedDictionaries.First());

                switch (theme)
                {
                    case Theme.Dark:
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case Theme.Light:
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
        }
    }
}