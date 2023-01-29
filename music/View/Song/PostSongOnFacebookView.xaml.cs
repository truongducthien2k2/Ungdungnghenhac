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
    /// Interaction logic for PostSongOnFacebookView.xaml
    /// </summary>
    public partial class PostSongOnFacebookView : Window
    {
        public PostSongOnFacebookView(FacebookClient FBClient)
        {
            InitializeComponent();
            dynamic me = FBClient.Get("Me");
            tbInfo.Text = me.name.ToString();
        }
    }
}
