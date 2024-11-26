using NUnit.Framework;
using Moq;
using System;
using System.Text.RegularExpressions;
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
        private SellingTicketDTO _ticketDTO;

        [SetUp]
        public void SetUp()
        {
            // Mock BLL để giả lập hành vi
            _mockSellTicketBLL = new Mock<BookingTicket_BLL>();
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
        #endregion

        #region Test Cases for Phone Number
        [TestCase(1234567890, true)]          // Valid phone number (digits only)
        [TestCase(12345, false)]              // Invalid phone (less than 10 digits)
        [TestCase(98765432, false)]          // Invalid phone (less than 10 digits)
        [TestCase(0, false)]                 // Invalid phone (0 is not a valid phone number)
        [TestCase(84912345678, true)]        // Valid phone number (without '+')
        [TestCase("abcdefghij", false)]     // Invalid phone (letters instead of numbers)
        [TestCase("12345abcde", false)]     // Invalid phone (mix of numbers and letters)
        [TestCase("+84912345678abc", false)] // Invalid phone (mix of numbers and letters)
        public void TestPhoneNumber_ShouldBeValid(object phone, bool isValid)
        {
            // Arrange
            string phoneString = phone.ToString();  // Convert input to string

            // Act & Assert
            var phoneRegex = new Regex(@"^\+?\d{10,15}$");  // Regex for valid phone numbers (only digits, optional '+' at the start, length 10-15)

            var isPhoneValid = phoneRegex.IsMatch(phoneString);

            Assert.AreEqual(isValid, isPhoneValid, $"Phone validation failed for {phoneString}");
        }
        #endregion


        #region Test Cases for Email
        [TestCase("john.doe@gmail.com", true)]  // Valid email
        [TestCase("john.doe@com", false)]      // Invalid email (missing top-level domain)
        [TestCase("invalid-email", false)]     // Invalid email format
        [TestCase("", false)]                  // Empty email
        public void TestEmail_ShouldBeValid(string email, bool isValid)
        {
            // Arrange
            _ticketDTO.Email = email;

            // Act & Assert
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");  // Email regex
            var isEmailValid = emailRegex.IsMatch(_ticketDTO.Email);

            Assert.AreEqual(isValid, isEmailValid, $"Email validation failed for {email}");
        }
        #endregion

        #region Test Cases for Selling Date
        [Test]
        public void TestSellingDate_ShouldBeValidDate()
        {
            // Arrange
            _ticketDTO.SellingDate = DateTime.Now;

            // Act & Assert
            Assert.That(_ticketDTO.SellingDate.Date, Is.EqualTo(DateTime.Now.Date), "Selling date should be today.");
        }
        #endregion

        #region Test Case for Ticket Deletion Status
        [Test]
        public void TestIsDeleted_ShouldBeValid()
        {
            // Arrange
            _ticketDTO.IsDeleted = 0; // Assuming 0 means not deleted

            // Act & Assert
            Assert.That(_ticketDTO.IsDeleted, Is.EqualTo(0), "Ticket should not be deleted initially.");
        }
        #endregion

        #region Test Case for Ticket Class ID
        [Test]
        public void TestTicketClassID_ShouldNotBeNull()
        {
            // Arrange
            _ticketDTO.TicketClassID = "Economy";

            // Act & Assert
            Assert.IsNotNull(_ticketDTO.TicketClassID, "Ticket class ID should not be null.");
            Assert.That(_ticketDTO.TicketClassID, Is.EqualTo("Economy"));
        }
        #endregion

        /*#region UI Tests (Button Clicks)
        [Test]
        public void Test_SearchButton_Click_ShouldOpenSearchForm()
        {
            // Arrange
            var searchButton = _sellTicketWindow.SearchButton;  // Assuming this is the SearchButton in your WPF window
            searchButton.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));

            // Act & Assert
            // You can check if the popup window is opened (or some other action based on your implementation)
            Assert.IsInstanceOf<SearchFlight_Popup>(_sellTicketWindow.SearchFlight_Popup, "Search button should open Search Form.");
        }
        #endregion*/
    }
}
