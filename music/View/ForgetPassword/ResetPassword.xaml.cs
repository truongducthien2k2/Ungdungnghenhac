using music.Component;
using music.LocalStore;
using music.ViewModel;
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

namespace music.View.ForgetPassword
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        public ResetPassword()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized( EventArgs e )
        {
            IconHelper.RemoveIcon(this);
        }

        private void btnSubmit_Click( object sender, RoutedEventArgs e )
        {
            if (txtVerifiedCode.Text != OTPCode.Instance.GetOTP())
            {
                CustomMessageBox message = new CustomMessageBox("Mã xác thực không chính xác", "Error");
                message.Show();
            } 
            else
            {
                if (txtNewPass.Password != txtVerifiedPass.Password)
                {
                    CustomMessageBox message = new CustomMessageBox("Mật khẩu xác nhận không chính xác", "Error");
                    message.Show();
                } 
                else
                {
                    string email = EmailResetPassword.Instance.GetEmail();
                    VerifyAccountViewModel verifyAccountVM = new VerifyAccountViewModel();
                    if ( verifyAccountVM.SettingNewPassword(email, txtNewPass.Password.ToString()) == 1 )
                    {
                        CustomMessageBox message = new CustomMessageBox("Khôi phục tài khoản thành công", "Success");
                        message.Show();
                    }
                }
            }
        }
    }
}
