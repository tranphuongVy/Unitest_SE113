using System;
using NUnit.Framework;
using System.Linq;
using GUI.View;
using DTO;
using System.Windows;
using System.Windows.Controls;
using Castle.Core.Resource;
using System.Collections.ObjectModel;

namespace GUI.Tests
{
    [TestFixture]
    public class Window6Tests
    {
        private Window6 _window;

        [SetUp]
        public void Setup()
        {
            // Tạo Application nếu chưa có
            if (Application.Current == null)
            {
                new Application();
            }

            // Tải ResourceDictionary chính
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/GUI;component/Styles/Page.xaml", UriKind.Absolute)
            };

            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            // Khởi tạo đối tượng Window6
            _window = new Window6();

            // Đảm bảo ViewCustomerData không phải null
            if (_window.ViewCustomerData == null)
            {
                _window.ViewCustomerData = new ObservableCollection<CustomerDTO> { }; // Khởi tạo nếu null
            }
        }

        #region Test Case
        [TestCase("1", true)] // Kiểm tra bán vé thành công
        [TestCase("999", false)] // Kiểm tra trường hợp không tìm thấy khách hàng
        #endregion
        [Test, Apartment(ApartmentState.STA)]
        public void TestConfirmTicket(string userId, bool expectedResult)
        {
            // Lấy thông tin khách hàng cần kiểm tra
            var customerToSell = _window.ViewCustomerData.FirstOrDefault(c => c.ID == userId);

            if (customerToSell == null)
            {
                Console.WriteLine($"Customer with ID {userId} not found.");
                Assert.That(expectedResult, Is.EqualTo(false));
                return;
            }

            // Mô phỏng việc gán DataContext cho nút xác nhận
            var confirmButton = new Button
            {
                DataContext = customerToSell
            };

            // Gọi sự kiện Confirm_Click
            _window.Confirm_Click(confirmButton, null);

            Console.WriteLine($"Expected: {expectedResult}, Actual: {_window.IsSuccess}");

            // Xác nhận kết quả
            Assert.That(expectedResult, Is.EqualTo(_window.IsSuccess));
        }

        #region Test Case
        [TestCase("1", true)] // Kiểm tra xóa thành công
        [TestCase("999", false)] // Kiểm tra trường hợp không tìm thấy khách hàng để xóa
        #endregion
        [Test, Apartment(ApartmentState.STA)]
        public void TestDeleteCustomer(string userId, bool expectedResult)
        {
            // Lấy thông tin khách hàng cần kiểm tra
            var customerToDelete = _window.ViewCustomerData.FirstOrDefault(c => c.ID == userId);

            if (customerToDelete == null)
            {
                Console.WriteLine($"Customer with ID {userId} not found.");
                Assert.That(expectedResult, Is.EqualTo(false));
                return;
            }

            // Mô phỏng việc gán DataContext cho nút xóa
            var deleteButton = new Button
            {
                DataContext = customerToDelete
            };

            // Gọi sự kiện DeleteItem
            _window.DeleteItem(customerToDelete);

            Console.WriteLine($"Expected: {expectedResult}, Actual: {_window.IsSuccess}");

            // Xác nhận kết quả
            Assert.That(expectedResult, Is.EqualTo(_window.IsSuccess));
        }
    }
}
