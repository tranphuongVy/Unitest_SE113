using System;
using GUI.View;
using NUnit.Framework;

namespace FlightOfListSearchTest
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class FlightOfListSearchTest
    {
        private Window7 _FlightOfListSearch;

        [SetUp]
        public void Setup()
        {
            _FlightOfListSearch = new Window7();
        }

      /*  #region Test Empty Airport

        [TestCase("Nội Bài", "", "Source or Destination airport cannot be empty.")]
        [TestCase("", "Vân Đồn", "Source or Destination airport cannot be empty.")]
        [TestCase("Tân Sơn Nhất", "Phú Quốc", "")]
        [TestCase("", "", "Source or Destination airport cannot be empty.")]
        public void TestEmptyAirport(string sourceAirport, string destinationAirport, string expectedresult)
        {
            // Act
            var result = _FlightOfListSearch.EmptyAirportCheck(sourceAirport, destinationAirport);

            // Assert
            Assert.That(result, Is.EqualTo(expectedresult));
        }

        #endregion*/

        #region Test Search Result
        private List<Flight> testFlights;

        [SetUp]
        public void Setup_result()
        {
            testFlights = new List<Flight>
            {
                new Flight("Haiphong", "HoChiMinh", new DateTime(2024, 11, 30), new DateTime(2024, 11, 30, 12, 0, 0)),
                new Flight("Hanoi", "Danang", new DateTime(2024, 12, 1), new DateTime(2024, 12, 1, 14, 0, 0)),
                new Flight("HoChiMinh", "Hanoi", new DateTime(2024, 12, 2), new DateTime(2024, 12, 2, 16, 0, 0)),
                new Flight("Phuquoc", "Hanoi", new DateTime(2024, 12, 2), new DateTime(2024, 12, 2, 16, 0, 0))
            };
        }

        [Test]
        public void GetMatchingFlights_ShouldReturnMatchingFlights1()
        {
            // Arrange
            string source = "Hanoi";
            string destination = "Danang";

            // Act
            var matchingFlights = testFlights
                .Where(f => f.SourceAirport == source && f.DestinationAirport == destination)
                .ToList();

            // Assert
            Assert.AreEqual(1, matchingFlights.Count);
            Assert.AreEqual("Hanoi", matchingFlights[0].SourceAirport);
            Assert.AreEqual("Danang", matchingFlights[0].DestinationAirport);

        }
        [Test]
        public void GetMatchingFlights_ShouldReturnMatchingFlights2()
        {
            // Arrange
            string source = "Haiphong";
            string destination = "";

            // Act
            var matchingFlights = testFlights
                .Where(f => f.SourceAirport == source)
                .ToList();

            // Assert
            Assert.AreEqual(1, matchingFlights.Count);
            Assert.AreEqual("Haiphong", matchingFlights[0].SourceAirport);


        }
        [Test]
        public void GetMatchingFlights_ShouldReturnMatchingFlights3()
        {
            // Arrange
            string source = "";
            string destination = "Hanoi";

            // Act
            var matchingFlights = testFlights
                .Where(f => f.DestinationAirport== destination)
                .ToList();

            // Assert
            Assert.AreEqual(2, matchingFlights.Count);
            Assert.AreEqual("Hanoi", matchingFlights[0].DestinationAirport);
            Assert.AreEqual("Hanoi", matchingFlights[1].DestinationAirport);


        }

        [Test]
        public void GetMatchingFlights_ShouldReturnEmptyList_WhenNoMatch()
        {
            // Arrange
            string source = "Hanoi";
            string destination = "Tokyo";

            // Act
            var matchingFlights = testFlights
                .Where(f => f.SourceAirport == source && f.DestinationAirport == destination)
                .ToList();

            // Assert
            Assert.AreEqual(0, matchingFlights.Count);
        }

        [Test]
        public void GetMatchingFlights_ShouldReturnDefaultList()
        {
            // Arrange
            string source = "";
            string destination = "";

            // Act
            var matchingFlights = testFlights.ToList();
            // Assert
            Assert.AreEqual(4, matchingFlights.Count);
        }
        #endregion

    }
}
