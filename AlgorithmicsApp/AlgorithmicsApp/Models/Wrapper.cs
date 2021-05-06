using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AlgorithmicsApp.Models
{
    public class Wrapper
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string BoldText { get; set; }
        public string Formula { get; set; }
        public Link Link { get; set; }
        public int HeightReq { get; set; }
        public int LabelHeightReq { get; set; }

        public Wrapper(TheoryContent tc, Link l)
        {
            Text1 = tc.Text1;
            Text2 = tc.Text2;
            BoldText = tc.BoldText;
            Formula = tc.Formula;
            var p = DeviceDisplay.MainDisplayInfo.Density;
            var temp1 = Convert.ToInt32(tc.FormulaCountStrings * 30 * (2.625 / p));
            var temp2 = Convert.ToInt32(tc.FormulaCountStrings * 30 + (2.625 / p)*30);
            HeightReq = Math.Min(temp1, temp2);
            LabelHeightReq = tc.TextCountStrings * 15;
            Link = l;
        }
    }
}
