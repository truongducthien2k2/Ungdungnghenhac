using music.View.ForgetPassword;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace music.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        AccountViewModel accountVM = new AccountViewModel();
        public LoginView()
        {
            InitializeComponent();
        }

        private void btnLogin_Click( object sender, RoutedEventArgs e )
        {
            string username = txtUserName.Text;
            string password = txtPass.Password;
            if (accountVM.LoginSuccessfully(username, password))
            {
                MessageBox.Show("Đăng nhập thành công");
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại");
            }
        }

        private void btnForgetPass_Click( object sender, RoutedEventArgs e )
        {
            VerifyAccountView verifyAccount = new VerifyAccountView();
            verifyAccount.Show();
        }
    }
}
