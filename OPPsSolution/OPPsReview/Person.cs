using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPsReview
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ResidentAddress Address { get; set; }
        public List<Employment> EmploymentPositions { get; set; }

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
            FirstName = firstname.Trim();
            LastName = lastname.Trim();
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
