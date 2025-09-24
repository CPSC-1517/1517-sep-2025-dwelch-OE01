using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using OPPsReview;
using FluentAssertions;
#endregion

namespace TDDxUnitTesting
{
    public class Person_Should
    {
        #region Constructors
        #region Valid Data
        //a Fact unit test executes once
        //without the [Fact] annotation, the method is NOT considered a unit test
        //  it would just be a method within this class
        [Fact]
        public void Successfully_Create_An_Instance_Using_Default_Constructor()
        {
            //Arrange (this is the setup of values need for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionsCount = 0;

            //Act (this is the action that is under testing)
            //sut: subject under test
            //Image that the Act is a line of code from a program
            Person sut = new Person();

            //Assert (check the results of the act (Act) against expected Values (Arrange))
            //this is where the FluentAssertions extension methods come into play
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionsCount);
        }

        [Fact]
        public void Successfully_Create_An_Instance_Using_Greedy_Constructor_With_No_Address_Or_Employments()
        {
            //Arrange (this is the setup of values need for doing the test)
            string expectedFirstName = "Don";
            string expectedLastName = "Welch";
            int expectedEmploymentPositionsCount = 0;

            //Act (this is the action that is under testing)
            Person sut = new Person("  Don  ", "  Welch  ", null, null);

            //Assert (check the results of the act (Act) against expected Values (Arrange))
 
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionsCount);
        }
        [Fact]
        public void Successfully_Create_An_Instance_Using_Greedy_Constructor_With_No_Employments()
        {
            //Arrange (this is the setup of values need for doing the test)
            string expectedFirstName = "Don";
            string expectedLastName = "Welch";
            ResidentAddress expectedAddress = new ResidentAddress(123, "Maple St.",
                            "Edmonton", "AB", "T6Y7U8");
            int expectedEmploymentPositionsCount = 0;

            //Act (this is the action that is under testing)
            Person sut = new Person("  Don  ", "  Welch  ", expectedAddress, null);

            //Assert (check the results of the act (Act) against expected Values (Arrange))

            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().Be(expectedAddress);
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionsCount);
        }

        [Fact]
        public void Successfully_Create_An_Instance_Using_Greedy_Constructor_With_Full_Data()
        {
            //Arrange (this is the setup of values need for doing the test)
            string expectedFirstName = "Don";
            string expectedLastName = "Welch";
            ResidentAddress expectedAddress = new ResidentAddress(123, "Maple St.",
                            "Edmonton", "AB", "T6Y7U8");

            //how to test a collection?
            //create individual instances of the item in the list
            //in this example those instances are objects
            //you must remember each object has a unique GUID
            //NOTE: you CANNOT reuse a single variable to hold the separate instances
            Employment one = new Employment("PG I", SupervisoryLevel.TeamMember,
                                DateTime.Parse("2013/10/04"), 6.5);

            //remember that if no year was supplied the length of holding the current
            //      position is calculated for the instance
            Employment two = new Employment("PG II", SupervisoryLevel.TeamLeader,
                                DateTime.Parse("2020/04/04"));
            List<Employment> employments = new List<Employment>();
            employments.Add(one);
            employments.Add(two);
            int expectedEmploymentPositionsCount = 2;

            //Act (this is the action that is under testing)
            Person sut = new Person("  Don  ", "  Welch  ", expectedAddress, employments);

            //Assert (check the results of the act (Act) against expected Values (Arrange))

            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().Be(expectedAddress);
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionsCount);
            //in addition to checking the number of items in your collection
            //  you SHOULD check that the expected order of the items in your collection
            //  is the same as what was sent in
            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(employments);
        }
        #endregion
        #region Exception Testing
        //the second test annotation used is called [Theory]
        //it will execute n number of times as a loop
        //n is determined by the number [InlineData()] annotations following the [Theory]
        //to setup the test header, you must include a parameter in a parameter list
        //  one for each, value in the InlineData set of values
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Throw_Exception_Creating_An_Instance_Using_Greedy_Constructor_With_Missing_FirstName(string firstname)
        {
            //Arrange
            //possible values for FirstName: null, empty string, blank string
            //the setup of an exception test does not have to be as extensive as a successful test
            //  as the objective is to catch the exception that is thrown
            //in this example there will be no need to check expected values as an instance will NOT be created

            //Act
            //the act in this case is the capture of the exception that has been thrown
            //use () => to indicate that the following delegate is to be executed as the required code
            //the datatype to capture the results of the code execution is called Action
            Action action = () => new Person(firstname, "Welch", null, null);

            //Assert
            //test to see if the expected exception was throw
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Throw_Exception_Creating_An_Instance_Using_Greedy_Constructor_With_Missing_LastName(string lastname)
        {
            //Arrange
          
            //Act
            Action action = () => new Person("Don", lastname, null, null);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }

        //combine the two previous Theory tests
        //multiple values in InlinData
        //multiple parameters on your method
        [Theory]
        [InlineData(null,"Welch")]
        [InlineData("", "Welch")]
        [InlineData("    ", "Welch")]
        [InlineData("Don",null)]
        [InlineData("Don", "")]
        [InlineData("Don", "    ")]
        //some new people to unit testing may attempt to shortern InlineData with multiple values
        //  that will not execute as expected
        //due to the coding of the constructor, logically, the code would NEVER get to test the last name
        //[InlineData(null,null)]
        //[InlineData("", "")]
        //[InlineData("   ", "   ")]
        public void Throw_Exception_Creating_An_Instance_Using_Greedy_Constructor_With_Missing_First_Or_Last_Name(string firstname, string lastname)
        {
            //Arrange

            //Act
            Action action = () => new Person(firstname, lastname, null, null);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }
        #endregion
        #endregion

        #region Properties
        #region Valid Data
        //directly change the FirstName on an instance of Person using a real name
        [Fact]
        public void Successfully_Directly_Change_FirstName_Using_Property()
        {
            //Arrange
            string expectedfirstName = "Diver";
            Person sut = new Person("Don", "Welch", null, null);
            //Person sut = new Person(); //why? the default assigns "Unknown" to the first name

            //Act
            sut.FirstName = "  Diver  ";

            //Assert
            sut.FirstName.Should().Be(expectedfirstName);

        }
        //directly change the LastName on an instance of Person using a real name
        [Fact]
        public void Successfully_Directly_Change_LastName_Using_Property()
        {
            //Arrange
            string expectedlastName = "Diver";
            Person sut = new Person("Don", "Welch", null, null);
            //Person sut = new Person(); //why? the default assigns "Unknown" to the first name

            //Act
            sut.LastName = "  Diver  ";

            //Assert
            sut.LastName.Should().Be(expectedlastName);

        }
        //directly change the address on an instance of Person 
        [Fact]
        public void Successfully_Change_Address_Via_Property()
        {
            //Arrange
            ResidentAddress expectedAddress = new ResidentAddress(321, "Sunflower St.", "Regina", "Sk", "S5G9R2");
            Person sut = new Person("Roy", "Nett",
                            new ResidentAddress(123, "Maple St.", "Edmonton", "AB", "T6Y7U8"), null);

            //Act
            sut.Address = new ResidentAddress(321, "Sunflower St.", "Regina", "Sk", "S5G9R2");

            //Assert
            sut.Address.Should().Be(expectedAddress);
        }

        [Fact]
        public void Successfully_Change_Address_To_Null_Via_Property()
        {
            //Arrange
          
            Person sut = new Person("Roy", "Nett",
                            new ResidentAddress(123, "Maple St.", "Edmonton", "AB", "T6Y7U8"), null);

            //Act
            sut.Address = null;

            //Assert
            sut.Address.Should().BeNull();
        }

        //consider making EmploymentPositions private set (must use method)
        //  do we wish to allow the entire employment collection to be replaced?
        //  consider, is the mutator set to private?
        //      if so, direct altering is not possible (access trouble)
        //      if private, any code to actually attempt to use the mutator (set) will NOT even compile
        //  so even though you my have "brainstormed" this test, it is possible to determind that
        //      the test is unnecessary


        //full name should return the Person name using the current instance data (last, first)
        [Fact]
        public void Successfully_Return_FullName_Using_Property()
        {
            //Arrange
            Person sut = new Person("Don", "Welch", null, null);
            string expectedFullName = "Welch, Don";

            //Act
            string fullName = sut.FullName;

            //Assert
            fullName.Should().Be(expectedFullName);

        }

        #endregion
        #region Exception Testing
        //throw ArgumentNullException if first name is missing went changed via the property
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_ArugmentNullException_When_FirstName_Missing_Changed_Via_Property(string name)
        {
            // Arrange
            Person sut = new Person("Scuba", "Steve", 
                   new ResidentAddress(123, "Maple St.", "Calgary", "AB", "T3H 4W1"), null);

            // Act
            Action action = () => sut.FirstName = name;

            // Assert
            action.Should().Throw<ArgumentNullException>();

        }
        //throw ArgumentNullException if first name is missing went changed via the property
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_ArugmentNullException_When_LastName_Missing_Changed_Via_Property(string name)
        {
            // Arrange
            Person sut = new Person("Scuba", "Steve",
                   new ResidentAddress(123, "Maple St.", "Calgary", "AB", "T3H 4W1"), null);

            // Act
            Action action = () => sut.LastName = name;

            // Assert
            action.Should().Throw<ArgumentNullException>();

        }
        #endregion
        #endregion

        #region Methods
        #region Valid Data
        //successfully change both first and last name using the ChangeFullName method
        [Fact]
        public void Change_Both_First_And_Last_Name_Using_ChangeFullName_Method()
        {
            //Arrange
            Person sut = new Person("Tyler", "Ewert", null, null);
            string expectedFullName = "Uwert, Taylor";

            //Act
            sut.ChangeFullName("Taylor", "Uwert");

            //Assert
            sut.FullName.Should().Be(expectedFullName);
            //sut.FirstName.Should().Be("Taylor");
            //sut.LastName.Should().Be("Uwert");

        }

        [Fact]
        //successfully add the first employment to the person
        public void Successfully_Add_First_Emploment_To_Person_via_AddEmployment()
        {
            //Arrange
            Person sut = new Person("Brad", "Lindgren", null, null);

            //create the Employee instance to be added to Person
            //remember Employment has been completely tested
            Employment newEmployment = new Employment("PG II", SupervisoryLevel.TeamLeader,
                                        DateTime.Parse("2020/04/04"));
            //expected results
            //one needs to test the collection of Person once the method
            //      has been executed
            List<Employment> expectedEmploymentPositions = new List<Employment>();
            //also need to have the instance in the list
            expectedEmploymentPositions.Add(newEmployment);
            //also need to know the expected count of instances in the list
            int expectedEmploymentPositionCount = 1;


            //Act
            sut.AddEmployment(newEmployment);

            //Assert
            //one could check all fields of Person (first and last name, address)
            //  BUT these properties have already be completely tested and as good

            //examine the current contents within EmploymentPositions to see if they are correct
            //best practice: check the collection count first, if it fails then there is no need
            //                  to check the collection
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionCount);

            //now that you know the correct number of Employment instances exist, the next
            //  check is the instance(s) themselves
            //Are they the instance(s) that were passed into the method
            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(expectedEmploymentPositions);

        }

        [Fact]
        //successfully add the next employment to the person
        //we do not know if this nex employment is the 2nd, 3rd, 4th, ...
        public void Successfully_Add_Another_Emploment_To_Person_via_AddEmployment()
        {
            //Arrange
           
            //create the Employee instance to be added to Person
            //remember Employment has been completely tested
            Employment oneEmployment = new Employment("PG I", SupervisoryLevel.TeamMember,
                                        DateTime.Parse("2013/10/10"), 6.5);
            Employment twoEmployment = new Employment("PG II", SupervisoryLevel.TeamLeader,
                                        DateTime.Parse("2020/04/04"));
            //all of these past employment instances will need to exist in the 
            //  EmploymentPositions collection
            List<Employment> currentEmployments = new List<Employment>();
            currentEmployments.Add(oneEmployment);
            currentEmployments.Add(twoEmployment);

            //now we can create the Person instance
            Person sut = new Person("Brad", "Lindgren", null, currentEmployments);


            //expected results
            Employment nextEmployment = new Employment("Sup I", SupervisoryLevel.Supervisor,
                                       DateTime.Today);
            //one needs to test the collection of Person once the method
            //      has been executed
            List<Employment> expectedEmploymentPositions = new List<Employment>();
            //also need to have the instance in the list
            expectedEmploymentPositions.Add(oneEmployment);
            expectedEmploymentPositions.Add(twoEmployment);
            expectedEmploymentPositions.Add(nextEmployment);
            //also need to know the expected count of instances in the list
            int expectedEmploymentPositionCount = 3;


            //Act
            sut.AddEmployment(nextEmployment);

            //Assert
            //one could check all fields of Person (first and last name, address)
            //  BUT these properties have already be completely tested and as good

            //examine the current contents within EmploymentPositions to see if they are correct
            //best practice: check the collection count first, if it fails then there is no need
            //                  to check the collection
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionCount);

            //now that you know the correct number of Employment instances exist, the next
            //  check is the instance(s) themselves
            //Are they the instance(s) that were passed into the method
            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(expectedEmploymentPositions);

        }
        #endregion
        #region Exception Testing
        //throw exception went using ChangeFullName within invalid data for first and/or last name
        [Theory]
        [InlineData(null, "Doe")]
        [InlineData("", "Doe")]
        [InlineData("   ", "Doe")]
        [InlineData("John", null)]
        [InlineData("John", "   ")]
        [InlineData("John", "")]

        public void Throw_ArgumentNullException_When_Changing_FullName_With_Invalid_Inputs(string firstName, string lastName)
        {

            // Arrange
            Person sut = new Person("Brad", "Lindgren", null, null);

            // Act
            Action action = () => sut.ChangeFullName(firstName, lastName);

            // Assert
            action.Should().Throw<ArgumentNullException>();

        }

        #endregion
        #endregion
    }
}
