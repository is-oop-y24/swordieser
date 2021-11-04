using System.Collections.Generic;

namespace IsuExtra
{
    public class ExtendedStudent : Isu.Services.Student
    {
        private List<Class> _personalTimetable;

        public ExtendedStudent(string name, ExtendedGroup group)
            : base(name, group)
        {
            _personalTimetable = new List<Class>();
            QualityOfJgtd = 0;
        }

        public IReadOnlyList<Class> StudentTimetable => _personalTimetable.AsReadOnly();

        public int QualityOfJgtd { get; }
    }
}