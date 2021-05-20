using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AlgorithmicsApp.Models
{
    public class ProgressUtils
    {
        private const float refHeight = 1080;
        private const float refWidth = 632;

        // Derived Proportinate Values
        private float deviceHeight = 1;
        private float deviceWidth = 1;

        // Empty Constructor
        public ProgressUtils() { }

        // Setting Device Specific Values
        public void setDevice(int deviceHeight, int deviceWidth)
        {
            this.deviceHeight = deviceHeight;
            this.deviceWidth = deviceWidth;
        }

        // Deriving Proportinate Height
        public float getFactoredHeight(int value)
        {
            return (float)((value / refHeight) * deviceHeight);
        }

        // Deriving Proportinate Width
        public float getFactoredWidth(int value)
        {
            return (float)((value / refWidth) * deviceWidth);
        }

        // Deriving Sweep Angle
        public int getSweepAngle(int goal, int achieved)
        {
            int SweepAngle = 260;
            float factor = (float)achieved / goal;
            Debug.WriteLine("SWEEP ANGLE : " + (int)(SweepAngle * factor));

            return (int)(SweepAngle * factor);

        }
    }
}
