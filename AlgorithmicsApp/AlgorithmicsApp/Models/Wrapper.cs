using System;
using System.Collections.Generic;
using System.Text;

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

        public Wrapper(TheoryContent tc, Link l)
        {
            Text1 = tc.Text1;
            Text2 = tc.Text2;
            BoldText = tc.BoldText;
            Formula = tc.Formula;
            HeightReq = tc.CountStrings * 30;
            Link = l;
        }
    }
}
