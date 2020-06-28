using System;

namespace DemoLibrary
{
    public class XValueAdjustmentCalculator : IXValueAdjustmentCalculator
    {
        private readonly DateTime _basel3ReportingDate = new DateTime(2022, 01, 01);

        public XValueAdjustmentCalculator()
        {
            
        }
        public double Calculate(double x, double y)
        {
            var reportDate = DateTime.Now.ToLocalTime();
            
            var z = reportDate >= _basel3ReportingDate.Date
                ? x + y
                : x - y;

            return z;
        }
    }
}