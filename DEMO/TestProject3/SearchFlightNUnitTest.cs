/*using System;
using NUnit.Framework;
using GUI; // Namespace cho giao diện chính
using System.Windows;

namespace Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)] // Chạy trong luồng STA cho UI testing
    public class SearchFlightNUnitTest
    {
        private MainWindow _mainWindow;

        [SetUp]
        public void Setup()
        {
            _mainWindow = new MainWindow(); // Khởi tạo cửa sổ chính
        }

        #region Test DatePicker
        [Test]
        public void DatePicker_DefaultValue_ShouldBeToday()
        {
            // Arrange
            var expectedDate = DateTime.Today.ToString("dd/MM/yyyy");

            // Act
            var actualDate = _mainWindow.DatePicker.Text;

            // Assert
            Assert.AreEqual(expectedDate, actualDate, "DatePicker should show today's date by default.");
        }

        [Test]
        public void DatePicker_SetInvalidDate_ShouldNotAccept()
        {
            // Arrange
            _mainWindow.DatePicker.Text = "invalid-date";

            // Act
            var actualDate = _mainWindow.DatePicker.Text;

            // Assert
            Assert.AreNotEqual("invalid-date", actualDate, "Invalid date should not be accepted.");
        }

        [Test]
        public void DatePicker_ChangeDate_ShouldUpdateValue()
        {
            // Arrange
            var newDate = "25/12/2024";
            _mainWindow.DatePicker.Text = newDate;

            // Act
            var actualDate = _mainWindow.DatePicker.Text;

            // Assert
            Assert.AreEqual(newDate, actualDate, "DatePicker value should update to the selected date.");
        }
        #endregion

        #region Test ComboBox "From" và "To"
        [Test]
        public void ComboBoxFrom_DefaultValue_ShouldBeEmpty()
        {
            // Arrange
            var expectedValue = "";

            // Act
            var actualValue = _mainWindow.ComboBoxFrom.Text;

            // Assert
            Assert.AreEqual(expectedValue, actualValue, "ComboBox 'From' should have an empty default value.");
        }

        [Test]
        public void ComboBoxTo_DefaultValue_ShouldBeEmpty()
        {
            // Arrange
            var expectedValue = "";

            // Act
            var actualValue = _mainWindow.ComboBoxTo.Text;

            // Assert
            Assert.AreEqual(expectedValue, actualValue, "ComboBox 'To' should have an empty default value.");
        }

        [Test]
        public void ComboBoxFrom_ChangeSelection_ShouldUpdateValue()
        {
            // Arrange
            var newValue = "Nội Bài";
            _mainWindow.ComboBoxFrom.SelectedItem = newValue;

            // Act
            var actualValue = _mainWindow.ComboBoxFrom.SelectedItem;

            // Assert
            Assert.AreEqual(newValue, actualValue, "ComboBox 'From' value should update correctly.");
        }
        #endregion

        #region Test TextBox "Ticket Quantity"
        [Test]
        public void TicketQuantity_DefaultValue_ShouldBeOne()
        {
            // Arrange
            var expectedValue = "1";

            // Act
            var actualValue = _mainWindow.TicketQuantity.Text;

            // Assert
            Assert.AreEqual(expectedValue, actualValue, "Ticket Quantity should default to 1.");
        }

        [Test]
        public void TicketQuantity_SetValidValue_ShouldUpdateCorrectly()
        {
            // Arrange
            var newValue = "3";
            _mainWindow.TicketQuantity.Text = newValue;

            // Act
            var actualValue = _mainWindow.TicketQuantity.Text;

            // Assert
            Assert.AreEqual(newValue, actualValue, "Ticket Quantity should update to the new value.");
        }

        [Test]
        public void TicketQuantity_SetInvalidValue_ShouldNotAccept()
        {
            // Arrange
            _mainWindow.TicketQuantity.Text = "abc";

            // Act
            var actualValue = _mainWindow.TicketQuantity.Text;

            // Assert
            Assert.AreNotEqual("abc", actualValue, "Invalid value should not be accepted.");
        }
        #endregion

        #region Test nút "Search"
        [Test]
        public void SearchButton_Click_ShouldPerformSearch()
        {
            // Arrange
            _mainWindow.DatePicker.Text = "24/06/2024";
            _mainWindow.ComboBoxFrom.SelectedItem = "Nội Bài";
            _mainWindow.ComboBoxTo.SelectedItem = "Tân Sơn Nhất";
            _mainWindow.TicketQuantity.Text = "3";

            // Act
            _mainWindow.SearchButton_Click(null, null);

            // Assert
            Assert.IsTrue(_mainWindow.SearchResults.Items.Count > 0, "Search results should be populated.");
        }
        #endregion

        #region Test nút "Select" và "Close"
        [Test]
        public void SelectButton_Click_ShouldSelectFlight()
        {
            // Arrange
            _mainWindow.SearchResults.SelectedItem = _mainWindow.SearchResults.Items[0]; // Select first flight

            // Act
            _mainWindow.SelectButton_Click(null, null);

            // Assert
            Assert.IsTrue(_mainWindow.IsFlightSelected, "A flight should be selected after clicking 'Select'.");
        }

        [Test]
        public void CloseButton_Click_ShouldCloseWindow()
        {
            // Act
            _mainWindow.CloseButton_Click(null, null);

            // Assert
            Assert.IsFalse(_mainWindow.IsVisible, "Window should close after clicking 'Close'.");
        }
        #endregion
    }
}*/
