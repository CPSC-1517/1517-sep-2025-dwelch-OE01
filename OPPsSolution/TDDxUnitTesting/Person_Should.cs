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
        public void Successfully_Change_Addres_Via_Property()
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
        #endregion
        #endregion

        #region Methods
        #region Valid Data
        #endregion
        #region Exception Testing
        #endregion
        #endregion
    }
}
