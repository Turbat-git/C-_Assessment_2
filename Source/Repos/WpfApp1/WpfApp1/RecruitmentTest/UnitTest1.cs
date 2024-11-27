using NUnit.Framework;
using System.Diagnostics.Contracts;

namespace RecruitmentTest
{
    [TestClass]
    public class Test
    {
        ContractorManagementSystem classsystem = new ContractorManagementSystem();


        [TestMethod]
        public void TestContractor()
        {
            // Arrange
            int expected_result = 3;

            // Act
            int actual_result = classsystem.contractors.Count;

            // Assert
            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestJob()
        {
            Job jb = new Job("19", "Test", 260, new DateOnly(2007, 1, 13));
            classsystem.AddJob(jb);

            int expected_result = 4;

            int actual_result = classsystem.jobs.Count;

            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestAddContractor()
        {
            Contractor cn = new Contractor(26, "Tom", "Cruise", 500, new DateOnly(1999, 12, 26));
            classsystem.AddContractor(cn);
            int expected_result = 4; //Test Data

            int actual_result = classsystem.contractors.Count;

            Assert.AreEqual(expected_result, actual_result);

        }

        [TestMethod]
        public void TestRemoveContractor()
        {
            Contractor cn = new Contractor(26, "Tom", "Cruise", 500, new DateOnly(1999, 12, 26));
            classsystem.RemoveContractor(cn);
            int expected_result = 3;

            int actual_result = classsystem.contractors.Count;

            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestContractorExist()
        {
            Contractor cn = new Contractor(124, "Ray", "White", 600, new DateOnly(2005, 10, 15));

            Assert.IsTrue(classsystem.ContractorExist(cn));
        }

        [TestMethod]
        public void TestAddJob()
        {
            Job jb = new Job("112", "IT Support", 100, new DateOnly(2021, 10, 15));
            classsystem.AddJob(jb);

            int expected_result = 4;

            int actual_result = classsystem.jobs.Count;

            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestRemoveJob()
        {
            Job jb = new Job("112", "IT Support", 100, new DateOnly(2021, 10, 15));
            classsystem.RemoveJob(jb);

            int expected_result = 3;

            int actual_result = classsystem.contractors.Count;

            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestAssignJob()
        {
            Job job = new Job("13", "Paint Job", 335, new DateOnly(2024, 1, 25));
            Contractor contractor = new Contractor(124, "Ray", "White", 600, new DateOnly(2005, 10, 15));

            job.AddContractor(contractor);

            int expected_result = 1;
            int actual_result = job.DoneJob.Count;

            Assert.AreEqual(expected_result, actual_result);

            Assert.IsTrue(job.DoneJob.Contains(contractor));
        }

        [TestMethod]
        public void TestCompleteJob()
        {
            Job job = new Job("13", "Paint Job", 335, new DateOnly(2024, 1, 25));
            Contractor contractor = new Contractor(124, "Ray", "White", 600, new DateOnly(2005, 10, 15));

            job.AddContractor(contractor);

            job.RemoveContractor(contractor);

            int expected_result = 0;
            int actual_result = job.DoneJob.Count;

            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestFindAllJobs()
        {
            ContractorManagementSystem system = new ContractorManagementSystem();

            List<Job> allJobs = system.jobs;

            int expected_result = 3;
            int actual_result = allJobs.Count;

            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void TestDatePickerJob()
        {
            // Arrange
            Job job = new Job("11", "Covid Nurse", 260, new DateOnly(2020, 3, 1));
            DateTime pastDate = DateTime.Now.AddDays(-1); // Yesterday

            // Act
            bool actual_result = classsystem.DatePickerJob(pastDate);

            // Assert
            Assert.IsFalse(actual_result);
        }


    }
}