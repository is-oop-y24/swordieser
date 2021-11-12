using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra
{
    public class ExtendedIsuService : Isu.Services.IsuService
    {
        private List<Jgtd> _courses = new List<Jgtd>();
        private List<ExtendedGroup> _groups = new List<ExtendedGroup>();

        public Jgtd AddJgtd(string name, List<Stream> streams)
        {
            var jgtd = new Jgtd(streams);
            _courses.Add(jgtd);
            return jgtd;
        }

        public ExtendedGroup AddGroup(string name, List<Class> timetable, MegaFaculty mf)
        {
            var group = new ExtendedGroup(name, timetable, mf);
            _groups.Add(group);
            return group;
        }

        public ExtendedStudent AddStudent(string name, ExtendedGroup group)
        {
            return group.AddStudent(name);
        }

        public void AddStudentOnStream(Stream stream, params ExtendedStudent[] students)
        {
            if (students.Any(student => student.StudentTimetable.Any(studentClass =>
                stream.StreamTimetable.Any(streamClass => studentClass.Time == streamClass.Time))))
            {
                throw new IntersectionOfClassesException();
            }

            stream.AddStudent(students);
            foreach (ExtendedStudent student in students)
            {
                student.AddStream(stream);
            }
        }

        public ExtendedStudent RemoveStudentFromStream(Stream stream, ExtendedStudent student)
        {
            student.RemoveStream(stream);
            return stream.RemoveStudent(student);
        }

        public IReadOnlyList<Stream> GetStreamsByCourse(Jgtd jgtd)
        {
            return jgtd.GetCourses();
        }

        public List<ExtendedStudent> GetStudentWithoutCoursesByGroup(ExtendedGroup group)
        {
            return group.GetExtendedStudents().Where(student => student.QualityOfJgtd == 0).ToList();
        }
    }
}