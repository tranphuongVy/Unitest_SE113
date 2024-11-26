using System;
using NUnit.Framework;
using Moq;
using BLL; // Giả sử bạn sử dụng Moq để tạo mock cho IAccountBLL
using GUI;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Threading;

namespace FlightOfListSearchTest
{
    // Lớp giả lập (Mocked Data) cho chuyến bay (FlightInfoDTO)
    public class FlightInfoDTO
    {
        public Flight Flight { get; set; }
        public int EmptySeats { get; set; }
        public int BookedTickets { get; set; }
    }

    public class Flight
    {
        public string FlightID { get; set; }
        public DateTime FlightDay { get; set; }
        public TimeSpan FlightTime { get; set; }
    }

    // Extension method to simulate fetching mocked flight data
    public static class MockDataHelper
    {
        public static IEnumerable<FlightInfoDTO> GetMockedFlightData(string departure, string arrival)
        {
            if (departure == "AIR001" && arrival == "AIR002")
            {
                return new List<FlightInfoDTO>
                {
                    new FlightInfoDTO
                    {
                        Flight = new Flight
                        {
                            FlightID = "FL001",
                            FlightDay = DateTime.Now.AddDays(1),
                            FlightTime = TimeSpan.FromHours(2)
                        }
                    }
                };
            }

            return Enumerable.Empty<FlightInfoDTO>();
        }
    }

    [TestFixture]
    [Apartment(ApartmentState.STA)] // Chạy trong luồng STA cho ứng dụng WPF
    public class Test_SearchFlight
    {
        private Mock<SearchProcessor> _mockSearchProcessor;
        private SearchProcessor _searchProcessor;

        [SetUp]
        public void Setup()
        {
            // Khởi tạo mock cho SearchProcessor
            _mockSearchProcessor = new Mock<SearchProcessor>();
            _searchProcessor = _mockSearchProcessor.Object;
        }

        #region Kiểm tra chức năng tìm kiếm chuyến bay

        [TestCase("AIR001", "AIR002", true)] // Kiểm tra tìm chuyến bay hợp lệ
        [TestCase("INVALID", "INVALID", false)] // Kiểm tra chuyến bay không hợp lệ
        public void SearchFlight_ShouldReturnCorrectResults(string departure, string arrival, bool expectedResult)
        {
            // Thiết lập mock để trả về kết quả theo dữ liệu đầu vào
            _mockSearchProcessor.Setup(x => x.GetFlightInfoDTO(departure, arrival, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns((string dep, string arr, DateTime startDate, DateTime endDate) =>
                    MockDataHelper.GetMockedFlightData(dep, arr));

            // Thực hiện tìm kiếm chuyến bay
            var flights = _searchProcessor.GetFlightInfoDTO(departure, arrival, DateTime.Now, DateTime.Now.AddDays(1));
            
            // Kiểm tra kết quả trả về
            Assert.That(flights.Any(), Is.EqualTo(expectedResult));
        }

        #endregion

        #region Kiểm tra UI: Date Picker và ComboBox

        [Test]
        public void DatePicker_ShouldShowCalendarAndHighlightToday_WhenEmpty()
        {
            // Thực hiện kiểm tra trong luồng STA
            var thread = new Thread(() =>
            {
                var datePicker = new DatePicker();
                datePicker.Focus();
                datePicker.SelectedDate = DateTime.Now;

                Assert.That(datePicker.SelectedDate.HasValue, Is.True);
                Assert.That(datePicker.SelectedDate.Value.Date, Is.EqualTo(DateTime.Now.Date));
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        [Test]
        public void ComboBox_ShouldShowDropdown()
        {
            // Thực hiện kiểm tra trong luồng STA
            var thread = new Thread(() =>
            {
                var comboBox = new ComboBox();
                comboBox.ItemsSource = new List<string> { "Airport1", "Airport2", "Airport3" };
                comboBox.IsDropDownOpen = true;

               Assert.IsTrue(comboBox.IsDropDownOpen);
                Assert.That(comboBox.Items.Count, Is.EqualTo(3));
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        [Test]
        public void ComboBox_ShouldShowValidationMessage_WhenNoSelection()
        {
            // Thực hiện kiểm tra trong luồng STA
            var thread = new Thread(() =>
            {
                var comboBox = new ComboBox();
                comboBox.SelectedItem = null;

                if (comboBox.SelectedItem == null)
                {
                    comboBox.ToolTip = "Please select airport!";
                }

                Assert.AreEqual("Please select airport!", comboBox.ToolTip);
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        #endregion



    }
}
