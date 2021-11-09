using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra
{
    public class Stream
    {
        private readonly List<ExtendedStudent> _students;

        private List<Class> _timetable;

        public Stream(string name, MegaFaculty megaFaculty, int maxQuality)
        {
            Name = name;
            MegaFaculty = megaFaculty;
            MaxQuality = maxQuality;
            _students = new List<ExtendedStudent>();
            _timetable = new List<Class>();
        }

        public int MaxQuality
        {
            get;
            private set;
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

        public IReadOnlyList<Class> StreamTimetable => _timetable.AsReadOnly();

        public void AddStudent(params ExtendedStudent[] students)
        {
            foreach (var student in students)
            {
                if (_students.Count < MaxQuality)
                    _students.Add(student);
                else
                    throw new NoPlaceForStudentException();
            }
        }

        public ExtendedStudent RemoveStudent(ExtendedStudent student)
        {
            _students.Remove(student);
            return student;
        }
    }
}