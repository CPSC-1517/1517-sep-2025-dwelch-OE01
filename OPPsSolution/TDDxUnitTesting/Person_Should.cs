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
        #endregion
        #endregion

        #region Properties
        #region Valid Data
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
