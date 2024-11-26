
using System.Windows.Controls;

namespace GUI.Test
{
    [TestFixture]
    public class Test_AddMember
    {
        private SignUp1 _addMemberWindow;

        [SetUp]
        public void SetUp()
        {
            _addMemberWindow = new SignUp1();

            // Khởi tạo ItemsSource
            _addMemberWindow.Day = new ComboBox { ItemsSource = Enumerable.Range(1, 31).ToList() };
            _addMemberWindow.Month = new ComboBox { ItemsSource = Enumerable.Range(1, 12).ToList() };
            _addMemberWindow.Year = new ComboBox { ItemsSource = Enumerable.Range(1900, 2000).ToList() };

        }

        #region Test Case
        [TestCase("","An","h","0123456789", "true", "password1", "password1", 1, 1, 2000, false)]
        [TestCase("Nguyễn", "", "h", "0123456789", "true", "password1", "password1", 1, 1, 2000, false)]
        [TestCase("Nguyễn", "An", "h", "9123456789", "true", "password1", "password1", 1, 1, 2000, false)]


        [Apartment(ApartmentState.STA)]
        #endregion
        public void TestAddMember(string fName, string lName, string email, string phone, bool admin, string password, string rePassword, int day, int month, int year, bool expectedResult)
        {
            // Gán giá trị cho email và password
            _addMemberWindow.fName.Text = fName;
            _addMemberWindow.lName.Text = lName;
            _addMemberWindow.Email.Text = email;
            _addMemberWindow.Phone.Text = phone;
            _addMemberWindow.Admin.IsChecked = admin;
           
            _addMemberWindow.Day.SelectedItem=day;
            _addMemberWindow.Month.SelectedItem=month;
            _addMemberWindow.Year.SelectedItem =year;
            _addMemberWindow.Password.Password = password;
            _addMemberWindow.RePassword.Password = rePassword;

            // Thực hiện hành động đăng nhập
            _addMemberWindow.Button_Click(null, null);

            Console.WriteLine($"Expected: {expectedResult}, Actual: {_addMemberWindow.IsSuccess}");

            // Xác nhận kết quả
            //Assert.AreEqual(expectedResult, _loginWindow.IsLoggedIn);
            Assert.That(expectedResult, Is.EqualTo(_addMemberWindow.IsSuccess));
        }
    }
}
