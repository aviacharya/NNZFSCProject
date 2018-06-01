using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNZFSC.Repository;
using NNZFSC.Models;


namespace NNZFSC.Tests.RegisterMemberTest
{
    /// <summary>
    /// Summary description for MemberTest
    /// </summary>
    [TestClass]
    public class MemberTest
    {
        RegisterMember obj = new RegisterMember();
        public MemberTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        
        public void CreateMember()
        {
            try
            {
                MemberRegistration member = new MemberRegistration();
                member.MemberFirstName = "Rohan";
                member.MemberLastName = "Rai";
                member.MemberAddress = "New Road, ktm";
                member.EmailAddress = "rohan@gmail.com";
                member.MembershipAmount = 20;
                member.MembershipDate = new DateTime(2018, 06, 1);
                member.MembershipExpiryDate = new DateTime(2019, 06, 1);
                member.CreateBy = "Avi";
                int Id = obj.InsertMember(member);
                Assert.AreEqual(1, Id);
            }

            catch(Exception e)
            {
                throw e;
            }
           
        }

        [TestMethod]
        public void UpdateMember()
        {

            try
            {

                var member = obj.GetMemberById(1);
                string OriginalLastName = member.MemberLastName;
                member.MemberLastName = "Rai";
                member.MembershipAmount = 30;
                obj.UpdateMember(member);
              
            }

            catch(Exception e)
            {
                throw e;

            }
        }
    }
}
