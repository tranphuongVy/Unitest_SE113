using NUnit.Framework;
using Moq;
using System;
using System.Text.RegularExpressions;
using DTO;
using BLL;
using GUI;
using GUI.View;
using System.Windows.Controls;
using System.Linq;

namespace Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)] // Ensure the unit test runs in STA thread
    public class SellTicketTests
    {
        private Window6 _sellTicketWindow;

        [SetUp]
        public void SetUp()
        {
            _sellTicketWindow = new Window6(); // Initialize the window
        }

        #region Test Case for Confirm Button Click
        [TestCase("0123456789", "John Doe", "0123456789", "john.doe@gmail.com", "2024-12-12", true)]
        [TestCase("0123456789", "John Doe", "0123456789", "john.doe@gmail.com", "2024-12-12", false)] // Invalid Flight ID
        [TestCase("0123456789", "", "0123456789", "john.doe@gmail.com", "2024-12-12", false)] // Invalid Customer Name
        [TestCase("0123456789", "John Doe", "0123456789", "invalid-email", "2024-12-12", false)] // Invalid Email
        [TestCase("0123456789", "John Doe", "12345", "john.doe@gmail.com", "2024-12-12", false)] // Invalid Phone
        [Apartment(ApartmentState.STA)]
        #endregion
        public void TestConfirmButton_Click_ShouldProcessTicket(string ID, string name, string phone, string email, string birthStr, bool expectedResult)
        {
            // Convert string to DateTime
            DateTime birth = DateTime.Parse(birthStr);

            // Arrange: Set form fields
            _sellTicketWindow.ID.Text = ID;
            _sellTicketWindow.Name.Text = name;
            _sellTicketWindow.Phone.Text = phone;
            _sellTicketWindow.Email.Text = email;
            _sellTicketWindow.Birth.SelectedDate = birth;

            // Act: Simulate the click on the confirm button
            _sellTicketWindow.Confirm_Click(null, null);

            // Assert the results
            Assert.That(expectedResult, Is.EqualTo(_sellTicketWindow.IsSuccess));
        }
    }

}



