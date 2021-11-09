using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra
{
    public class ExtendedIsuService : Isu.Services.IsuService
    {
        private readonly List<Stream> _courses = new List<Stream>();

        public Stream AddJgtd(string name, MegaFaculty megaFaculty, int maxQuality)
        {
            var stream = new Stream(name, megaFaculty, maxQuality);
            _courses.Add(stream);
            return stream;
        }

        public void AddStudent(Stream stream, params ExtendedStudent[] students)
        {
            if ((from student in students
                from studentClass in student.StudentTimetable
                from streamClass in stream.StreamTimetable
                where studentClass.Time == streamClass.Time
                select studentClass).Any())
            {
                throw new IntersectionOfClassesException();
            }

            stream.AddStudent(students);
            foreach (ExtendedStudent student in students)
            {
                student.AddStream(stream);
            }
        }

        public ExtendedStudent RemoveStudent(Stream stream, ExtendedStudent student)
        {
            student.RemoveStream(stream);
            return stream.RemoveStudent(student);
        }

        public IReadOnlyList<ExtendedStudent> GetStudentsByJgtdGroup(ExtendedGroup group)
        {
            return group.GetExtendedStudents();
        }

        public List<ExtendedStudent> GetStudentWithoutCoursesByGroup(ExtendedGroup group)
        {
            return group.GetExtendedStudents().Where(student => student.QualityOfJgtd == 0).ToList();
        }
    }
}