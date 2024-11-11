using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Contractor
    {
        /// <summary>
        /// Contractor class has 5 properties
        /// </summary>
        public string ContractorID { get; set; }
        public string CFirstName { get; set; }
        public string CLastName { get; set; }
        public DateOnly StartDate { get; set; } // DateOnly is a date data type
        public double CHourlyWage { get; set; }


        /// <summary>
        /// This is a constructor which initilizes all instance attrubutes
        /// </summary>
        /// <param name="ContractorID"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="studentDOB"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Contractor(string contractorID, string cfirstname, string clastname, DateOnly startdate, double chourlywage)
        {
            ContractorID = contractorID ?? throw new ArgumentNullException(nameof(contractorID));
            CFirstName = cfirstname ?? throw new ArgumentNullException(nameof(cfirstname));
            CLastName = clastname ?? throw new ArgumentNullException(nameof(clastname));
            StartDate = startdate;
            CHourlyWage = chourlywage;
        }

        // This method is overriding the ToString  method of base class.
        public override string? ToString() // ? accept empty string 
        {
            //return base.ToString();
            return $"[{ContractorID}] {CFirstName} {CLastName}"; // This is a custom way to display 
        }
    }
}
