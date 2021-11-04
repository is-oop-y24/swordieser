using System.Collections.Generic;

namespace IsuExtra
{
    public class Stream
    {
        private readonly List<ExtendedStudent> _students;

        private List<Class> _timetable;

        public Stream(string name, MegaFaculty megaFaculty)
        {
            Name = name;
            MegaFaculty = megaFaculty;
            _students = new List<ExtendedStudent>();
            _timetable = new List<Class>();
        }

        public string Name
        {
            get;
            private set;
        }

        public MegaFaculty MegaFaculty
        {
            get;
        }

        public IReadOnlyList<ExtendedStudent> StreamStudents => _students.AsReadOnly();

        public void AddStudent(params ExtendedStudent[] student)
        {
            _students.AddRange(student);
        }
    }
}