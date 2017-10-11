using LiveScoreUpdateSystem.Services.Common.Contracts;
using System;

namespace LiveScoreUpdateSystem.Services.Common
{
    public class TimeProvider : ITimeProvider
    {
        static TimeProvider()
        {
            CurrentProvider = new TimeProvider();
        }

        public static ITimeProvider CurrentProvider { get; private set; }

        public DateTime CurrentDate
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
