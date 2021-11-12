using System.Collections.Generic;

namespace IsuExtra
{
    public class Jgtd
    {
        public Jgtd(List<Stream> courses)
        {
            Courses = courses;
        }

        private List<Stream> Courses
        {
            get;
        }

        public IReadOnlyList<Stream> GetCourses()
        {
            return Courses.AsReadOnly();
        }
    }
}