using System.Collections.Generic;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTests
    {
        private ExtendedIsuService _isuService;
        private List<Class> _groupTimetable;
        private List<Class> _foipTimetable; // fundamentals of intellectual property
        MegaFaculty ftmi;
        MegaFaculty tint;

        [SetUp]
        public void Setup()
        {
            ftmi = new MegaFaculty(MegaFacultyName.FTMI);
            tint = new MegaFaculty(MegaFacultyName.TINT);
            _isuService = new ExtendedIsuService();
            _groupTimetable = new List<Class>()
            {
                new Class(new DayTime(Day.Monday, NumberOfClass.Second),
                    "Nosovitskii", 403, "oop"),
                new Class(new DayTime(Day.Tuesday, NumberOfClass.Third),
                    "Suslina", 0, "probability theory"),
                new Class(new DayTime(Day.Thursday, NumberOfClass.First),
                    "Egorov", 539, "physics"),
                new Class(new DayTime(Day.Friday, NumberOfClass.Fourth),
                    "Batotsyrenov", 230, "operating systems"),
                new Class(new DayTime(Day.Saturday, NumberOfClass.Third),
                    "Vozianova", 429, "additional chapters of higher mathematics")
            };

            _foipTimetable = new List<Class>()
            {
                new Class(new DayTime(Day.Wednesday, NumberOfClass.Third),
                    "Nikolaev", 206, "foip"),
                new Class(new DayTime(Day.Saturday, NumberOfClass.Fourth),
                    "Nikolaev", 403, "foip")
            };
        }

        [Test]
        public void AddThenRemoveStudentFromJgtd()
        {
            var streams = new List<Stream>()
            {
                new Stream(ftmi, 20, _foipTimetable),
            };

            var foip = _isuService.AddJgtd("foip", streams);
            var m3204 = _isuService.AddGroup("M3204", _groupTimetable, tint);
            var someone = _isuService.AddStudent("someone", m3204);

            _isuService.AddStudentOnStream(streams[0], someone);
            var temp = streams[0].StreamStudents;
            Assert.AreEqual(someone, temp[0]);

            _isuService.RemoveStudentFromStream(streams[0], someone);
            Assert.IsEmpty(streams[0].StreamStudents);
        }

        [Test]
        public void GetStreamsByCourse()
        {
            var foipSecondTimetable = new List<Class>()
            {
                new Class(new DayTime(Day.Tuesday, NumberOfClass.Fifth),
                    "Nikolaev", 206, "foip"),
                new Class(new DayTime(Day.Saturday, NumberOfClass.Eighth),
                    "Nikolaev", 403, "foip")
            };

            var foipStreams = new List<Stream>()
            {
                new Stream(ftmi, 30, _foipTimetable),
                new Stream(ftmi, 20, foipSecondTimetable)
            };

            var foip = new Jgtd(foipStreams);

            Assert.AreEqual(foipStreams, _isuService.GetStreamsByCourse(foip));
        }

        [Test]
        public void GetStudentsByStream()
        {
            var stream = new Stream(ftmi, 20, _foipTimetable);
            var streams = new List<Stream>()
            {
                stream
            };
            var foip = _isuService.AddJgtd("foip", streams);


            var m3204 = _isuService.AddGroup("M3204", _groupTimetable, tint);
            var m3203 = _isuService.AddGroup("M3203", _groupTimetable, tint);

            var student1 = _isuService.AddStudent("student1", m3204);
            var student2 = _isuService.AddStudent("student2", m3203);


            _isuService.AddStudentOnStream(stream, student1, student2);
            var expected = new List<ExtendedStudent>()
            {
                student1,
                student2,
            };

            Assert.AreEqual(expected, stream.StreamStudents);
        }

        [Test]
        public void GetStudentsWithoutCourse()
        {
            var stream = new Stream(ftmi, 20, _foipTimetable);
            var streams = new List<Stream>()
            {
                stream
            };
            var foip = _isuService.AddJgtd("foip", streams);


            var m3204 = _isuService.AddGroup("M3204", _groupTimetable, tint);

            var student1 = _isuService.AddStudent("student1", m3204);
            var student2 = _isuService.AddStudent("student2", m3204);

            _isuService.AddStudentOnStream(stream, student1);
            var temp = _isuService.GetStudentWithoutCoursesByGroup(m3204)[0];
            Assert.AreEqual(student2, temp);
        }
    }
}