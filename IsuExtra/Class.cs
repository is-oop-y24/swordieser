namespace IsuExtra
{
    public class Class
    {
        public Class(ExtendedGroup group, DayTime time, string teacher, int classroom)
        {
            Group = group;
            Time = time;
            Teacher = teacher;
            Classroom = classroom;
        }

        public ExtendedGroup Group
        {
            get;
            private set;
        }

        public DayTime Time
        {
            get;
            private set;
        }

        public string Teacher
        {
            get;
            private set;
        }

        public int Classroom
        {
            get;
            private set;
        }
    }
}