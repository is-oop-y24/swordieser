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
            if (name.Length != 5)
            {
                throw new InvalidGroupNameException();
            }

            try
            {
                int temp = int.Parse(name.Substring(2, 1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new InvalidGroupNameException();
            }

            int courseNumber = int.Parse(name.Substring(2, 1));
            int groupNumber = int.Parse(name.Substring(3, 2));
            if (!name[..2].Equals(CourseName) ||

                // courseNumber is < 1 or > 4 ||
                // groupNumber is < 0 or > 15
                courseNumber < 1 || courseNumber > 4 ||
                groupNumber < 0 || groupNumber > 15)
            {
                throw new InvalidGroupNameException();
            }

            GroupName = name;
            GroupLimit = 20;
            Students = new List<Student>();
        }

        public List<Student> Students { get; }

        public string GroupName
        {
            get;
        }

        public int GroupLimit
        {
            get;
        }
    }
}