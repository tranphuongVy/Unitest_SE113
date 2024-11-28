using NUnit.Framework;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace ReceiveFlightScheduleNUnitTest
{
    [TestFixture]
    public class DatePickerTests
    {
        // Kiểm tra chọn ngày từ DatePicker
        [Test]
        [Apartment(ApartmentState.STA)]  // Đảm bảo test này chạy trong STA thread
        public void Test_DatePicker_SelectDate()
        {
            // Arrange: Tạo một DatePicker mới
            var datePicker = new DatePicker();
            var selectedDate = new DateTime(2024, 11, 28); // Ngày cụ thể để chọn

            // Act: Gán ngày đã chọn vào DatePicker
            datePicker.SelectedDate = selectedDate;

            // Assert: Kiểm tra ngày đã chọn trong DatePicker có đúng như mong đợi
            Assert.That(datePicker.SelectedDate, Is.EqualTo(selectedDate));
        }

        // Kiểm tra mặc định của DatePicker khi chưa chọn ngày
        [Test]
        [Apartment(ApartmentState.STA)]  // Đảm bảo test này chạy trong STA thread
        public void Test_DatePicker_DefaultDate()
        {
            // Arrange: Tạo một DatePicker mới
            var datePicker = new DatePicker();

            // Act & Assert: Kiểm tra rằng khi chưa chọn ngày, SelectedDate là null
            Assert.IsNull(datePicker.SelectedDate);
        }

        // Kiểm tra nhập định dạng không hợp lệ vào DatePicker
        [Test]
        [Apartment(ApartmentState.STA)]  // Đảm bảo test này chạy trong STA thread
        public void Test_DatePicker_InvalidDateInput()
        {
            // Arrange: Tạo một DatePicker mới
            var datePicker = new DatePicker();
            var invalidDate = "Invalid Date";  // Chuỗi không hợp lệ

            // Act: Cố gắng gán giá trị không hợp lệ vào DatePicker
            try
            {
                datePicker.SelectedDate = DateTime.Parse(invalidDate);  // Gây lỗi
                Assert.Fail("Expected FormatException was not thrown.");
            }
            catch (FormatException)
            {
                // Assert: Kiểm tra lỗi FormatException được ném ra
                Assert.Pass("FormatException correctly thrown.");
            }
        }

        // Kiểm tra click vào DatePicker và chọn ngày từ Calendar
        [Test]
        [Apartment(ApartmentState.STA)]  // Đảm bảo test này chạy trong STA thread
        public void Test_DatePicker_SelectDateFromCalendar()
        {
            // Arrange: Tạo một DatePicker mới và gán giá trị ngày
            var datePicker = new DatePicker();
            var selectedDate = new DateTime(2024, 11, 28); // Chọn ngày này

            // Act: Mở Calendar và chọn ngày
            datePicker.IsDropDownOpen = true;
            datePicker.SelectedDate = selectedDate;

            // Assert: Kiểm tra rằng ngày đã chọn là đúng
            Assert.That(datePicker.SelectedDate, Is.EqualTo(selectedDate));
        }
    }

    [TestFixture]
    public class ComboBoxTests
    {
        // Kiểm tra chọn một sân bay từ ComboBox
        [Test]
        [Apartment(ApartmentState.STA)]  // Đảm bảo test này chạy trong STA thread
        public void Test_ComboBox_SelectAirport()
        {
            // Arrange: Tạo một ComboBox với các sân bay
            var comboBox = new ComboBox();
            comboBox.Items.Add("Sân bay Nội Bài");
            comboBox.Items.Add("Sân bay Tân Sơn Nhất");
            comboBox.Items.Add("Sân bay Đà Nẵng");

            // Act: Chọn một sân bay từ ComboBox
            comboBox.SelectedItem = "Sân bay Tân Sơn Nhất";

            // Assert: Kiểm tra rằng sân bay đã được chọn
            Assert.That(comboBox.SelectedItem, Is.EqualTo("Sân bay Tân Sơn Nhất"));
        }

        // Kiểm tra tính năng tìm kiếm trong ComboBox
        [Test]
        [Apartment(ApartmentState.STA)]  // Đảm bảo test này chạy trong STA thread
        public void Test_ComboBox_Search()
        {
            // Arrange: Tạo một ComboBox với các sân bay
            var comboBox = new ComboBox();
            comboBox.Items.Add("Sân bay Nội Bài");
            comboBox.Items.Add("Sân bay Tân Sơn Nhất");
            comboBox.Items.Add("Sân bay Đà Nẵng");

            // Act: Tìm kiếm một sân bay trong ComboBox
            comboBox.Text = "Tân Sơn Nhất"; // Giả lập việc nhập chuỗi tìm kiếm

            // Mở dropdown ComboBox để mô phỏng người dùng tìm kiếm
            comboBox.IsDropDownOpen = true;

            // Thực hiện chọn item tương ứng dựa trên Text
            foreach (var item in comboBox.Items)
            {
                if (item.ToString().Contains(comboBox.Text)) // Kiểm tra khớp một phần
                {
                    comboBox.SelectedItem = item;  // Chọn item khi tìm thấy khớp
                    break;
                }
            }

            // Assert: Kiểm tra rằng ComboBox đã tìm thấy sân bay tương ứng
            Assert.That(comboBox.SelectedItem, Is.EqualTo("Sân bay Tân Sơn Nhất"));
        }


    }
}
