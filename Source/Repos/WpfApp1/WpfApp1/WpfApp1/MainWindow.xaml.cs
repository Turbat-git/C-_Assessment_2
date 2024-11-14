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
            FilterJobListx.ItemsSource = null;
            FilterJobListx.ItemsSource = recruitsystem.filteredjobs;
        }

        private void AddContractorClick(object sender, RoutedEventArgs e)
        {
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

        private void RemoveContractorBTN_Click(object sender, RoutedEventArgs e)
        {
            Contractor selectedcontractor = ContractorListx.SelectedItem as Contractor;
            if (selectedcontractor != null)
            {
                recruitsystem.RemoveContractor(selectedcontractor);
                ContractorListx.Items.Refresh();
            }
        }

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
                Job newjob = new Job(JobTitlex.Text, DateOnly.FromDateTime(JobDatex.SelectedDate ?? DateTime.Now), Convert.ToDouble(JobCostx.Text), JobIdx.Text, );
                recruitsystem.AddJob(newjob);
                
                //To refresh the Job list in the application
                GetJobs();
            }
        }

        private void RemoveJobx_Click(object sender, RoutedEventArgs e)
        {
            Job selectedjob = JobListx.SelectedItem as Job;
            if (selectedjob != null)
            {
                recruitsystem.RemoveJob(selectedjob);
                JobListx.Items.Refresh();
            }
        }

        private void AssignContractorx_Click(object sender, RoutedEventArgs e)
        {
            Contractor ct = ContractorListx.SelectedItem as Contractor;
            Job jt = JobListx.SelectedItem as Job;

            if (ct != null && jt != null)
            {
                recruitsystem.AddContractorToJob(ct, jt);
                recruitsystem.RemoveContractor(ct);
                ContractorListx.Items.Refresh();
                GetAssignedContractors(jt);
            }
        }

        private void CompleteJobx_Click(object sender, RoutedEventArgs e)
        {
            Contractor selectedcontractor = AssignedListx.SelectedItem as Contractor;
            Job selectedjob = JobListx.SelectedItem as Job;

            if (selectedjob != null && selectedcontractor != null)
            {
                //Remove the contractor from assigned list
                selectedjob.RemoveAssignedContractor(selectedcontractor);

                //Add the Contractor to the pool
                recruitsystem.AssignToContractor    (selectedcontractor);

                //Refreshing the lists
                GetAssignedContractors(selectedjob);
                GetContractors();
            }
        }

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
    }
}