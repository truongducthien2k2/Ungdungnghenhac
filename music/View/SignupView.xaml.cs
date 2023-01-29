using music.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace music.View
{
    /// <summary>
    /// Interaction logic for SignupView.xaml
    /// </summary>
    public partial class SignupView : Page
    {
        Frame MainContent;
        AccountViewModel accountVM = new AccountViewModel();
        public SignupView()
        {
            InitializeComponent();
        }

        public SignupView(Frame MainContent)
        {
            InitializeComponent();
            this.MainContent = MainContent;
        }

        private bool IsValidEmail( string email )
        {
            var trimmedEmail = email.Trim();

            if ( trimmedEmail.EndsWith(".") )
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private bool IsPhoneNumber( string number )
        {
            if(!number.StartsWith("0") || number.Length > 10)
                return false;
            for (int i = 0; i < number.Length; i++ )
            {
                if ( !int.TryParse(number [0].ToString(), out _) )
                    return false;
            }
            return true;
        }

        private void btnSignup_Click( object sender, RoutedEventArgs e )
        {
            string userName = txtUserName.Text;
            string pass = txtPass.Password;
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(pass) || String.IsNullOrEmpty(fullName) || String.IsNullOrEmpty (email) || String.IsNullOrEmpty(phone)) 
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                if (accountVM.GetAllUser().Where(user => user.userName == userName).Count() > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại");

                }
                else
                {
                    if ( !IsValidEmail(email) )
                    {
                        MessageBox.Show("Vui lòng kiểm tra lại Email");
                    }
                    else
                    {
                        if ( !IsPhoneNumber(phone) )
                        {
                            MessageBox.Show("Vui lòng kiểm tra lại Số điện thoại");
                        }
                        else
                        {
                            if ( accountVM.SignUp(userName, pass, fullName, email, phone, 0, 0) == 1)
                            {
                                MessageBox.Show("Đăng ký thành công");
                                MainContent.Navigate(new LoginView(MainContent));
                            }
                        }
                    }
                }    
            } 
                
        }
    }
}
