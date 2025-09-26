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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("FirstName", "First name cannot be missing or blank.");
                _FirstName = value.Trim();
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("LastName", "Last name cannot be missing or blank.");
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
            //the name validation is not required in the constructor as it is 
            //      also in the property and the constructor sends the incoming
            //      parameter value to the property and NOT directly into the data member
            //if (string.IsNullOrWhiteSpace(firstname))
            //    throw new ArgumentNullException("FirstName", "First name cannot be missing or blank.");
            //if (string.IsNullOrWhiteSpace(lastname))
            //    throw new ArgumentNullException("LastName", "Last name cannot be missing or blank.");
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

        public void ChangeFullName(string firstname, string lastname)
        {
            //using best practises, the incoming data will be stored
            //  via the properties
            FirstName = firstname;
            LastName = lastname;
        }

        public void AddEmployment(Employment employment)
        {
            if (employment == null)
                throw new ArgumentNullException("Employment","Employment missing data. Cannot add employment.");

            //one could code a loop to examine each item in the collection to determine if there
            //  is a duplicate history instance
            //However, lets used methods that have already been built to do searching of a collection
            //First step: determine if you need a copy of the instance
            //  in this case: only the knowledge that an instance exist is needed
            //  (do not actual need the instance)
            //  condition: needs to know at least one exists: .Any()

            //within the method one can place one or more delegates (conditions) that
            //  determine if the action is true or false
            //delegate syntax structure:
            //      collectionplaceholderlabel => collectionplaceholderlabel[.property] [condition] value 
            //                  [ && or || another condition ...]
            //typically the collectionplaceholderlabel is very short such x
            //the collectionplaceholderlabel represents any instance in your collection at any time
            if (EmploymentPositions.Any(x => x.Title == employment.Title
                                        && x.StartDate.Equals(employment.StartDate)))
                throw new ArgumentException($"Duplicate Emloyment: {employment.Title} on {employment.StartDate}","Employment");

            EmploymentPositions.Add(employment);
        }
    }
}
