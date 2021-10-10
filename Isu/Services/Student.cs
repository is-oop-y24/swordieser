namespace Isu.Services
{
    public class Student
    {
        private static int _id = 100000;

        public Student(string name)
        {
            Name = name;
            StudentId = _id;
            _id++;
        }

        public string Name
        {
            get;
        }

        public int StudentId
        {
            get;
        }

        public Group Group
        {
            get;
            set;
        }
    }
}