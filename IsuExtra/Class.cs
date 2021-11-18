namespace IsuExtra
{
    public class Class
    {
        public Class(ClassTime time, string teacher, int classroom, string name)
        {
            Time = time;
            Teacher = teacher;
            Classroom = classroom;
            Name = name;
        }

        public ClassTime Time
        {
            get;
        }

        public string Teacher
        {
            get;
        }

        public int Classroom
        {
            get;
        }

        public string Name
        {
            get;
        }
    }
}