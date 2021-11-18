using System;

namespace IsuExtra
{
    public class ClassTime
    {
        public ClassTime(DayOfWeek day, NumberOfClass numberOfClass)
        {
            Day = day;
            NumberOfClass = numberOfClass;
        }

        private DayOfWeek Day { get; }
        private NumberOfClass NumberOfClass { get; }
    }
}