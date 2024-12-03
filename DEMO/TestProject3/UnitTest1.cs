﻿using System;
using NUnit.Framework;
using Moq;
using BLL; // Nếu bạn sử dụng Moq để tạo mock cho IAccountBLL
using GUI;
using System.Windows;

namespace GUI.Test
{
    [TestFixture]
    public class Test_Login
    {
        private Mock<IAccountBLL> _mockAccountBLL;
        private Login _loginWindow;

        [SetUp]
        public void SetUp()
        {
            _mockAccountBLL = new Mock<IAccountBLL>();
            _loginWindow = new Login();
        }

        #region Test Case
        //Test email
        [TestCase("", "N", false)]
        [TestCase("NguyennnnnnnnVannnnnnnAiiiiiiiiiiiiiiiiiiiiiiiiiiiiii12345678@gmail.com", "N", false)]
        [TestCase("Admin", "N", false)]
        [TestCase("h@gmail.com", "N", true)]
        [TestCase("Admin@gmail.com", "password1", true)]
        [TestCase("admin@gmail.com", "password1", true)]
        [TestCase("staff@gmail.com", "password1", true)]
        [TestCase("notexist@gmail.com", "password1", false)]
        [TestCase("NguyennnnnnnnVannnnnnnAiiiiiiiiiiiiiiiiiiiiiiiiiiiii12345678@gmail.com", "N", false)]
        [TestCase("NguyennnnnnnVannnnnnnAiiiiiiiiiiiiiiiiiiiiiiiiiiiii12345678@gmail.com", "N", false)]
        [TestCase("                 staff@gmail.com", "password1", true)]

        //Test password
        [TestCase("admin@gmail.com", "", false)]
        [TestCase("admin@gmail.com", "NgggggggggggVannnnnnnnnnnnAiiiiiiiiiiiiiiiiiiii12341234123456", false)]
        [TestCase("admin@gmail.com", "h", true)]
        [TestCase("admin@gmail.com", "ha", true)]
        [TestCase("admin@gmail.com", "pássword0", true)]
        [TestCase("admin@gmail.com", "Password1", true)]
        [TestCase("h@gmail.com", "NgggggggggggVannnnnnnnnnnnAiiiiiiiiiiiiiiiiiii12341234123456", true)]
        [TestCase("h@gmail.com", "NggggggggggVannnnnnnnnnnnAiiiiiiiiiiiiiiiiiii12341234123456", true)]
        [TestCase("admin@gmail.com", "                 password1", true)]

        [TestCase("", "", false)]

        [Apartment(ApartmentState.STA)]
        #endregion

        public void TestLogin(string email, string password, bool expectedResult)
        {
            // Thiết lập mock cho phương thức AuthenticateAccount
            _mockAccountBLL.Setup(x => x.AuthenticateAccount(email, password, out It.Ref<int>.IsAny))
                .Returns(expectedResult);

            // Gán giá trị cho email và password
            _loginWindow.Email.Text = email;
            _loginWindow.Password.Password = password;

            // Thực hiện hành động đăng nhập
            _loginWindow.Button_Click(null, null);

            // Xác nhận kết quả
            Assert.That(_loginWindow.IsLoggedIn, Is.EqualTo(expectedResult));
        }
    }
}

