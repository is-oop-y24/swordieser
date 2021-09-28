using System.Collections.Generic;
using System.Linq;
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
            int courseNumber = CourseNumber.StringToIntNumber(name);
            foreach (Group group in _courses[courseNumber].Groups)
            {
                if (group.GroupName.Equals(name))
                {
                    return group;
                }
            }

            _courses[courseNumber].Groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= group.GroupLimit)
                throw new MaxStudentPerGroupException();
            var s = new Student(name, group);
            group.Students.Add(s);
            return s;
        }

        public Student GetStudent(int id)
        {
            foreach (CourseNumber course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    foreach (Student student in group.StudentsGroup.ToList())
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
                    foreach (Student student in group.StudentsGroup.ToList())
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
            Group.CheckGroupName(groupName);
            int courseNumber = CourseNumber.StringToIntNumber(groupName);
            foreach (Group group in _courses[courseNumber].Groups)
            {
                if (group.GroupName == groupName)
                {
                    return group.StudentsGroup.ToList();
                }
            }

            return new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (Group group in courseNumber.Groups)
            {
                students.AddRange(group.StudentsGroup.ToList());
            }

            return students;
        }

        public Group FindGroup(string groupName)
        {
            Group.CheckGroupName(groupName);
            int courseNumber = CourseNumber.StringToIntNumber(groupName);
            foreach (Group group in _courses[courseNumber].Groups)
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
            return courseNumber.Groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (newGroup.Students.Count == newGroup.GroupLimit)
            {
                throw new MaxStudentPerGroupException();
            }

            Group firstGroup = student.Group;
            firstGroup.Students.Remove(student);
            newGroup.Students.Add(student);
            student.ChangeGroup(newGroup);
        }
    }
}