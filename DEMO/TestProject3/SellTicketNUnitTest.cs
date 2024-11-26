using NUnit.Framework;
using Moq;
using System;
using DTO;
using BLL;
using GUI;
using GUI.View;
using System.Threading;

namespace Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)] // Đảm bảo rằng unit test chạy trong luồng STA
    public class SellTicketTests
    {
        private Mock<BookingTicket_BLL> _mockSellTicketBLL;
       // private Window6 _sellTicketWindow;
        private SellingTicketDTO _ticketDTO;

        [SetUp]
        public void SetUp()
        {
            _mockSellTicketBLL = new Mock<BookingTicket_BLL>();
           // _sellTicketWindow = new Window6();
            _ticketDTO = new SellingTicketDTO();
        }


    #region Test Cases for Flight ID
    [Test]
        public void TestFlightID_ShouldBeNotNullOrEmpty()
        {
            // Arrange
            _ticketDTO.FlightID = "F123";

            // Act & Assert
            Assert.IsNotNull(_ticketDTO.FlightID);
            Assert.That(_ticketDTO.FlightID, Is.EqualTo("F123"));
        }
        #endregion

        #region Test Cases for Customer Name
        [TestCase("John Doe", true)]
        [TestCase(" ", false)]  // Invalid case: empty name
        [TestCase("", false)]  // Invalid case: null name
        #endregion

        public void TestCustomerName_ShouldBeValid(string name, bool isValid)
        {
            // Arrange
            _ticketDTO.CustomerName = name;

            // Act & Assert
            if (isValid)
            {
                Assert.IsNotNull(_ticketDTO.CustomerName);
                Assert.That(_ticketDTO.CustomerName, Is.EqualTo(name));
            }
            else
            {
                Assert.IsTrue(string.IsNullOrEmpty(_ticketDTO.CustomerName));
            }
        }

        #region Test Cases for Phone Number
        [TestCase(1234567890, true)] // Valid phone
        [TestCase(0, false)]          // Invalid phone (0 is not a valid phone number)
        [TestCase(12345, false)]      // Invalid phone (less than 10 digits)
        #endregion
        public void TestPhoneNumber_ShouldBeValid(int phone, bool isValid)
        {
            // Arrange
            _ticketDTO.Phone = phone;

            // Act & Assert
            if (isValid)
            {
                Assert.That(_ticketDTO.Phone, Is.EqualTo(phone));
            }
            else
            {
                Assert.That(_ticketDTO.Phone, Is.Not.EqualTo(1234567890)); // Invalid cases
                Assert.That(phone < 1000000000 || phone > 9999999999, Is.True); // Optional: add validation for phone length
            }
        }

        #region Test Cases for Email
        [TestCase("john.doe@gmail.com", true)]  // Valid email
        [TestCase("john.doe@com", false)]      // Invalid email
        [TestCase("", false)]                   // Empty email
        public void TestEmail_ShouldBeValid(string email, bool isValid)
        {
            // Arrange
            _ticketDTO.Email = email;

            // Act & Assert
            if (isValid)
            {
                Assert.That(_ticketDTO.Email, Is.EqualTo(email));
            }
            else
            {
                Assert.IsTrue(string.IsNullOrEmpty(_ticketDTO.Email));
            }
        }
        #endregion

        #region Test Cases for Selling Date
        [Test]
        public void TestSellingDate_ShouldBeValidDate()
        {
            // Arrange
            _ticketDTO.SellingDate = DateTime.Now;

            // Act & Assert
            // Chỉ so sánh ngày, bỏ qua phần thời gian
            Assert.That(_ticketDTO.SellingDate.Date, Is.EqualTo(DateTime.Now.Date));
        }
        #endregion

        #region Test Case for Ticket Deletion Status
        [Test]
        public void TestIsDeleted_ShouldBeValid()
        {
            // Arrange
            _ticketDTO.IsDeleted = 0; // Assuming 0 means not deleted

            // Act & Assert
            Assert.That(_ticketDTO.IsDeleted, Is.EqualTo(0));
        }
        #endregion

        #region Test Case for Ticket Class ID
        [Test]
        public void TestTicketClassID_ShouldNotBeNull()
        {
            // Arrange
            _ticketDTO.TicketClassID = "Economy";

            // Act & Assert
            Assert.IsNotNull(_ticketDTO.TicketClassID);
            Assert.That(_ticketDTO.TicketClassID, Is.EqualTo("Economy"));
        }
        #endregion
    }
}
