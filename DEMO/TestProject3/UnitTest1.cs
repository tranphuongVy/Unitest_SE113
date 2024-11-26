using System;
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
        [TestCase("admin@gmail.com", "password1", true)]
        [TestCase("vy@gmail.com", "vy1", true)]
        [TestCase("user@gmail.com", "wrongpassword", false)]
        [TestCase("nonexistent@gmail.com", "password123", false)]
        [TestCase("admin@gmail.com", "", false)]
        [TestCase("", "password123", false)]
        [TestCase("", "", false)]
        [TestCase("admin@gmail.com", " ", false)] // Test với khoảng trắng
        [TestCase(" ", "password123", false)] // Test với khoảng trắng
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

