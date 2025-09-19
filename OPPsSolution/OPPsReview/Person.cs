using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPsReview
{
    public class Person
    {
        private string _FirstName;
        private string _LastName;
        public string FirstName 
        { 
            get { return _FirstName; } 
            set
            {
                _FirstName = value.Trim();
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value.Trim();
            }
        }
        public ResidentAddress Address { get; set; }

        //consider making EmploymentPositions private set (must use method)
        //  do we wish to allow the entire employment collection to be replaced?
        //  consider, is the mutator set to private?
        //      if so, direct altering is not possible (access trouble)
        //      if private, any code to actually attempt to use the mutator (set) will NOT even compile
  
        public List<Employment> EmploymentPositions { get; private set; }

        public string FullName
        {
            get { return LastName + ", " + FirstName; }
            //get { return $"{LastName}, {FirstName}"; }
        }

        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
            //the default of a class is null
            //Lits<T> is a class
            //If I want a count of items in a class, one NEEDS an instance of the class
            EmploymentPositions = new List<Employment>();
        }

        public Person(string firstname, string lastname, ResidentAddress address, 
                        List<Employment> employmentpositions)
        {
            if (string.IsNullOrWhiteSpace(firstname))
                throw new ArgumentNullException("FirstName", "First name cannot be missing or blank.");
            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentNullException("LastName", "Last name cannot be missing or blank.");
            FirstName = firstname; // the Trim() can be Refatored out of the constructor as it is in the Property
            LastName = lastname; //.Trim();
            Address = address;
            if (employmentpositions == null)
            {
                EmploymentPositions = new List<Employment>();
            }
            else
            {
                EmploymentPositions = employmentpositions;
            }
        }
    }
}
