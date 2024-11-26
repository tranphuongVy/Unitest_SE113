using System;
using NUnit.Framework;
using Moq;
using BLL; // Nếu bạn sử dụng Moq để tạo mock cho IAccountBLL
using GUI;
using System.Windows;
using GUI.View;
using DTO;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace GUI.Test
{
    [TestFixture]
    public class Test_DeleteMember
    {
        private Window3 _deleteMemberWindow;
        private ObservableCollection<ACCOUNT> _members;

        [SetUp]
        public void SetUp()
        {
            _deleteMemberWindow = new Window3();
            _members = new ObservableCollection<ACCOUNT>
             {
                 new ACCOUNT { UserID = "1", PermissonID = 1, UserName = "Admin User", Email = "admin@example.com", Phone = "123456789", Birth = new DateTime(1990, 1, 1), IsDeleted = 0 },
                 new ACCOUNT { UserID = "2", PermissonID = 2, UserName = "Normal User", Email = "staff@example.com", Phone = "987654321", Birth = new DateTime(1995, 12, 25), IsDeleted = 0 }
             };

            // Gán ItemsSource cho DataGrid
            _deleteMemberWindow.memberDataGrid = new DataGrid
            {
                ItemsSource = _members
            };
        }

        #region Test Case
        [TestCase("1", true)]
        [TestCase("2", true)]
        [Apartment(ApartmentState.STA)]
        #endregion

        public void TestDeleteMember(string userId, bool expectedResult)
        {
            //Lấy thông tin tài khoản cần kiểm tra
            var accountToDelete = _members.FirstOrDefault(acc => acc.UserID == userId);

            //Mô phỏng việc gán DataContext cho nút xóa
            var deleteButton = new Button
            {
                DataContext = accountToDelete
            };

            // Gọi sự kiện Delete_Click
            _deleteMemberWindow.Delete_Click(deleteButton, null);

            Console.WriteLine($"Expected: {expectedResult}, Actual: {_deleteMemberWindow.IsSuccess}");

            // Xác nhận kết quả
            //Assert.AreEqual(expectedResult, _loginWindow.IsLoggedIn);
            Assert.That(expectedResult, Is.EqualTo(_deleteMemberWindow.IsSuccess));

        }
    }
}

