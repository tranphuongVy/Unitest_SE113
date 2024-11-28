using DTO;
using GUI.View;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace TestProject3
{
    [TestFixture]
    public class SellTicketNUnitTest_YT
    {
        private Window6 sellTicketWindow;

        [SetUp]
        public void Setup()
        {
            sellTicketWindow = new Window6();
        }
        #region Test Case
        [TestCase("John Doe", "123456789012", "john@gmail.com", true)] // Thông tin hợp lệ
        [TestCase("Invalid Customer", "999999999999", "invalid@example.com", false)] // Thông tin không hợp lệ
        #endregion
        [Test, Apartment(ApartmentState.STA)]
        public void TestConfirm_Click(string name, string id, string email, bool expectedResult)
        {
            // Arrange: Giả lập dữ liệu khách hàng hợp lệ
            var eventArgs = new RoutedEventArgs();
            sellTicketWindow.selectedFlight = new FlightDTO
            {
                FlightID = "FL002",
                SourceAirportID = "001",
                DestinationAirportID = "002",
                FlightDay = new DateTime(2024, 6, 2),
                FlightTime = TimeSpan.Parse("09:00:00"),
                Price = 150.00m,
                IsDeleted = 0
            };
            
            sellTicketWindow.selectedTicketClass = new TicketClassDTO
            {
                TicketClassID = "002",
                TicketClassName = "Business",
                BaseMultiplier = 1.5m,
                IsDeleted = 0
            };

            // Tạo danh sách khách hàng
            var customerList = new ObservableCollection<CustomerDTO>
    {
        new CustomerDTO
        {
            CustomerName = name,
            ID = id,
            Phone = "0123456789",
            Email = email,
            Birth = new DateTime(1990, 5, 15)
        }
    };

            // Giả lập khách hàng
            sellTicketWindow.ViewCustomerData = customerList;
            sellTicketWindow.customerView = CollectionViewSource.GetDefaultView(sellTicketWindow.ViewCustomerData);
            //if(sellTicketWindow.myListView == null)
            //{
            //    throw new Exception("customerView null");
            //}    
            sellTicketWindow.myListView.ItemsSource = sellTicketWindow.customerView;
            // Act: Gọi Confirm_Click
            sellTicketWindow.Confirm_Click(this, eventArgs);
            Console.WriteLine($"Expected: {expectedResult}, Actual: {sellTicketWindow.IsSuccess}");

            // Assert: Tất cả khách hàng hợp lệ thì ResetData được gọi
            Assert.That(expectedResult, Is.EqualTo(sellTicketWindow.IsSuccess));
        }
    }
}
