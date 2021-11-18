using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra
{
    public class ExtendedStudent : Isu.Services.Student
    {
        private List<Class> _personalTimetable;

        private List<Stream> _jgtdStreams;

        public ExtendedStudent(string name, ExtendedGroup group)
            : base(name, group)
        {
            _jgtdStreams = new List<Stream>();
            _personalTimetable = new List<Class>();
            _personalTimetable.AddRange(group.TimetableOfGroup);
            QualityOfJgtd = 0;
        }

        public IReadOnlyList<Class> StudentTimetable => _personalTimetable.AsReadOnly();

        public int QualityOfJgtd { get; private set; }

        public void AddStream(Stream stream)
        {
            if (QualityOfJgtd == 2)
            {
                throw new TooManyJGTDException();
            }

            _personalTimetable.AddRange(stream.StreamTimetable);
            _jgtdStreams.Add(stream);
            QualityOfJgtd++;
        }

        public void RemoveStream(Stream stream)
        {
            _jgtdStreams.Remove(stream);
            QualityOfJgtd--;
        }
    }
}