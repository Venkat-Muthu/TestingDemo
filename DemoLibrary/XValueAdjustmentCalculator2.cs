using System;
using NodaTime;
using NodaTime.Extensions;

namespace DemoLibrary
{
    public class XValueAdjustmentCalculator2 : IXValueAdjustmentCalculator
    {
        private readonly IClock _clock;
        private readonly DateTime _basel3ReportingDate = new DateTime(2022, 01, 01);

        public XValueAdjustmentCalculator2(IClock clock)
        {
            _clock = clock;
        }

        public double Calculate(double x, double y)
        {
            var reportDate = _clock.GetCurrentInstant().InZone(DateTimeZone.Utc).LocalDateTime.Date;

            var z = reportDate >= _basel3ReportingDate.ToLocalDateTime().Date
                ? x + y
                : x - y;

            return z;
        }
    }
}