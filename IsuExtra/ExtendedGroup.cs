using System.Collections.Generic;

namespace IsuExtra
{
    public class ExtendedGroup : Isu.Services.Group
    {
        private List<Class> _timetable;

        public ExtendedGroup(string name, MegaFaculty megaFaculty)
            : base(name)
        {
            MegaFaculty = megaFaculty;
            _timetable = new List<Class>();
            ExtendedStudents = new List<ExtendedStudent>();
        }

        public ExtendedGroup(string name, List<Class> timetable, MegaFaculty megaFaculty)
            : base(name)
        {
            _timetable = timetable;
            MegaFaculty = megaFaculty;
        }

        public IReadOnlyList<Class> TimetableOfGroup => _timetable.AsReadOnly();

        public MegaFaculty MegaFaculty { get; }

        private List<ExtendedStudent> ExtendedStudents
        {
            get;
        }

        public IReadOnlyList<ExtendedStudent> GetExtendedStudents()
        {
            return ExtendedStudents.AsReadOnly();
        }

        public void AddClass(params Class[] @class)
        {
            _timetable.AddRange(@class);
        }
    }
}