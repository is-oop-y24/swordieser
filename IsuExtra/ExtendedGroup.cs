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
        }

        public ExtendedGroup(string name, List<Class> timetable, MegaFaculty megaFaculty)
            : base(name)
        {
            _timetable = timetable;
            MegaFaculty = megaFaculty;
        }

        public IReadOnlyList<Class> TimetableOfGroup => _timetable.AsReadOnly();

        public MegaFaculty MegaFaculty { get; }

        public void AddClass(params Class[] @class)
        {
            _timetable.AddRange(@class);
        }
    }
}