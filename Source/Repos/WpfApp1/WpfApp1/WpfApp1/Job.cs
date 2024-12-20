﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Job
    {
        /// <summary>
        /// Lecture is a class which has 4 properties.
        /// </summary>
        public string JobTitle { get; set; }
        public DateOnly JobDate { get; set; }
        public double JobCost { get; set; }
        public string JobID { get; set; }
        public bool Status { get; set; }

        // create a list of contractors who would be assigned to specific jobs
        public List<Contractor> AssignedContractors { get; set; } = new List<Contractor>();

        // This is a Job constructor
        public Job(string jobtitle, DateOnly jobdate, double jobcost, string jobid, bool status = false)
        {
            // It is checking the not null entry of the JobTitle and JobID
            JobTitle = jobtitle ?? throw new ArgumentNullException(nameof(jobtitle));
            JobDate = jobdate;
            JobCost = jobcost;
            JobID = jobid ?? throw new ArgumentNullException(nameof(jobid));
            Status = status;
        }

        public bool AddContractor(Contractor contractor)
        {
            //TODO: Need to validate the contractor is already added into the list or not.
            AssignedContractors.Add(contractor);
            return true;
        }

        // Method to remove a contractor from the assigned contractor list
        public void RemoveAssignedContractor(Contractor contractor)
        {
            AssignedContractors.Remove(contractor);
        }

        public override string? ToString()
        {
            // return base.ToString();
            return $"[{JobID}] [Cost: ${JobCost}] [Started at: {JobDate}] {JobTitle}.";
        }
    }
}
