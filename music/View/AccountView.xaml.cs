using music.Model;
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
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : Page
    {
        AccountViewModel accountVM = new AccountViewModel();
        SongViewModel songVM = new SongViewModel();
        string userName = Properties.Settings.Default.user;
        CLIENT client;
        Frame MainContent;
       
        public AccountView( Frame mainContent )
        {
            InitializeComponent();
            MainContent = mainContent;
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            client = accountVM.GetAllUser().Where(user => user.userName == userName).FirstOrDefault();
            tbUserName.Text = userName;
            tbFullName.Text = client.fullName;
            tbEmail.Text = client.email;
            tbPhone.Text = client.phone;
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            tbViewedSongs.Text = songVM.GetViewSongList().Where(user => user.id == client.id).Select(view => view.songId).ToList().Count().ToString();
            tbLikes.Text = songVM.GetLikeSongList().Where(user => user.id == client.id).Count().ToString();
            tbVIP.Text = (bool) client.VIP ? "VIP" : "STANDARD";
        }

        private void btnLogout_Click( object sender, RoutedEventArgs e )
        {
            if (accountVM.Logout() == 1)
            {
                MessageBox.Show("Đăng xuất thành công");
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }

        private void EditData(TextBox tb, string choice)
        {
            if (choice == "edit")
            {
                tb.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tb.IsReadOnly = false;
            }
            else
            {
                tb.BorderBrush = new SolidColorBrush(Colors.Transparent);
                tb.IsReadOnly = true;
            }
        }

        private void btnAdjustProfile_Click( object sender, RoutedEventArgs e )
        {
            EditData(tbFullName, "edit");
            EditData(tbPhone, "edit");
            EditData(tbEmail, "edit");
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
            if ( !number.StartsWith("0") || number.Length > 10 )
                return false;
            for ( int i = 0; i < number.Length; i++ )
            {
                if ( !int.TryParse(number [0].ToString(), out _) )
                    return false;
            }
            return true;
        }

        private void btnSubmit_Click( object sender, RoutedEventArgs e )
        {
            string name = tbFullName.Text;
            string email = tbEmail.Text;
            string phone = tbPhone.Text;
            if (name == "" || email == "" || phone == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
            }
            else
            {
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Vui lòng kiểm tra lại email");
                }
                else
                {
                    if (!IsPhoneNumber(phone))
                    {
                        MessageBox.Show("Vui lòng kiểm tra lại số điện thoại");
                    }
                    else
                    {
                        if ( accountVM.UpdateProfile(client.id, name, email, phone) == 1 )
                        {
                            MessageBox.Show("Cập nhật thông tin cá nhân thành công");
                            MainContent.Navigate(new AccountView(MainContent));
                            EditData(tbFullName, "hide");
                            EditData(tbPhone, "hide");
                            EditData(tbEmail, "hide");
                        }
                    }
                }
             
            }
        }
    }
}
