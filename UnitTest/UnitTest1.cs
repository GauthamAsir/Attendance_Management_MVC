using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BusinessLayer;
using System.Transactions;
using DataLayer;
using System.Collections.Generic;

namespace UnitTest
{
    public class IntegrationTestBase
    {
        private TransactionScope scope;

        [TestInitialize]
        public void Initialize()
        {
            this.scope = new TransactionScope();
        }
        [TestCleanup]
        public void TestCleanup()
        {
            this.scope.Dispose();
        }
    }

    [TestClass]
    public class UnitTest1
    {
        private Business Business = new Business();

        /// <summary>
        /// GetAllProjectsIdName()
        /// </summary>
        [TestMethod]
        public async void TestGetAllProjectsIdName()
        {
            //List<ProjectDetail> projects = await Business.GetAllProjectsIdName();

            //Assert.IsNotNull(projects);

        }
    }
}
