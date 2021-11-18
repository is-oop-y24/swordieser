using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra
{
    public class Stream
    {
        private readonly List<ExtendedStudent> _students;

        private List<Class> _timetable;

        public Stream(MegaFaculty megaFaculty, int maxQuality, List<Class> timetable)
        {
            MegaFaculty = megaFaculty;
            MaxQuality = maxQuality;
            _students = new List<ExtendedStudent>();
            _timetable = timetable;
        }

        public int MaxQuality
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

        public void AddStudent(List<ExtendedStudent> students)
        {
            foreach (ExtendedStudent student in students)
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