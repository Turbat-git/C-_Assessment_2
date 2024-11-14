using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RecruitmentSystem recruitsystem = new RecruitmentSystem();
        public MainWindow()
        {
            InitializeComponent();
            GetContractors();
            GetJobs();
            DatePickerDate.DisplayDateStart = DateTime.Today;
            JobDatex.DisplayDateStart = DateTime.Today;
        }

        public void GetContractors()
        {
            ContractorListx.ItemsSource = null;
            ContractorListx.ItemsSource = recruitsystem.contractors;
        }

        public void GetJobs()
        {
            JobListx.ItemsSource = null;
            JobListx.ItemsSource = recruitsystem.jobs;
        }

        private void GetAssignedContractors(Job job)
        {
            AssignedListx.ItemsSource = null;
            AssignedListx.ItemsSource = job.AssignedContractors;
        }

        private void GetFilteredJob()
        {
            JobListx.ItemsSource = null;
            JobListx.ItemsSource = recruitsystem.filteredjobs;
        }
        private void GetCompletedJobs()
        {
            CompletedJobListx.ItemsSource = null;
            CompletedJobListx.ItemsSource = recruitsystem.completedjobs;
        }

        //Adding contractors to contractor list
        private void AddContractorClick(object sender, RoutedEventArgs e)
        {   
            //If else to check for exceptions
            if(ContractorFNamex.Text == string.Empty || ContractorLNamex.Text == string.Empty || ContractorIdx.Text == string.Empty || double.TryParse(ContractorHWagex.Text, out double result) != true) 
            { 
                MessageBox.Show("Please enter correct values to the Textbox. (The Wage should be a number and every other textbox except Date Picker should not be empty.");
                GetContractors();
            }
            else
            {
                Contractor newcontractor = new Contractor(ContractorIdx.Text, ContractorFNamex.Text, ContractorLNamex.Text, DateOnly.FromDateTime(DatePickerDate.SelectedDate ?? DateTime.Now), Convert.ToDouble(ContractorHWagex.Text));
                recruitsystem.AddContractor(newcontractor);
                GetContractors();
            }
        }

        //Removing contractors from the contractor list
        private void RemoveContractorBTN_Click(object sender, RoutedEventArgs e)
        {
            Contractor selectedcontractor = ContractorListx.SelectedItem as Contractor;
            if (selectedcontractor != null)
            {
                recruitsystem.RemoveContractor(selectedcontractor);
                ContractorListx.Items.Refresh();
            }
        }

        //Adding jobs to joblist
        private void AddJobx_Click(object sender, RoutedEventArgs e)
        {
            // If else to give error if there is any
            if(JobTitlex.Text == string.Empty || double.TryParse(JobCostx.Text, out double result) != true || JobIdx.Text == string.Empty) 
            {
                MessageBox.Show("Please enter correct values to the Textbox. (The Cost should be a number and every other textbox except Date Picker should not be empty.");
                GetJobs();            
            }
            else
            {
                Job newjob = new Job(JobTitlex.Text, DateOnly.FromDateTime(JobDatex.SelectedDate ?? DateTime.Now), Convert.ToDouble(JobCostx.Text), JobIdx.Text);
                recruitsystem.AddJob(newjob);
                
                //To refresh the Job list in the application
                GetJobs();
            }
        }

        //Removing jobs from the joblist
        private void RemoveJobx_Click(object sender, RoutedEventArgs e)
        {
            Job selectedjob = JobListx.SelectedItem as Job;
            if (selectedjob != null)
            {
                recruitsystem.RemoveJob(selectedjob);
                JobListx.Items.Refresh();
            }
        }

        //Assigning contractors to a job
        private void AssignContractorx_Click(object sender, RoutedEventArgs e)
        {
            Contractor ct = ContractorListx.SelectedItem as Contractor;
            Job jt = JobListx.SelectedItem as Job;

            if (ct != null && jt != null && jt.Status != true)
            {
                recruitsystem.AddContractorToJob(ct, jt);
                recruitsystem.RemoveContractor(ct);
                ContractorListx.Items.Refresh();
                GetAssignedContractors(jt);
            }
        }

        //Completing a job and removing the said job from joblist before adding it to completedjoblist
        private void CompleteJobx_Click(object sender, RoutedEventArgs e)
        {
            Job selectedjob = JobListx.SelectedItem as Job;

            if (selectedjob != null)
            {
                // Change the boolean value of the job to true
                selectedjob.Status = true;
                recruitsystem.CompleteJob(selectedjob);

                //Refreshing the lists
                GetAssignedContractors(selectedjob);
                GetContractors();

                //Removing the Job from the Joblist and adding it to CompletedJobList
                recruitsystem.jobs.Remove(selectedjob);
                recruitsystem.completedjobs.Add(selectedjob);

                //Refreshing the lists
                GetJobs();
                GetCompletedJobs();
            }
        }

        //Event to fill out textboxes from the selectedcontractor
        private void ContractorListx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contractor? selectedcontractor = ContractorListx.SelectedItem as Contractor;

            if (selectedcontractor != null)
            {
                ContractorFNamex.Text = selectedcontractor.CFirstName.ToString();
                ContractorLNamex.Text = selectedcontractor.CLastName.ToString();
                ContractorHWagex.Text = selectedcontractor.CHourlyWage.ToString();
                ContractorIdx.Text = selectedcontractor.ContractorID.ToString();
            }
            else
            {
                return;
            }
        }

        //Get Assigned contractors for the chosen job
        private void JobListx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Job selectedjob = JobListx.SelectedItem as Job;

            if (selectedjob != null)
            {
                GetAssignedContractors(selectedjob);
            }
        }

        //Event to refresh joblist
        private void GetAllJobx_Click(object sender, RoutedEventArgs e)
        {
            GetJobs();
        }

        //Get all the unassigned jobs and displays it on JobListx
        private void GetUnassignedJobxx_Click(object sender, RoutedEventArgs e)
        {
            GetJobs();

            //Use clear to not add previous Jobs into the Joblist
            recruitsystem.unassignedjobs.Clear();

            //Iteration to go through joblist and check for jobs with 0 AssignedContractors
            foreach (Job existingjob in recruitsystem.jobs)
            {
                if (existingjob != null && existingjob.AssignedContractors.Count == 0)
                {   
                    recruitsystem.unassignedjobs.Add(existingjob);
                }
            }
            //Update Joblist to only display unassigned jobs
            JobListx.ItemsSource = recruitsystem.unassignedjobs;

        }

        //Event to display all assigned jobs in joblist
        private void GetAssignedJobx_Click_1(object sender, RoutedEventArgs e)
        {
            GetJobs();

            //Use clear to not add previous Jobs into the Joblist
            recruitsystem.unassignedjobs.Clear();

            //Iteration to go through joblist and check for jobs with AssignedContractors
            foreach (Job existingjob in recruitsystem.jobs)
            {
                if (existingjob != null && existingjob.AssignedContractors.Count != 0)
                {
                    recruitsystem.unassignedjobs.Add(existingjob);
                }
            }
            //Update Joblist to only display unassigned jobs
            JobListx.ItemsSource = recruitsystem.unassignedjobs;
        }

        //Event to apply the minimum and maximum cost filter to query
        private void FilterButtonx_Click(object sender, RoutedEventArgs e)
        {
            GetFilteredJob();

            recruitsystem.filteredjobs.Clear();

            if(FilterMinx.Text == string.Empty || FilterMax.Text == string.Empty 
                || double.TryParse(FilterMinx.Text, out double result) != true || double.TryParse(FilterMax.Text, out double result2) != true)
            {
                MessageBox.Show("Please enter values before searching");
                return;
            }
            else
            {
                foreach (Job existingjob in recruitsystem.jobs)
                {
                    if(Convert.ToDouble(FilterMinx.Text) < existingjob.JobCost && existingjob.JobCost < Convert.ToDouble(FilterMax.Text))
                    {
                        recruitsystem.filteredjobs.Add(existingjob);
                    }
                }
                GetFilteredJob();
            }
        }

        //Disallowing users from choosing past dates from DatePickerDate
        private void DatePickerDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = DatePickerDate.SelectedDate ?? DateTime.Today;

            if (selectedDate < DateTime.Today)
            {
                MessageBox.Show("Please don't choose date from the past");
                DatePickerDate.SelectedDate = DateTime.Today;
            }
            else 
            {
                return;
            }
        }

        //Disallowing users from choosing past dates from JobDatex
        private void JobDatex_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = JobDatex.SelectedDate ?? DateTime.Today;

            if (selectedDate < DateTime.Today)
            {
                MessageBox.Show("Please don't choose date from the past");
                JobDatex.SelectedDate = DateTime.Today;
            }
            else
            {
                return;
            }
        }
    }
}