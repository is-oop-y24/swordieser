using System.Collections.Generic;
using NUnit.Framework;
using System;

namespace IsuExtra.Tests
{
    public class IsuExtraTests
    {
        private ExtendedIsuService _isuService;
        private List<Class> _groupTimetable;
        private List<Class> _foipTimetable; // fundamentals of intellectual property
        MegaFaculty _ftmi;
        MegaFaculty _tint;

        [SetUp]
        public void Setup()
        {
            _ftmi = new MegaFaculty(MegaFacultyName.FTMI);
            _tint = new MegaFaculty(MegaFacultyName.TINT);
            _isuService = new ExtendedIsuService();
            _groupTimetable = new List<Class>()
            {
                new Class(new ClassTime(DayOfWeek.Monday, NumberOfClass.Second),
                    "Nosovitskii", 403, "oop"),
                new Class(new ClassTime(DayOfWeek.Tuesday, NumberOfClass.Third),
                    "Suslina", 0, "probability theory"),
                new Class(new ClassTime(DayOfWeek.Thursday, NumberOfClass.First),
                    "Egorov", 539, "physics"),
                new Class(new ClassTime(DayOfWeek.Friday, NumberOfClass.Fourth),
                    "Batotsyrenov", 230, "operating systems"),
                new Class(new ClassTime(DayOfWeek.Saturday, NumberOfClass.Third),
                    "Vozianova", 429, "additional chapters of higher mathematics")
            };

            _foipTimetable = new List<Class>()
            {
                new Class(new ClassTime(DayOfWeek.Wednesday, NumberOfClass.Third),
                    "Nikolaev", 206, "foip"),
                new Class(new ClassTime(DayOfWeek.Saturday, NumberOfClass.Fourth),
                    "Nikolaev", 403, "foip")
            };
        }

        [Test]
        public void AddThenRemoveStudentFromJgtd()
        {
            var streams = new List<Stream>()
            {
                new Stream(_ftmi, 20, _foipTimetable),
            };

            Jgtd foip = _isuService.AddJgtd("foip", streams);
            ExtendedGroup m3204 = _isuService.AddGroup("M3204", _groupTimetable, _tint);
            ExtendedStudent someone = _isuService.AddStudent("someone", m3204);

            _isuService.AddStudentOnStream(streams[0], new List<ExtendedStudent>() {someone});
            IReadOnlyList<ExtendedStudent> temp = streams[0].StreamStudents;
            Assert.AreEqual(someone, temp[0]);

            _isuService.RemoveStudentFromStream(streams[0], someone);
            Assert.IsEmpty(streams[0].StreamStudents);
        }

        [Test]
        public void GetStreamsByCourse()
        {
            var foipSecondTimetable = new List<Class>()
            {
                new Class(new ClassTime(DayOfWeek.Tuesday, NumberOfClass.Fifth),
                    "Nikolaev", 206, "foip"),
                new Class(new ClassTime(DayOfWeek.Saturday, NumberOfClass.Eighth),
                    "Nikolaev", 403, "foip")
            };

            var foipStreams = new List<Stream>()
            {
                new Stream(_ftmi, 30, _foipTimetable),
                new Stream(_ftmi, 20, foipSecondTimetable)
            };

            var foip = new Jgtd(foipStreams);

            Assert.AreEqual(foipStreams, _isuService.GetStreamsByCourse(foip));
        }

        [Test]
        public void GetStudentsByStream()
        {
            var stream = new Stream(_ftmi, 20, _foipTimetable);
            var streams = new List<Stream>()
            {
                stream
            };
            Jgtd foip = _isuService.AddJgtd("foip", streams);


            ExtendedGroup m3204 = _isuService.AddGroup("M3204", _groupTimetable, _tint);
            ExtendedGroup m3203 = _isuService.AddGroup("M3203", _groupTimetable, _tint);

            ExtendedStudent student1 = _isuService.AddStudent("student1", m3204);
            ExtendedStudent student2 = _isuService.AddStudent("student2", m3203);


            _isuService.AddStudentOnStream(stream, new List<ExtendedStudent>() {student1, student2});
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
            var stream = new Stream(_ftmi, 20, _foipTimetable);
            var streams = new List<Stream>()
            {
                stream
            };
            Jgtd foip = _isuService.AddJgtd("foip", streams);


            ExtendedGroup m3204 = _isuService.AddGroup("M3204", _groupTimetable, _tint);

            ExtendedStudent student1 = _isuService.AddStudent("student1", m3204);
            ExtendedStudent student2 = _isuService.AddStudent("student2", m3204);

            _isuService.AddStudentOnStream(stream, new List<ExtendedStudent>() {student1});
            ExtendedStudent temp = _isuService.GetStudentWithoutCoursesByGroup(m3204)[0];
            Assert.AreEqual(student2, temp);
        }
    }
}