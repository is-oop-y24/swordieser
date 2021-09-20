using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<CourseNumber> _courses;

        public IsuService()
        {
            _courses = new List<CourseNumber>
            {
                new CourseNumber(1), // first course
                new CourseNumber(2), // second course
                new CourseNumber(3), // third course
                new CourseNumber(4), // fourth course
            };
        }

        public Group AddGroup(string name)
        {
            var newGroup = new Group(name);
            int courseNumber = int.Parse(name.Substring(2, 1));
            foreach (Group group in _courses[courseNumber - 1].Groups)
            {
                if (group.GroupName.Equals(name))
                {
                    return group;
                }
            }

            _courses[courseNumber - 1].Groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= group.GroupLimit)
                throw new MaxStudentPerGroupException();
            var s = new Student(name);
            group.Students.Add(s);
            s.Group = group;
            return s;
        }

        public Student GetStudent(int id)
        {
            foreach (CourseNumber course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    foreach (Student student in group.Students)
                    {
                        if (student.StudentId == id)
                        {
                            return student;
                        }
                    }
                }
            }

            throw new StudentIdDoesntExist();
        }

        public Student FindStudent(string name)
        {
            foreach (CourseNumber course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    foreach (Student student in group.Students)
                    {
                        if (student.Name == name)
                        {
                            return student;
                        }
                    }
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            int courseNumber = int.Parse(groupName.Substring(2, 1));
            foreach (Group group in _courses[courseNumber - 1].Groups)
            {
                if (group.GroupName == groupName)
                {
                    return group.Students;
                }
            }

            return new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (Group group in courseNumber.Groups)
            {
                students.AddRange(group.Students);
            }

            return students.Count != 0 ? students : new List<Student>();
        }

        public Group FindGroup(string groupName)
        {
            int courseNumber = int.Parse(groupName.Substring(2, 1));
            foreach (Group group in _courses[courseNumber - 1].Groups)
            {
                if (group.GroupName == groupName)
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return courseNumber.Groups.Count != 0 ? courseNumber.Groups : new List<Group>();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (newGroup.Students.Count == newGroup.GroupLimit)
            {
                throw new MaxStudentPerGroupException();
            }

            Group firstGroup = FindGroup(student.Group.GroupName);
            firstGroup.Students.Remove(student);
            newGroup.Students.Add(student);
            student.Group = newGroup;
        }
    }
}