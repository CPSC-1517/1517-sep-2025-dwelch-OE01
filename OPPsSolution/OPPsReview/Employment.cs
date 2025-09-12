using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPPsReview
{
    public class Employment
    {
        //data members
        //aka fields, variables, attributes
        //typically data members are private and hold data for use
        //  within your class
        //usually associated with a property
        //a data member does not have any built-in validation
        private string _Title;
        private double _Years;

        //properties
        //are associated with a single piece of data.
        //Properties can be implemented by:
        //  a) fully implemented property
        //  b) auto implemented property

        //A property does not need to store data
        //  this type of property is referred to as a read-only
        //  this property typically uses existing data values
        //      within the instance to return a computed value 
        // NOTE there would be NO set for the property

        //Assume two data fields _FirstName and _LastName
        //public string FullName
        //{
        //    get { return _FastName + " " + _LastName; }
        //}

        //fully implemented properties usually has additional logic
        //  to execute for control over the data: such as validation
        //fully implemented properties will have a declared data
        //  member to store the data into

        //auto implemented properties do not have additional logic
        //Auto implemented properties do not have a declared
        //  data member instead the o/s will create on the property's
        //  behave a storage that is accessible ONLY by the property


        ///<summary>
        ///Property: Title
        ///datatype: string
        ///validation: there must be a character in the string
        ///a property will always have a getter (accessor)
        ///a property may or maynot have a setter (mutator)
        /// no mutator the property is consider "read-only" and is
        ///         usually returning a computed field
        /// has a mutator, the property will at some point save the data
        ///     to storage
        /// the mutator may be public (default) or private
        ///     public: accessible by outside users of the class
        ///     private: accessible ONLY within the class, usually
        ///                 via the constructor or a method
        /// !!!!! a property DOES NOT have ANY declared incoming parameters !!!!!!
        /// </summary>
        /// 

        //fully implemented property

        // string mytitle = myobject.Title;
        public string Title
        {
            get
            {
                //accessor (getter)
                //returns the string associated with this property
                return _Title;
            }

            set 
            {
                //mutator (setter)
                //it is within the set that the validation of the data
                //  is done to determine if the data is acceptable
                //if all processing of the string is done via the property
                //  it will ensure that good data is within the associated string
                if (string.IsNullOrWhiteSpace(value))
                {
                    //classes typically do not write to the console.
                    //classes will throw Exceptions that must be handled in a 
                    //  user friendly fashion by the outside user
                    throw new ArgumentNullException("Title", "Title cannot be empty or just blank"); 
                }
                else
                {
                    //it is a very good practice to remove leading and trailing spaces on strings
                    //  so that only the required and important characters are stored.
                    //to do this sanitization use .Trim()
                    _Title = value.Trim();
                }
            }
        }

        ///<summary>
        ///Property: Years
        ///validation: the value must be 0 or greater
        ///datatype: double
        ///</summary>
        ///

        public double Years
        {
            //notice that the get/return can physically be coded on the same line
            get { return _Years; }

            set
            {
               // if (value < 0)

                //replace the above code relation condition test with a static method
                //when using a static class, one does not instanated the class
                //  one uses the class name and the method
                if (!Utilities.IsZeroOrPositive(value))
                {
                    //in this error message example, the incorrect value is being include
                    //  within the error message itself
                    throw new ArgumentException($"The years of {value} is incorrect. Years must be 0 or greater");
                }
                else
                {
                    _Years = value;
                }
            }
        }

        ///<summary>
        ///Property: StartDate
        ///datatype: datetime
        ///validation: none
        ///set access: private
        ///</summary>
        ///

        //since the access to this property for the mutator is private ANY validation
        //  for this data will need to be done elsewhere
        //possible locations for the validation could be in
        //  a) a constructor
        //  b) any method that will alter the data
        //a private mutator will NOT allow alteration of the data via a property for the
        //  outside user, however, methods within the class will still be able to
        //  use the property

        //NOTE: the use of a private set is ONLY as an example of using a private set
        //      this property could easily have has an access level of public

        //this property can be coded as an auto-implemented property
        //the private is independent of the auto-implemented property
        
        public DateTime StartDate { get; private set; }

        ///<summary>
        ///Property: Level
        ///validation: none
        ///datatype: this is an enum (SupervisoryLevel)
        ///</summary>

        //coded as an auto implemented property

        public SupervisoryLevel Level { get; set; }

        //can an auto-implemented be coded as a fully implemented
        //private SupervisoryLevel _Level;
        //public SupervisoryLevel Level
        //{
        //    get { return _Level; }
        //    set { _Level = value; }
        //}



        //constructors

        //your class does not technically need a coded constructor
        //if you code a constructor for your class you are responsible for coding ALL constructors
        //if you do not code a constructor then the system will assign the software datatype defaults
        //  to your variables (data members/auto-implemented properties)

        //syntax: accesslevel constructorname([list of parameters]) { .... }
        //NOTE: NO return datatype
        //      the constructorname MUST be the class name

        //Default
        //simulates the "system defaults"

        public Employment()
        {

            //if there is no code within this constructor, the actions for setting
            //  your internal fields will be using the system defaults for the datatype


            //optionally
            // you could assign values to your initial fields within this constructor typically
            //      using literal values
            //Why?
            // your internal fields may have validation attached to the data for the field
            // this validation is usually within the property
            // you would wish to have valid data values for your internal fields

            //What is the internal default value for a string? :null
            //Is null a valid value for the data member _Title? NO
            //Is there a conflict? YES check the property validation. Null is invalid
            //  Therefore we MUST assign our own default value to _Title

            //it is a GOOD practice to referrence all class data member via their property
            //  thus ensuring any coded validation is executed when using the data member

            Title = "Unknown"; //this satisfies the property validation
            Level = SupervisoryLevel.TeamMember; //desired a different initial value
            StartDate = DateTime.Today; //system default is unaccept

            //Years?
            //the default is fine (0.0)
            //HOWEVER< IF YOU WISH you could actually assign the value 0 yourself
            Years = 0.0;

        }

        //Greedy
        //this is the constructor typically used to assign values to a instance at the time of
        //    creation
        //the list of parameters may or maynot contain default parameter values
        //if you have assigned default parameter values then those parameters MUST be at the end of
        //  the parameter list
        //in this example years is a default parameter (it has an assigned value if the value
        //  is not included on the coded constructor in the user program
        //using a call to a method with default parameter
        //     Employment myJob = new Employment("PGI", SupervisoryLevel.Entry,
        //                                          DateTime.Today);

        public Employment(string title, SupervisoryLevel level, 
                            DateTime startdate, double years = 0.0)
        {
            Title = title; //once again all access to the title data will be done via the property
            Level = level;

            //one could add validation, especially if the property has a private set  OR the property
            //  is an auto-implemented property that has restrictions
            //example
            //validation, start date must not exist in the future
            //validation can be done anywhere in your class
            //since the property is auto-implemented AND/OR has a private set,
            //      validation can be done  in the constructor OR a behaviour 
            //      that alters the property
            //IF the validation is done in the property, IT WOULD NOT be an
            //      auto-implemented property BUT a fully-implemented property
            // .Today has a time of 00:00:00 AM
            // .Now has a specific time of day 13:05:45 PM
            //by using the .Today.AddDays(1) you cover all times on a specific date
           
            //if (startdate >= DateTime.Today.AddDays(1))
            //{
            //    throw new ArgumentException($"The start date of {startdate} is invalid, date cannot be in the future");
            //}

            //replace the duplicate code in the constructor with the private method
            if (CheckDate(startdate))
                StartDate = startdate;

            if(years != 0.0)
            {
                //even if a negative value was enter and passed to the property
                //  the validation in the property will catch the error
                Years = years;
            }
            else
            {
                if (startdate == DateTime.Today)
                {
                    Years = 0.0;
                }
                else
                {
                    //calculate the actual years from startdate to today
                    TimeSpan timediff = DateTime.Today - startdate;
                    Years = Math.Round((timediff.Days / 365.2), 1);
                }
            }
        }


        //methods (aka behaviours) )

        public override string ToString()
        {
            //this string is known as a "comma separate value" string (csv)
            //concern: when the date is used, it could have a , within the data value
            //solution: IF this is a possibility that a value that is used in creating the string pattern
            //              could make the pattern invalid, you should explicitly handle how the value should be
            //              displayed in the string
            //example Date:  Jan 05, 2025 (due to using StartDate.ToShortDate())
            //solution:  specific your own format  StartDate.ToString("MMM dd yyyy")

            //Another solution is to change your delimitator that separates your values to a character
            //  that is not within your range of possible values
            //example use a '/'
            //when you use the .Split(delimitator) method to breakup the string into separate values
            //  you would use the delimitator '/':  string [] pieces = thestring.Split('/')

            //used at Nait for VS software which appears to NOT included the . after the
            //  MMM of date ToString
            //return $"{Title},{Level},{StartDate.ToString("MMM. dd yyyy")},{Years}";

            //used at home for VS software which appears to included the . after the
            //  MMM of date ToString

            return $"{Title},{Level},{StartDate.ToString("MMM dd yyyy")},{Years}";
        }

        // !!!! WHAT IF scenarios EXAMPLES

        //Sample action:Assume the SupervisoryLevel is a private set
        //this means altering the Level must be done in constructor (which executes ONLY ONCE during creation) or
        //  via a method
        public void SetEmploymentResponsibilityLevel(SupervisoryLevel level)
        {
            Level = level;
        }

        //What if the StartDate was a private set
        //you need to correct the startdate after the instance was created

        public void CorrectStartDate(DateTime startdate)
        {
            //one MAY have to duplicate logic in multiple places
            //if (startdate >= DateTime.Today.AddDays(1))
            //{
            //    throw new ArgumentException($"The start date of {startdate} is invalid, date cannot be in the future");
            //}

            //instead of having duplicate code, we can use fundamental coding practices
            //one could create a method solely used by the class to logic
            //this method would be a private method
            if (CheckDate(startdate))
                StartDate = startdate;

            //since the StartDate has beeb alter, the Years needs to be recalculated
            TimeSpan timediff = DateTime.Today - startdate;
            Years = Math.Round((timediff.Days / 365.2), 1);
        }

        //this is a private method use solely within the class for class  purposes
        private bool CheckDate(DateTime startdate)
        {
            if (startdate >= DateTime.Today.AddDays(1))
            {
                throw new ArgumentException($"The start date of {startdate} is invalid, date cannot be in the future");
            }
            return true;
        }
    }
}
