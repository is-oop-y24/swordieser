using System.Collections.Generic;

namespace IsuExtra
{
    public class ExtendedGroup : Isu.Services.Group
    {
        private List<Class> _timetable;
        private List<ExtendedStudent> _extendedStudents;

        public ExtendedGroup(string name, MegaFaculty megaFaculty)
            : base(name)
        {
            MegaFaculty = megaFaculty;
            _timetable = new List<Class>();
            _extendedStudents = new List<ExtendedStudent>();
        }

        public ExtendedGroup(string name, List<Class> timetable, MegaFaculty megaFaculty)
            : base(name)
        {
            _timetable = timetable;
            MegaFaculty = megaFaculty;
            _extendedStudents = new List<ExtendedStudent>();
        }

        public IReadOnlyList<Class> TimetableOfGroup => _timetable.AsReadOnly();

        public MegaFaculty MegaFaculty { get; }

        public IReadOnlyList<ExtendedStudent> GetExtendedStudents()
        {
            return _extendedStudents.AsReadOnly();
        }

        public void AddClass(params Class[] @class)
        {
            _timetable.AddRange(@class);
        }

        public ExtendedStudent AddStudent(string name)
        {
            var student = new ExtendedStudent(name, this);
            _extendedStudents.Add(student);
            return student;
        }
    }
}