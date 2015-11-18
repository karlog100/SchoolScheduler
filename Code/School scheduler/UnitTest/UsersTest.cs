using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modules;
using System.Globalization;

namespace UnitTest
{
    [TestClass]
    public class UsersTest
    {
        #region Validate_Email
        public void Validate_Email(string Email)
        {
            User TestUser = new User();
            TestUser.Validate_Email(Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Empty_Email()
        {
            Validate_Email("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MissingAt_Email()
        {
            Validate_Email("TesterTest.test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MissingDot_Email()
        {
            Validate_Email("Tester@Testtest");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MissingSender_Email()
        {
            Validate_Email("@Test.test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MissingDomain_Email()
        {
            Validate_Email("Tester@.test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MissingTopDomain_Email()
        {
            Validate_Email("Tester@Test.");
        }
        #endregion

        [TestMethod]
        public void GetUser_ByEmail()
        {
            User Testuser = new User();
            Assert.IsNull(Testuser.Name);
            Assert.IsNull(Testuser.Email);
            Assert.IsNull(Testuser.Address);
            Assert.IsNull(Testuser.PostCode);
            Assert.IsNull(Testuser.Phone);

            Testuser.Email = "GetTester@Test.test";
            Testuser = Testuser.GetUser_ByEmail();

            Assert.IsNotNull(Testuser.Id);
            Assert.AreEqual("GetTester", Testuser.Name);
            Assert.AreEqual("GetTester@Test.test", Testuser.Email);
            Assert.AreEqual("Testvej 1", Testuser.Address);
            Assert.AreEqual("1234", Testuser.PostCode);
            Assert.AreEqual("11223344", Testuser.Phone);
        }

        [TestMethod]
        public void Save_NewStudent()
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime Time = DateTime.Parse(dateTime);

            Student Testuser = new Student();
            Assert.IsNull(Testuser.Name);
            Assert.IsNull(Testuser.Email);
            Assert.IsNull(Testuser.Address);
            Assert.IsNull(Testuser.PostCode);
            Assert.IsNull(Testuser.Phone);
            Assert.IsNotNull(Testuser.Education_StartDate);
            Assert.IsNotNull(Testuser.Education_EndDate);

            Testuser.Name = "Tester";
            Testuser.Email = "Tester@Test.test";
            Testuser.Address = "Testvej 1";
            Testuser.PostCode = "1234";
            Testuser.Phone = "11223344";
            Testuser.Education_StartDate = Time;
            Testuser.Education_EndDate = Time;

            Assert.IsNull(Testuser.Id);
            Assert.AreEqual("Tester", Testuser.Name);
            Assert.AreEqual("Tester@Test.test", Testuser.Email);
            Assert.AreEqual("Testvej 1", Testuser.Address);
            Assert.AreEqual("1234", Testuser.PostCode);
            Assert.AreEqual("11223344", Testuser.Phone);
            Assert.AreEqual(Time, Testuser.Education_StartDate);
            Assert.AreEqual(Time, Testuser.Education_EndDate);

            bool Test = Testuser.Save_User();
            Assert.IsTrue(Test);
            Testuser = (Student)Testuser.GetUser_ByEmail();

            Assert.IsNotNull(Testuser.Id);
            Assert.AreEqual("Tester", Testuser.Name);
            Assert.AreEqual("Tester@Test.test", Testuser.Email);
            Assert.AreEqual("Testvej 1", Testuser.Address);
            Assert.AreEqual("1234", Testuser.PostCode);
            Assert.AreEqual("11223344", Testuser.Phone);
            Assert.AreEqual(Time, Testuser.Education_StartDate);
            Assert.AreEqual(Time, Testuser.Education_EndDate);
        }

        [TestMethod]
        public void Save_NewTeacher()
        {
            System.DateTime Time = System.DateTime.Now;

            Teacher Testuser = new Teacher();
            Assert.IsNull(Testuser.Name);
            Assert.IsNull(Testuser.Email);
            Assert.IsNull(Testuser.Address);
            Assert.IsNull(Testuser.PostCode);
            Assert.IsNull(Testuser.Phone);
            Assert.AreEqual(0,Testuser.Payrole);

            Testuser.Name = "Tester";
            Testuser.Email = "Tester@Test.test";
            Testuser.Address = "Testvej 1";
            Testuser.PostCode = "1234";
            Testuser.Phone = "11223344";
            Testuser.Payrole = 1000;

            Assert.IsNull(Testuser.Id);
            Assert.AreEqual("Tester", Testuser.Name);
            Assert.AreEqual("Tester@Test.test", Testuser.Email);
            Assert.AreEqual("Testvej 1", Testuser.Address);
            Assert.AreEqual("1234", Testuser.PostCode);
            Assert.AreEqual("11223344", Testuser.Phone);
            Assert.AreEqual(1000, Testuser.Payrole);

            bool Test = Testuser.Save_User();
            Assert.IsTrue(Test);
            Testuser = (Teacher)Testuser.GetUser_ByEmail();

            Assert.IsNotNull(Testuser.Id);
            Assert.AreEqual("Tester", Testuser.Name);
            Assert.AreEqual("Tester@Test.test", Testuser.Email);
            Assert.AreEqual("Testvej 1", Testuser.Address);
            Assert.AreEqual("1234", Testuser.PostCode);
            Assert.AreEqual("11223344", Testuser.Phone);
            Assert.AreEqual(1000, Testuser.Payrole);

            bool test = Testuser.Delete_User(); //For clean up porpose in sql
            Assert.IsTrue(test);      
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: User does not exists")]
        public void Delete_User()
        {
            User Testuser = new User();
            Assert.IsNull(Testuser.Name);
            Assert.IsNull(Testuser.Email);
            Assert.IsNull(Testuser.Address);
            Assert.IsNull(Testuser.PostCode);
            Assert.IsNull(Testuser.Phone);

            Testuser.Email = "Tester@Test.test";
            Testuser.GetUser_ByEmail();

            Assert.IsNotNull(Testuser.Id);
            Assert.AreEqual("Tester", Testuser.Name);
            Assert.AreEqual("Tester@Test.test", Testuser.Email);
            Assert.AreEqual("Testvej 1", Testuser.Address);
            Assert.AreEqual("1234", Testuser.PostCode);
            Assert.AreEqual("11223344", Testuser.Phone);

            bool test = Testuser.Delete_User();
            Assert.IsTrue(test);
            Testuser.GetUser_ByEmail();
        }

    }
}
