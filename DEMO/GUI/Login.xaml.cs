using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public TextBox Email { get; set; }
        public PasswordBox Password { get; set; }
        //private readonly IAccountBLL _accountBLL;
        private IAccountBLL accountBLL;

        //public Login(IAccountBLL accountBLL) // Constructor nhận IAccountBLL
        //{
        //    accountBLL = accountBLL;
        //}

        // Thay đổi object thành bool và thêm getter và setter
        public bool IsLoggedIn { get; private set; }

        //public Login(IAccountBLL accountBLL)
        //{
        //    InitializeComponent();
        //    _accountBLL = accountBLL;
        //}

        public Login()
        {
            InitializeComponent();
            Email = new TextBox();
            Password = new PasswordBox();
            accountBLL = accountBLL;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void openAdminForm(string mail, List<string> permissions)
        {
            ClientSession.Instance.StartSession(mail, permissions);
            SessionManager.StartSession(mail, mail, permissions);

            // Mở giao diện admin
            MainWindow f = new MainWindow();
            f.Show();
            Window.GetWindow(this).Close();
        }

        private void openUserForm(string mail, List<string> permissions)
        {
            ClientSession.Instance.StartSession(mail, permissions);
            SessionManager.StartSession(mail, mail, permissions);

            // Mở giao diện người dùng thông thường
            StaffWindow f = new StaffWindow();
            f.Show();
            Window.GetWindow(this).Close();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{


        //    string email = txtEmail.Text;
        //    string password = txtPassword.Password;

        //    ACCOUNT_BLL accountBLL = new ACCOUNT_BLL();
        //    int permissionID;

        //    if (accountBLL.AuthenticateAccount(email, password, out permissionID))
        //    {
        //        // Tài khoản và mật khẩu đúng
        //        switch (permissionID)
        //        {
        //            case 1:
        //                // Xử lý khi permissionID = 1 (Ví dụ: mở giao diện admin)
        //                openAdminForm(email, new[] {permissionID.ToString()}.ToList());
        //                break;
        //            case 2:
        //                // Xử lý khi permissionID = 2 (Ví dụ: mở giao diện người dùng thông thường)
        //                openUserForm(email, new[] { permissionID.ToString() }.ToList());
        //                break;
        //            default:
        //                MessageBox.Show("Invalid permission", "Error");
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        // Tài khoản hoặc mật khẩu không đúng
        //        MessageBox.Show("Invalid account", "Error");
        //    }


        //}
        //public void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    string email = txtEmail.Text;
        //    string password = txtPassword.Password;

        //    int permissionID;
        //    if (Email == null || Password == null)
        //    {
        //        IsLoggedIn = false; // Nếu điều khiển là null, thiết lập IsLoggedIn là false
        //        return;
        //    }

        //    // Kiểm tra đầu vào
        //    if (string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(Password.Password))
        //    {
        //        IsLoggedIn = false; // Nếu không có đầu vào, không cho phép đăng nhập
        //        return;
        //    }


        //    if (_accountBLL.AuthenticateAccount(email, password, out permissionID))
        //    {
        //        IsLoggedIn = true;
        //        // Tài khoản và mật khẩu đúng
        //        switch (permissionID)
        //        {
        //            case 1:
        //                openAdminForm(email, new[] { permissionID.ToString() }.ToList());
        //                break;
        //            case 2:
        //                openUserForm(email, new[] { permissionID.ToString() }.ToList());
        //                break;
        //            default:
        //                MessageBox.Show("Invalid permission", "Error");
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        IsLoggedIn = false;
        //        MessageBox.Show("Invalid account", "Error");
        //    }

        //}
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            int permissionID;
            IsLoggedIn = true;
            // Kiểm tra xem Email và Password có null không
            if (txtEmail == null || txtPassword == null)
            {
                IsLoggedIn = false; // Nếu điều khiển là null, thiết lập IsLoggedIn là false
                return;
            }

            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                IsLoggedIn = false; // Nếu không có đầu vào, không cho phép đăng nhập
                //MessageBox.Show("Email and password cannot be empty", "Error");
                return;
            }
                
            // Thực hiện xác thực
            if (accountBLL.AuthenticateAccount(email, password, out permissionID))
            {
                IsLoggedIn = true; // Nếu xác thực thành công

                // Kiểm tra quyền truy cập dựa trên permissionID
                switch (permissionID)
                {
                    case 1:
                        openAdminForm(email, new[] { permissionID.ToString() }.ToList());
                        break;
                    case 2:
                        openUserForm(email, new[] { permissionID.ToString() }.ToList());
                        break;
                    default:
                        MessageBox.Show("Invalid permission", "Error");
                        IsLoggedIn = false; // Thiết lập IsLoggedIn là false
                        break;
                }
            }
            else
            {
                IsLoggedIn = false; // Nếu xác thực không thành công
                MessageBox.Show("Invalid account", "Error");
            }
        }



        //public void Button_Click(object value1, object value2)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
