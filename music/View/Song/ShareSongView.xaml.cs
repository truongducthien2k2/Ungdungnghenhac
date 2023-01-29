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
using Facebook;

namespace music.View.Song
{
    /// <summary>
    /// Interaction logic for ShareSongView.xaml
    /// </summary>
    public partial class ShareSongView : Window
    {
        private FacebookClient FBClient;
        public string AccessToken { get; set; }
        public ShareSongView()
        {
            InitializeComponent();
            webBrowser.Navigate(new Uri("http://www.facebook.com", UriKind.Absolute));
        }

        private void webBrowser_Navigated( object sender, System.Windows.Navigation.NavigationEventArgs e )
        {
     
        }
    }
}
