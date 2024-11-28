using System;
using GUI.View;
using NUnit.Framework;

namespace FlightOfListSearchTest
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class FlightOfListSearchTest
    {
        private Window2 _FlightOfListSearch;

        [SetUp]
        public void Setup()
        {
            _FlightOfListSearch = new Window2();
        }

        #region Test Empty Airport

        [TestCase("Nội Bài", "", "Source or Destination airport cannot be empty.")]
        [TestCase("", "Vân Đồn", "Source or Destination airport cannot be empty.")]
        [TestCase("Tân Sơn Nhất", "Phú Quốc", "Correct")]
        [TestCase("", "", "Source or Destination airport cannot be empty.")]
        public void TestEmptyAirport(string sourceAirport, string destinationAirport, string expectedresult)
        {
            // Act
            var result = _FlightOfListSearch.EmptyAirportCheck(sourceAirport, destinationAirport);

            // Assert
            Assert.That(result, Is.EqualTo(expectedresult));
        }

        #endregion
    }
}
