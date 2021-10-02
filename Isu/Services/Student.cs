namespace Isu.Services
{
    public class Student
    {
        private static int _id = 100000;

        public Student(string name, Group group)
        {
            Name = name;
            StudentId = _id;
            _id++;
            Group = group;
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
            private set;
        }

        public void ChangeGroup(Group newGroup)
        {
            Group = newGroup;
        }
    }
}