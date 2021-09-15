using System.Collections.Generic;

namespace Isu.Services
{
    public class CourseNumber
    {
        private int _courseNumber;

        public CourseNumber(int number)
        {
            _courseNumber = number;
            Groups = new List<Group>();
        }

        public List<Group> Groups { get; }
    }
}