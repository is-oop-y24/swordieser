namespace IsuExtra
{
    public class ClassTime
    {
        public ClassTime(System.DayOfWeek day, NumberOfClass numberOfClass)
        {
            Day = day;
            NumberOfClass = numberOfClass;
        }

        private System.DayOfWeek Day { get; }
        private NumberOfClass NumberOfClass { get; }
    }
}