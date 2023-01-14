using music.View.ForgetPassword;
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
        public LoginView()
        {
            InitializeComponent();
        }

        private void lbForgetPass_PreviewMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {

        }

        private void lbForgetPass_Click( object sender, RoutedEventArgs e )
        {
            VerifyAccountView verifyAccount = new VerifyAccountView();
            verifyAccount.Show();
        }
    }
}
