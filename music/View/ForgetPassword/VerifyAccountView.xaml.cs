using music.Component;
using music.LocalStore;
using music.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace music.View.ForgetPassword
{
    /// <summary>
    /// Interaction logic for VerifyAccountView.xaml
    /// </summary>
    public partial class VerifyAccountView : Window
    {
        string randomCode = RandomString(5);
        public VerifyAccountView()
        {
            InitializeComponent();
            txtCode.Content = randomCode;
        }

        protected override void OnSourceInitialized( EventArgs e )
        {
            IconHelper.RemoveIcon(this);
        }

        public static string RandomString( int length )
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s [random.Next(s.Length)]).ToArray());
        }

        private bool IsValidEmail( string email )
        {
            var trimmedEmail = email.Trim();

            if ( trimmedEmail.EndsWith(".") )
            {
                return false; // suggested by @TK-421
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

        private int InitOTPCode( int begin, int end )
        {
            Random rd = new Random();
            return rd.Next(begin, end);
        }
        private void SendOTP( string mailfrom, string pass, string mailto, string subject, string content )
        {

            MailMessage mail = new MailMessage(mailfrom, mailto, subject, content);
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential("DreamCoffeeHCM@gmail.com", "deuallbrhtbiydsm");
            client.EnableSsl = true;
            client.Send(mail);
        }

        private void btnVerify_Click( object sender, RoutedEventArgs e )
        {
            if (txtEmail.Text.Trim().Length == 0 || txtVerifyCode.Text.Trim().Length == 0)
            {
                CustomMessageBox message = new CustomMessageBox("Vui lòng nhập đầy đủ thông tin", "Warning");
                message.Show();
            } 
            else
            {
                VerifyAccountViewModel verifyAccountVM = new VerifyAccountViewModel();
                if ( !IsValidEmail(txtEmail.Text) || !verifyAccountVM.IsExistedEmailInSysten(txtEmail.Text) )
                {
                    CustomMessageBox message = new CustomMessageBox("Email không chính xác", "Error");
                    message.Show();
                } 
                else
                {
                    if ( txtVerifyCode.Text != randomCode )
                    {
                        CustomMessageBox message = new CustomMessageBox("Mã xác thực không chính xác", "Error");
                        message.Show();
                    } 
                    else
                    {
                        OTPCode.Instance.SetOTP(InitOTPCode(100000, 999999).ToString());
                        EmailResetPassword.Instance.SetEmail(txtEmail.Text);
                        string subject = "Khôi phục tài khoản";
                        string content = "Xin chào bạn, bạn vừa thực hiện khôi phục tài khoản. Mã xác thực của bạn là: " + OTPCode.Instance.GetOTP()
                                            + ". Vui lòng xác nhận mã để thực hiện cài đặt lại mật khẩu.";
                        SendOTP("DreamCoffeeHCM@gmail.com", "123!@#123!@#", txtEmail.Text, subject, content);
                        bool? Result = new CustomMessageBox("Hệ thống đã gửi mã xác thực qua Email của bạn. Vui lòng kiểm tra hộp thư để khôi phục tài khoản!!!", "Info").ShowDialog();
                        //System.Threading.Thread.Sleep(2000);
                        if ( Result.Value ) { 
                            ResetPassword resetPass = new ResetPassword();
                            resetPass.Show();
                            this.Close();
                        }
                    }    
                }
            }
        }

        private void btnResetCode_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            string newRandomCode = RandomString(5);
            txtCode.Content = newRandomCode;    
            this.randomCode = newRandomCode;
        }
    }
}
