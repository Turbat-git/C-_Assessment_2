
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    internal class RecruitmentSystem
    {
        public List<Contractor> contractors { get; set; } = new List<Contractor>()
        {
            new Contractor("v123", "David", "Juan", new DateOnly(2000,6,20), 55),
            new Contractor("v234", "Iano", "Con", new DateOnly(2001,7,12), 29),
            new Contractor("v345", "John", "Mathew", new DateOnly(1996,10,25), 26.9),
            new Contractor("v456", "Ingrid", "Less", new DateOnly(1985, 3,24), 24),
            new Contractor("v567", "Tanmay", "Singha", new DateOnly(1978, 9,17), 25.5),
        };

        public List<Job> jobs { get; set; } = new List<Job>()
        {
            new Job("Tanmay Job", new DateOnly(2024, 10, 24), 260, "J16"),
            new Job("Chris Job", new DateOnly(2022, 1, 24), 360, "J20"),
            new Job("Adrian Job", new DateOnly(2023, 5, 8), 307, "J15"),
            new Job("Kris Job", new DateOnly(2024, 2, 16), 306, "J18")
        };
        public List<Job> unassignedjobs { get; set; } = new List<Job>();
        public List<Job> filteredjobs { get; set; } = new List<Job>();
        public List<Job> completedjobs { get; set; } = new List<Job>();

        public void AddContractor(Contractor contractor)
        {
            if (ContractorExist(contractor))
            {
                MessageBox.Show("The Contractor already exists");
                return;
            }
            contractors.Add(contractor);

        }

        public void AddContractorToJob(Contractor contractor, Job job)
        {
            if (AssignedContractorExists(contractor, job))
            {
                MessageBox.Show("The Contractor is already assigned to this Job");
                return;
            }
            job.AddContractor(contractor);
            
        }
        public bool AssignedContractorExists(Contractor contractor, Job job)
        {
            foreach (Contractor existingcontractor in job.AssignedContractors)
            {
                if (existingcontractor.ContractorID == contractor.ContractorID) return true;
            }
            return false;
        }

        // This method is checking that the student is already there on the list or not.
        public bool ContractorExist(Contractor contractor)
        {
            foreach (Contractor existingcontractor in contractors)
            {
                if (existingcontractor.ContractorID == contractor.ContractorID) return true;
            }
            return false;
        }


        public void RemoveContractor(Contractor contractor)
        {
            contractors.Remove(contractor);

        }

        public void AddJob(Job job)
        {
            if (JobExist(job)) 
            {
                MessageBox.Show("The Job already exists");
                return; 
            }

            jobs.Add(job);
        }

        public bool JobExist(Job job)
        {
            foreach (Job existingjob in jobs)
            {
                if (existingjob.JobID == job.JobID) return true;
            }
            return false;
        }

        public void RemoveJob(Job job)
        {
            jobs.Remove(job);
        }

        public void AssignToContractor(Contractor contractor)
        {
            contractors.Add(contractor);
        }

        public void CompleteJob(Job job)
        {
            for (int i = job.AssignedContractors.Count - 1; i >= 0; --i)
            {
                AddContractor(job.AssignedContractors[i]);
                job.RemoveAssignedContractor(job.AssignedContractors[i]);
            }
        }
    }
}
