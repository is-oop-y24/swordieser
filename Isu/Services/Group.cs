using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        private const string CourseName = "M3";

        public Group(string name)
        {
            CheckGroupName(name);
            GroupName = name;
            GroupLimit = 20;
            Students = new List<Student>();
            StudentsGroup = Students.AsReadOnly();
        }

        public List<Student> Students { get; }

        public IReadOnlyList<Student> StudentsGroup { get; }

        public string GroupName
        {
            get;
        }

        public int GroupLimit
        {
            get;
        }

        public static void CheckGroupName(string name)
        {
            if (name.Length != 5)
            {
                throw new InvalidGroupNameException();
            }

            try
            {
                int temp = int.Parse(name.Substring(2, 1));
            }
            catch (Exception)
            {
                throw new InvalidGroupNameException();
            }

            int courseNumber = CourseNumber.StringToIntNumber(name);
            int groupNumber = int.Parse(name.Substring(3, 2));
            if (!name[..2].Equals(CourseName) ||

                // courseNumber is < 1 or > 4 ||
                // groupNumber is < 0 or > 15
                courseNumber < 0 || courseNumber > 3 ||
                groupNumber < 0 || groupNumber > 15)
            {
                throw new InvalidGroupNameException();
            }
        }
    }
}