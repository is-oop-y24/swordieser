namespace IsuExtra
{
    public enum Day
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
    }

    public enum NumberOfClass
    {
        First = 1,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eighth,
    }

    public class DayTime
    {
        public DayTime(Day day, NumberOfClass numberOfClass)
        {
            Day = day;
            NumberOfClass = numberOfClass;
        }

        private Day Day { get; }
        private NumberOfClass NumberOfClass { get; }
    }
}