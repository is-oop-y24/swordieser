using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //done: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3200");
            Student student = _isuService.AddStudent(group, "Taylor Swift");
            Assert.Contains(student, group.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var group = new Group("M3100");
                for (int i = 0; i < 21; i++)
                {
                    Student student = _isuService.AddStudent(group, "A A A " + i);
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            // Assert.Catch<IsuException>(() =>
            // {
            //     var group1 = new Group("M2400");
            //     var group2 = new Group("lolkekcheburek");
            //     var group3 = new Group("♂AHH♂");
            //     var group4 = new Group("M3600");
            //     var group5 = new Group("M3220");
            // });
            
            Assert.Catch<IsuException>(() => new Group("M2400"));
            Assert.Catch<IsuException>(() => new Group("lolkekcheburek"));
            Assert.Catch<IsuException>(() => new Group("♂AHH♂"));
            Assert.Catch<IsuException>(() => new Group("M3600"));
            Assert.Catch<IsuException>(() => new Group("M3220"));
            
            // а райдер говорит, что лучше через переменные...
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group1 = _isuService.AddGroup("M3201");
            Group group2 = _isuService.AddGroup("M3202");
            Student student = _isuService.AddStudent(group1, "Me Me Me");
            Student student1 = _isuService.AddStudent(group1, "Meme Hehe");
            Student student2 = _isuService.AddStudent(group1, "Meme");
            _isuService.ChangeStudentGroup(student, group2);
            Assert.IsFalse(group1.Students.Contains(student));
        }
    }
}