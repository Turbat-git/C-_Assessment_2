using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1;

namespace UnitTest

{
    [TestClass]
    public class UnitTest1
    {
        RecruitmentSystem recruitsystem;
        Contractor contractor;

        [TestInitialize]
        public void Setup()
        {
            recruitsystem = new RecruitmentSystem();
        }

        [TestMethod]
        public void TestAddContractor()
        {
            Contractor ct = new Contractor("2", "Bob", "Ross", new DateOnly(2024, 11, 22), 25.3);
            recruitsystem.AddContractor(ct);
            // Arrange
            int expectedCount = 6;

            // Act
            int actualCount = recruitsystem.contractors.Count();

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRemoveContractor()
        {
            Contractor ct = new Contractor("2", "Bob", "Ross", new DateOnly(2024, 11, 22), 25.3);
            recruitsystem.AddContractor(ct);

            recruitsystem.RemoveContractor(ct);

            // Arrange
            int expectedCount = 5;

            // Act
            int actualCount = recruitsystem.contractors.Count();

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestAddJob()
        {
            Job jt = new Job("Gas Station's Job", new DateOnly(2024, 11, 22), 25000, "2");
            recruitsystem.AddJob(jt);

            // Arrange
            int expectedCount = 5;

            // Act
            int actualCount = recruitsystem.jobs.Count();

            // Assert
            Assert.AreEqual (expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRemoveJob() 
        {
            Job jt = new Job("Gas Station's Job", new DateOnly(2024, 11, 22), 25000, "2");
            recruitsystem.AddJob(jt);

            recruitsystem.RemoveJob(jt);

            // Arrange 
            int expectedCount = 4;

            // Act
            int actualCount = recruitsystem.jobs.Count();

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestAssignContractor()
        {
            Contractor ct = new Contractor("2", "Bob", "Ross", new DateOnly(2024, 11, 22), 25.3);
            Job jt = new Job("Gas Station's Job", new DateOnly(2024, 11, 22), 25000, "2");
            recruitsystem.AddContractor(ct);
            recruitsystem.AddJob(jt);

            recruitsystem.AddContractorToJob(ct, jt);
            recruitsystem.RemoveContractor(ct);

            // Arrange
            int expectedCountContractor = 5;
            int expectedCountJob = 5;
            int expectedCountAssigned = 1;

            // Act
            int actualCountContractor = recruitsystem.contractors.Count();
            int actualCountJob = recruitsystem.jobs.Count();
            int actualCountAssigned = jt.AssignedContractors.Count();

            // Assert
            Assert.AreEqual(expectedCountJob, actualCountJob);
            Assert.AreEqual(expectedCountContractor, actualCountContractor);
            Assert.AreEqual(expectedCountAssigned, actualCountAssigned);
        }

        [TestMethod]
        public void TestCompleteJob()
        {
            Contractor ct = new Contractor("2", "Bob", "Ross", new DateOnly(2024, 11, 22), 25.3);
            Job jt = new Job("Gas Station's Job", new DateOnly(2024, 11, 22), 25000, "2");
            recruitsystem.AddContractor(ct);
            recruitsystem.AddJob(jt);

            recruitsystem.AddContractorToJob(ct, jt);
            recruitsystem.RemoveContractor(ct);

            recruitsystem.CompleteJob(jt);
            recruitsystem.jobs.Remove(jt);
            recruitsystem.completedjobs.Add(jt);

            // Arrange
            int expectedCountJob = 4;
            int expectedCountCompletedJob = 1;
            int expectedCountContractors = 6;

            // Act

            int actualCountJob = recruitsystem.jobs.Count();
            int actualCountCompletedJob = recruitsystem.completedjobs.Count();
            int actualCountContractors = recruitsystem.contractors.Count();

            // Assert

            Assert.AreEqual(expectedCountJob, actualCountJob);
            Assert.AreEqual(expectedCountCompletedJob, actualCountCompletedJob);
            Assert.AreEqual(expectedCountContractors, actualCountContractors);

        }

        [TestMethod]
        public void TestContractor()
        {
            // Arrange
            int expectedResult = 5;

            // Act
            int actualResult = recruitsystem.contractors.Count();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetAllJobs()
        {
            RecruitmentSystem recruitsystem  = new RecruitmentSystem();
            List<Job> allJobs = recruitsystem.jobs;

            // Arrange 

            int expectedCount = 4;

            // Act

            int actualCount = allJobs.Count();

            // Assert

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestJob()
        {
            // Arrange
            int expectedResult = 4;

            // Act
            int actualResult = recruitsystem.jobs.Count;

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestContractorExist()
        {
            Contractor ct = new Contractor("v123", "David", "Juan", new DateOnly(2000, 6, 20), 55);

            // Arrange
            bool expectedResult = true;
            
            // Act
            bool actualResult = recruitsystem.ContractorExist(ct);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
    
        }


    }
}